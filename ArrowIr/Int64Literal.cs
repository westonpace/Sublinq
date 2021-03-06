// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Int64Literal : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Int64Literal GetRootAsInt64Literal(ByteBuffer _bb) { return GetRootAsInt64Literal(_bb, new Int64Literal()); }
  public static Int64Literal GetRootAsInt64Literal(ByteBuffer _bb, Int64Literal obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Int64Literal __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }

  public static Offset<SubLinq.ArrowIr.Int64Literal> CreateInt64Literal(FlatBufferBuilder builder,
      long value = 0) {
    builder.StartTable(1);
    Int64Literal.AddValue(builder, value);
    return Int64Literal.EndInt64Literal(builder);
  }

  public static void StartInt64Literal(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, long value) { builder.AddLong(0, value, 0); }
  public static Offset<SubLinq.ArrowIr.Int64Literal> EndInt64Literal(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.Int64Literal>(o);
  }
};


}
