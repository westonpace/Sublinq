// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Float32Literal : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Float32Literal GetRootAsFloat32Literal(ByteBuffer _bb) { return GetRootAsFloat32Literal(_bb, new Float32Literal()); }
  public static Float32Literal GetRootAsFloat32Literal(ByteBuffer _bb, Float32Literal obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Float32Literal __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public float Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }

  public static Offset<SubLinq.ArrowIr.Float32Literal> CreateFloat32Literal(FlatBufferBuilder builder,
      float value = 0.0f) {
    builder.StartTable(1);
    Float32Literal.AddValue(builder, value);
    return Float32Literal.EndFloat32Literal(builder);
  }

  public static void StartFloat32Literal(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, float value) { builder.AddFloat(0, value, 0.0f); }
  public static Offset<SubLinq.ArrowIr.Float32Literal> EndFloat32Literal(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.Float32Literal>(o);
  }
};


}
