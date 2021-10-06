// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// A cast expression
public struct Cast : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Cast GetRootAsCast(ByteBuffer _bb) { return GetRootAsCast(_bb, new Cast()); }
  public static Cast GetRootAsCast(ByteBuffer _bb, Cast obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Cast __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// The expression to cast
  public Datalinq.ArrowIr.Expression? Operand { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.Expression?)(new Datalinq.ArrowIr.Expression()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// The type to cast to. This value is a `Field` to allow complete representation
  /// of arrow types.
  ///
  /// `Type` is unable to completely represent complex types like lists and
  /// maps.
  public Datalinq.ArrowIr.Arrow.Field? To { get { int o = __p.__offset(6); return o != 0 ? (Datalinq.ArrowIr.Arrow.Field?)(new Datalinq.ArrowIr.Arrow.Field()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Datalinq.ArrowIr.Cast> CreateCast(FlatBufferBuilder builder,
      Offset<Datalinq.ArrowIr.Expression> operandOffset = default(Offset<Datalinq.ArrowIr.Expression>),
      Offset<Datalinq.ArrowIr.Arrow.Field> toOffset = default(Offset<Datalinq.ArrowIr.Arrow.Field>)) {
    builder.StartTable(2);
    Cast.AddTo(builder, toOffset);
    Cast.AddOperand(builder, operandOffset);
    return Cast.EndCast(builder);
  }

  public static void StartCast(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddOperand(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Expression> operandOffset) { builder.AddOffset(0, operandOffset.Value, 0); }
  public static void AddTo(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Arrow.Field> toOffset) { builder.AddOffset(1, toOffset.Value, 0); }
  public static Offset<Datalinq.ArrowIr.Cast> EndCast(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // operand
    builder.Required(o, 6);  // to
    return new Offset<Datalinq.ArrowIr.Cast>(o);
  }
};


}
