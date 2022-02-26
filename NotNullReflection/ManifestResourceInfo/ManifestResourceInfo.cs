namespace NotNullReflection;

using System.Diagnostics;
using OriginManifestResourceInfo = System.Reflection.ManifestResourceInfo;

/// <summary>
/// Provides access to manifest resources, which are XML files that describe application dependencies.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class ManifestResourceInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManifestResourceInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin manifest resource information.</param>
    private ManifestResourceInfo(OriginManifestResourceInfo origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin manifest resource information for which this class is a proxy.
    /// </summary>
    public OriginManifestResourceInfo Origin { get; }
}
