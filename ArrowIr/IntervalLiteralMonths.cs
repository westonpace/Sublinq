// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct IntervalLiteralMonths : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static IntervalLiteralMonths GetRootAsIntervalLiteralMonths(ByteBuffer _bb) { return GetRootAsIntervalLiteralMonths(_bb, new IntervalLiteralMonths()); }
  public static IntervalLiteralMonths GetRootAsIntervalLiteralMonths(ByteBuffer _bb, IntervalLiteralMonths obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public IntervalLiteralMonths __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Months { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Datalinq.ArrowIr.IntervalLiteralMonths> CreateIntervalLiteralMonths(FlatBufferBuilder builder,
      int months = 0) {
    builder.StartTable(1);
    IntervalLiteralMonths.AddMonths(builder, months);
    return IntervalLiteralMonths.EndIntervalLiteralMonths(builder);
  }

  public static void StartIntervalLiteralMonths(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddMonths(FlatBufferBuilder builder, int months) { builder.AddInt(0, months, 0); }
  public static Offset<Datalinq.ArrowIr.IntervalLiteralMonths> EndIntervalLiteralMonths(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Datalinq.ArrowIr.IntervalLiteralMonths>(o);
  }
};


}
