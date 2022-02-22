namespace NotNullReflection;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Configuration.Assemblies;
using System.Security;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization;
using System.Linq;
using OriginAssembly = System.Reflection.Assembly;
using OriginCustomAttributeData = System.Reflection.CustomAttributeData;
using OriginTypeInfo = System.Reflection.TypeInfo;
using OriginMethodInfo = System.Reflection.MethodInfo;
using OriginModule = System.Reflection.Module;
using BindingFlags = System.Reflection.BindingFlags;
using OriginBinder = System.Reflection.Binder;
using OriginModuleResolveEventHandler = System.Reflection.ModuleResolveEventHandler;
using OriginManifestResourceInfo = System.Reflection.ManifestResourceInfo;
using OriginReflectionTypeLoadException = System.Reflection.ReflectionTypeLoadException;
using OriginAssemblyName = System.Reflection.AssemblyName;
using OriginType = System.Type;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    /// <summary>
    /// Gets a collection that contains this assembly's custom attributes.
    /// </summary>
    /// <returns>A collection that contains this assembly's custom attributes.</returns>
    public IEnumerable<OriginCustomAttributeData> CustomAttributes
    {
        get
        {
            return Origin.CustomAttributes;
        }
    }

    /// <summary>
    /// Gets a collection of the types defined in this assembly.
    /// </summary>
    /// <returns>A collection of the types defined in this assembly.</returns>
    public IEnumerable<OriginTypeInfo> DefinedTypes
    {
#if NET5_0_OR_GREATER
        [RequiresUnreferencedCode("Types might be removed")]
#endif
        get
        {
            return Origin.DefinedTypes;
        }
    }

    /// <summary>
    /// Gets the entry point of this assembly.
    /// </summary>
    /// <returns>An object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), this method throws an exception.</returns>
    /// <exception cref="NullReferenceException">Assembly doesn't have an entry point.</exception>
    public OriginMethodInfo EntryPoint
    {
        get
        {
            return Origin.EntryPoint ?? throw new NullReferenceException("Assembly doesn't have an entry point.");
        }
    }

    /// <summary>
    /// Gets a collection of the public types defined in this assembly that are visible outside the assembly.
    /// </summary>
    /// <returns>A collection of the public types defined in this assembly that are visible outside the assembly.</returns>
    public IEnumerable<Type> ExportedTypes
    {
#if NET5_0_OR_GREATER
        [RequiresUnreferencedCode("Types might be removed")]
#endif
        get
        {
            return Type.GetList(Origin.ExportedTypes);
        }
    }

    /// <summary>
    /// Gets the display name of the assembly.
    /// </summary>
    /// <returns>The display name of the assembly.</returns>
    /// <exception cref="NullReferenceException">Assembly doesn't have a display name.</exception>
    public string FullName
    {
        get
        {
            return Origin.FullName ?? throw new NullReferenceException("Assembly doesn't have a display name.");
        }
    }

    /// <summary>
    /// Gets the host context with which the assembly was loaded.
    /// </summary>
    /// <returns>A <see cref="long"/> value that indicates the host context with which the assembly was loaded, if any.</returns>
    public long HostContext
    {
        get
        {
            return Origin.HostContext;
        }
    }

    /// <summary>
    /// Gets a string representing the version of the common language runtime (CLR) saved in the file containing the manifest.
    /// </summary>
    /// <returns>The CLR version folder name. This is not a full path.</returns>
    public string ImageRuntimeVersion
    {
        get
        {
            return Origin.ImageRuntimeVersion;
        }
    }

