// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr.Arrow
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Bool : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Bool GetRootAsBool(ByteBuffer _bb) { return GetRootAsBool(_bb, new Bool()); }
  public static Bool GetRootAsBool(ByteBuffer _bb, Bool obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Bool __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartBool(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<SubLinq.ArrowIr.Arrow.Bool> EndBool(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<SubLinq.ArrowIr.Arrow.Bool>(o);
  }
};


}
