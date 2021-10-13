using System;
using System.Runtime.InteropServices;
using SubLinq.ArrowIr;
using SubLinq.ArrowIr.Arrow;
using FlatBuffers;
using Google.Protobuf;
using Substrait.Protobuf;
using Expression = SubLinq.ArrowIr.Expression;
using Field = SubLinq.ArrowIr.Arrow.Field;
using Schema = SubLinq.ArrowIr.Arrow.Schema;
using Type = Substrait.Protobuf.Type;
using System.Collections.Generic;
using Array = System.Array;
using Plan = SubLinq.ArrowIr.Plan;

namespace SubLinq
{

    public class SubstraitToArrow
    {
        private static readonly Dictionary<ulong, string> FunctionIdsToArrowName = new()
        {
            {1, "less"}
        };

        private readonly FlatBufferBuilder _builder;

        private SubstraitToArrow()
        {
            _builder = new FlatBufferBuilder(1024 * 1024);
        }
        
        public static byte[] Convert(Rel rel)
        {
            SubstraitToArrow converter = new();
            var outputRel = converter.VisitRootRel(rel);
            converter._builder.Finish(outputRel.Value);
            return converter._builder.DataBuffer.ToSizedArray();
        }

        private Offset<Plan> VisitRootRel(Rel rel)
        {
            var sinks = new Offset<Relation>[1];
            sinks[0] = VisitRel(rel);
            var sinksVector = Plan.CreateSinksVector(_builder, sinks);
            Plan.StartPlan(_builder);
            Plan.AddSinks(_builder, sinksVector);
            return Plan.EndPlan(_builder);
        }
        
        private Offset<Relation> VisitRel(Rel rel)
        {
            RelationImpl implType;
            int impl;
            switch (rel.RelTypeCase)
            {
                case Rel.RelTypeOneofCase.Read:
                    implType = RelationImpl.Source;
                    impl = VisitRead(rel.Read).Value;
                    break;
                case Rel.RelTypeOneofCase.Filter:
                    implType = RelationImpl.Filter;
                    impl = VisitFilter(rel.Filter).Value;
                    break;
                case Rel.RelTypeOneofCase.Project:
                    implType = RelationImpl.Project;
                    impl = VisitProject(rel.Project).Value;
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion does not handle relation type {rel.RelTypeCase}");
            }

            Relation.StartRelation(_builder);
            Relation.AddImplType(_builder, implType);
            Relation.AddImpl(_builder, impl);
            return Relation.EndRelation(_builder);
        }

        private Offset<Project> VisitProject(ProjectRel project)
        {
            // ReSharper disable once InconsistentNaming
            var base_ = VisitRelCommon(project.Common);
            var rel = VisitRel(project.Input);
            Offset<Expression>[] exprs = new Offset<Expression>[project.Expressions.Count];
            for (int i = 0; i < project.Expressions.Count; i++)
            {
                exprs[i] = VisitExpression(project.Expressions[i]);
            }

            VectorOffset exprsVector = Project.CreateExpressionsVector(_builder, exprs);
            Project.StartProject(_builder);
            Project.AddBase(_builder, base_);
            Project.AddRel(_builder, rel);
            Project.AddExpressions(_builder, exprsVector);
            return Project.EndProject(_builder);
        }

        private Offset<Filter> VisitFilter(FilterRel filter)
        {
            // ReSharper disable once InconsistentNaming
            var base_ = VisitRelCommon(filter.Common);
            var rel = VisitRel(filter.Input);
            var predicate = VisitExpression(filter.Condition);
            Filter.StartFilter(_builder);
            Filter.AddBase(_builder, base_);
            Filter.AddRel(_builder, rel);
            Filter.AddPredicate(_builder, predicate);
            return Filter.EndFilter(_builder);
        }

        private Offset<Expression> VisitExpression(Substrait.Protobuf.Expression expression)
        {
            ExpressionImpl implType;
            int impl;
            switch (expression.RexTypeCase)
            {
                case Substrait.Protobuf.Expression.RexTypeOneofCase.Literal:
                    implType = ExpressionImpl.Literal;
                    impl = VisitLiteral(expression.Literal).Value;
                    break;
                case Substrait.Protobuf.Expression.RexTypeOneofCase.ScalarFunction:
                    implType = ExpressionImpl.Call;
                    impl = VisitScalarFunction(expression.ScalarFunction).Value;
                    break;
                case Substrait.Protobuf.Expression.RexTypeOneofCase.Selection:
                    implType = ExpressionImpl.FieldRef;
                    impl = VisitFieldReference(expression.Selection).Value;
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion not supported for expression type {expression.RexTypeCase}");
            }
            Expression.StartExpression(_builder);
            Expression.AddImplType(_builder, implType);
            Expression.AddImpl(_builder, impl);
            return Expression.EndExpression(_builder);
        }

