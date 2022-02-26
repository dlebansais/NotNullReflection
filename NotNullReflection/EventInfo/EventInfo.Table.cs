namespace NotNullReflection;

using System.Collections.Generic;
using OriginEventInfo = System.Reflection.EventInfo;

/// <summary>
/// Discovers the attributes of an event and provides access to event metadata.
/// </summary>
public partial class EventInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="EventInfo"/>.
    /// </summary>
    /// <param name="origin">The origin event information.</param>
    /// <returns>The new instance.</returns>
    internal static EventInfo CreateNew(OriginEventInfo origin)
    {
        if (EventInfoCache.ContainsKey(origin))
            return EventInfoCache[origin];
        else
        {
            EventInfo NewInstance = new EventInfo(origin);
            EventInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginEventInfo, EventInfo> EventInfoCache = new Dictionary<OriginEventInfo, EventInfo>();
}
