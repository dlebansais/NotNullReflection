namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginEventInfo = System.Reflection.EventInfo;

/// <summary>
/// Discovers the attributes of an event and provides access to event metadata.
/// </summary>
public partial class EventInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginEventInfo"/> objects to a collection of <see cref="EventInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginEventInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="EventInfo"/> objects.</returns>
    internal static IEnumerable<EventInfo> GetList(IEnumerable<OriginEventInfo> collection) => from OriginEventInfo Item in collection
                                                                                               select CreateNew(Item);
}
