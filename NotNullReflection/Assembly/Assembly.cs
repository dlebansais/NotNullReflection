namespace NotNullReflection;

using OriginAssembly = System.Reflection.Assembly;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Assembly"/> class.
    /// </summary>
    /// <param name="origin">The origin assembly.</param>
    internal Assembly(OriginAssembly origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Assembly"/> class.
    /// </summary>
    private Assembly()
    {
        Origin = OriginAssembly.GetExecutingAssembly();
    }

    /// <summary>
    /// Gets the origin assembly for which this class is a proxy.
    /// </summary>
    public OriginAssembly Origin { get; }
}