        private Offset<FieldRef> VisitFieldReference(FieldReference fieldRef)
        {
            switch (fieldRef.ReferenceTypeCase)
            {
                case FieldReference.ReferenceTypeOneofCase.DirectReference:
                    return VisitReferenceSegment(fieldRef.DirectReference);
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion not supported for field reference type {fieldRef.ReferenceTypeCase}");
            }
        }

        private Offset<FieldRef> VisitReferenceSegment(ReferenceSegment referenceSegment)
        {
            Deref refType;
            int refValue;
            switch (referenceSegment.ReferenceTypeCase)
            {
                case ReferenceSegment.ReferenceTypeOneofCase.StructField:
                    refType = Deref.FieldIndex;
                    refValue = VisitStructFieldReference(referenceSegment.StructField).Value;
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion not supported for reference type {referenceSegment.ReferenceTypeCase}");
            }
            FieldRef.StartFieldRef(_builder);
            // TODO
            FieldRef.AddRelationIndex(_builder, 0);
            FieldRef.AddRefType(_builder, refType);
            FieldRef.AddRef(_builder, refValue);
            return FieldRef.EndFieldRef(_builder);
        }

        private Offset<FieldIndex> VisitStructFieldReference(Substrait.Protobuf.ReferenceSegment.Types.StructField structField)
        {
            FieldIndex.StartFieldIndex(_builder);
            FieldIndex.AddPosition(_builder, (uint)structField.Field);
            return FieldIndex.EndFieldIndex(_builder);
        }

        private Offset<Call> VisitScalarFunction(Substrait.Protobuf.Expression.Types.ScalarFunction func)
        {
            var nameStr = FunctionIdsToArrowName[func.Id.Id];
            var name = _builder.CreateString(nameStr);
            var args = new Offset<Expression>[func.Args.Count];
            var orderings = Array.Empty<Offset<SortKey>>();

            for (int i = 0; i < func.Args.Count; i++)
            {
                args[i] = VisitExpression(func.Args[i]);
            }
            
            var argsVector = Call.CreateArgumentsVector(_builder, args);
            var orderingsVector = Call.CreateOrderingsVector(_builder, orderings);
            Call.StartCall(_builder);
            Call.AddName(_builder, name);
            Call.AddArguments(_builder, argsVector);
            Call.AddOrderings(_builder, orderingsVector);
            return Call.EndCall(_builder);
        }

        private Offset<Literal> VisitLiteral(Substrait.Protobuf.Expression.Types.Literal literal)
        {
            LiteralImpl implType;
            int impl;
            switch (literal.LiteralTypeCase)
            {
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Binary:
                    implType = LiteralImpl.BinaryLiteral;
                    impl = CreateBinaryLiteral(literal.Binary).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Boolean:
                    implType = LiteralImpl.BooleanLiteral;
                    impl = CreateBooleanLiteral(literal.Boolean).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Fp32:
                    implType = LiteralImpl.Float32Literal;
                    impl = CreateFloat32Literal(literal.Fp32).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Fp64:
                    implType = LiteralImpl.Float64Literal;
                    impl = CreateFloat64Literal(literal.Fp64).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I8:
                    implType = LiteralImpl.Int8Literal;
                    impl = CreateInt8Literal(literal.I8).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I16:
                    implType = LiteralImpl.Int16Literal;
                    impl = CreateInt16Literal(literal.I16).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I32:
                    implType = LiteralImpl.Int32Literal;
                    impl = CreateInt32Literal(literal.I32).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I64:
                    implType = LiteralImpl.Int64Literal;
                    impl = CreateInt64Literal(literal.I64).Value;
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.String:
                    implType = LiteralImpl.StringLiteral;
                    impl = CreateStringLiteral(literal.String).Value;
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion not supported for literal type {literal.LiteralTypeCase}");
            }

            Literal.StartLiteral(_builder);
            Literal.AddImplType(_builder, implType);
            Literal.AddImpl(_builder, impl);
            return Literal.EndLiteral(_builder);
        }

