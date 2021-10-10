// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SubLinq.ArrowIr
{

/// A union of possible dereference operations
public enum Deref : byte
{
  NONE = 0,
  /// Access a value for a given map key
  MapKey = 1,
  /// Access the value at a struct field
  StructField = 2,
  /// Access the element at a given index in an array
  ArraySubscript = 3,
  /// Access a range of elements in an array
  ArraySlice = 4,
  /// Access a field of a relation
  FieldIndex = 5,
};


}
