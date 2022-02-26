namespace NotNullReflection;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Security;
using ArgumentException = System.ArgumentException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using BadImageFormatException = System.BadImageFormatException;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using CallingConventions = System.Reflection.CallingConventions;
using CustomAttributeData = System.Reflection.CustomAttributeData;
using Guid = System.Guid;
using ImageFileMachine = System.Reflection.ImageFileMachine;
using ModuleHandle = System.ModuleHandle;
using NullReferenceException = System.NullReferenceException;
using ParameterModifier = System.Reflection.ParameterModifier;
using PortableExecutableKinds = System.Reflection.PortableExecutableKinds;
using ReflectionTypeLoadException = System.Reflection.ReflectionTypeLoadException;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TypeFilter = System.Reflection.TypeFilter;
using TypeLoadException = System.TypeLoadException;

/// <summary>
/// Performs reflection on a module.
/// </summary>
public partial class Module
{
    /// <summary>
    /// Gets the appropriate <see cref="Assembly"/> for this instance of <see cref="Module"/>.
    /// </summary>
    /// <returns>An <see cref="Assembly"/> object.</returns>
    public Assembly Assembly
    {
        get
        {
            return Assembly.CreateNew(Origin.Assembly);
        }
    }

    /// <summary>
    /// Gets a collection that contains this module's custom attributes.
    /// </summary>
    /// <returns>A collection that contains this module's custom attributes.</returns>
    public IEnumerable<CustomAttributeData> CustomAttributes
    {
        get
        {
            return Origin.CustomAttributes;
        }
    }

    /// <summary>
    /// Gets a string representing the fully qualified name and path to this module.
    /// </summary>
    /// <returns>The fully qualified module name.</returns>
    /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
#if NET6_0_OR_GREATER
    [RequiresAssemblyFiles("Returns <Unknown> for modules with no file path")]
#endif
    public string FullyQualifiedName
    {
        get
        {
            return Origin.FullyQualifiedName;
        }
    }

    /// <summary>
    /// Gets the metadata stream version.
    /// </summary>
    /// <returns>A 32-bit integer representing the metadata stream version. The high-order two bytes represent the major version number, and the low-order two bytes represent the minor version number.</returns>
    public int MDStreamVersion
    {
        get
        {
            return Origin.MDStreamVersion;
        }
    }

    /// <summary>
    /// Gets a token that identifies the module in metadata.
    /// </summary>
    /// <returns>An integer token that identifies the current module in metadata.</returns>
    public int MetadataToken
    {
        get
        {
            return Origin.MetadataToken;
        }
    }

    /// <summary>
    /// Gets a handle for the module.
    /// </summary>
    /// <returns>A <see cref="ModuleHandle"/> structure for the current module.</returns>
    public ModuleHandle ModuleHandle
    {
        get
        {
            return Origin.ModuleHandle;
        }
    }

    /// <summary>
    /// Gets a universally unique identifier (UUID) that can be used to distinguish between two versions of a module.
    /// </summary>
    /// <returns>A <see cref="Guid"/> that can be used to distinguish between two versions of a module.</returns>
    public Guid ModuleVersionId
    {
        get
        {
            return Origin.ModuleVersionId;
        }
    }

    /// <summary>
    /// Gets a String representing the name of the module with the path removed.
    /// </summary>
    /// <returns>The module name with no path.</returns>
#if NET6_0_OR_GREATER
    [RequiresAssemblyFiles("Returns <Unknown> for modules with no file path")]
#endif
    public string Name
    {
        get
        {
            return Origin.Name;
        }
    }

    /// <summary>
    /// Gets a string representing the name of the module.
    /// </summary>
    /// <returns>The module name.</returns>
    public string ScopeName
    {
        get
        {
            return Origin.ScopeName;
        }
    }