#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether this assembly is held in a collectible System.Runtime.Loader.AssemblyLoadContext.
    /// </summary>
    /// <returns>true if this assembly is held in a collectible System.Runtime.Loader.AssemblyLoadContext; otherwise, false.</returns>
    public bool IsCollectible
    {
        get
        {
            return Origin.IsCollectible;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the current assembly was generated dynamically in the current process by using reflection emit.
    /// </summary>
    /// <returns>true if the current assembly was generated dynamically in the current process; otherwise, false.</returns>
    public bool IsDynamic
    {
        get
        {
            return Origin.IsDynamic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current assembly is loaded with full trust.
    /// </summary>
    /// <returns>true if the current assembly is loaded with full trust; otherwise, false.</returns>
    public bool IsFullyTrusted
    {
        get
        {
            return Origin.IsFullyTrusted;
        }
    }

    /// <summary>
    /// Gets the full path or UNC location of the loaded file that contains the manifest.
    /// </summary>
    /// <returns>The location of the loaded file that contains the manifest. If the assembly is loaded from a byte array, such as when using <see cref="OriginAssembly.Load(byte[])"/>, the value returned is an empty string ("").</returns>
    /// <exception cref="NotSupportedException">The current assembly is a dynamic assembly, represented by a <see cref="AssemblyBuilder"/> object.</exception>
    public string Location
    {
        get
        {
            return Origin.Location;
        }
    }

    /// <summary>
    /// Gets the module that contains the manifest for the current assembly.
    /// </summary>
    /// <returns>The module that contains the manifest for the assembly.</returns>
    public OriginModule ManifestModule
    {
        get
        {
            return Origin.ManifestModule;
        }
    }

    /// <summary>
    /// Gets a collection that contains the modules in this assembly.
    /// </summary>
    /// <returns>A collection that contains the modules in this assembly.</returns>
    public IEnumerable<OriginModule> Modules
    {
        get
        {
            return Origin.Modules;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this assembly was loaded into the reflection-only context.
    /// </summary>
    /// <returns>true if the assembly was loaded into the reflection-only context, rather than the execution context; otherwise, false.</returns>
    public bool ReflectionOnly
    {
        get
        {
            return Origin.ReflectionOnly;
        }
    }

    /// <summary>
    /// Gets a value that indicates which set of security rules the common language runtime (CLR) enforces for this assembly.
    /// </summary>
    /// <returns>The security rule set that the CLR enforces for this assembly.</returns>
    public SecurityRuleSet SecurityRuleSet
    {
        get
        {
            return Origin.SecurityRuleSet;
        }
    }

    /// <summary>
    /// Occurs when the common language runtime class loader cannot resolve a reference to an internal module of an assembly through normal means.
    /// </summary>
    public event OriginModuleResolveEventHandler ModuleResolve
    {
        add
        {
            Origin.ModuleResolve += value;
        }
        remove
        {
            Origin.ModuleResolve -= value;
        }
    }

    /// <summary>
    /// Locates the specified type from this assembly and creates an instance of it using the system activator, using case-sensitive search.
    /// </summary>
    /// <param name="typeName">The <see cref="Type.FullName"/> of the type to locate.</param>
    /// <returns>An instance of the specified type created with the parameterless constructor; if <paramref name="typeName"/> is not found this method throws an exception. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="BindingFlags"/> set to <see cref="BindingFlags.Public"/> or <see cref="BindingFlags.Instance"/>.</returns>
    /// <exception cref="ArgumentException"><paramref name="typeName"/> is an empty string ("") or a string beginning with a null character.</exception>
    /// <exception cref="ArgumentException">The current assembly was loaded into the reflection-only context.</exception>
    /// <exception cref="MissingMethodException">No matching constructor was found.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="typeName"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="typeName"/> requires a dependent assembly that was found but could not be loaded.</exception>
    /// <exception cref="FileLoadException">The current assembly was loaded into the reflection-only context, and <paramref name="typeName"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="typeName"/> requires a dependent assembly, but the file is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="typeName"/> requires a dependent assembly that was compiled for a version of the runtime that is later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="typeName"/> is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Assembly.CreateInstance is not supported with trimming. Use Type.GetType instead.")]
#endif
    public object CreateInstance(string typeName)
    {
        return Origin.CreateInstance(typeName) ?? throw new NullReferenceException($"{nameof(typeName)} is not found");
    }

    /// <summary>
    /// Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search.
    /// </summary>
    /// <param name="typeName">The <see cref="Type.FullName"/> of the type to locate.</param>
    /// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false.</param>
    /// <returns>An instance of the specified type created with the parameterless constructor; if <paramref name="typeName"/> is not found this method throws an exception. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="BindingFlags"/> set to <see cref="BindingFlags.Public"/> or <see cref="BindingFlags.Instance"/>.</returns>
    /// <exception cref="ArgumentException"><paramref name="typeName"/> is an empty string ("") or a string beginning with a null character.</exception>
    /// <exception cref="ArgumentException">The current assembly was loaded into the reflection-only context.</exception>
    /// <exception cref="MissingMethodException">No matching constructor was found.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="typeName"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="typeName"/> requires a dependent assembly that was found but could not be loaded.</exception>
    /// <exception cref="FileLoadException">The current assembly was loaded into the reflection-only context, and <paramref name="typeName"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="typeName"/> requires a dependent assembly, but the file is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="typeName"/> requires a dependent assembly that was compiled for a version of the runtime that is later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="typeName"/> is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Assembly.CreateInstance is not supported with trimming. Use Type.GetType instead.")]
#endif
    public object CreateInstance(string typeName, bool ignoreCase)
    {
        return Origin.CreateInstance(typeName, ignoreCase) ?? throw new NullReferenceException($"{nameof(typeName)} is not found");
    }

    /// <summary>
    /// Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search and having the specified culture, arguments, and binding and activation attributes.
    /// </summary>
    /// <param name="typeName">The <see cref="Type.FullName"/> of the type to locate.</param>
    /// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false.</param>
    /// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of bit flags from <see cref="BindingFlags"/>.</param>
    /// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="MemberInfo"/> objects via reflection. If binder is <see cref="DefaultBinder"/>, the default binder is used.</param>
    /// <param name="args">An array that contains the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the parameterless constructor is desired, args must be <see cref="Array.Empty"/>.</param>
    /// <param name="culture">An instance of <see cref="CultureInfo"/> used to govern the coercion of types. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired. (This is necessary to convert a string that represents 1000 to a <see cref="double"/> value, for example, since 1000 is represented differently by different cultures.)</param>
    /// <returns>An instance of the specified type; if <paramref name="typeName"/> is not found this method throws an exception. The supplied arguments are used to resolve the type, and to bind the constructor that is used to create the instance.</returns>
    /// <exception cref="ArgumentException"><paramref name="typeName"/> is an empty string ("") or a string beginning with a null character.</exception>
    /// <exception cref="ArgumentException">The current assembly was loaded into the reflection-only context.</exception>
    /// <exception cref="MissingMethodException">No matching constructor was found.</exception>
    /// <exception cref="NotSupportedException">A non-empty activation attributes array is passed to a type that does not inherit from <see cref="MarshalByRefObject"/>.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="typeName"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="typeName"/> requires a dependent assembly that was found but could not be loaded.</exception>
    /// <exception cref="FileLoadException">The current assembly was loaded into the reflection-only context, and <paramref name="typeName"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="typeName"/> requires a dependent assembly, but the file is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="typeName"/> requires a dependent assembly that was compiled for a version of the runtime that is later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="typeName"/> is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Assembly.CreateInstance is not supported with trimming. Use Type.GetType instead.")]
#endif
    public object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, OriginBinder binder, object[] args, CultureInfo culture)
    {
        return Origin.CreateInstance(typeName, ignoreCase, bindingAttr, binder != DefaultBinder ? binder : null, args, culture, null) ?? throw new NullReferenceException($"{nameof(typeName)} is not found");
    }

    /// <summary>
    /// Creates the name of a type qualified by the display name of its assembly.
    /// </summary>
    /// <param name="assemblyName">The display name of an assembly.</param>
    /// <param name="typeName">The full name of a type.</param>
    /// <returns>The full name of the type qualified by the display name of the assembly.</returns>
    /// <exception cref="NullReferenceException"><paramref name="assemblyName"/> or <paramref name="typeName"/> is null.</exception>
    public static string CreateQualifiedName(string assemblyName, string typeName)
    {
        return OriginAssembly.CreateQualifiedName(assemblyName, typeName) ?? throw new NullReferenceException($"{nameof(assemblyName)} or {nameof(typeName)} is null");
    }

    /// <summary>
    /// Determines whether this assembly and the specified object are equal.
    /// </summary>
    /// <param name="o">The object to compare with this instance.</param>
    /// <returns>true if o is equal to this instance; otherwise, false.</returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object o)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        return o is Assembly AsAssembly ? Origin.Equals(AsAssembly.Origin) : false;
    }

    /// <summary>
    /// Gets the currently loaded assembly in which the specified type is defined.
    /// </summary>
    /// <param name="type">An object representing a type in the assembly that will be returned.</param>
    /// <returns>The assembly in which the specified type is defined.</returns>
    /// <exception cref="NullReferenceException">Assembly not found.</exception>
    public static Assembly GetAssembly(Type type)
    {
        OriginAssembly Origin = OriginAssembly.GetAssembly(type.Origin) ?? throw new NullReferenceException("Assembly not found.");
        return new Assembly(Origin);
    }

    /// <summary>
    /// Gets all the custom attributes for this assembly.
    /// </summary>
    /// <param name="inherit">This argument is ignored for objects of type <see cref="OriginAssembly"/>.</param>
    /// <returns>An array that contains the custom attributes for this assembly.</returns>
    public object[] GetCustomAttributes(bool inherit)
    {
        return Origin.GetCustomAttributes(inherit);
    }

    /// <summary>
    /// Gets the custom attributes for this assembly as specified by type.
    /// </summary>
    /// <param name="attributeType">The type for which the custom attributes are to be returned.</param>
    /// <param name="inherit">This argument is ignored for objects of type <see cref="OriginAssembly"/>.</param>
    /// <returns>An array that contains the custom attributes for this assembly as specified by attributeType.</returns>
    /// <exception cref="ArgumentException"><paramref name="attributeType"/> is not a runtime type.</exception>
    public object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
        return Origin.GetCustomAttributes(attributeType.Origin, inherit);
    }

    /// <summary>
    /// Returns information about the attributes that have been applied to the current <see cref="OriginAssembly"/>, expressed as <see cref="OriginCustomAttributeData"/> objects.
    /// </summary>
    /// <returns>A generic list of <see cref="OriginCustomAttributeData"/> objects representing data about the attributes that have been applied to the current assembly.</returns>
    public IList<OriginCustomAttributeData> GetCustomAttributesData()
    {
        return Origin.GetCustomAttributesData();
    }

    /// <summary>
    /// Gets the process executable in the default application domain. In other application domains, this is the first executable that was executed by <see cref="AppDomain.ExecuteAssembly(string)"/>.
    /// </summary>
    /// <returns>The assembly that is the process executable in the default application domain, or the first executable that was executed by <see cref="AppDomain.ExecuteAssembly(string)"/>. Can throw an exception when called from unmanaged code.</returns>
    /// <exception cref="NullReferenceException">Assembly not found.</exception>
    public static Assembly GetEntryAssembly()
    {
        OriginAssembly Origin = OriginAssembly.GetEntryAssembly() ?? throw new NullReferenceException("Assembly not found.");
        return new Assembly(Origin);
    }

    /// <summary>
    /// Gets the assembly that contains the code that is currently executing.
    /// </summary>
    /// <returns>The assembly that contains the code that is currently executing.</returns>
    public static Assembly GetExecutingAssembly()
    {
        OriginAssembly Origin = OriginAssembly.GetCallingAssembly();
        return new Assembly(Origin);
    }

    /// <summary>
    /// Gets the public types defined in this assembly that are visible outside the assembly.
    /// </summary>
    /// <returns>An array that represents the types defined in this assembly that are visible outside the assembly.</returns>
    /// <exception cref="NotSupportedException">The assembly is a dynamic assembly.</exception>
    /// <exception cref="FileNotFoundException">Unable to load a dependent assembly.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type[] GetExportedTypes()
    {
        return Type.GetList(Origin.GetExportedTypes()).ToArray();
    }

    /// <summary>
    /// Gets a <see cref="FileStream"/> for the specified file in the file table of the manifest of this assembly.
    /// </summary>
    /// <param name="name">The name of the specified file. Do not include the path to the file.</param>
    /// <returns>A stream that contains the specified file; if the file is not found the method throws an exception.</returns>
    /// <exception cref="ArgumentException">The name parameter is an empty string ("").</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> was not found.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> is not a valid assembly.</exception>
#if NET6_0_OR_GREATER
    [RequiresAssemblyFiles("This member throws an exception for assemblies embedded in a single-file app")]
#endif
    public FileStream GetFile(string name)
    {
        return Origin.GetFile(name) ?? throw new FileNotFoundException($"{nameof(name)} was not found.");
    }

    /// <summary>
    /// Gets the files in the file table of an assembly manifest.
    /// </summary>
    /// <returns>An array of streams that contain the files.</returns>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileNotFoundException">A file was not found.</exception>
    /// <exception cref="BadImageFormatException">A file was not a valid assembly.</exception>
#if NET6_0_OR_GREATER
    [RequiresAssemblyFiles("This member throws an exception for assemblies embedded in a single-file app")]
#endif
    public FileStream[] GetFiles()
    {
        return Origin.GetFiles();
    }

    /// <summary>
    /// Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.
    /// </summary>
    /// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
    /// <returns>An array of streams that contain the files.</returns>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileNotFoundException">A file was not found.</exception>
    /// <exception cref="BadImageFormatException">A file was not a valid assembly.</exception>
#if NET6_0_OR_GREATER
    [RequiresAssemblyFiles("This member throws an exception for assemblies embedded in a single-file app")]
#endif
    public FileStream[] GetFiles(bool getResourceModules)
    {
        return Origin.GetFiles(getResourceModules);
    }

#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
    /// <summary>
    /// Gets forwarded types.
    /// </summary>
    /// <returns>The forwarded types.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type[] GetForwardedTypes()
    {
        return Type.GetList(Origin.GetForwardedTypes()).ToArray();
    }
#endif

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        return Origin.GetHashCode();
    }

    /// <summary>
    /// Gets all the loaded modules that are part of this assembly.
    /// </summary>
    /// <returns>An array of modules.</returns>
    public OriginModule[] GetLoadedModules()
    {
        return Origin.GetLoadedModules();
    }

    /// <summary>
    /// Gets all the loaded modules that are part of this assembly, specifying whether to include resource modules.
    /// </summary>
    /// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
    /// <returns>An array of modules.</returns>
    public OriginModule[] GetLoadedModules(bool getResourceModules)
    {
        return Origin.GetLoadedModules(getResourceModules);
    }

    /// <summary>
    /// Returns information about how the given resource has been persisted.
    /// </summary>
    /// <param name="resourceName">The case-sensitive name of the resource.</param>
    /// <returns>An object that is populated with information about the resource's topology; this method throws an exception if the resource is not found.</returns>
    /// <exception cref="ArgumentException">The <paramref name="resourceName"/> parameter is an empty string ("").</exception>
    /// <exception cref="NullReferenceException">Resource not found.</exception>
    public OriginManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
        return Origin.GetManifestResourceInfo(resourceName) ?? throw new NullReferenceException("Resource not found.");
    }

    /// <summary>
    /// Returns the names of all the resources in this assembly.
    /// </summary>
    /// <returns>An array that contains the names of all the resources.</returns>
    public string[] GetManifestResourceNames()
    {
        return Origin.GetManifestResourceNames();
    }

    /// <summary>
    /// Loads the specified manifest resource from this assembly.
    /// </summary>
    /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
    /// <returns>The manifest resource; this method throws an exception if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string ("").</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> was not found.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> is not a valid assembly.</exception>
    /// <exception cref="NotImplementedException">Resource length is greater than <see cref="long.MaxValue"/>.</exception>
    /// <exception cref="NullReferenceException">No resources were specified during compilation or the resource is not visible to the caller.</exception>
    public Stream GetManifestResourceStream(string name)
    {
        return Origin.GetManifestResourceStream(name) ?? throw new NullReferenceException("No resources were specified during compilation or the resource is not visible to the caller.");
    }

    /// <summary>
    /// Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.
    /// </summary>
    /// <param name="type">The type whose namespace is used to scope the manifest resource name.</param>
    /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
    /// <returns>The manifest resource; this method throws an exception if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string ("").</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> was not found.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> is not a valid assembly.</exception>
    /// <exception cref="NotImplementedException">Resource length is greater than <see cref="long.MaxValue"/>.</exception>
    /// <exception cref="NullReferenceException">No resources were specified during compilation or the resource is not visible to the caller.</exception>
    public Stream GetManifestResourceStream(Type type, string name)
    {
        return Origin.GetManifestResourceStream(type.Origin, name) ?? throw new NullReferenceException("No resources were specified during compilation or the resource is not visible to the caller.");
    }

    /// <summary>
    /// Gets the specified module in this assembly.
    /// </summary>
    /// <param name="name">The name of the module being requested.</param>
    /// <returns>The module being requested; this method throws an exception if if the module is not found.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string ("").</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> was not found.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> is not a valid assembly.</exception>
    /// <exception cref="NullReferenceException">Module not found.</exception>
    public OriginModule GetModule(string name)
    {
        return Origin.GetModule(name) ?? throw new NullReferenceException("Module not found.");
    }

    /// <summary>
    /// Gets all the modules that are part of this assembly.
    /// </summary>
    /// <returns>An array of modules.</returns>
    /// <exception cref="FileNotFoundException">The module to be loaded does not specify a file name extension.</exception>
    public OriginModule[] GetModules()
    {
        return Origin.GetModules();
    }

    /// <summary>
    /// Gets all the modules that are part of this assembly, specifying whether to include resource modules.
    /// </summary>
    /// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
    /// <returns>An array of modules.</returns>
    public OriginModule[] GetModules(bool getResourceModules)
    {
        return Origin.GetModules(getResourceModules);
    }

    /// <summary>
    /// Gets a <see cref="AssemblyName"/> for this assembly.
    /// </summary>
    /// <returns>An object that contains the fully parsed display name for this assembly.</returns>
    public AssemblyName GetName()
    {
        return new AssemblyName(Origin.GetName());
    }

    /// <summary>
    /// Gets a <see cref="AssemblyName"/> for this assembly, setting the codebase as specified by copiedName.
    /// </summary>
    /// <param name="copiedName">true to set the <see cref="OriginAssembly.CodeBase"/> to the location of the assembly after it was shadow copied; false to set <see cref="OriginAssembly.CodeBase"/> to the original location.</param>
    /// <returns>An object that contains the fully parsed display name for this assembly.</returns>
    public AssemblyName GetName(bool copiedName)
    {
        return new AssemblyName(Origin.GetName(copiedName));
    }

    /// <summary>
    /// Gets serialization information with all of the data needed to reinstantiate this assembly.
    /// </summary>
    /// <param name="info">The object to be populated with serialization information.</param>
    /// <param name="context">The destination context of the serialization.</param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        Origin.GetObjectData(info, context);
    }

    /// <summary>
    /// Gets the <see cref="AssemblyName"/> objects for all the assemblies referenced by this assembly.
    /// </summary>
    /// <returns>An array that contains the fully parsed display names of all the assemblies referenced by this assembly.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Assembly references might be removed")]
