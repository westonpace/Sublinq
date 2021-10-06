// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: plan.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Substrait.Protobuf {

  /// <summary>Holder for reflection information generated from plan.proto</summary>
  public static partial class PlanReflection {

    #region Descriptor
    /// <summary>File descriptor for plan.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PlanReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpwbGFuLnByb3RvEgxpby5zdWJzdHJhaXQaD3JlbGF0aW9ucy5wcm90bxoQ",
            "ZXh0ZW5zaW9ucy5wcm90byKYAQoEUGxhbhI2CgpleHRlbnNpb25zGAEgAygL",
            "MiIuaW8uc3Vic3RyYWl0LkV4dGVuc2lvbnMuRXh0ZW5zaW9uEjIKCG1hcHBp",
            "bmdzGAIgAygLMiAuaW8uc3Vic3RyYWl0LkV4dGVuc2lvbnMuTWFwcGluZxIk",
            "CglyZWxhdGlvbnMYAyADKAsyES5pby5zdWJzdHJhaXQuUmVsQhdQAaoCElN1",
            "YnN0cmFpdC5Qcm90b2J1ZmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Substrait.Protobuf.RelationsReflection.Descriptor, global::Substrait.Protobuf.ExtensionsReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Substrait.Protobuf.Plan), global::Substrait.Protobuf.Plan.Parser, new[]{ "Extensions", "Mappings", "Relations" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Describe a set of operations to complete.
  /// For compactness sake, identifiers are normalized at the plan level.
  /// </summary>
  public sealed partial class Plan : pb::IMessage<Plan>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Plan> _parser = new pb::MessageParser<Plan>(() => new Plan());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Plan> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Substrait.Protobuf.PlanReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Plan() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Plan(Plan other) : this() {
      extensions_ = other.extensions_.Clone();
      mappings_ = other.mappings_.Clone();
      relations_ = other.relations_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Plan Clone() {
      return new Plan(this);
    }

    /// <summary>Field number for the "extensions" field.</summary>
    public const int ExtensionsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Substrait.Protobuf.Extensions.Types.Extension> _repeated_extensions_codec
        = pb::FieldCodec.ForMessage(10, global::Substrait.Protobuf.Extensions.Types.Extension.Parser);
    private readonly pbc::RepeatedField<global::Substrait.Protobuf.Extensions.Types.Extension> extensions_ = new pbc::RepeatedField<global::Substrait.Protobuf.Extensions.Types.Extension>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Substrait.Protobuf.Extensions.Types.Extension> Extensions {
      get { return extensions_; }
    }

    /// <summary>Field number for the "mappings" field.</summary>
    public const int MappingsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Substrait.Protobuf.Extensions.Types.Mapping> _repeated_mappings_codec
        = pb::FieldCodec.ForMessage(18, global::Substrait.Protobuf.Extensions.Types.Mapping.Parser);
    private readonly pbc::RepeatedField<global::Substrait.Protobuf.Extensions.Types.Mapping> mappings_ = new pbc::RepeatedField<global::Substrait.Protobuf.Extensions.Types.Mapping>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Substrait.Protobuf.Extensions.Types.Mapping> Mappings {
      get { return mappings_; }
    }

    /// <summary>Field number for the "relations" field.</summary>
    public const int RelationsFieldNumber = 3;
    private static readonly pb::FieldCodec<global::Substrait.Protobuf.Rel> _repeated_relations_codec
        = pb::FieldCodec.ForMessage(26, global::Substrait.Protobuf.Rel.Parser);
    private readonly pbc::RepeatedField<global::Substrait.Protobuf.Rel> relations_ = new pbc::RepeatedField<global::Substrait.Protobuf.Rel>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Substrait.Protobuf.Rel> Relations {
      get { return relations_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Plan);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Plan other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!extensions_.Equals(other.extensions_)) return false;
      if(!mappings_.Equals(other.mappings_)) return false;
      if(!relations_.Equals(other.relations_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= extensions_.GetHashCode();
      hash ^= mappings_.GetHashCode();
      hash ^= relations_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      extensions_.WriteTo(output, _repeated_extensions_codec);
      mappings_.WriteTo(output, _repeated_mappings_codec);
      relations_.WriteTo(output, _repeated_relations_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      extensions_.WriteTo(ref output, _repeated_extensions_codec);
      mappings_.WriteTo(ref output, _repeated_mappings_codec);
      relations_.WriteTo(ref output, _repeated_relations_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += extensions_.CalculateSize(_repeated_extensions_codec);
      size += mappings_.CalculateSize(_repeated_mappings_codec);
      size += relations_.CalculateSize(_repeated_relations_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Plan other) {
      if (other == null) {
        return;
      }
      extensions_.Add(other.extensions_);
      mappings_.Add(other.mappings_);
      relations_.Add(other.relations_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            extensions_.AddEntriesFrom(input, _repeated_extensions_codec);
            break;
          }
          case 18: {
            mappings_.AddEntriesFrom(input, _repeated_mappings_codec);
            break;
          }
          case 26: {
            relations_.AddEntriesFrom(input, _repeated_relations_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            extensions_.AddEntriesFrom(ref input, _repeated_extensions_codec);
            break;
          }
          case 18: {
            mappings_.AddEntriesFrom(ref input, _repeated_mappings_codec);
            break;
          }
          case 26: {
            relations_.AddEntriesFrom(ref input, _repeated_relations_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
