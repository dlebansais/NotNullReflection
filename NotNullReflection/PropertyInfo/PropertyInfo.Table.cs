namespace NotNullReflection;

using System.Collections.Generic;
using OriginPropertyInfo = System.Reflection.PropertyInfo;

/// <summary>
/// Discovers the attributes of a property and provides access to property metadata.
/// </summary>
public partial class PropertyInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyInfo"/>.
    /// </summary>
    /// <param name="origin">The origin property information.</param>
    /// <returns>The new instance.</returns>
    internal static PropertyInfo CreateNew(OriginPropertyInfo origin)
    {
        if (PropertyInfoCache.ContainsKey(origin))
            return PropertyInfoCache[origin];
        else
        {
            PropertyInfo NewInstance = new PropertyInfo(origin);
            PropertyInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginPropertyInfo, PropertyInfo> PropertyInfoCache = new Dictionary<OriginPropertyInfo, PropertyInfo>();
}