        private Offset<StringLiteral> CreateStringLiteral(string value)
        {
            StringLiteral.StartStringLiteral(_builder);
            var valueOffset = _builder.CreateString(value);
            StringLiteral.AddValue(_builder, valueOffset);
            return StringLiteral.EndStringLiteral(_builder);
        }

        private Offset<Int8Literal> CreateInt8Literal(int value)
        {
            Int8Literal.StartInt8Literal(_builder);
            Int8Literal.AddValue(_builder, (sbyte)value);
            return Int8Literal.EndInt8Literal(_builder);
        }

        private Offset<Int16Literal> CreateInt16Literal(int value)
        {
            Int16Literal.StartInt16Literal(_builder);
            Int16Literal.AddValue(_builder, (short)value);
            return Int16Literal.EndInt16Literal(_builder);
        }

        private Offset<Int32Literal> CreateInt32Literal(int value)
        {
            Int32Literal.StartInt32Literal(_builder);
            Int32Literal.AddValue(_builder, value);
            return Int32Literal.EndInt32Literal(_builder);
        }

        private Offset<Int64Literal> CreateInt64Literal(long value)
        {
            Int64Literal.StartInt64Literal(_builder);
            Int64Literal.AddValue(_builder, value);
            return Int64Literal.EndInt64Literal(_builder);
        }

        private Offset<Float64Literal> CreateFloat64Literal(double value)
        {
            Float64Literal.StartFloat64Literal(_builder);
            Float64Literal.AddValue(_builder, value);
            return Float64Literal.EndFloat64Literal(_builder);
        }
        
        private Offset<Float32Literal> CreateFloat32Literal(float value)
        {
            Float32Literal.StartFloat32Literal(_builder);
            Float32Literal.AddValue(_builder, value);
            return Float32Literal.EndFloat32Literal(_builder);
        }

        private Offset<BinaryLiteral> CreateBinaryLiteral(ByteString byteString)
        {
            BinaryLiteral.StartBinaryLiteral(_builder);
            BinaryLiteral.CreateValueVector(_builder, MemoryMarshal.Cast<byte, sbyte>(byteString.Span).ToArray());
            return BinaryLiteral.EndBinaryLiteral(_builder);
        }
        
        private Offset<BooleanLiteral> CreateBooleanLiteral(bool value)
        {
            BooleanLiteral.StartBooleanLiteral(_builder);
            BooleanLiteral.AddValue(_builder, value);
            return BooleanLiteral.EndBooleanLiteral(_builder);
        }
        
        private Offset<Source> VisitRead(ReadRel read)
        {
            if (read.NamedTable == null)
            {
                throw new NotImplementedException("The Arrow query engine only supports named data sources");
            }

            switch (read.NamedTable.Names.Count)
            {
                case 0:
                    throw new Exception("The named table did not have any names");
                case > 1:
                    throw new Exception("Cannot convert a NamedTable that has multiple names");
            }

            // ReSharper disable once InconsistentNaming
            var base_ = VisitRelCommon(read.Common);
            var name = _builder.CreateString(read.NamedTable.Names[0]);
            var schema = VisitSchema(read.BaseSchema);

            Source.StartSource(_builder);
            Source.AddBase(_builder, base_);
            Source.AddName(_builder, name);
            Source.AddSchema(_builder, schema);
            return Source.EndSource(_builder);
        }

