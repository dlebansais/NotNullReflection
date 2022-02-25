namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginPropertyInfo = System.Reflection.PropertyInfo;

/// <summary>
/// Discovers the attributes of a property and provides access to property metadata.
/// </summary>
public partial class PropertyInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginPropertyInfo"/> objects to a collection of <see cref="PropertyInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginPropertyInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="PropertyInfo"/> objects.</returns>
    public static IEnumerable<PropertyInfo> GetList(IEnumerable<OriginPropertyInfo> collection) => from OriginPropertyInfo Item in collection
                                                                                                   select CreateNew(Item);
}
