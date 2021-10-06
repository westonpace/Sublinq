// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// Boundary is preceding rows, determined by the contained expression
public struct Preceding : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Preceding GetRootAsPreceding(ByteBuffer _bb) { return GetRootAsPreceding(_bb, new Preceding()); }
  public static Preceding GetRootAsPreceding(ByteBuffer _bb, Preceding obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Preceding __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Datalinq.ArrowIr.ConcreteBoundImpl ImplType { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.ConcreteBoundImpl)__p.bb.Get(o + __p.bb_pos) : Datalinq.ArrowIr.ConcreteBoundImpl.NONE; } }
  public TTable? Impl<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(6); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public Datalinq.ArrowIr.Expression ImplAsExpression() { return Impl<Datalinq.ArrowIr.Expression>().Value; }
  public Datalinq.ArrowIr.Unbounded ImplAsUnbounded() { return Impl<Datalinq.ArrowIr.Unbounded>().Value; }

  public static Offset<Datalinq.ArrowIr.Preceding> CreatePreceding(FlatBufferBuilder builder,
      Datalinq.ArrowIr.ConcreteBoundImpl impl_type = Datalinq.ArrowIr.ConcreteBoundImpl.NONE,
      int implOffset = 0) {
    builder.StartTable(2);
    Preceding.AddImpl(builder, implOffset);
    Preceding.AddImplType(builder, impl_type);
    return Preceding.EndPreceding(builder);
  }

  public static void StartPreceding(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddImplType(FlatBufferBuilder builder, Datalinq.ArrowIr.ConcreteBoundImpl implType) { builder.AddByte(0, (byte)implType, 0); }
  public static void AddImpl(FlatBufferBuilder builder, int implOffset) { builder.AddOffset(1, implOffset, 0); }
  public static Offset<Datalinq.ArrowIr.Preceding> EndPreceding(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // impl
    return new Offset<Datalinq.ArrowIr.Preceding>(o);
  }
};


}