    /// <summary>
    /// Determines whether this module and the specified object are equal.
    /// </summary>
    /// <param name="o">The object to compare with this instance.</param>
    /// <returns>true if <paramref name="o"/> is equal to this instance; otherwise, false.</returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object o)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        return o is Module AsModule && Origin.Equals(AsModule.Origin);
    }

    /// <summary>
    /// Returns an array of classes accepted by the given filter and filter criteria.
    /// </summary>
    /// <param name="filter">The delegate used to filter the classes.</param>
    /// <param name="filterCriteria">An <see cref="object"/> used to filter the classes.</param>
    /// <returns>An array of type <see cref="Type"/> containing classes that were accepted by the filter.</returns>
    /// <exception cref="ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type[] FindTypes(TypeFilter filter, object filterCriteria)
    {
        return Type.GetList(Origin.FindTypes(filter, filterCriteria)).ToArray();
    }

    /// <summary>
    /// Returns all custom attributes.
    /// </summary>
    /// <param name="inherit">This argument is ignored for objects of this type.</param>
    /// <returns>An array of type <see cref="object"/> containing all custom attributes.</returns>
    public object[] GetCustomAttributes(bool inherit)
    {
        return Origin.GetCustomAttributes(inherit);
    }

    /// <summary>
    /// Gets custom attributes of the specified type.
    /// </summary>
    /// <param name="attributeType">The type of attribute to get.</param>
    /// <param name="inherit">This argument is ignored for objects of this type.</param>
    /// <returns>An array of type <see cref="object"/> containing all custom attributes of the specified type.</returns>
    /// <exception cref="ArgumentException"><paramref name="attributeType"/> is not a <see cref="Type"/> object supplied by the runtime. For example, <paramref name="attributeType"/> is a <see cref="TypeBuilder"/> object.</exception>
    public object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
        return Origin.GetCustomAttributes(attributeType.Origin, inherit);
    }

    /// <summary>
    /// Returns a list of <see cref="CustomAttributeData"/> objects for the current module, which can be used in the reflection-only context.
    /// </summary>
    /// <returns>A generic list of <see cref="CustomAttributeData"/> objects representing data about the attributes that have been applied to the current module.</returns>
    public IList<CustomAttributeData> GetCustomAttributesData()
    {
        return Origin.GetCustomAttributesData();
    }

    /// <summary>
    /// Returns a field having the specified name.
    /// </summary>
    /// <param name="name">The field name.</param>
    /// <returns>A <see cref="FieldInfo"/> object having the specified name, or throws an exception if the field does not exist.</returns>
    /// <exception cref="NullReferenceException">Field does not exist.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Fields might be removed")]
#endif
    public FieldInfo GetField(string name)
    {
        return FieldInfo.CreateNew(Origin.GetField(name) ?? throw new NullReferenceException("Field does not exist."));
    }

    /// <summary>
    /// Returns a field having the specified name and binding attributes.
    /// </summary>
    /// <param name="name">The field name.</param>
    /// <param name="bindingAttr">One of the <see cref="BindingFlags"/> bit flags used to control the search.</param>
    /// <returns>A <see cref="FieldInfo"/> object having the specified name and binding attributes, or throws an exception if the field does not exist.</returns>
    /// <exception cref="NullReferenceException">Field does not exist.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Fields might be removed")]
#endif
    public FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
        return FieldInfo.CreateNew(Origin.GetField(name, bindingAttr) ?? throw new NullReferenceException("Field does not exist."));
    }

    /// <summary>
    /// Returns the global fields defined on the module.
    /// </summary>
    /// <returns>An array of <see cref="FieldInfo"/> objects representing the global fields defined on the module; if there are no global fields, an empty array is returned.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Fields might be removed")]
#endif
    public FieldInfo[] GetFields()
    {
        return FieldInfo.GetList(Origin.GetFields()).ToArray();
    }

    /// <summary>
    /// Returns the global fields defined on the module that match the specified binding flags.
    /// </summary>
    /// <param name="bindingFlags">A bitwise combination of <see cref="BindingFlags"/> values that limit the search.</param>
    /// <returns>An array of type <see cref="FieldInfo"/> representing the global fields defined on the module that match the specified binding flags; if no global fields match the binding flags, an empty array is returned.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Fields might be removed")]
