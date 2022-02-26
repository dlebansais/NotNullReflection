namespace NotNullReflection;

using System.Collections.Generic;
using OriginMethodInfo = System.Reflection.MethodInfo;

/// <summary>
/// Discovers the attributes of a method and provides access to method metadata.
/// </summary>
public partial class MethodInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="MethodInfo"/>.
    /// </summary>
    /// <param name="origin">The origin method information.</param>
    /// <returns>The new instance.</returns>
    internal static MethodInfo CreateNew(OriginMethodInfo origin)
    {
        if (MethodInfoCache.ContainsKey(origin))
            return MethodInfoCache[origin];
        else
        {
            MethodInfo NewInstance = new MethodInfo(origin);
            MethodInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginMethodInfo, MethodInfo> MethodInfoCache = new Dictionary<OriginMethodInfo, MethodInfo>();
}
