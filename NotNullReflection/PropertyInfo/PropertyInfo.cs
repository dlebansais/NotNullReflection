namespace NotNullReflection;

using System.Diagnostics;
using OriginPropertyInfo = System.Reflection.PropertyInfo;

/// <summary>
/// Discovers the attributes of a property and provides access to property metadata.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class PropertyInfo : MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin property information.</param>
    private PropertyInfo(OriginPropertyInfo origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin property information this class encapsulates.
    /// </summary>
    public new OriginPropertyInfo Origin { get; }
}
