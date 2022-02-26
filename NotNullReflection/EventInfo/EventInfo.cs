namespace NotNullReflection;

using System.Diagnostics;
using OriginEventInfo = System.Reflection.EventInfo;

/// <summary>
/// Discovers the attributes of an event and provides access to event metadata.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class EventInfo : MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin event information.</param>
    private EventInfo(OriginEventInfo origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin event information for which this class is a proxy.
    /// </summary>
    public new OriginEventInfo Origin { get; }
}
