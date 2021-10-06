// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// Literal relation
public struct LiteralRelation : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static LiteralRelation GetRootAsLiteralRelation(ByteBuffer _bb) { return GetRootAsLiteralRelation(_bb, new LiteralRelation()); }
  public static LiteralRelation GetRootAsLiteralRelation(ByteBuffer _bb, LiteralRelation obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LiteralRelation __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Common options
  public Datalinq.ArrowIr.RelBase? Base { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.RelBase?)(new Datalinq.ArrowIr.RelBase()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// The columns of this literal relation.
  public Datalinq.ArrowIr.LiteralColumn? Columns(int j) { int o = __p.__offset(6); return o != 0 ? (Datalinq.ArrowIr.LiteralColumn?)(new Datalinq.ArrowIr.LiteralColumn()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int ColumnsLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Datalinq.ArrowIr.LiteralRelation> CreateLiteralRelation(FlatBufferBuilder builder,
      Offset<Datalinq.ArrowIr.RelBase> baseOffset = default(Offset<Datalinq.ArrowIr.RelBase>),
      VectorOffset columnsOffset = default(VectorOffset)) {
    builder.StartTable(2);
    LiteralRelation.AddColumns(builder, columnsOffset);
    LiteralRelation.AddBase(builder, baseOffset);
    return LiteralRelation.EndLiteralRelation(builder);
  }

  public static void StartLiteralRelation(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddBase(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.RelBase> baseOffset) { builder.AddOffset(0, baseOffset.Value, 0); }
  public static void AddColumns(FlatBufferBuilder builder, VectorOffset columnsOffset) { builder.AddOffset(1, columnsOffset.Value, 0); }
  public static VectorOffset CreateColumnsVector(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.LiteralColumn>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateColumnsVectorBlock(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.LiteralColumn>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartColumnsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Datalinq.ArrowIr.LiteralRelation> EndLiteralRelation(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // base
    builder.Required(o, 6);  // columns
    return new Offset<Datalinq.ArrowIr.LiteralRelation>(o);
  }
};


}
