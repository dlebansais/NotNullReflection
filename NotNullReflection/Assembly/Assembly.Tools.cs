namespace NotNullReflection;

using OriginType = System.Type;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    /// <summary>
    /// Gets the <see cref="Type"/> object with the specified name in the assembly instance if it exists.
    /// </summary>
    /// <param name="name">The full name of the type.</param>
    /// <param name="type">An object that represents the specified class, or <see cref="Type.Missing"/> if the class is not found.</param>
    /// <returns>true if the type is found; otherwise, false.</returns>
    public bool HasType(string name, out Type type)
    {
        OriginType? TestedType = Origin.GetType(name);

        if (TestedType is not null)
        {
            type = Type.CreateNew(TestedType);
            return true;
        }
        else
        {
            type = Type.Missing;
            return false;
        }
    }
}
