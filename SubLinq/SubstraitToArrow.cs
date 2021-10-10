using System;
using System.Runtime.InteropServices;
using Apache.Arrow;
using SubLinq.ArrowIr;
using SubLinq.ArrowIr.Arrow;
using FlatBuffers;
using Google.Protobuf;
using Substrait.Protobuf;
using Expression = SubLinq.ArrowIr.Expression;
using Field = SubLinq.ArrowIr.Arrow.Field;
using Schema = SubLinq.ArrowIr.Arrow.Schema;
using Type = Substrait.Protobuf.Type;

namespace SubLinq
{
    public class SubstraitToArrow
    {
        private readonly FlatBufferBuilder _builder;

        public SubstraitToArrow()
        {
            _builder = new FlatBufferBuilder(1024 * 1024);
        }

        public Offset<Relation> VisitRel(Rel rel)
        {
            Relation.StartRelation(_builder);
            switch (rel.RelTypeCase)
            {
                case Rel.RelTypeOneofCase.Read:
                    var source = VisitRead(rel.Read);
                    Relation.AddImplType(_builder, RelationImpl.Source);
                    Relation.AddImpl(_builder, source.Value);
                    break;
                case Rel.RelTypeOneofCase.Filter:
                    var filter = VisitFilter(rel.Filter);
                    Relation.AddImplType(_builder, RelationImpl.Filter);
                    Relation.AddImpl(_builder, filter.Value);
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion does not handle relation type {rel.RelTypeCase}");
            }

            return Relation.EndRelation(_builder);
        }

        private Offset<Filter> VisitFilter(FilterRel filter)
        {
            Filter.StartFilter(_builder);
            Filter.AddBase(_builder, VisitRelCommon(filter.Common));
            Filter.AddRel(_builder, VisitRel(filter.Input));
            Filter.AddPredicate(_builder, VisitExpression(filter.Condition));
            return Filter.EndFilter(_builder);
        }

        private Offset<Expression> VisitExpression(Substrait.Protobuf.Expression expression)
        {
            Expression.StartExpression(_builder);
            switch (expression.RexTypeCase)
            {
                case Substrait.Protobuf.Expression.RexTypeOneofCase.Literal:
                    Expression.AddImplType(_builder, ExpressionImpl.Literal);
                    Expression.AddImpl(_builder, VisitLiteral(expression.Literal).Value);
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion not supported for expression type {expression.RexTypeCase}");
            }
            return Expression.EndExpression(_builder);
        }

        private Offset<Literal> VisitLiteral(Substrait.Protobuf.Expression.Types.Literal literal)
        {
            Literal.StartLiteral(_builder);
            switch (literal.LiteralTypeCase)
            {
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Binary:
                    Literal.AddImplType(_builder, LiteralImpl.BinaryLiteral);
                    Literal.AddImpl(_builder, CreateBinaryLiteral(literal.Binary).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Boolean:
                    Literal.AddImplType(_builder, LiteralImpl.BooleanLiteral);
                    Literal.AddImpl(_builder, CreateBooleanLiteral(literal.Boolean).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Fp32:
                    Literal.AddImplType(_builder, LiteralImpl.Float32Literal);
                    Literal.AddImpl(_builder, CreateFloat32Literal(literal.Fp32).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.Fp64:
                    Literal.AddImplType(_builder, LiteralImpl.Float64Literal);
                    Literal.AddImpl(_builder, CreateFloat64Literal(literal.Fp64).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I8:
                    Literal.AddImplType(_builder, LiteralImpl.Int8Literal);
                    Literal.AddImpl(_builder, CreateInt8Literal(literal.I8).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I16:
                    Literal.AddImplType(_builder, LiteralImpl.Int16Literal);
                    Literal.AddImpl(_builder, CreateInt16Literal(literal.I16).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I32:
                    Literal.AddImplType(_builder, LiteralImpl.Int32Literal);
                    Literal.AddImpl(_builder, CreateInt32Literal(literal.I32).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.I64:
                    Literal.AddImplType(_builder, LiteralImpl.Int64Literal);
                    Literal.AddImpl(_builder, CreateInt64Literal(literal.I64).Value);
                    break;
                case Substrait.Protobuf.Expression.Types.Literal.LiteralTypeOneofCase.String:
                    Literal.AddImplType(_builder, LiteralImpl.StringLiteral);
                    Literal.AddImpl(_builder, CreateStringLiteral(literal.String).Value);
                    break;
                default:
                    throw new NotImplementedException(
                        $"Substrait->Arrow conversion not supported for literal type {literal.LiteralTypeCase}");
            }

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
            Source.StartSource(_builder);
            Source.AddBase(_builder, VisitRelCommon(read.Common));
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

            Source.AddName(_builder, _builder.CreateString(read.NamedTable.Names[0]));
            Source.AddSchema(_builder, VisitSchema(read.BaseSchema));
            return Source.EndSource(_builder);
        }

        private Offset<Schema> VisitSchema(Type.Types.NamedStruct schema)
        {
            Schema.StartSchema(_builder);
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
                Field.StartField(_builder);
                Field.AddName(_builder, _builder.CreateString(fieldName));
                switch (type.KindCase)
                {
                    case Type.KindOneofCase.Binary:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Binary);
                        Field.AddType(_builder, CreateBinary().Value);
                        Field.AddNullable(_builder, type.Binary.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.Bool:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Bool);
                        Field.AddType(_builder, CreateBool().Value);
                        Field.AddNullable(_builder, type.Bool.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.Fp32:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.FloatingPoint);
                        Field.AddType(_builder, CreateFloat().Value);
                        Field.AddNullable(_builder, type.Fp32.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.Fp64:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.FloatingPoint);
                        Field.AddType(_builder, CreateDouble().Value);
                        Field.AddNullable(_builder, type.Fp64.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.I8:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Int);
                        Field.AddType(_builder, CreateSignedInt(8).Value);
                        Field.AddNullable(_builder, type.I8.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.I16:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Int);
                        Field.AddType(_builder, CreateSignedInt(16).Value);
                        Field.AddNullable(_builder, type.I16.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.I32:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Int);
                        Field.AddType(_builder, CreateSignedInt(32).Value);
                        Field.AddNullable(_builder, type.I32.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.I64:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Int);
                        Field.AddType(_builder, CreateSignedInt(64).Value);
                        Field.AddNullable(_builder, type.I64.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.String:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Utf8);
                        Field.AddType(_builder, CreateString().Value);
                        Field.AddNullable(_builder, type.String.Nullability != Type.Types.Nullability.Required);
                        break;
                    case Type.KindOneofCase.Struct:
                        Field.AddTypeType(_builder, SubLinq.ArrowIr.Arrow.Type.Struct_);
                        throw new Exception("TO-DO");
                    default:
                        throw new NotImplementedException(
                            "Conversion from substrait type to Arrow type not implemented");
                }

                fields[i] = Field.EndField(_builder);
            }
            Schema.CreateFieldsVector(_builder, fields);
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
            RelBase.StartRelBase(_builder);
            switch (common.KindCase)
            {
                case RelCommon.KindOneofCase.Direct:
                    RelBase.AddOutputMappingType(_builder, Emit.PassThrough);
                    RelBase.AddOutputMapping(_builder, VisitDirect(common.Direct).Value);
                    break;
                case RelCommon.KindOneofCase.Emit:
                    RelBase.AddOutputMappingType(_builder, Emit.Remap);
                    RelBase.AddOutputMapping(_builder, VisitEmit(common.Emit).Value);
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