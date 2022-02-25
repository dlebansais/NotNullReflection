namespace NotNullReflection;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    private class Unused
    {
    }

    /// <summary>
    /// Represents a missing value in the <see cref="Type"/> information. This field is read-only.
    /// </summary>
    public static readonly Type Missing = new(typeof(Unused));

    private static Type GetInstanceOfMissing()
    {
        return Missing;
    }
}
