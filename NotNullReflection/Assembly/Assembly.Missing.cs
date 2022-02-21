namespace NotNullReflection;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    /// <summary>
    /// Represents a missing value in the <see cref="Assembly"/> information. This field is read-only.
    /// </summary>
    public static readonly Assembly Missing = new Assembly();
}
