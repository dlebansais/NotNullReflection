namespace NotNullReflection;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using GenericParameterAttributes = System.Reflection.GenericParameterAttributes;
using Guid = System.Guid;
using InvalidOperationException = System.NullReferenceException;
using MemberTypes = System.Reflection.MemberTypes;
using NotSupportedException = System.NotSupportedException;
using NullReferenceException = System.NullReferenceException;
using OriginModule = System.Reflection.Module;
using OriginType = System.Type;
using ParameterModifier = System.Reflection.ParameterModifier;
using RuntimeTypeHandle = System.RuntimeTypeHandle;
using TypeAttributes = System.Reflection.TypeAttributes;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Gets the <see cref="Assembly"/> in which the type is declared. For generic types, gets the <see cref="Assembly"/> in which the generic type is defined.
    /// </summary>
    /// <returns>A <see cref="Assembly"/> instance that describes the assembly containing the current type. For generic types, the instance describes the assembly that contains the generic type definition, not the assembly that creates and uses a particular constructed type.</returns>
    public Assembly Assembly
    {
        get
        {
            return Assembly.CreateNew(Origin.Assembly);
        }
    }

    /// <summary>
    /// Gets the assembly-qualified name of the type, which includes the name of the assembly from which this <see cref="Type"/> object was loaded.
    /// </summary>
    /// <returns>The assembly-qualified name of the <see cref="Type"/>, which includes the name of the assembly from which the <see cref="Type"/> was loaded, or throws an exception if the current instance represents a generic type parameter.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have an assembly-qualified name.</exception>
    public string AssemblyQualifiedName
    {
        get
        {
            return Origin.AssemblyQualifiedName ?? throw new NullReferenceException("Type doesn't have an assembly-qualified name.");
        }
    }

    /// <summary>
    /// Gets the attributes associated with the <see cref="Type"/>.
    /// </summary>
    /// <returns>A <see cref="TypeAttributes"/> object representing the attribute set of the <see cref="Type"/>, unless the <see cref="Type"/> represents a generic type parameter, in which case the value is unspecified.</returns>
    public TypeAttributes Attributes
    {
        get
        {
            return Origin.Attributes;
        }
    }

    /// <summary>
    /// Gets the type from which the current <see cref="Type"/> directly inherits.
    /// </summary>
    /// <returns>The <see cref="Type"/> from which the current <see cref="Type"/> directly inherits, or throws an exception if the current <see cref="Type"/> represents the <see cref="object"/> class or an interface.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a base type.</exception>
    public Type BaseType
    {
        get
        {
            return CreateNew(Origin.BaseType ?? throw new NullReferenceException("Type doesn't have a base type."));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> object has type parameters that have not been replaced by specific types.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> object is itself a generic type parameter or has type parameters for which specific types have not been supplied; otherwise, <see langword="false"/>.</returns>
    public bool ContainsGenericParameters
    {
        get
        {
            return Origin.ContainsGenericParameters;
        }
    }

    /// <summary>
    /// Gets a <see cref="MethodBase"/> that represents the declaring method, if the current <see cref="Type"/> represents a type parameter of a generic method.
    /// </summary>
    /// <returns>If the current <see cref="Type"/> represents a type parameter of a generic method, a <see cref="MethodBase"/> that represents declaring method; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a declaring methods.</exception>
    public MethodBase DeclaringMethod
    {
        get
        {
            return MethodBase.CreateNew(Origin.DeclaringMethod ?? throw new NullReferenceException("Type doesn't have a declaring methods."));
        }
    }

    /// <summary>
    /// Gets the type that declares the current nested type or generic type parameter.
    /// </summary>
    /// <returns>A <see cref="Type"/> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a declaring type.</exception>
    public override Type DeclaringType
    {
        get
        {
            return CreateNew(Origin.DeclaringType ?? throw new NullReferenceException("Type doesn't have a declaring type."));
        }
    }

    /// <summary>
    /// Gets a reference to the default binder, which implements internal rules for selecting the appropriate members to be called by <see cref="InvokeMember(string,BindingFlags,Binder,object,object[],ParameterModifier[],CultureInfo,string[])"/>.
    /// </summary>
    /// <returns>A reference to the default binder used by the system.</returns>
    public static Binder DefaultBinder
    {
        get
        {
            return OriginType.DefaultBinder;
        }
    }

    /// <summary>
    /// Gets the fully qualified name of the type, including its namespace but not its assembly.
    /// </summary>
    /// <returns>The fully qualified name of the type, including its namespace but not its assembly; or throws an exception if the current instance represents a generic type parameter, an array type, pointer type, or byref type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a full name.</exception>
    public string FullName
    {
        get
        {
            return Origin.FullName ?? throw new NullReferenceException("Type doesn't have a full name.");
        }
    }

    /// <summary>
    /// Gets a combination of <see cref="OriginType.GenericParameterAttributes"/> flags that describe the covariance and special constraints of the current generic type parameter.
    /// </summary>
    /// <returns>A bitwise combination of <see cref="OriginType.GenericParameterAttributes"/> values that describes the covariance and special constraints of the current generic type parameter.</returns>
    /// <exception cref="InvalidOperationException">The current <see cref="Type"/> object is not a generic type parameter. That is, the <see cref="IsGenericParameter"/> property returns <see langword="false"/>.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    public GenericParameterAttributes GenericParameterAttributes
    {
        get
        {
            return Origin.GenericParameterAttributes;
        }
    }

    /// <summary>
    /// Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter, when the <see cref="Type"/> object represents a type parameter of a generic type or a generic method.
    /// </summary>
    /// <returns>The position of a type parameter in the type parameter list of the generic type or method that defines the parameter. Position numbers begin at 0.</returns>
    /// <exception cref="InvalidOperationException">The current type does not represent a type parameter. That is, <see cref="IsGenericParameter"/> returns <see langword="false"/>.</exception>
    public int GenericParameterPosition
    {
        get
        {
            return Origin.GenericParameterPosition;
        }
    }

    /// <summary>
    /// Gets an array of the generic type arguments for this type.
    /// </summary>
    /// <returns>An array of the generic type arguments for this type.</returns>
    public Type[] GenericTypeArguments
    {
        get
        {
            return GetList(Origin.GenericTypeArguments).ToArray();
        }
    }

    /// <summary>
    /// Gets the GUID associated with the <see cref="Type"/>.
    /// </summary>
    /// <returns>The GUID associated with the <see cref="Type"/>.</returns>
    public Guid GUID
    {
        get
        {
            return Origin.GUID;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current System.Type encompasses or refers to another type; that is, whether the current System.Type is an array, a pointer, or is passed by reference.
    /// </summary>
    /// <returns><see langword="true"/> if the System.Type is an array, a pointer, or is passed by reference; otherwise, <see langword="false"/>.</returns>
    public bool HasElementType
    {
        get
        {
            return Origin.HasElementType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is abstract and must be overridden.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is abstract; otherwise, <see langword="false"/>.</returns>
    public bool IsAbstract
    {
        get
        {
            return Origin.IsAbstract;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the string format attribute AnsiClass is selected for the <see cref="Type"/>.
    /// </summary>
    /// <returns><see langword="true"/> if the string format attribute AnsiClass is selected for the <see cref="Type"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsAnsiClass
    {
        get
        {
            return Origin.IsAnsiClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is an array.
    /// </summary>
    /// <returns><see langword="true"/> if the current type is an array; otherwise, <see langword="false"/>.</returns>
    public bool IsArray
    {
        get
        {
            return Origin.IsArray;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the string format attribute AutoClass is selected for the <see cref="Type"/>.
    /// </summary>
    /// <returns><see langword="true"/> if the string format attribute AutoClass is selected for the <see cref="Type"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsAutoClass
    {
        get
        {
            return Origin.IsAutoClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the fields of the current type are laid out automatically by the common language runtime.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Attributes"/> property of the current type includes <see cref="TypeAttributes.AutoLayout"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsAutoLayout
    {
        get
        {
            return Origin.IsAutoLayout;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is passed by reference.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is passed by reference; otherwise, <see langword="false"/>.</returns>
    public bool IsByRef
    {
        get
        {
            return Origin.IsByRef;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether the type is a byref-like structure.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is a a byref-like structure; otherwise, <see langword="false"/>.</returns>
    public bool IsByRefLike
    {
        get
        {
            return Origin.IsByRefLike;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a class or a delegate; that is, not a value type or interface.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is a class; otherwise, <see langword="false"/>.</returns>
    public bool IsClass
    {
        get
        {
            return Origin.IsClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a COM object.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is a COM object; otherwise, <see langword="false"/>.</returns>
    public bool IsCOMObject
    {
        get
        {
            return Origin.IsCOMObject;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this object represents a constructed generic type. You can create instances of a constructed generic type.
    /// </summary>
    /// <returns><see langword="true"/> if this object represents a constructed generic type; otherwise, <see langword="false"/>.</returns>
    public bool IsConstructedGenericType
    {
        get
        {
            return Origin.IsConstructedGenericType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> can be hosted in a context.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> can be hosted in a context; otherwise, <see langword="false"/>.</returns>
    public bool IsContextful
    {
        get
        {
            return Origin.IsContextful;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents an enumeration.
    /// </summary>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> represents an enumeration; otherwise, <see langword="false"/>.</returns>
    public bool IsEnum
    {
        get
        {
            return Origin.IsEnum;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the fields of the current type are laid out at explicitly specified offsets.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Attributes"/> property of the current type includes <see cref="TypeAttributes.ExplicitLayout"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsExplicitLayout
    {
        get
        {
            return Origin.IsExplicitLayout;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a type parameter in the definition of a generic method.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> object represents a type parameter of a generic method definition; otherwise, <see langword="false"/>.</returns>
    public bool IsGenericMethodParameter
    {
        get
        {
            return Origin.IsGenericMethodParameter;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a type parameter in the definition of a generic type or method.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> object represents a type parameter of a generic type definition or generic method definition; otherwise, <see langword="false"/>.</returns>
    public bool IsGenericParameter
    {
        get
        {
            return Origin.IsGenericParameter;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is a generic type.
    /// </summary>
    /// <returns><see langword="true"/> if the current type is a generic type; otherwise, <see langword="false"/>.</returns>
    public bool IsGenericType
    {
        get
        {
            return Origin.IsGenericType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a generic type definition, from which other generic types can be constructed.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> object represents a generic type definition; otherwise, <see langword="false"/>.</returns>
    public bool IsGenericTypeDefinition
    {
        get
        {
            return Origin.IsGenericTypeDefinition;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a type parameter in the definition of a generic type.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> object represents a type parameter of a generic type definition; otherwise, <see langword="false"/>.</returns>
    public bool IsGenericTypeParameter
    {
        get
        {
            return Origin.IsGenericTypeParameter;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> has a <see cref="ComImportAttribute"/> attribute applied, indicating that it was imported from a COM type library.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> has a <see cref="ComImportAttribute"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsImport
    {
        get
        {
            return Origin.IsImport;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is an interface; that is, not a class or a value type.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is an interface; otherwise, <see langword="false"/>.</returns>
    public bool IsInterface
    {
        get
        {
            return Origin.IsInterface;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the fields of the current type are laid out sequentially, in the order that they were defined or emitted to the metadata.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Attributes"/> property of the current type includes <see cref="TypeAttributes.SequentialLayout"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsLayoutSequential
    {
        get
        {
            return Origin.IsLayoutSequential;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is marshaled by reference.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is marshaled by reference; otherwise, <see langword="false"/>.</returns>
    public bool IsMarshalByRef
    {
        get
        {
            return Origin.IsMarshalByRef;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> object represents a type whose definition is nested inside the definition of another type.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is nested inside another type; otherwise, <see langword="false"/>.</returns>
    public bool IsNested
    {
        get
        {
            return Origin.IsNested;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only within its own assembly.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is nested and visible only within its own assembly; otherwise, <see langword="false"/>.</returns>
    public bool IsNestedAssembly
    {
        get
        {
            return Origin.IsNestedAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only to classes that belong to both its own family and its own assembly.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is nested and visible only to classes that belong to both its own family and its own assembly; otherwise, <see langword="false"/>.</returns>
    public bool IsNestedFamANDAssem
    {
        get
        {
            return Origin.IsNestedFamANDAssem;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only within its own family.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is nested and visible only within its own family; otherwise, <see langword="false"/>.</returns>
    public bool IsNestedFamily
    {
        get
        {
            return Origin.IsNestedFamily;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only to classes that belong to either its own family or to its own assembly.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is nested and visible only to classes that belong to its own family or to its own assembly; otherwise, <see langword="false"/>.</returns>
    public bool IsNestedFamORAssem
    {
        get
        {
            return Origin.IsNestedFamORAssem;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and declared private.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is nested and declared private; otherwise, <see langword="false"/>.</returns>
    public bool IsNestedPrivate
    {
        get
        {
            return Origin.IsNestedPrivate;
        }
    }

    /// <summary>
    /// Gets a value indicating whether a class is nested and declared public.
    /// </summary>
    /// <returns><see langword="true"/> if the class is nested and declared public; otherwise, <see langword="false"/>.</returns>
    public bool IsNestedPublic
    {
        get
        {
            return Origin.IsNestedPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is not declared public.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is not declared public and is not a nested type; otherwise, <see langword="false"/>.</returns>
    public bool IsNotPublic
    {
        get
        {
            return Origin.IsNotPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a pointer.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is a pointer; otherwise, <see langword="false"/>.</returns>
    public bool IsPointer
    {
        get
        {
            return Origin.IsPointer;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is one of the primitive types.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is one of the primitive types; otherwise, <see langword="false"/>.</returns>
    public bool IsPrimitive
    {
        get
        {
            return Origin.IsPointer;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is declared public.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is declared public and is not a nested type; otherwise, <see langword="false"/>.</returns>
    public bool IsPublic
    {
        get
        {
            return Origin.IsPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is declared sealed.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is declared sealed; otherwise, <see langword="false"/>.</returns>
    public bool IsSealed
    {
        get
        {
            return Origin.IsSealed;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations.
    /// </summary>
    /// <returns><see langword="true"/> if the current type is security-critical or security-safe-critical at the current trust level; <see langword="false"/> if it is transparent.</returns>
    public bool IsSecurityCritical
    {
        get
        {
            return Origin.IsSecurityCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.
    /// </summary>
    /// <returns><see langword="true"/> if the current type is security-safe-critical at the current trust level; <see langword="false"/> if it is security-critical or transparent.</returns>
    public bool IsSecuritySafeCritical
    {
        get
        {
            return Origin.IsSecuritySafeCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is transparent at the current trust level, and therefore cannot perform critical operations.
    /// </summary>
    /// <returns><see langword="true"/> if the type is security-transparent at the current trust level; otherwise, <see langword="false"/>.</returns>
    public bool IsSecurityTransparent
    {
        get
        {
            return Origin.IsSecurityTransparent;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is serializable.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is serializable; otherwise, <see langword="false"/>.</returns>
    public bool IsSerializable
    {
        get
        {
            return Origin.IsSerializable;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether the type is a signature type.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is a signature type; otherwise, <see langword="false"/>.</returns>
    public bool IsSignatureType
    {
        get
        {
            return Origin.IsSignatureType;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the type has a name that requires special handling.
    /// </summary>
    /// <returns><see langword="true"/> if the type has a name that requires special handling; otherwise, <see langword="false"/>.</returns>
    public bool IsSpecialName
    {
        get
        {
            return Origin.IsSpecialName;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether the type is an array type that can represent only a single-dimensional array with a zero lower bound.
    /// </summary>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> is an array type that can represent only a single-dimensional array with a zero lower bound; otherwise, <see langword="false"/>.</returns>
    public bool IsSZArray
    {
        get
        {
            return Origin.IsSZArray;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is a type definition.
    /// </summary>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> is a type definition; otherwise, <see langword="false"/>.</returns>
    public bool IsTypeDefinition
    {
        get
        {
            return Origin.IsTypeDefinition;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the string format attribute UnicodeClass is selected for the <see cref="Type"/>.
    /// </summary>
    /// <returns><see langword="true"/> if the string format attribute UnicodeClass is selected for the <see cref="Type"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsUnicodeClass
    {
        get
        {
            return Origin.IsUnicodeClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a value type.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="Type"/> is a value type; otherwise, <see langword="false"/>.</returns>
    public bool IsValueType
    {
        get
        {
            return Origin.IsValueType;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether the type is an array type that can represent a multi-dimensional array or an array with an arbitrary lower bound.
    /// </summary>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> is an array type that can represent a multi-dimensional array or an array with an arbitrary lower bound; otherwise, <see langword="false"/>.</returns>
    public bool IsVariableBoundArray
    {
        get
        {
            return Origin.IsVariableBoundArray;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> can be accessed by code outside the assembly.
    /// </summary>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> is a public type or a public nested type such that all the enclosing types are public; otherwise, <see langword="false"/>.</returns>
    public bool IsVisible
    {
        get
        {
            return Origin.IsVisible;
        }
    }

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is a type or a nested type.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is a type or a nested type.</returns>
    public override MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Gets the module (the DLL) in which the current <see cref="Type"/> is defined.
    /// </summary>
    /// <returns>The module in which the current <see cref="Type"/> is defined.</returns>
    public new OriginModule Module
    {
        get
        {
            return Origin.Module;
        }
    }

    /// <summary>
    /// Gets the namespace of the <see cref="Type"/>.
    /// </summary>
    /// <returns>The namespace of the <see cref="Type"/>; throws an exception if the current instance has no namespace or represents a generic parameter.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a namespace.</exception>
    public string Namespace
    {
        get
        {
            return Origin.Namespace ?? throw new NullReferenceException("Type doesn't have a namespace.");
        }
    }

    /// <summary>
    /// Gets the class object that was used to obtain this member.
    /// </summary>
    /// <returns>The Type object through which this <see cref="Type"/> object was obtained.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a reflected type.</exception>
    public override Type ReflectedType
    {
        get
        {
            return CreateNew(Origin.ReflectedType ?? throw new NullReferenceException("Type doesn't have a reflected type."));
        }
    }

    /// <summary>
    /// Gets a <see cref="System.Runtime.InteropServices.StructLayoutAttribute"/> that describes the layout of the current type.
    /// </summary>
    /// <returns>Gets a <see cref="System.Runtime.InteropServices.StructLayoutAttribute"/> that describes the gross layout features of the current type.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="NullReferenceException">Type doesn't have a StructLayout attribute.</exception>
    public StructLayoutAttribute StructLayoutAttribute
    {
        get
        {
            return Origin.StructLayoutAttribute ?? throw new NullReferenceException("Type doesn't have a StructLayout attribute.");
        }
    }

    /// <summary>
    /// Gets the handle for the current <see cref="Type"/>.
    /// </summary>
    /// <returns>The handle for the current <see cref="Type"/>.</returns>
    /// <exception cref="NotSupportedException">The .NET Compact Framework does not currently support this property.</exception>
    public RuntimeTypeHandle TypeHandle
    {
        get
        {
            return Origin.TypeHandle;
        }
    }

    /// <summary>
    /// Gets the initializer for the type.
    /// </summary>
    /// <returns>An object that contains the name of the class constructor for the <see cref="Type"/>.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a type initializer.</exception>
    public ConstructorInfo TypeInitializer
    {
        get
        {
            return ConstructorInfo.CreateNew(Origin.TypeInitializer ?? throw new NullReferenceException("Type doesn't have a type initializer."));
        }
    }

    /// <summary>
    /// Gets the type provided by the common language runtime that represents this type.
    /// </summary>
    /// <returns>The underlying system type for the System.Type.</returns>
    public Type UnderlyingSystemType
    {
        get
        {
            return CreateNew(Origin.UnderlyingSystemType);
        }
    }
}
