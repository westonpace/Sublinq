// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Float64Literal : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Float64Literal GetRootAsFloat64Literal(ByteBuffer _bb) { return GetRootAsFloat64Literal(_bb, new Float64Literal()); }
  public static Float64Literal GetRootAsFloat64Literal(ByteBuffer _bb, Float64Literal obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Float64Literal __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public double Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetDouble(o + __p.bb_pos) : (double)0.0; } }

  public static Offset<SubLinq.ArrowIr.Float64Literal> CreateFloat64Literal(FlatBufferBuilder builder,
      double value = 0.0) {
    builder.StartTable(1);
    Float64Literal.AddValue(builder, value);
    return Float64Literal.EndFloat64Literal(builder);
  }

  public static void StartFloat64Literal(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, double value) { builder.AddDouble(0, value, 0.0); }
  public static Offset<SubLinq.ArrowIr.Float64Literal> EndFloat64Literal(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.Float64Literal>(o);
  }
};


}
