namespace NotNullReflection;

using System.Diagnostics;
using OriginFieldInfo = System.Reflection.FieldInfo;

/// <summary>
/// Discovers the attributes of a field and provides access to field metadata.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class FieldInfo : MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FieldInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin field information.</param>
    private FieldInfo(OriginFieldInfo origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin field information this class encapsulates.
    /// </summary>
    public new OriginFieldInfo Origin { get; }
}
