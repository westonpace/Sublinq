// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr.Arrow
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// Unicode with UTF-8 encoding
public struct Utf8 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Utf8 GetRootAsUtf8(ByteBuffer _bb) { return GetRootAsUtf8(_bb, new Utf8()); }
  public static Utf8 GetRootAsUtf8(ByteBuffer _bb, Utf8 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Utf8 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartUtf8(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Datalinq.ArrowIr.Arrow.Utf8> EndUtf8(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Datalinq.ArrowIr.Arrow.Utf8>(o);
  }
};


}
