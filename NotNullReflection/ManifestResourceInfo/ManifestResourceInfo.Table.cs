namespace NotNullReflection;

using System.Collections.Generic;
using OriginManifestResourceInfo = System.Reflection.ManifestResourceInfo;

/// <summary>
/// Provides access to manifest resources, which are XML files that describe application dependencies.
/// </summary>
public partial class ManifestResourceInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="ManifestResourceInfo"/>.
    /// </summary>
    /// <param name="origin">The origin manifest resource information.</param>
    /// <returns>The new instance.</returns>
    internal static ManifestResourceInfo CreateNew(OriginManifestResourceInfo origin)
    {
        if (ManifestResourceInfoCache.ContainsKey(origin))
            return ManifestResourceInfoCache[origin];
        else
        {
            ManifestResourceInfo NewInstance = new ManifestResourceInfo(origin);
            ManifestResourceInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginManifestResourceInfo, ManifestResourceInfo> ManifestResourceInfoCache = new Dictionary<OriginManifestResourceInfo, ManifestResourceInfo>();
}
