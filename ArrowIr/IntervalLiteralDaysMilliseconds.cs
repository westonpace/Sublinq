// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct IntervalLiteralDaysMilliseconds : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static IntervalLiteralDaysMilliseconds GetRootAsIntervalLiteralDaysMilliseconds(ByteBuffer _bb) { return GetRootAsIntervalLiteralDaysMilliseconds(_bb, new IntervalLiteralDaysMilliseconds()); }
  public static IntervalLiteralDaysMilliseconds GetRootAsIntervalLiteralDaysMilliseconds(ByteBuffer _bb, IntervalLiteralDaysMilliseconds obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public IntervalLiteralDaysMilliseconds __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Days { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Milliseconds { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<SubLinq.ArrowIr.IntervalLiteralDaysMilliseconds> CreateIntervalLiteralDaysMilliseconds(FlatBufferBuilder builder,
      int days = 0,
      int milliseconds = 0) {
    builder.StartTable(2);
    IntervalLiteralDaysMilliseconds.AddMilliseconds(builder, milliseconds);
    IntervalLiteralDaysMilliseconds.AddDays(builder, days);
    return IntervalLiteralDaysMilliseconds.EndIntervalLiteralDaysMilliseconds(builder);
  }

  public static void StartIntervalLiteralDaysMilliseconds(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddDays(FlatBufferBuilder builder, int days) { builder.AddInt(0, days, 0); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(1, milliseconds, 0); }
  public static Offset<SubLinq.ArrowIr.IntervalLiteralDaysMilliseconds> EndIntervalLiteralDaysMilliseconds(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.IntervalLiteralDaysMilliseconds>(o);
  }
};


}
