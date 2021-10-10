// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// Order by relation
public struct OrderBy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static OrderBy GetRootAsOrderBy(ByteBuffer _bb) { return GetRootAsOrderBy(_bb, new OrderBy()); }
  public static OrderBy GetRootAsOrderBy(ByteBuffer _bb, OrderBy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public OrderBy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Common options
  public SubLinq.ArrowIr.RelBase? Base { get { int o = __p.__offset(4); return o != 0 ? (SubLinq.ArrowIr.RelBase?)(new SubLinq.ArrowIr.RelBase()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// Child relation
  public SubLinq.ArrowIr.Relation? Rel { get { int o = __p.__offset(6); return o != 0 ? (SubLinq.ArrowIr.Relation?)(new SubLinq.ArrowIr.Relation()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// Define sort order for rows of output.
  /// Keys with higher precedence are ordered ahead of other keys.
  public SubLinq.ArrowIr.SortKey? Keys(int j) { int o = __p.__offset(8); return o != 0 ? (SubLinq.ArrowIr.SortKey?)(new SubLinq.ArrowIr.SortKey()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int KeysLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<SubLinq.ArrowIr.OrderBy> CreateOrderBy(FlatBufferBuilder builder,
      Offset<SubLinq.ArrowIr.RelBase> baseOffset = default(Offset<SubLinq.ArrowIr.RelBase>),
      Offset<SubLinq.ArrowIr.Relation> relOffset = default(Offset<SubLinq.ArrowIr.Relation>),
      VectorOffset keysOffset = default(VectorOffset)) {
    builder.StartTable(3);
    OrderBy.AddKeys(builder, keysOffset);
    OrderBy.AddRel(builder, relOffset);
    OrderBy.AddBase(builder, baseOffset);
    return OrderBy.EndOrderBy(builder);
  }

  public static void StartOrderBy(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddBase(FlatBufferBuilder builder, Offset<SubLinq.ArrowIr.RelBase> baseOffset) { builder.AddOffset(0, baseOffset.Value, 0); }
  public static void AddRel(FlatBufferBuilder builder, Offset<SubLinq.ArrowIr.Relation> relOffset) { builder.AddOffset(1, relOffset.Value, 0); }
  public static void AddKeys(FlatBufferBuilder builder, VectorOffset keysOffset) { builder.AddOffset(2, keysOffset.Value, 0); }
  public static VectorOffset CreateKeysVector(FlatBufferBuilder builder, Offset<SubLinq.ArrowIr.SortKey>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateKeysVectorBlock(FlatBufferBuilder builder, Offset<SubLinq.ArrowIr.SortKey>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartKeysVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<SubLinq.ArrowIr.OrderBy> EndOrderBy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // base
    builder.Required(o, 6);  // rel
    builder.Required(o, 8);  // keys
    return new Offset<SubLinq.ArrowIr.OrderBy>(o);
  }
};


}
