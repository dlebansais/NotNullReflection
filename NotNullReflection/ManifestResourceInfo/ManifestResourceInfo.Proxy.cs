namespace NotNullReflection;

using NullReferenceException = System.NullReferenceException;
using ResourceLocation = System.Reflection.ResourceLocation;

/// <summary>
/// Provides access to manifest resources, which are XML files that describe application dependencies.
/// </summary>
public partial class ManifestResourceInfo
{
    /// <summary>
    /// Gets the name of the file that contains the manifest resource, if it is not the same as the manifest file.
    /// </summary>
    /// <returns>The manifest resource's file name.</returns>
    /// <exception cref="NullReferenceException">No resource file name.</exception>
    public string FileName
    {
        get
        {
            return Origin.FileName ?? throw new NullReferenceException("No resource file name.");
        }
    }

    /// <summary>
    /// Gets the containing assembly for the manifest resource.
    /// </summary>
    /// <returns>The manifest resource's containing assembly.</returns>
    /// <exception cref="NullReferenceException">No resource referenced assembly.</exception>
    public Assembly ReferencedAssembly
    {
        get
        {
            return Assembly.CreateNew(Origin.ReferencedAssembly ?? throw new NullReferenceException("No resource referenced assembly."));
        }
    }

    /// <summary>
    /// Gets the manifest resource's location.
    /// </summary>
    /// <returns>A bitwise combination of System.Reflection.ResourceLocation flags that indicates the location of the manifest resource.</returns>
    public ResourceLocation ResourceLocation
    {
        get
        {
            return Origin.ResourceLocation;
        }
    }
}
