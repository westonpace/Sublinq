// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct UInt32Literal : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static UInt32Literal GetRootAsUInt32Literal(ByteBuffer _bb) { return GetRootAsUInt32Literal(_bb, new UInt32Literal()); }
  public static UInt32Literal GetRootAsUInt32Literal(ByteBuffer _bb, UInt32Literal obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public UInt32Literal __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public uint Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }

  public static Offset<SubLinq.ArrowIr.UInt32Literal> CreateUInt32Literal(FlatBufferBuilder builder,
      uint value = 0) {
    builder.StartTable(1);
    UInt32Literal.AddValue(builder, value);
    return UInt32Literal.EndUInt32Literal(builder);
  }

  public static void StartUInt32Literal(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, uint value) { builder.AddUint(0, value, 0); }
  public static Offset<SubLinq.ArrowIr.UInt32Literal> EndUInt32Literal(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.UInt32Literal>(o);
  }
};


}
