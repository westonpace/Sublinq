// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct DecimalLiteral : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static DecimalLiteral GetRootAsDecimalLiteral(ByteBuffer _bb) { return GetRootAsDecimalLiteral(_bb, new DecimalLiteral()); }
  public static DecimalLiteral GetRootAsDecimalLiteral(ByteBuffer _bb, DecimalLiteral obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public DecimalLiteral __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Bytes of a Decimal value; bytes must be in little-endian order.
  public sbyte Value(int j) { int o = __p.__offset(4); return o != 0 ? __p.bb.GetSbyte(__p.__vector(o) + j * 1) : (sbyte)0; }
  public int ValueLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<sbyte> GetValueBytes() { return __p.__vector_as_span<sbyte>(4, 1); }
#else
  public ArraySegment<byte>? GetValueBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public sbyte[] GetValueArray() { return __p.__vector_as_array<sbyte>(4); }

  public static Offset<SubLinq.ArrowIr.DecimalLiteral> CreateDecimalLiteral(FlatBufferBuilder builder,
      VectorOffset valueOffset = default(VectorOffset)) {
    builder.StartTable(1);
    DecimalLiteral.AddValue(builder, valueOffset);
    return DecimalLiteral.EndDecimalLiteral(builder);
  }

  public static void StartDecimalLiteral(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, VectorOffset valueOffset) { builder.AddOffset(0, valueOffset.Value, 0); }
  public static VectorOffset CreateValueVector(FlatBufferBuilder builder, sbyte[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddSbyte(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateValueVectorBlock(FlatBufferBuilder builder, sbyte[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartValueVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static Offset<SubLinq.ArrowIr.DecimalLiteral> EndDecimalLiteral(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // value
    return new Offset<SubLinq.ArrowIr.DecimalLiteral>(o);
  }
};


}
