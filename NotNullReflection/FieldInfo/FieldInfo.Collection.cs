namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginFieldInfo = System.Reflection.FieldInfo;

/// <summary>
/// Discovers the attributes of a field and provides access to field metadata.
/// </summary>
public partial class FieldInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginFieldInfo"/> objects to a collection of <see cref="FieldInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginFieldInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="FieldInfo"/> objects.</returns>
    public static IEnumerable<FieldInfo> GetList(IEnumerable<OriginFieldInfo> collection) => from OriginFieldInfo Item in collection
                                                                                             select CreateNew(Item);
}
