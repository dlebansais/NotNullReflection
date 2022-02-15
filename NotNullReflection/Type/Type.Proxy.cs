namespace NotNullReflection;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Gets the fully qualified name of the type, including its namespace but not its assembly.
    /// </summary>
    public string FullName
    {
        get
        {
            return Origin.FullName!;
        }
    }
}