#endif
    public FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
        return FieldInfo.GetList(Origin.GetFields(bindingFlags)).ToArray();
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        return Origin.GetHashCode();
    }

    /// <summary>
    /// Returns a method having the specified name.
    /// </summary>
    /// <param name="name">The method name.</param>
    /// <returns>A <see cref="MethodInfo"/> object having the specified name, or throws an exception if the method does not exist.</returns>
    /// <exception cref="NullReferenceException">Method does not exist.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Methods might be removed")]
#endif
    public MethodInfo GetMethod(string name)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name) ?? throw new NullReferenceException("Method does not exist."));
    }

    /// <summary>
    /// Returns a method having the specified name, binding information, calling convention, and parameter types and modifiers.
    /// </summary>
    /// <param name="name">The method name.</param>
    /// <param name="bindingAttr">One of the <see cref="BindingFlags"/> bit flags used to control the search.</param>
    /// <param name="binder">An object that implements <see cref="Binder"/>, containing properties related to this method.</param>
    /// <param name="callConvention">The calling convention for the method.</param>
    /// <param name="types">The parameter types to search for.</param>
    /// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
    /// <returns>A <see cref="MethodInfo"/> object in accordance with the specified criteria, or throws an exception if the method does not exist.</returns>
    /// <exception cref="NullReferenceException">Method does not exist.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Methods might be removed")]
#endif
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, bindingAttr, binder != Assembly.DefaultBinder ? binder : null, callConvention, Type.GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method does not exist."));
    }

    /// <summary>
    /// Returns a method having the specified name and parameter types.
    /// </summary>
    /// <param name="name">The method name.</param>
    /// <param name="types">The parameter types to search for.</param>
    /// <returns>A <see cref="MethodInfo"/> object in accordance with the specified criteria, or throws an exception if the method does not exist.</returns>
    /// <exception cref="NullReferenceException">Method does not exist.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Methods might be removed")]
#endif
    public MethodInfo GetMethod(string name, Type[] types)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, Type.GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method does not exist."));
    }

    /// <summary>
    /// Returns the global methods defined on the module.
    /// </summary>
    /// <returns>An array of <see cref="MethodInfo"/> objects representing all the global methods defined on the module; if there are no global methods, an empty array is returned.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Methods might be removed")]
#endif
    public MethodInfo[] GetMethods()
    {
        return MethodInfo.GetList(Origin.GetMethods()).ToArray();
    }

    /// <summary>
    /// Returns the global methods defined on the module that match the specified binding flags.
    /// </summary>
    /// <param name="bindingFlags">A bitwise combination of <see cref="BindingFlags"/> values that limit the search.</param>
    /// <returns>An array of type <see cref="MethodInfo"/> representing the global methods defined on the module that match the specified binding flags; if no global methods match the binding flags, an empty array is returned.</returns>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Methods might be removed")]
