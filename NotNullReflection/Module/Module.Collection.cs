namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginModule = System.Reflection.Module;

/// <summary>
/// Performs reflection on a module.
/// </summary>
public partial class Module
{
    /// <summary>
    /// Converts a collection of <see cref="OriginModule"/> objects to a collection of <see cref="Module"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginModule"/> to convert.</param>
    /// <returns>A collection of <see cref="Module"/> objects.</returns>
    public static IEnumerable<Module> GetList(IEnumerable<OriginModule> collection) => from OriginModule Item in collection
                                                                                       select CreateNew(Item);
}
