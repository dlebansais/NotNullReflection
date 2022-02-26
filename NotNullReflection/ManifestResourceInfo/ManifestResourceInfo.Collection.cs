namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginManifestResourceInfo = System.Reflection.ManifestResourceInfo;

/// <summary>
/// Provides access to manifest resources, which are XML files that describe application dependencies.
/// </summary>
public partial class ManifestResourceInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginManifestResourceInfo"/> objects to a collection of <see cref="ManifestResourceInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginManifestResourceInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="ManifestResourceInfo"/> objects.</returns>
    internal static IEnumerable<ManifestResourceInfo> GetList(IEnumerable<OriginManifestResourceInfo> collection) => from OriginManifestResourceInfo Item in collection
                                                                                                                     select CreateNew(Item);
}
