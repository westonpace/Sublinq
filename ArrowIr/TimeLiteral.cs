// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TimeLiteral : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TimeLiteral GetRootAsTimeLiteral(ByteBuffer _bb) { return GetRootAsTimeLiteral(_bb, new TimeLiteral()); }
  public static TimeLiteral GetRootAsTimeLiteral(ByteBuffer _bb, TimeLiteral obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TimeLiteral __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }

  public static Offset<SubLinq.ArrowIr.TimeLiteral> CreateTimeLiteral(FlatBufferBuilder builder,
      long value = 0) {
    builder.StartTable(1);
    TimeLiteral.AddValue(builder, value);
    return TimeLiteral.EndTimeLiteral(builder);
  }

  public static void StartTimeLiteral(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, long value) { builder.AddLong(0, value, 0); }
  public static Offset<SubLinq.ArrowIr.TimeLiteral> EndTimeLiteral(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.TimeLiteral>(o);
  }
};


}
