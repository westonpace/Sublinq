// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct IntervalLiteral : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static IntervalLiteral GetRootAsIntervalLiteral(ByteBuffer _bb) { return GetRootAsIntervalLiteral(_bb, new IntervalLiteral()); }
  public static IntervalLiteral GetRootAsIntervalLiteral(ByteBuffer _bb, IntervalLiteral obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public IntervalLiteral __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Datalinq.ArrowIr.IntervalLiteralImpl ValueType { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.IntervalLiteralImpl)__p.bb.Get(o + __p.bb_pos) : Datalinq.ArrowIr.IntervalLiteralImpl.NONE; } }
  public TTable? Value<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(6); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public Datalinq.ArrowIr.IntervalLiteralMonths ValueAsIntervalLiteralMonths() { return Value<Datalinq.ArrowIr.IntervalLiteralMonths>().Value; }
  public Datalinq.ArrowIr.IntervalLiteralDaysMilliseconds ValueAsIntervalLiteralDaysMilliseconds() { return Value<Datalinq.ArrowIr.IntervalLiteralDaysMilliseconds>().Value; }

  public static Offset<Datalinq.ArrowIr.IntervalLiteral> CreateIntervalLiteral(FlatBufferBuilder builder,
      Datalinq.ArrowIr.IntervalLiteralImpl value_type = Datalinq.ArrowIr.IntervalLiteralImpl.NONE,
      int valueOffset = 0) {
    builder.StartTable(2);
    IntervalLiteral.AddValue(builder, valueOffset);
    IntervalLiteral.AddValueType(builder, value_type);
    return IntervalLiteral.EndIntervalLiteral(builder);
  }

  public static void StartIntervalLiteral(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddValueType(FlatBufferBuilder builder, Datalinq.ArrowIr.IntervalLiteralImpl valueType) { builder.AddByte(0, (byte)valueType, 0); }
  public static void AddValue(FlatBufferBuilder builder, int valueOffset) { builder.AddOffset(1, valueOffset, 0); }
  public static Offset<Datalinq.ArrowIr.IntervalLiteral> EndIntervalLiteral(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // value
    return new Offset<Datalinq.ArrowIr.IntervalLiteral>(o);
  }
};


}