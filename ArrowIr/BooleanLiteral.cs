// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct BooleanLiteral : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static BooleanLiteral GetRootAsBooleanLiteral(ByteBuffer _bb) { return GetRootAsBooleanLiteral(_bb, new BooleanLiteral()); }
  public static BooleanLiteral GetRootAsBooleanLiteral(ByteBuffer _bb, BooleanLiteral obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public BooleanLiteral __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public bool Value { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<SubLinq.ArrowIr.BooleanLiteral> CreateBooleanLiteral(FlatBufferBuilder builder,
      bool value = false) {
    builder.StartTable(1);
    BooleanLiteral.AddValue(builder, value);
    return BooleanLiteral.EndBooleanLiteral(builder);
  }

  public static void StartBooleanLiteral(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddValue(FlatBufferBuilder builder, bool value) { builder.AddBool(0, value, false); }
  public static Offset<SubLinq.ArrowIr.BooleanLiteral> EndBooleanLiteral(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.BooleanLiteral>(o);
  }
};


}