#endif
    public AssemblyName[] GetReferencedAssemblies()
    {
        return Enumerable.ToArray(AssemblyName.GetList(Origin.GetReferencedAssemblies()));
    }

    /// <summary>
    /// Gets the satellite assembly for the specified culture.
    /// </summary>
    /// <param name="culture">The specified culture.</param>
    /// <returns>The specified satellite assembly.</returns>
    /// <exception cref="FileNotFoundException">The assembly cannot be found.</exception>
    /// <exception cref="FileLoadException">The satellite assembly with a matching file name was found, but the <see cref="CultureInfo"/> did not match the one specified.</exception>
    /// <exception cref="BadImageFormatException">The satellite assembly is not a valid assembly.</exception>
    public Assembly GetSatelliteAssembly(CultureInfo culture)
    {
        OriginAssembly SatelliteOrigin = Origin.GetSatelliteAssembly(culture);
        return new Assembly(SatelliteOrigin);
    }

    /// <summary>
    /// Gets the specified version of the satellite assembly for the specified culture.
    /// </summary>
    /// <param name="culture">The specified culture.</param>
    /// <param name="version">The version of the satellite assembly.</param>
    /// <returns>The specified satellite assembly.</returns>
    /// <exception cref="FileLoadException">The satellite assembly with a matching file name was found, but the <see cref="CultureInfo"/> or the version did not match the one specified.</exception>
    /// <exception cref="FileNotFoundException">The assembly cannot be found.</exception>
    /// <exception cref="BadImageFormatException">The satellite assembly is not a valid assembly.</exception>
    public Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
    {
        OriginAssembly SatelliteOrigin = Origin.GetSatelliteAssembly(culture, version);
        return new Assembly(SatelliteOrigin);
    }

    /// <summary>
    /// Gets the <see cref="Type"/> object with the specified name in the assembly instance.
    /// </summary>
    /// <param name="name">The full name of the type.</param>
    /// <returns>An object that represents the specified class; this method throws an exception if the class is not found.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is invalid.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="name"/> requires a dependent assembly that was found but could not be loaded.</exception>
    /// <exception cref="FileLoadException">The current assembly was loaded into the reflection-only context, and <paramref name="name"/> requires a dependent assembly that was not preloaded. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> requires a dependent assembly, but the file is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException">Class was not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type GetType(string name)
    {
        return new Type(Origin.GetType(name) ?? throw new NullReferenceException("Class was not found."));
    }

    /// <summary>
    /// Gets the <see cref="Type"/> object with the specified name in the assembly instance and optionally throws <see cref="TypeLoadException"/> if the type is not found.
    /// </summary>
    /// <param name="name">The full name of the type.</param>
    /// <param name="throwOnError">true to throw <see cref="TypeLoadException"/> if the type is not found; false to throw <see cref="NullReferenceException"/>.</param>
    /// <returns>An object that represents the specified class.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is invalid.</exception>
    /// <exception cref="ArgumentException">The length of <paramref name="name"/> exceeds 1024 characters.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true, and the type cannot be found.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="name"/> requires a dependent assembly that was found but could not be loaded.</exception>
    /// <exception cref="FileLoadException">The current assembly was loaded into the reflection-only context, and <paramref name="name"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> requires a dependent assembly, but the file is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type cannot be found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type GetType(string name, bool throwOnError)
    {
        return new Type(Origin.GetType(name, throwOnError) ?? throw new NullReferenceException("Type cannot be found."));
    }

    /// <summary>
    /// Gets the <see cref="Type"/> object with the specified name in the assembly instance, with the options of ignoring the case, and of throwing an exception if the type is not found.
    /// </summary>
    /// <param name="name">The full name of the type.</param>
    /// <param name="throwOnError">true to throw <see cref="TypeLoadException"/> if the type is not found; false to throw <see cref="NullReferenceException"/>.</param>
    /// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false.</param>
    /// <returns>An object that represents the specified class.</returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is invalid.</exception>
    /// <exception cref="ArgumentException">The length of <paramref name="name"/> exceeds 1024 characters.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true, and the type cannot be found.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="name"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="name"/> requires a dependent assembly that was found but could not be loaded.</exception>
    /// <exception cref="FileLoadException">The current assembly was loaded into the reflection-only context, and <paramref name="name"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> requires a dependent assembly, but the file is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="name"/> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type cannot be found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
        return new Type(Origin.GetType(name, throwOnError, ignoreCase) ?? throw new NullReferenceException("Type cannot be found."));
    }

    /// <summary>
    /// Gets the types defined in this assembly.
    /// </summary>
    /// <returns>An array that contains all the types that are defined in this assembly.</returns>
    /// <exception cref="OriginReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="OriginReflectionTypeLoadException.Types"/> property of this exception contains a <see cref="Type"/> object for each type that was loaded and <see cref="Type.Missing"/> for each type that could not be loaded, while the <see cref="OriginReflectionTypeLoadException.LoaderExceptions"/> property contains an exception for each type that could not be loaded.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type[] GetTypes()
    {
        OriginType[] Result;

        try
        {
            Result = Origin.GetTypes();
        }
        catch (OriginReflectionTypeLoadException loadException)
        {
            OriginType[] LoadExceptionTypes = loadException.Types!;

            for (int i = 0; i < LoadExceptionTypes.Length; i++)
                if (LoadExceptionTypes[i] == null)
                    LoadExceptionTypes[i] = Type.Missing.Origin;

            throw loadException;
        }

        return Type.GetList(Result).ToArray();
    }

    /// <summary>
    /// Indicates whether or not a specified attribute has been applied to the assembly.
    /// </summary>
    /// <param name="attributeType">The type of the attribute to be checked for this assembly.</param>
    /// <param name="inherit">This argument is ignored for objects of this type.</param>
    /// <returns>true if the attribute has been applied to the assembly; otherwise, false.</returns>
    /// <exception cref="ArgumentException"><paramref name="attributeType"/> uses an invalid type.</exception>
    public bool IsDefined(Type attributeType, bool inherit)
    {
        return Origin.IsDefined(attributeType.Origin, inherit);
    }

    /// <summary>
    /// Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the application domain of the caller.
    /// </summary>
    /// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="BadImageFormatException"><paramref name="rawAssembly"/> is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException">Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly"/> was compiled with a later version.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
