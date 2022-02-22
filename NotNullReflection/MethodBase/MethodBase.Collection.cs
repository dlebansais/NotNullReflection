namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginMethodBase = System.Reflection.MethodBase;

/// <summary>
/// Provides information about methods and constructors.
/// </summary>
public partial class MethodBase
{
    /// <summary>
    /// Converts a collection of <see cref="OriginMethodBase"/> objects to a collection of <see cref="MethodBase"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginMethodBase"/> to convert.</param>
    /// <returns>A collection of <see cref="MethodBase"/> objects.</returns>
    public static IEnumerable<MethodBase> GetList(IEnumerable<OriginMethodBase> collection) => from OriginMethodBase Item in collection
                                                                                               select new MethodBase(Item);
}