#endif
    public MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
        return MethodInfo.GetList(Origin.GetMethods(bindingFlags)).ToArray();
    }

    /// <summary>
    /// Provides an <see cref="ISerializable"/> implementation for serialized objects.
    /// </summary>
    /// <param name="info">The information and data needed to serialize or deserialize an object.</param>
    /// <param name="context">The context for the serialization.</param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        Origin.GetObjectData(info, context);
    }

    /// <summary>
    /// Gets a pair of values indicating the nature of the code in a module and the platform targeted by the module.
    /// </summary>
    /// <param name="peKind">When this method returns, a combination of the <see cref="PortableExecutableKinds"/> values indicating the nature of the code in the module.</param>
    /// <param name="machine">When this method returns, one of the <see cref="ImageFileMachine"/> values indicating the platform targeted by the module.</param>
    public void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
        Origin.GetPEKind(out peKind, out machine);
    }

    /// <summary>
    /// Returns the specified type, performing a case-sensitive search.
    /// </summary>
    /// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace.</param>
    /// <returns>A <see cref="Type"/> object representing the given type, if the type is in this module; otherwise, throws an exception.</returns>
    /// <exception cref="TargetInvocationException">The class initializers are invoked and an exception is thrown.</exception>
    /// <exception cref="ArgumentException"><paramref name="className"/> is a zero-length string.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="className"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="className"/> requires a dependent assembly that was found but could not be loaded. -or- The current assembly was loaded into the reflection-only context, and <paramref name="className"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="className"/> requires a dependent assembly, but the file is not a valid assembly. -or- <paramref name="className"/> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException">Type not in this module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type GetType(string className)
    {
        return Type.CreateNew(Origin.GetType(className) ?? throw new NullReferenceException("Type not in this module."));
    }

    /// <summary>
    /// Returns the specified type, searching the module with the specified case sensitivity.
    /// </summary>
    /// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace.</param>
    /// <param name="ignoreCase">true for case-insensitive search; otherwise, false.</param>
    /// <returns>A <see cref="Type"/> object representing the given type, if the type is in this module; otherwise, throws an exception.</returns>
    /// <exception cref="TargetInvocationException">The class initializers are invoked and an exception is thrown.</exception>
    /// <exception cref="ArgumentException"><paramref name="className"/> is a zero-length string.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="className"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="className"/> requires a dependent assembly that was found but could not be loaded. -or- The current assembly was loaded into the reflection-only context, and <paramref name="className"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="className"/> requires a dependent assembly, but the file is not a valid assembly. -or- <paramref name="className"/> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException">Type not in this module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type GetType(string className, bool ignoreCase)
    {
        return Type.CreateNew(Origin.GetType(className, ignoreCase) ?? throw new NullReferenceException("Type not in this module."));
    }

    /// <summary>
    /// Returns the specified type, specifying whether to make a case-sensitive search of the module and which exception to throw if the type cannot be found.
    /// </summary>
    /// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace.</param>
    /// <param name="throwOnError">true to throw <see cref="TypeLoadException"/> if the type is not found; false to throw <see cref="NullReferenceException"/>.</param>
    /// <param name="ignoreCase">true for case-insensitive search; otherwise, false.</param>
    /// <returns>A <see cref="Type"/> object representing the given type, if the type is in this module; otherwise, throws an exception.</returns>
    /// <exception cref="TargetInvocationException">The class initializers are invoked and an exception is thrown.</exception>
    /// <exception cref="ArgumentException"><paramref name="className"/> is a zero-length string.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true, and the type cannot be found.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="className"/> requires a dependent assembly that could not be found.</exception>
    /// <exception cref="FileLoadException"><paramref name="className"/> requires a dependent assembly that was found but could not be loaded. -or- The current assembly was loaded into the reflection-only context, and <paramref name="className"/> requires a dependent assembly that was not preloaded.</exception>
    /// <exception cref="BadImageFormatException"><paramref name="className"/> requires a dependent assembly, but the file is not a valid assembly. -or- <paramref name="className"/> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type not in this module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
        return Type.CreateNew(Origin.GetType(className, throwOnError, ignoreCase) ?? throw new NullReferenceException("Type not in this module."));
    }

    /// <summary>
    /// Returns all the types defined within this module.
    /// </summary>
    /// <returns>An array of type <see cref="Type"/> containing types defined within the module that is reflected by this instance.</returns>
    /// <exception cref="ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
    /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Types might be removed")]
