namespace NotNullReflection;

using System.Diagnostics;
using OriginAssembly = System.Reflection.Assembly;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class Assembly
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Assembly"/> class.
    /// </summary>
    /// <param name="origin">The origin assembly.</param>
    private Assembly(OriginAssembly origin)
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
    /// Gets the origin assembly this class encapsulates.
    /// </summary>
    public OriginAssembly Origin { get; }
}
