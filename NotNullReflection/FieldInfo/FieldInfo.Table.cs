namespace NotNullReflection;

using System.Collections.Generic;
using OriginFieldInfo = System.Reflection.FieldInfo;

/// <summary>
/// Discovers the attributes of a field and provides access to field metadata.
/// </summary>
public partial class FieldInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="FieldInfo"/>.
    /// </summary>
    /// <param name="origin">The origin field information.</param>
    /// <returns>The new instance.</returns>
    internal static FieldInfo CreateNew(OriginFieldInfo origin)
    {
        if (FieldInfoCache.ContainsKey(origin))
            return FieldInfoCache[origin];
        else
        {
            FieldInfo NewInstance = new FieldInfo(origin);
            FieldInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginFieldInfo, FieldInfo> FieldInfoCache = new Dictionary<OriginFieldInfo, FieldInfo>();
}
