namespace NotNullReflection;

using System;
using OriginAssemblyName = System.Reflection.AssemblyName;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class AssemblyName : ICloneable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyName"/> class.
    /// </summary>
    /// <param name="origin">The origin assembly.</param>
    public AssemblyName(OriginAssemblyName origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin assembly for which this class is a proxy.
    /// </summary>
    public OriginAssemblyName Origin { get; }

    /// <summary>
    /// Makes a copy of this System.Reflection.AssemblyName object.
    /// </summary>
    /// <returns>An object that is a copy of this System.Reflection.AssemblyName object.</returns>
    public object Clone()
    {
        OriginAssemblyName CloneOrigin = (OriginAssemblyName)Origin.Clone();
        return new AssemblyName(CloneOrigin);
    }
}
