// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct UInt8Literal : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static UInt8Literal GetRootAsUInt8Literal(ByteBuffer _bb) { return GetRootAsUInt8Literal(_bb, new UInt8Literal()); }
  public static UInt8Literal GetRootAsUInt8Literal(ByteBuffer _bb, UInt8Literal obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public UInt8Literal __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public byte Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte)0; } }

  public static Offset<SubLinq.ArrowIr.UInt8Literal> CreateUInt8Literal(FlatBufferBuilder builder,
      byte value = 0) {
    builder.StartTable(1);
    UInt8Literal.AddValue(builder, value);
    return UInt8Literal.EndUInt8Literal(builder);
  }

  public static void StartUInt8Literal(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, byte value) { builder.AddByte(0, value, 0); }
  public static Offset<SubLinq.ArrowIr.UInt8Literal> EndUInt8Literal(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.UInt8Literal>(o);
  }
};


}