#endif
    public static Assembly Load(byte[] rawAssembly)
    {
        OriginAssembly Origin = OriginAssembly.Load(rawAssembly);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols for the assembly. The assembly is loaded into the application domain of the caller.
    /// </summary>
    /// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
    /// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="BadImageFormatException"><paramref name="rawAssembly"/> is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException">Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly"/> was compiled with a later version.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
#endif
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
        OriginAssembly Origin = OriginAssembly.Load(rawAssembly, rawSymbolStore);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads an assembly given its <see cref="AssemblyName"/>.
    /// </summary>
    /// <param name="assemblyRef">The object that describes the assembly to be loaded.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="FileNotFoundException"><paramref name="assemblyRef"/> is not found.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileLoadException"><paramref name="assemblyRef"/> specifies a remote assembly, but the ability to execute code in remote assemblies is disabled. See &lt;LoadFromRemoteSources&gt;. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyRef"/> is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException">Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef"/> was compiled with a later version.</exception>
    public static Assembly Load(AssemblyName assemblyRef)
    {
        OriginAssembly Origin = OriginAssembly.Load(assemblyRef.Origin);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads an assembly with the specified name.
    /// </summary>
    /// <param name="assemblyString">The long or short form of the assembly name.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="ArgumentException"><paramref name="assemblyString"/> is a zero-length string.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="assemblyString"/> is not found.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyString"/> is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException">Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString"/> was compiled with a later version.</exception>
    public static Assembly Load(string assemblyString)
    {
        OriginAssembly Origin = OriginAssembly.Load(assemblyString);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads the contents of an assembly file on the specified path.
    /// </summary>
    /// <param name="path">The fully qualified path of the file to load.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="ArgumentException">The <paramref name="path"/> argument is not an absolute path.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileLoadException">The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
    /// <exception cref="FileNotFoundException">The <paramref name="path"/> parameter is an empty string ("") or does not exist.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="path"/> is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException">Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path"/> was compiled with a later version.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
#endif
    public static Assembly LoadFile(string path)
    {
        OriginAssembly Origin = OriginAssembly.LoadFile(path);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads an assembly given its file name or path.
    /// </summary>
    /// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="ArgumentException">The <paramref name="assemblyFile"/> parameter is an empty string ("").</exception>
    /// <exception cref="PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="assemblyFile"/> is not found, or the module you are trying to load does not specify a filename extension.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileLoadException">The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyFile"/> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information.</exception>
    /// <exception cref="BadImageFormatException">Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile"/> was compiled with a later version.</exception>
    /// <exception cref="SecurityException">A codebase that does not start with "file://" was specified without the required System.Net.WebPermission.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
#endif
    public static Assembly LoadFrom(string assemblyFile)
    {
        OriginAssembly Origin = OriginAssembly.LoadFrom(assemblyFile);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads an assembly given its file name or path, hash value, and hash algorithm.
    /// </summary>
    /// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
    /// <param name="hashValue">The value of the computed hash code.</param>
    /// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="ArgumentException">The <paramref name="assemblyFile"/> parameter is an empty string ("").</exception>
    /// <exception cref="PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="assemblyFile"/> is not found, or the module you are trying to load does not specify a filename extension.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="FileLoadException">The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyFile"/> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyFile"/> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="SecurityException">A codebase that does not start with "file://" was specified without the required System.Net.WebPermission.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
#endif
    public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
        OriginAssembly Origin = OriginAssembly.LoadFrom(assemblyFile, hashValue, hashAlgorithm);
        return new Assembly(Origin);
    }

    /// <summary>
    /// Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file.
    /// </summary>
    /// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest.</param>
    /// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource.</param>
    /// <returns>The loaded module.</returns>
    /// <exception cref="ArgumentException"><paramref name="moduleName"/> does not match a file entry in this assembly's manifest.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="rawModule"/> is not a valid module.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded module depends on might be removed")]
#endif
    public OriginModule LoadModule(string moduleName, byte[]? rawModule)
    {
        return Origin.LoadModule(moduleName, rawModule);
    }

    /// <summary>
    /// Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file. The raw bytes representing the symbols for the module are also loaded.
    /// </summary>
    /// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest.</param>
    /// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource.</param>
    /// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be null if this is a resource file.</param>
    /// <returns>The loaded module.</returns>
    /// <exception cref="ArgumentException"><paramref name="moduleName"/> does not match a file entry in this assembly's manifest.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="rawModule"/> is not a valid module.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded module depends on might be removed")]
#endif
    public OriginModule LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
    {
        return Origin.LoadModule(moduleName, rawModule, rawSymbolStore);
    }

    /// <summary>
    /// Indicates whether two <see cref="Assembly"/> objects are equal.
    /// </summary>
    /// <param name="left">The assembly to compare to right.</param>
    /// <param name="right">The assembly to compare to left.</param>
    /// <returns>true if left is equal to right; otherwise, false.</returns>
    public static bool operator ==(Assembly left, Assembly right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="Assembly"/> objects are not equal.
    /// </summary>
    /// <param name="left">The assembly to compare to right.</param>
    /// <param name="right">The assembly to compare to left.</param>
    /// <returns>true if left is not equal to right; otherwise, false.</returns>
    public static bool operator !=(Assembly left, Assembly right)
    {
        return left.Origin != right.Origin;
    }

    /// <summary>
    /// Get Returns the full name of the assembly, also known as the display name.
    /// </summary>
    /// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
    public override string ToString()
    {
        return Origin.ToString();
    }

    /// <summary>
    /// Loads an assembly into the load-from context, bypassing some security checks.
    /// </summary>
    /// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
    /// <returns>The loaded assembly.</returns>
    /// <exception cref="ArgumentException">The <paramref name="assemblyFile"/> parameter is an empty string ("").</exception>
    /// <exception cref="PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="assemblyFile"/> is not found, or the module you are trying to load does not specify a filename extension.</exception>
    /// <exception cref="FileLoadException">A file that was found could not be loaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyFile"/> is not a valid assembly.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="assemblyFile"/> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="SecurityException">A codebase that does not start with "file://" was specified without the required System.Net.WebPermission.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
#endif
    public static Assembly UnsafeLoadFrom(string assemblyFile)
    {
        OriginAssembly Origin = OriginAssembly.UnsafeLoadFrom(assemblyFile);
        return new Assembly(Origin);
    }
}
