// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// A table holding an instance of the possible relation types.
public struct Relation : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Relation GetRootAsRelation(ByteBuffer _bb) { return GetRootAsRelation(_bb, new Relation()); }
  public static Relation GetRootAsRelation(ByteBuffer _bb, Relation obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Relation __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Datalinq.ArrowIr.RelationImpl ImplType { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.RelationImpl)__p.bb.Get(o + __p.bb_pos) : Datalinq.ArrowIr.RelationImpl.NONE; } }
  public TTable? Impl<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(6); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public Datalinq.ArrowIr.Aggregate ImplAsAggregate() { return Impl<Datalinq.ArrowIr.Aggregate>().Value; }
  public Datalinq.ArrowIr.Filter ImplAsFilter() { return Impl<Datalinq.ArrowIr.Filter>().Value; }
  public Datalinq.ArrowIr.Join ImplAsJoin() { return Impl<Datalinq.ArrowIr.Join>().Value; }
  public Datalinq.ArrowIr.Limit ImplAsLimit() { return Impl<Datalinq.ArrowIr.Limit>().Value; }
  public Datalinq.ArrowIr.LiteralRelation ImplAsLiteralRelation() { return Impl<Datalinq.ArrowIr.LiteralRelation>().Value; }
  public Datalinq.ArrowIr.OrderBy ImplAsOrderBy() { return Impl<Datalinq.ArrowIr.OrderBy>().Value; }
  public Datalinq.ArrowIr.Project ImplAsProject() { return Impl<Datalinq.ArrowIr.Project>().Value; }
  public Datalinq.ArrowIr.SetOperation ImplAsSetOperation() { return Impl<Datalinq.ArrowIr.SetOperation>().Value; }
  public Datalinq.ArrowIr.Source ImplAsSource() { return Impl<Datalinq.ArrowIr.Source>().Value; }

  public static Offset<Datalinq.ArrowIr.Relation> CreateRelation(FlatBufferBuilder builder,
      Datalinq.ArrowIr.RelationImpl impl_type = Datalinq.ArrowIr.RelationImpl.NONE,
      int implOffset = 0) {
    builder.StartTable(2);
    Relation.AddImpl(builder, implOffset);
    Relation.AddImplType(builder, impl_type);
    return Relation.EndRelation(builder);
  }

  public static void StartRelation(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddImplType(FlatBufferBuilder builder, Datalinq.ArrowIr.RelationImpl implType) { builder.AddByte(0, (byte)implType, 0); }
  public static void AddImpl(FlatBufferBuilder builder, int implOffset) { builder.AddOffset(1, implOffset, 0); }
  public static Offset<Datalinq.ArrowIr.Relation> EndRelation(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // impl
    return new Offset<Datalinq.ArrowIr.Relation>(o);
  }
  public static void FinishRelationBuffer(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Relation> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedRelationBuffer(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Relation> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}