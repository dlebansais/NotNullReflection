namespace NotNullReflection;

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ArgumentException = System.ArgumentException;
using BadImageFormatException = System.BadImageFormatException;
using Guid = System.Guid;
using NullReferenceException = System.NullReferenceException;
using OriginAssembly = System.Reflection.Assembly;
using OriginAssemblyName = System.Reflection.AssemblyName;
using OriginType = System.Type;
using RuntimeTypeHandle = System.RuntimeTypeHandle;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TypeCode = System.TypeCode;
using TypedReference = System.TypedReference;
using TypeLoadException = System.TypeLoadException;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Gets the current <see cref="Type"/>.
    /// </summary>
    /// <returns>The current <see cref="Type"/>.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    public new Type GetType()
    {
        return CreateNew(Origin.GetType());
    }

    /// <summary>
    /// Gets the <see cref="Type"/> with the specified name, performing a case-sensitive search.
    /// </summary>
    /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
    /// <returns>The type with the specified name, if found; otherwise, thows an exception.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="ArgumentException"><paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- typeName represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="TypeLoadException"><paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
    /// <exception cref="NullReferenceException">Type not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("The type might be removed")]
#endif
    public static Type GetType(string typeName)
    {
        return CreateNew(OriginType.GetType(typeName) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the <see cref="Type"/> with the specified name, performing a case-sensitive search and specifying exception to throw if the type is not found.
    /// </summary>
    /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; <see langword="false"/> to throw a <see cref="NullReferenceException"/> exception. Specifying <see langword="false"/> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is <see langword="true"/> and the type is not found. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid syntax. For example, "MyType[,*,]". -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is <see langword="true"/> and the assembly or one of its dependencies was not found.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the type is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("The type might be removed")]
#endif
    public static Type GetType(string typeName, bool throwOnError)
    {
        return CreateNew(OriginType.GetType(typeName, throwOnError) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the <see cref="Type"/> with the specified name, specifying whether to throw an exception if the type is not found and whether to perform a case-sensitive search.
    /// </summary>
    /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; <see langword="false"/> to throw a <see cref="NullReferenceException"/> exception. Specifying <see langword="false"/> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <param name="ignoreCase"><see langword="true"/> to perform a case-insensitive search for <paramref name="typeName"/>, <see langword="false"/> to perform a case-sensitive search for <paramref name="typeName"/>.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is <see langword="true"/> and the type is not found. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid syntax. For example, "MyType[,*,]". -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is <see langword="true"/> and the assembly or one of its dependencies was not found.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the type is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("The type might be removed")]
#endif
    public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
        return CreateNew(OriginType.GetType(typeName, throwOnError, ignoreCase) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the type with the specified name, optionally providing custom methods to resolve the assembly and the type.
    /// </summary>
    /// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver"/> parameter is provided, the type name can be any string that <paramref name="typeResolver"/> is capable of resolving. If the <paramref name="assemblyResolver"/> parameter is provided or if standard type resolution is used, <paramref name="typeName"/> must be an assembly-qualified name (see <see cref="Type.AssemblyQualifiedName"/>), unless the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName"/>. The assembly name is passed to <paramref name="assemblyResolver"/> as an <see cref="OriginAssemblyName"/> object. If <paramref name="typeName"/> does not contain the name of an assembly, <paramref name="assemblyResolver"/> is not called. If <paramref name="assemblyResolver"/> is not supplied, standard assembly resolution is performed. Caution Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
    /// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName"/> from the assembly that is returned by <paramref name="assemblyResolver"/> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver"/> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; <see langword="false"/> is passed to that parameter. Caution Do not pass methods from unknown or untrusted callers.</param>
    /// <returns>The type with the specified name, or throws an exception if the type is not found.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="ArgumentException">An error occurs when <paramref name="typeName"/> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character). -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="TypeLoadException"><paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. -or- <paramref name="typeName"/> contains an invalid assembly name. -or- <paramref name="typeName"/> is a valid assembly name without a type name.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="NullReferenceException">Type not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("The type might be removed")]
#endif
    public static Type GetType(string typeName, System.Func<AssemblyName, Assembly> assemblyResolver, System.Func<Assembly, string, bool, Type> typeResolver)
    {
        System.Func<OriginAssemblyName, OriginAssembly?>? AssemblyResolver = (assemblyResolver != DefaultAssemblyResolver) ? (OriginAssemblyName name) => assemblyResolver(new AssemblyName(name)).Origin : null;
        System.Func<OriginAssembly?, string, bool, OriginType?>? TypeResolver = (typeResolver != DefaultTypeResolver) ? (OriginAssembly? assembly, string typeName, bool ignoreCase) => typeResolver(assembly is null ? Assembly.Missing : Assembly.CreateNew(assembly), typeName, ignoreCase).Origin : null;

        return CreateNew(OriginType.GetType(typeName, AssemblyResolver, TypeResolver) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the type with the specified name, specifying whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.
    /// </summary>
    /// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver"/> parameter is provided, the type name can be any string that <paramref name="typeResolver"/> is capable of resolving. If the <paramref name="assemblyResolver"/> parameter is provided or if standard type resolution is used, <paramref name="typeName"/> must be an assembly-qualified name (see <see cref="Type.AssemblyQualifiedName"/>), unless the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName"/>. The assembly name is passed to <paramref name="assemblyResolver"/> as an <see cref="OriginAssemblyName"/> object. If <paramref name="typeName"/> does not contain the name of an assembly, <paramref name="assemblyResolver"/> is not called. If <paramref name="assemblyResolver"/> is not supplied, standard assembly resolution is performed. Caution Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
    /// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName"/> from the assembly that is returned by <paramref name="assemblyResolver"/> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver"/> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; <see langword="false"/> is passed to that parameter. Caution Do not pass methods from unknown or untrusted callers.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; <see langword="false"/> to throw a <see cref="NullReferenceException"/> exception. Specifying <see langword="false"/> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is <see langword="true"/> and the type is not found. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException">An error occurs when <paramref name="typeName"/> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character). -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid syntax (for example, "MyType[,*,]"). -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is <see langword="true"/> and the assembly or one of its dependencies was not found. -or- <paramref name="typeName"/> contains an invalid assembly name. -or- <paramref name="typeName"/> is a valid assembly name without a type name.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the type is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("The type might be removed")]
#endif
    public static Type GetType(string typeName, System.Func<AssemblyName, Assembly> assemblyResolver, System.Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
    {
        System.Func<OriginAssemblyName, OriginAssembly?>? AssemblyResolver = (assemblyResolver != DefaultAssemblyResolver) ? (OriginAssemblyName name) => assemblyResolver(new AssemblyName(name)).Origin : null;
        System.Func<OriginAssembly?, string, bool, OriginType?>? TypeResolver = (typeResolver != DefaultTypeResolver) ? (OriginAssembly? assembly, string typeName, bool ignoreCase) => typeResolver(assembly is null ? Assembly.Missing : Assembly.CreateNew(assembly), typeName, ignoreCase).Origin : null;

        return CreateNew(OriginType.GetType(typeName, AssemblyResolver, TypeResolver, throwOnError) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the type with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.
    /// </summary>
    /// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver"/> parameter is provided, the type name can be any string that <paramref name="typeResolver"/> is capable of resolving. If the <paramref name="assemblyResolver"/> parameter is provided or if standard type resolution is used, <paramref name="typeName"/> must be an assembly-qualified name (see <see cref="Type.AssemblyQualifiedName"/>), unless the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName"/>. The assembly name is passed to <paramref name="assemblyResolver"/> as an <see cref="OriginAssemblyName"/> object. If <paramref name="typeName"/> does not contain the name of an assembly, <paramref name="assemblyResolver"/> is not called. If <paramref name="assemblyResolver"/> is not supplied, standard assembly resolution is performed. Caution Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
    /// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName"/> from the assembly that is returned by <paramref name="assemblyResolver"/> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver"/> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; <see langword="false"/> is passed to that parameter. Caution Do not pass methods from unknown or untrusted callers.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; <see langword="false"/> to throw a <see cref="NullReferenceException"/> exception. Specifying <see langword="false"/> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <param name="ignoreCase"><see langword="true"/> to perform a case-insensitive search for <paramref name="typeName"/>, <see langword="false"/> to perform a case-sensitive search for <paramref name="typeName"/>.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is <see langword="true"/> and the type is not found. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException">An error occurs when <paramref name="typeName"/> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character). -or- <paramref name="throwOnError"/> is <see langword="true"/> and <paramref name="typeName"/> contains invalid syntax (for example, "MyType[,*,]"). -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is <see langword="true"/> and the assembly or one of its dependencies was not found.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. -or- <paramref name="typeName"/> contains an invalid assembly name. -or- <paramref name="typeName"/> is a valid assembly name without a type name.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the type is not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("The type might be removed")]
#endif
    public static Type GetType(string typeName, System.Func<AssemblyName, Assembly> assemblyResolver, System.Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
    {
        System.Func<OriginAssemblyName, OriginAssembly?>? AssemblyResolver = (assemblyResolver != DefaultAssemblyResolver) ? (OriginAssemblyName name) => assemblyResolver(new AssemblyName(name)).Origin : null;
        System.Func<OriginAssembly?, string, bool, OriginType?>? TypeResolver = (typeResolver != DefaultTypeResolver) ? (OriginAssembly? assembly, string typeName, bool ignoreCase) => typeResolver(assembly is null ? Assembly.Missing : Assembly.CreateNew(assembly), typeName, ignoreCase).Origin : null;

        return CreateNew(OriginType.GetType(typeName, AssemblyResolver, TypeResolver, throwOnError, ignoreCase) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the types of the objects in the specified array.
    /// </summary>
    /// <param name="args">An array of objects whose types to determine.</param>
    /// <returns>An array of <see cref="Type"/> objects representing the types of the corresponding elements in args.</returns>
    /// <exception cref="TargetInvocationException">The class initializers are invoked and at least one throws an exception.</exception>
    public static Type[] GetTypeArray(object[] args)
    {
        return GetList(OriginType.GetTypeArray(args)).ToArray();
    }

    /// <summary>
    /// Gets the underlying type code of the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The type whose underlying type code to get.</param>
    /// <returns>The code of the underlying type, or <see cref="TypeCode.Empty"/> if type is <see cref="Missing"/>.</returns>
    public static TypeCode GetTypeCode(Type type)
    {
        return OriginType.GetTypeCode(type != Missing ? type.Origin : null);
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID).
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException">Platform not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid)
    {
        return CreateNew(OriginType.GetTypeFromCLSID(clsid) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID), specifying which exception to throw if an error occurs while loading the type.
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw any exception that occurs. -or- <see langword="false"/> to throw <see cref="NullReferenceException"/> if an error occurs.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the platform is not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
        return CreateNew(OriginType.GetTypeFromCLSID(clsid, throwOnError) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID) from the specified server.
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is <see cref="string.Empty"/>, this method automatically reverts to the local machine.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException">Platform not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid, string server)
    {
        return CreateNew(OriginType.GetTypeFromCLSID(clsid, server.Length > 0 ? server : null) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is <see cref="string.Empty"/>, this method automatically reverts to the local machine.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw any exception that occurs. -or- <see langword="false"/> to throw <see cref="NullReferenceException"/> if an error occurs.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the platform is not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
    {
        return CreateNew(OriginType.GetTypeFromCLSID(clsid, server.Length > 0 ? server : null, throwOnError) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type referenced by the specified type handle.
    /// </summary>
    /// <param name="handle">The object that refers to the type.</param>
    /// <returns>The type referenced by the specified <see cref="RuntimeTypeHandle"/>, or throws an exception if the <see cref="RuntimeTypeHandle.Value"/> property of handle is null.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="NullReferenceException"><paramref name="handle"/> value is null.</exception>
    public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
    {
        return CreateNew(OriginType.GetTypeFromHandle(handle));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (ProgID), throwing an exception if an error is encountered while loading the <see cref="Type"/>.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <returns>The type associated with the specified ProgID, if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID)
    {
        return CreateNew(OriginType.GetTypeFromProgID(progID) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (ProgID), specifying whether to throw an exception if an error occurs while loading the type.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw any exception that occurs. -or- <see langword="false"/> to throw the <see cref="NullReferenceException"/> exception if an error occurs.</param>
    /// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="COMException">The specified <paramref name="progID"/> is not registered.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID, bool throwOnError)
    {
        return CreateNew(OriginType.GetTypeFromProgID(progID, throwOnError) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (progID) from the specified server, throwing an exception if an error is encountered while loading the <see cref="Type"/>.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is <see cref="string.Empty"/>, this method automatically reverts to the local machine.</param>
    /// <returns>The type associated with the specified ProgID, if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID, string server)
    {
        return CreateNew(OriginType.GetTypeFromProgID(progID, server.Length > 0 ? server : null) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (progID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine.</param>
    /// <param name="throwOnError"><see langword="true"/> to throw any exception that occurs. -or- <see langword="false"/> to throw the <see cref="NullReferenceException"/> exception if an error occurs.</param>
    /// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="COMException">The specified <paramref name="progID"/> is not registered.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is <see langword="false"/>, and the platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
    {
        return CreateNew(OriginType.GetTypeFromProgID(progID, server.Length > 0 ? server : null, throwOnError) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the handle for the <see cref="Type"/> of a specified object.
    /// </summary>
    /// <param name="o">The object for which to get the type handle.</param>
    /// <returns>The handle for the <see cref="Type"/> of the specified <see cref="object"/>.</returns>
    public static RuntimeTypeHandle GetTypeHandle(object o)
    {
        return OriginType.GetTypeHandle(o);
    }
}
