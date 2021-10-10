// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
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

  public SubLinq.ArrowIr.RelationImpl ImplType { get { int o = __p.__offset(4); return o != 0 ? (SubLinq.ArrowIr.RelationImpl)__p.bb.Get(o + __p.bb_pos) : SubLinq.ArrowIr.RelationImpl.NONE; } }
  public TTable? Impl<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(6); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public SubLinq.ArrowIr.Aggregate ImplAsAggregate() { return Impl<SubLinq.ArrowIr.Aggregate>().Value; }
  public SubLinq.ArrowIr.Filter ImplAsFilter() { return Impl<SubLinq.ArrowIr.Filter>().Value; }
  public SubLinq.ArrowIr.Join ImplAsJoin() { return Impl<SubLinq.ArrowIr.Join>().Value; }
  public SubLinq.ArrowIr.Limit ImplAsLimit() { return Impl<SubLinq.ArrowIr.Limit>().Value; }
  public SubLinq.ArrowIr.LiteralRelation ImplAsLiteralRelation() { return Impl<SubLinq.ArrowIr.LiteralRelation>().Value; }
  public SubLinq.ArrowIr.OrderBy ImplAsOrderBy() { return Impl<SubLinq.ArrowIr.OrderBy>().Value; }
  public SubLinq.ArrowIr.Project ImplAsProject() { return Impl<SubLinq.ArrowIr.Project>().Value; }
  public SubLinq.ArrowIr.SetOperation ImplAsSetOperation() { return Impl<SubLinq.ArrowIr.SetOperation>().Value; }
  public SubLinq.ArrowIr.Source ImplAsSource() { return Impl<SubLinq.ArrowIr.Source>().Value; }

  public static Offset<SubLinq.ArrowIr.Relation> CreateRelation(FlatBufferBuilder builder,
      SubLinq.ArrowIr.RelationImpl impl_type = SubLinq.ArrowIr.RelationImpl.NONE,
      int implOffset = 0) {
    builder.StartTable(2);
    Relation.AddImpl(builder, implOffset);
    Relation.AddImplType(builder, impl_type);
    return Relation.EndRelation(builder);
  }

  public static void StartRelation(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddImplType(FlatBufferBuilder builder, SubLinq.ArrowIr.RelationImpl implType) { builder.AddByte(0, (byte)implType, 0); }
  public static void AddImpl(FlatBufferBuilder builder, int implOffset) { builder.AddOffset(1, implOffset, 0); }
  public static Offset<SubLinq.ArrowIr.Relation> EndRelation(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // impl
    return new Offset<SubLinq.ArrowIr.Relation>(o);
  }
  public static void FinishRelationBuffer(FlatBufferBuilder builder, Offset<SubLinq.ArrowIr.Relation> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedRelationBuffer(FlatBufferBuilder builder, Offset<SubLinq.ArrowIr.Relation> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
