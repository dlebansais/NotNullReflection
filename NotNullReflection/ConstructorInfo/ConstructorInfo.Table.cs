namespace NotNullReflection;

using System.Collections.Generic;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;

/// <summary>
/// Discovers the attributes of a class constructor and provides access to constructor metadata.
/// </summary>
public partial class ConstructorInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="ConstructorInfo"/>.
    /// </summary>
    /// <param name="origin">The origin constructor information.</param>
    /// <returns>The new instance.</returns>
    public static ConstructorInfo CreateNew(OriginConstructorInfo origin)
    {
        if (ConstructorInfoCache.ContainsKey(origin))
            return ConstructorInfoCache[origin];
        else
        {
            ConstructorInfo NewInstance = new ConstructorInfo(origin);
            ConstructorInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginConstructorInfo, ConstructorInfo> ConstructorInfoCache = new Dictionary<OriginConstructorInfo, ConstructorInfo>();
}
