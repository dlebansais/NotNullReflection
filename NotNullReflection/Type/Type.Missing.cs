namespace NotNullReflection;

using OriginType = System.Type;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Represents a missing value in the System.Type information. This field is read-only.
    /// </summary>
    public static readonly Type Missing = new Type((OriginType)OriginType.Missing);
}
