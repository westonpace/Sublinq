// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// A set of grouping keys
public struct Grouping : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Grouping GetRootAsGrouping(ByteBuffer _bb) { return GetRootAsGrouping(_bb, new Grouping()); }
  public static Grouping GetRootAsGrouping(ByteBuffer _bb, Grouping obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Grouping __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Expressions to group by
  public Datalinq.ArrowIr.Expression? Keys(int j) { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.Expression?)(new Datalinq.ArrowIr.Expression()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int KeysLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Datalinq.ArrowIr.Grouping> CreateGrouping(FlatBufferBuilder builder,
      VectorOffset keysOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Grouping.AddKeys(builder, keysOffset);
    return Grouping.EndGrouping(builder);
  }

  public static void StartGrouping(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddKeys(FlatBufferBuilder builder, VectorOffset keysOffset) { builder.AddOffset(0, keysOffset.Value, 0); }
  public static VectorOffset CreateKeysVector(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Expression>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateKeysVectorBlock(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Expression>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartKeysVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Datalinq.ArrowIr.Grouping> EndGrouping(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // keys
    return new Offset<Datalinq.ArrowIr.Grouping>(o);
  }
};


}