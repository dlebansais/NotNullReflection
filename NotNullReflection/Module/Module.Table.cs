namespace NotNullReflection;

using System.Collections.Generic;
using OriginModule = System.Reflection.Module;

/// <summary>
/// Performs reflection on a module.
/// </summary>
public partial class Module
{
    /// <summary>
    /// Creates a new instance of <see cref="Module"/>.
    /// </summary>
    /// <param name="origin">The origin field information.</param>
    /// <returns>The new instance.</returns>
    public static Module CreateNew(OriginModule origin)
    {
        if (ModuleCache.ContainsKey(origin))
            return ModuleCache[origin];
        else
        {
            Module NewInstance = new Module(origin);
            ModuleCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginModule, Module> ModuleCache = new Dictionary<OriginModule, Module>();
}
