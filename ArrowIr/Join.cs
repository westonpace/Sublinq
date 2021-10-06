// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Datalinq.ArrowIr
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

/// Join between two tables
public struct Join : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Join GetRootAsJoin(ByteBuffer _bb) { return GetRootAsJoin(_bb, new Join()); }
  public static Join GetRootAsJoin(ByteBuffer _bb, Join obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Join __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Common options
  public Datalinq.ArrowIr.RelBase? Base { get { int o = __p.__offset(4); return o != 0 ? (Datalinq.ArrowIr.RelBase?)(new Datalinq.ArrowIr.RelBase()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// Left relation
  public Datalinq.ArrowIr.Relation? Left { get { int o = __p.__offset(6); return o != 0 ? (Datalinq.ArrowIr.Relation?)(new Datalinq.ArrowIr.Relation()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// Right relation
  public Datalinq.ArrowIr.Relation? Right { get { int o = __p.__offset(8); return o != 0 ? (Datalinq.ArrowIr.Relation?)(new Datalinq.ArrowIr.Relation()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// The expression which will be evaluated against rows from each
  /// input to determine whether they should be included in the
  /// join relation's output.
  public Datalinq.ArrowIr.Expression? OnExpression { get { int o = __p.__offset(10); return o != 0 ? (Datalinq.ArrowIr.Expression?)(new Datalinq.ArrowIr.Expression()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  /// The kind of join to use.
  public Datalinq.ArrowIr.JoinKind JoinKind { get { int o = __p.__offset(12); return o != 0 ? (Datalinq.ArrowIr.JoinKind)__p.bb.Get(o + __p.bb_pos) : Datalinq.ArrowIr.JoinKind.Anti; } }

  public static Offset<Datalinq.ArrowIr.Join> CreateJoin(FlatBufferBuilder builder,
      Offset<Datalinq.ArrowIr.RelBase> baseOffset = default(Offset<Datalinq.ArrowIr.RelBase>),
      Offset<Datalinq.ArrowIr.Relation> leftOffset = default(Offset<Datalinq.ArrowIr.Relation>),
      Offset<Datalinq.ArrowIr.Relation> rightOffset = default(Offset<Datalinq.ArrowIr.Relation>),
      Offset<Datalinq.ArrowIr.Expression> on_expressionOffset = default(Offset<Datalinq.ArrowIr.Expression>),
      Datalinq.ArrowIr.JoinKind join_kind = Datalinq.ArrowIr.JoinKind.Anti) {
    builder.StartTable(5);
    Join.AddOnExpression(builder, on_expressionOffset);
    Join.AddRight(builder, rightOffset);
    Join.AddLeft(builder, leftOffset);
    Join.AddBase(builder, baseOffset);
    Join.AddJoinKind(builder, join_kind);
    return Join.EndJoin(builder);
  }

  public static void StartJoin(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddBase(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.RelBase> baseOffset) { builder.AddOffset(0, baseOffset.Value, 0); }
  public static void AddLeft(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Relation> leftOffset) { builder.AddOffset(1, leftOffset.Value, 0); }
  public static void AddRight(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Relation> rightOffset) { builder.AddOffset(2, rightOffset.Value, 0); }
  public static void AddOnExpression(FlatBufferBuilder builder, Offset<Datalinq.ArrowIr.Expression> onExpressionOffset) { builder.AddOffset(3, onExpressionOffset.Value, 0); }
  public static void AddJoinKind(FlatBufferBuilder builder, Datalinq.ArrowIr.JoinKind joinKind) { builder.AddByte(4, (byte)joinKind, 0); }
  public static Offset<Datalinq.ArrowIr.Join> EndJoin(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // base
    builder.Required(o, 6);  // left
    builder.Required(o, 8);  // right
    builder.Required(o, 10);  // on_expression
    return new Offset<Datalinq.ArrowIr.Join>(o);
  }
};


}