#endif
    public Type[] GetTypes()
    {
        return Type.GetList(Origin.GetTypes()).ToArray();
    }

    /// <summary>
    /// Returns a value that indicates whether the specified attribute type has been applied to this module.
    /// </summary>
    /// <param name="attributeType">The type of custom attribute to test for.</param>
    /// <param name="inherit">This argument is ignored for objects of this type.</param>
    /// <returns>true if one or more instances of <paramref name="attributeType"/> have been applied to this module; otherwise, false.</returns>
    /// <exception cref="ArgumentException"><paramref name="attributeType"/> is not a <see cref="Type"/> object supplied by the runtime. For example, <paramref name="attributeType"/> is a <see cref="TypeBuilder"/> object.</exception>
    public bool IsDefined(Type attributeType, bool inherit)
    {
        return Origin.IsDefined(attributeType.Origin, inherit);
    }

    /// <summary>
    /// Gets a value indicating whether the object is a resource.
    /// </summary>
    /// <returns>true if the object is a resource; otherwise, false.</returns>
    public bool IsResource()
    {
        return Origin.IsResource();
    }

    /// <summary>
    /// Indicates whether two <see cref="Module"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(Module left, Module right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="Module"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(Module left, Module right)
    {
        return left.Origin != right.Origin;
    }

    /// <summary>
    /// Returns the field identified by the specified metadata token.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
    /// <returns>A <see cref="FieldInfo"/> object representing the field that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a field in the scope of the current module. -or- <paramref name="metadataToken"/> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
    /// <exception cref="NullReferenceException">Metadata token not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public FieldInfo ResolveField(int metadataToken)
    {
        return FieldInfo.CreateNew(Origin.ResolveField(metadataToken) ?? throw new NullReferenceException("Metadata token not found."));
    }

    /// <summary>
    /// Returns the field identified by the specified metadata token, in the context defined by the specified generic type parameters.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
    /// <param name="genericTypeArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the type where the token is in scope, or an empty array if that type is not generic.</param>
    /// <param name="genericMethodArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the method where the token is in scope, or an empty array if that method is not generic.</param>
    /// <returns>A <see cref="FieldInfo"/> object representing the field that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a field in the scope of the current module. -or- <paramref name="metadataToken"/> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments"/> and <paramref name="genericMethodArguments"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
    /// <exception cref="NullReferenceException">Metadata token not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
        return FieldInfo.CreateNew(Origin.ResolveField(metadataToken, genericTypeArguments.Length > 0 ? Type.GetOriginList(genericTypeArguments).ToArray() : null, genericMethodArguments.Length > 0 ? Type.GetOriginList(genericMethodArguments).ToArray() : null) ?? throw new NullReferenceException("Metadata token not found."));
    }

    /// <summary>
    /// Returns the type or member identified by the specified metadata token.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
    /// <returns>A <see cref="MemberInfo"/> object representing the type or member that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a type or member in the scope of the current module. -or- <paramref name="metadataToken"/> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method). -or- <paramref name="metadataToken"/> identifies a property or event.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
    /// <exception cref="NullReferenceException">Metadata token not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public MemberInfo ResolveMember(int metadataToken)
    {
        return MemberInfo.CreateNew(Origin.ResolveMember(metadataToken) ?? throw new NullReferenceException("Metadata token not found."));
    }

    /// <summary>
    /// Returns the type or member identified by the specified metadata token, in the context defined by the specified generic type parameters.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
    /// <param name="genericTypeArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the type where the token is in scope, or an empty array if that type is not generic.</param>
    /// <param name="genericMethodArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the method where the token is in scope, or an empty array if that method is not generic.</param>
    /// <returns>A <see cref="MemberInfo"/> object representing the type or member that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a type or member in the scope of the current module. -or- <paramref name="metadataToken"/> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments"/> and <paramref name="genericMethodArguments"/>. -or- metadataToken identifies a property or event.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
    /// <exception cref="NullReferenceException">Metadata token not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
        return MemberInfo.CreateNew(Origin.ResolveMember(metadataToken, genericTypeArguments.Length > 0 ? Type.GetOriginList(genericTypeArguments).ToArray() : null, genericMethodArguments.Length > 0 ? Type.GetOriginList(genericMethodArguments).ToArray() : null) ?? throw new NullReferenceException("Metadata token not found."));
    }

    /// <summary>
    /// Returns the method or constructor identified by the specified metadata token.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
    /// <returns>A System.Reflection.MethodBase object representing the method or constructor that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException">metadataToken is not a token for a method or constructor in the scope of the current module. -or- metadataToken is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
    /// <exception cref="NullReferenceException">Metadata token not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public MethodBase ResolveMethod(int metadataToken)
    {
        return MethodBase.CreateNew(Origin.ResolveMethod(metadataToken) ?? throw new NullReferenceException("Metadata token not found."));
    }

    /// <summary>
    /// Returns the method or constructor identified by the specified metadata token, in the context defined by the specified generic type parameters.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
    /// <param name="genericTypeArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the type where the token is in scope, or an empty array if that type is not generic.</param>
    /// <param name="genericMethodArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the method where the token is in scope, or an empty array if that method is not generic.</param>
    /// <returns>A System.Reflection.MethodBase object representing the method that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a method or constructor in the scope of the current module. -or- <paramref name="metadataToken"/> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments"/> and <paramref name="genericMethodArguments"/>. -or- metadataToken identifies a property or event.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
    /// <exception cref="NullReferenceException">Metadata token not found.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
        return MethodBase.CreateNew(Origin.ResolveMethod(metadataToken, genericTypeArguments.Length > 0 ? Type.GetOriginList(genericTypeArguments).ToArray() : null, genericMethodArguments.Length > 0 ? Type.GetOriginList(genericMethodArguments).ToArray() : null) ?? throw new NullReferenceException("Metadata token not found."));
    }

    /// <summary>
    /// Returns the signature blob identified by a metadata token.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a signature in the module.</param>
    /// <returns>An array of bytes representing the signature blob.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a valid MemberRef, MethodDef, TypeSpec, signature, or FieldDef token in the scope of the current module.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public byte[] ResolveSignature(int metadataToken)
    {
        return Origin.ResolveSignature(metadataToken);
    }

    /// <summary>
    /// Returns the string identified by the specified metadata token.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a string in the string heap of the module.</param>
    /// <returns>A <see cref="string"/> containing a string value from the metadata string heap.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a string in the scope of the current module.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public string ResolveString(int metadataToken)
    {
        return Origin.ResolveString(metadataToken);
    }

    /// <summary>
    /// Returns the type identified by the specified metadata token.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
    /// <returns>A <see cref="Type"/> object representing the type that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a type in the scope of the current module. -or- <paramref name="metadataToken"/> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public Type ResolveType(int metadataToken)
    {
        return Type.CreateNew(Origin.ResolveType(metadataToken));
    }

    /// <summary>
    /// Returns the type identified by the specified metadata token, in the context defined by the specified generic type parameters.
    /// </summary>
    /// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
    /// <param name="genericTypeArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the type where the token is in scope, or an empty array if that type is not generic.</param>
    /// <param name="genericMethodArguments">An array of <see cref="Type"/> objects representing the generic type arguments of the method where the token is in scope, or an empty array if that method is not generic.</param>
    /// <returns>A <see cref="Type"/> object representing the type that is identified by the specified metadata token.</returns>
    /// <exception cref="ArgumentException"><paramref name="metadataToken"/> is not a token for a type in the scope of the current module. -or- <paramref name="metadataToken"/> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments"/> and <paramref name="genericMethodArguments"/>s.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="metadataToken"/> is not a valid token in the scope of the current module.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming changes metadata tokens")]
#endif
    public Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
        return Type.CreateNew(Origin.ResolveType(metadataToken, genericTypeArguments.Length > 0 ? Type.GetOriginList(genericTypeArguments).ToArray() : null, genericMethodArguments.Length > 0 ? Type.GetOriginList(genericMethodArguments).ToArray() : null));
    }

    /// <summary>
    /// Returns the name of the module.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the name of this module.</returns>
    public override string ToString()
    {
        return Origin.ToString();
    }
}
