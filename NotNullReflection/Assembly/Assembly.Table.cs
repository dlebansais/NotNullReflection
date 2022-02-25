namespace NotNullReflection;

using System.Collections.Generic;
using OriginAssembly = System.Reflection.Assembly;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    /// <summary>
    /// Creates a new instance of <see cref="Assembly"/>.
    /// </summary>
    /// <param name="origin">The origin assembly.</param>
    /// <returns>The new instance.</returns>
    public static Assembly CreateNew(OriginAssembly origin)
    {
        if (AssemblyCache.ContainsKey(origin))
            return AssemblyCache[origin];
        else
        {
            Assembly NewInstance = new Assembly(origin);
            AssemblyCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginAssembly, Assembly> AssemblyCache = new Dictionary<OriginAssembly, Assembly>();
}