        private Offset<Schema> VisitSchema(Type.Types.NamedStruct schema)
        {
            if (schema.Names == null || schema.Struct.Types_ == null ||
                schema.Names.Count != schema.Struct.Types_.Count)
            {
                throw new Exception("Invalid Substrait schema");
            }

            var fields = new Offset<Field>[schema.Names.Count];
            for (int i = 0; i < schema.Names.Count; i++)
            {
                var fieldName = schema.Names[i];
                var type = schema.Struct.Types_[i];
                var name = _builder.CreateString(fieldName);
                int typeTypeOffset;
                bool nullable;
                ArrowIr.Arrow.Type typeType;
                switch (type.KindCase)
                {
                    case Type.KindOneofCase.Binary:
                        typeTypeOffset = CreateBinary().Value;
                        typeType = ArrowIr.Arrow.Type.Binary;
                        nullable = type.Binary.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.Bool:
                        typeTypeOffset = CreateBool().Value;
                        typeType = ArrowIr.Arrow.Type.Bool;
                        nullable = type.Bool.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.Fp32:
                        typeTypeOffset = CreateFloat().Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.FloatingPoint;
                        nullable = type.Fp32.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.Fp64:
                        typeTypeOffset = CreateDouble().Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.FloatingPoint;
                        nullable = type.Fp64.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.I8:
                        typeTypeOffset = CreateSignedInt(8).Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.Int;
                        nullable = type.I8.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.I16:
                        typeTypeOffset = CreateSignedInt(16).Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.Int;
                        nullable = type.I16.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.I32:
                        typeTypeOffset = CreateSignedInt(32).Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.Int;
                        nullable = type.I32.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.I64:
                        typeTypeOffset = CreateSignedInt(64).Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.Int;
                        nullable = type.I64.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.String:
                        typeTypeOffset = CreateString().Value;
                        typeType = SubLinq.ArrowIr.Arrow.Type.Utf8;
                        nullable = type.String.Nullability != Type.Types.Nullability.Required;
                        break;
                    case Type.KindOneofCase.Struct:
                        throw new Exception("TO-DO");
                    default:
                        throw new NotImplementedException(
                            "Conversion from substrait type to Arrow type not implemented");
                }
                Field.StartField(_builder);
                Field.AddName(_builder, name);
                Field.AddTypeType(_builder, typeType);
                Field.AddType(_builder, typeTypeOffset);
                Field.AddNullable(_builder, nullable);
                fields[i] = Field.EndField(_builder);
            }
            var fieldsVector = Schema.CreateFieldsVector(_builder, fields);
            Schema.StartSchema(_builder);
            Schema.AddFields(_builder, fieldsVector);
            return Schema.EndSchema(_builder);
        }

        private Offset<Utf8> CreateString()
        {
            Utf8.StartUtf8(_builder);
            return Utf8.EndUtf8(_builder);
        }

        private Offset<Int> CreateSignedInt(int bitWidth)
        {
            Int.StartInt(_builder);
            Int.AddBitWidth(_builder, bitWidth);
            Int.AddIsSigned(_builder, true);
            return Int.EndInt(_builder);
        }

        private Offset<FloatingPoint> CreateFloat()
        {
            FloatingPoint.StartFloatingPoint(_builder);
            FloatingPoint.AddPrecision(_builder, Precision.SINGLE);
            return FloatingPoint.EndFloatingPoint(_builder);
        }

        private Offset<FloatingPoint> CreateDouble()
        {
            FloatingPoint.StartFloatingPoint(_builder);
            FloatingPoint.AddPrecision(_builder, Precision.DOUBLE);
            return FloatingPoint.EndFloatingPoint(_builder);
        }

        private Offset<Bool> CreateBool()
        {
            Bool.StartBool(_builder);
            return Bool.EndBool(_builder);
        }

        private Offset<Binary> CreateBinary()
        {
            Binary.StartBinary(_builder);
            return Binary.EndBinary(_builder);
        }

        private Offset<RelBase> VisitRelCommon(RelCommon common)
        {
            switch (common.KindCase)
            {
                case RelCommon.KindOneofCase.Direct:
                    var outputMapping = VisitDirect(common.Direct).Value;
                    RelBase.StartRelBase(_builder);
                    RelBase.AddOutputMappingType(_builder, Emit.PassThrough);
                    RelBase.AddOutputMapping(_builder, outputMapping);
                    break;
                case RelCommon.KindOneofCase.Emit:
                    outputMapping = VisitEmit(common.Emit).Value;
                    RelBase.StartRelBase(_builder);
                    RelBase.AddOutputMappingType(_builder, Emit.Remap);
                    RelBase.AddOutputMapping(_builder, outputMapping);
                    break;
                default:
                    throw new Exception(
                        $"Substrait->Arrow conversion does not handle output type mapping of {common.KindCase}");
            }

            return RelBase.EndRelBase(_builder);
        }

        // ReSharper disable once UnusedParameter.Local
        private Offset<PassThrough> VisitDirect(RelCommon.Types.Direct direct)
        {
            PassThrough.StartPassThrough(_builder);
            return PassThrough.EndPassThrough(_builder);
        }

        private Offset<Remap> VisitEmit(RelCommon.Types.Emit emit)
        {
            Remap.StartRemap(_builder);
            var mappings = new Offset<FieldIndex>[emit.OutputMapping.Count];
            for (int i = 0; i < emit.OutputMapping.Count; i++)
            {
                FieldIndex.StartFieldIndex(_builder);
                FieldIndex.AddPosition(_builder, (uint) emit.OutputMapping[i]);
                mappings[i] = FieldIndex.EndFieldIndex(_builder);
            }

            Remap.CreateMappingVector(_builder, mappings);
            return Remap.EndRemap(_builder);
        }
    }
}