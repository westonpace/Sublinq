// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct PassThrough : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static PassThrough GetRootAsPassThrough(ByteBuffer _bb) { return GetRootAsPassThrough(_bb, new PassThrough()); }
  public static PassThrough GetRootAsPassThrough(ByteBuffer _bb, PassThrough obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public PassThrough __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartPassThrough(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Datalinq.ArrowIr.PassThrough> EndPassThrough(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Datalinq.ArrowIr.PassThrough>(o);
  }
};


}