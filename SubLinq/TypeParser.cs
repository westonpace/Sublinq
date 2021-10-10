using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Type = Substrait.Protobuf.Type;

namespace SubLinq
{
    public static class TypeParser
    {
        private static Dictionary<System.Type, Type> csharpTypeToSubstraitType = new Dictionary<System.Type, Type>()
        {
            {typeof(bool), new Type {Bool = new Type.Types.Boolean()}},
            {typeof(byte), new Type {I16 = new Type.Types.I16()}},
            {typeof(sbyte), new Type {I8 = new Type.Types.I8()}},
            {typeof(short), new Type {I16 = new Type.Types.I16()}},
            {typeof(ushort), new Type {I32 = new Type.Types.I32()}},
            {typeof(int), new Type {I32 = new Type.Types.I32()}},
            {typeof(uint), new Type {I64 = new Type.Types.I64()}},
            {typeof(long), new Type {I64 = new Type.Types.I64()}},
            {typeof(float), new Type {Fp32 = new Type.Types.FP32()}},
            {typeof(double), new Type {Fp64 = new Type.Types.FP64()}},
            {typeof(string), new Type {String = new Type.Types.String()}},
            {typeof(byte[]), new Type {Binary = new Type.Types.Binary()}},
        };

        public static Type.Types.NamedStruct SchemaFromType<T>()
        {
            return StructFromObjectType(typeof(T));
        }

        public static Type.Types.NamedStruct SchemaFromType(System.Type t)
        {
            return StructFromObjectType(t);
        }

        public static Type.Types.NamedStruct StructFromObjectType(System.Type t)
        {
            var result = new Type.Types.NamedStruct();
            var struct_ = new Type.Types.Struct();
            foreach (var prop in t.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                result.Names.Add(prop.Name);
                struct_.Types_.Add(SubstraitTypeFromCSharpType(prop.PropertyType));
            }

            result.Struct = struct_;
            return result;
        }

        public static Type SubstraitTypeFromCSharpType(System.Type t)
        {
            if (csharpTypeToSubstraitType.ContainsKey(t))
            {
                return csharpTypeToSubstraitType[t];
            }
            if (t.IsValueType)
            {
                throw new Exception($"Type {t} cannot be mapped to a Substrait type");
            }
            return new Type {Struct = StructFromObjectType(t).Struct};
        }
    }
}
