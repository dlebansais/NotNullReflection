namespace NotNullReflection;

using System;
using System.Configuration.Assemblies;
using System.Globalization;
using OriginAssemblyName = System.Reflection.AssemblyName;
using AssemblyContentType = System.Reflection.AssemblyContentType;
using AssemblyNameFlags = System.Reflection.AssemblyNameFlags;
using StrongNameKeyPair = System.Reflection.StrongNameKeyPair;
using ProcessorArchitecture = System.Reflection.ProcessorArchitecture;
using System.IO;
using System.Security;
using System.Runtime.Serialization;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class AssemblyName
{
    /// <summary>
    /// Gets the location of the assembly as a URL.
    /// </summary>
    /// <returns>A string that is the URL location of the assembly.</returns>
    /// <exception cref="NullReferenceException">The assembly URL is not supported.</exception>
    public string CodeBase
    {
        get
        {
            return Origin.CodeBase ?? throw new NullReferenceException("The assembly URL is not supported.");
        }
    }

    /// <summary>
    /// Gets a value that indicates what type of content the assembly contains.
    /// </summary>
    /// <returns>A value that indicates what type of content the assembly contains.</returns>
    public AssemblyContentType ContentType
    {
        get
        {
            return Origin.ContentType;
        }
    }

    /// <summary>
    /// Gets the culture supported by the assembly.
    /// </summary>
    /// <returns>An object that represents the culture supported by the assembly.</returns>
    /// <exception cref="NullReferenceException">The culture is not supported.</exception>
    public CultureInfo CultureInfo
    {
        get
        {
            return Origin.CultureInfo ?? throw new NullReferenceException("The culture is not supported.");
        }
    }

    /// <summary>
    /// Gets the name of the culture associated with the assembly.
    /// </summary>
    /// <returns>The culture name.</returns>
    /// <exception cref="NullReferenceException">The culture name is not supported.</exception>
    public string CultureName
    {
        get
        {
            return Origin.CultureName ?? throw new NullReferenceException("The culture name is not supported.");
        }
    }

    /// <summary>
    /// Gets the URI, including escape characters, that represents the codebase.
    /// </summary>
    /// <returns>A URI with escape characters.</returns>
    /// <exception cref="NullReferenceException">The code base is not supported.</exception>
    public string EscapedCodeBase
    {
        get
        {
            return Origin.EscapedCodeBase ?? throw new NullReferenceException("The code base is not supported.");
        }
    }

    /// <summary>
    /// Gets the attributes of the assembly.
    /// </summary>
    /// <returns>A value that represents the attributes of the assembly.</returns>
    public AssemblyNameFlags Flags
    {
        get
        {
            return Origin.Flags;
        }
    }

    /// <summary>
    /// Gets the full name of the assembly, also known as the display name.
    /// </summary>
    /// <returns>A string that is the full name of the assembly, also known as the display name.</returns>
    public string FullName
    {
        get
        {
            return Origin.FullName;
        }
    }

    /// <summary>
    /// Gets the hash algorithm used by the assembly manifest.
    /// </summary>
    /// <returns>The hash algorithm used by the assembly manifest.</returns>
    public AssemblyHashAlgorithm HashAlgorithm
    {
        get
        {
            return Origin.HashAlgorithm;
        }
    }

#if NETSTANDARD2_1 || NETCOREAPP3_1 || NET48 || NET5_0
    /// <summary>
    /// Gets the public and private cryptographic key pair that is used to create a strong name signature for the assembly.
    /// </summary>
    /// <returns>The public and private cryptographic key pair to be used to create a strong name for the assembly.</returns>
    /// <exception cref="NullReferenceException">The key pair is not supported.</exception>
    public StrongNameKeyPair KeyPair
    {
        get
        {
            return Origin.KeyPair ?? throw new NullReferenceException("The key pair is not supported.");
        }
    }
#endif

    /// <summary>
    /// Gets the simple name of the assembly. This is usually, but not necessarily, the file name of the manifest file of the assembly, minus its extension.
    /// </summary>
    /// <returns>The simple name of the assembly.</returns>
    /// <exception cref="NullReferenceException">The simple name is not supported.</exception>
    public string Name
    {
        get
        {
            return Origin.Name ?? throw new NullReferenceException("The simple name is not supported.");
        }
    }

    /// <summary>
    /// Gets a value that identifies the processor and bits-per-word of the platform targeted by an executable.
    /// </summary>
    /// <returns>One of the enumeration values that identifies the processor and bits-per-word of the platform targeted by an executable.</returns>
    public ProcessorArchitecture ProcessorArchitecture
    {
        get
        {
            return Origin.ProcessorArchitecture;
        }
    }

    /// <summary>
    /// Gets the major, minor, build, and revision numbers of the assembly.
    /// </summary>
    /// <returns>An object that represents the major, minor, build, and revision numbers of the assembly.</returns>
    /// <exception cref="NullReferenceException">The version is not supported.</exception>
    public Version Version
    {
        get
        {
            return Origin.Version ?? throw new NullReferenceException("The version is not supported.");
        }
    }

    /// <summary>
    /// Gets the information related to the assembly's compatibility with other assemblies.
    /// </summary>
    /// <returns>A value that represents information about the assembly's compatibility with other assemblies.</returns>
    public AssemblyVersionCompatibility VersionCompatibility
    {
        get
        {
            return Origin.VersionCompatibility;
        }
    }

    /// <summary>
    /// Gets the <see cref="AssemblyName"/> for a given file.
    /// </summary>
    /// <param name="assemblyFile">The path for the assembly whose <see cref="AssemblyName"/> is to be returned.</param>
    /// <returns>An object that represents the given assembly file.</returns>
    /// <exception cref="ArgumentException"><paramref name="assemblyFile"/> is invalid, such as an assembly with an invalid culture.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="assemblyFile"/> is not found.</exception>
    /// <exception cref="SecurityException">The caller does not have path discovery permission.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyFile"/> is not a valid assembly.</exception>
    /// <exception cref="FileLoadException">An assembly or module was loaded twice with two different sets of evidence.</exception>
    public static AssemblyName GetAssemblyName(string assemblyFile)
    {
        OriginAssemblyName LoadedOrigin = OriginAssemblyName.GetAssemblyName(assemblyFile);
        return new AssemblyName(LoadedOrigin);
    }

    /// <summary>
    /// Gets serialization information with all the data needed to recreate an instance of this <see cref="AssemblyName"/>.
    /// </summary>
    /// <param name="info">The object to be populated with serialization information.</param>
    /// <param name="context">The destination context of the serialization.</param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        Origin.GetObjectData(info, context);
    }

    /// <summary>
    /// Gets the public key of the assembly.
    /// </summary>
    /// <returns>A byte array that contains the public key of the assembly.</returns>
    /// <exception cref="SecurityException">A public key was provided (for example, by using the <see cref="System.Reflection.AssemblyName.SetPublicKey(byte[])"/> method), but no public key token was provided.</exception>
    /// <exception cref="NullReferenceException">The public key is not supported.</exception>
    public byte[] GetPublicKey()
    {
        return Origin.GetPublicKey() ?? throw new NullReferenceException("The public key is not supported.");
    }

    /// <summary>
    /// Gets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.
    /// </summary>
    /// <returns>A byte array that contains the public key token.</returns>
    /// <exception cref="NullReferenceException">The public key token is not supported.</exception>
    public byte[] GetPublicKeyToken()
    {
        return Origin.GetPublicKeyToken() ?? throw new NullReferenceException("The public key token is not supported.");
    }

    /// <summary>
    /// Returns a value indicating whether two assembly names are the same. The comparison is based on the simple assembly names.
    /// </summary>
    /// <param name="reference">The reference assembly name.</param>
    /// <param name="definition">The assembly name that is compared to the reference assembly.</param>
    /// <returns>true if the simple assembly names are the same; otherwise, false.</returns>
    public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
    {
        return OriginAssemblyName.ReferenceMatchesDefinition(reference.Origin, definition.Origin);
    }

    /// <summary>
    /// Returns the full name of the assembly, also known as the display name.
    /// </summary>
    /// <returns>The full name of the assembly, or the class name if the full name cannot be determined.</returns>
    public override string ToString()
    {
        return Origin.ToString();
    }
}
