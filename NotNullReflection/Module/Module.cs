namespace NotNullReflection;

using OriginModule = System.Reflection.Module;

/// <summary>
/// Performs reflection on a module.
/// </summary>
public partial class Module
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Module"/> class.
    /// </summary>
    /// <param name="origin">The origin field information.</param>
    private Module(OriginModule origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin field information for which this class is a proxy.
    /// </summary>
    public OriginModule Origin { get; }
}
