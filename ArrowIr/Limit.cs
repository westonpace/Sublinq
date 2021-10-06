// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// Limit operation
public struct Limit : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Limit GetRootAsLimit(ByteBuffer _bb) { return GetRootAsLimit(_bb, new Limit()); }
  public static Limit GetRootAsLimit(ByteBuffer _bb, Limit obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Limit __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Common options
  public Datalinq.ArrowIr.RelBase? Base { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.RelBase?)(new Datalinq.ArrowIr.RelBase()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// Child relation
  public Datalinq.ArrowIr.Relation? Rel { get { int o = __p.__offset(6); return o != 0 ? (Datalinq.ArrowIr.Relation?)(new Datalinq.ArrowIr.Relation()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// Starting index of rows
  public uint Offset { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  /// The maximum number of rows of output.
  public uint Count { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }

  public static Offset<Datalinq.ArrowIr.Limit> CreateLimit(FlatBufferBuilder builder,
      Offset<Datalinq.ArrowIr.RelBase> baseOffset = default(Offset<Datalinq.ArrowIr.RelBase>),
      Offset<Datalinq.ArrowIr.Relation> relOffset = default(Offset<Datalinq.ArrowIr.Relation>),
      uint offset = 0,
      uint count = 0) {
    builder.StartTable(4);
    Limit.AddCount(builder, count);
    Limit.AddOffset(builder, offset);
    Limit.AddRel(builder, relOffset);
    Limit.AddBase(builder, baseOffset);
    return Limit.EndLimit(builder);
  }

  public static void StartLimit(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddBase(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.RelBase> baseOffset) { builder.AddOffset(0, baseOffset.Value, 0); }
  public static void AddRel(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Relation> relOffset) { builder.AddOffset(1, relOffset.Value, 0); }
  public static void AddOffset(FlatBufferBuilder builder, uint offset) { builder.AddUint(2, offset, 0); }
  public static void AddCount(FlatBufferBuilder builder, uint count) { builder.AddUint(3, count, 0); }
  public static Offset<Datalinq.ArrowIr.Limit> EndLimit(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // base
    builder.Required(o, 6);  // rel
    return new Offset<Datalinq.ArrowIr.Limit>(o);
  }
};


}
