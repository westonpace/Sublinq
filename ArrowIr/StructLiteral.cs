// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct StructLiteral : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static StructLiteral GetRootAsStructLiteral(ByteBuffer _bb) { return GetRootAsStructLiteral(_bb, new StructLiteral()); }
  public static StructLiteral GetRootAsStructLiteral(ByteBuffer _bb, StructLiteral obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public StructLiteral __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Values for each struct field; the order must match the order of fields
  /// in the `type` field of `Literal`.
  public Datalinq.ArrowIr.Literal? Values(int j) { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.Literal?)(new Datalinq.ArrowIr.Literal()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int ValuesLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Datalinq.ArrowIr.StructLiteral> CreateStructLiteral(FlatBufferBuilder builder,
      VectorOffset valuesOffset = default(VectorOffset)) {
    builder.StartTable(1);
    StructLiteral.AddValues(builder, valuesOffset);
    return StructLiteral.EndStructLiteral(builder);
  }

  public static void StartStructLiteral(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValues(FlatBufferBuilder builder, VectorOffset valuesOffset) { builder.AddOffset(0, valuesOffset.Value, 0); }
  public static VectorOffset CreateValuesVector(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Literal>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateValuesVectorBlock(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Literal>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartValuesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Datalinq.ArrowIr.StructLiteral> EndStructLiteral(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // values
    return new Offset<Datalinq.ArrowIr.StructLiteral>(o);
  }
};


}