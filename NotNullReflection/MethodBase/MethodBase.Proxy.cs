namespace NotNullReflection;

using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using MethodAttributes = System.Reflection.MethodAttributes;
using CallingConventions = System.Reflection.CallingConventions;
using MethodImplAttributes = System.Reflection.MethodImplAttributes;
using NullReferenceException = System.NullReferenceException;
using NotSupportedException = System.NotSupportedException;
using RuntimeMethodHandle = System.RuntimeMethodHandle;
using Exception = System.Exception;
using InvalidOperationException = System.InvalidOperationException;
using ArgumentException = System.ArgumentException;
using MethodAccessException = System.MethodAccessException;
using MemberAccessException = System.MemberAccessException;
using RuntimeTypeHandle = System.RuntimeTypeHandle;
using OriginMethodBase = System.Reflection.MethodBase;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;
using OriginParameterInfo = System.Reflection.ParameterInfo;
using TargetException = System.Reflection.TargetException;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TargetParameterCountException = System.Reflection.TargetParameterCountException;
using MethodBody = System.Reflection.MethodBody;
using BindingFlags = System.Reflection.BindingFlags;
using OriginBinder = System.Reflection.Binder;

/// <summary>
/// Provides information about methods and constructors.
/// </summary>
public partial class MethodBase
{
    /// <summary>
    /// Gets the attributes associated with this method.
    /// </summary>
    /// <returns>One of the System.Reflection.MethodAttributes values.</returns>
    public MethodAttributes Attributes
    {
        get
        {
            return Origin.Attributes;
        }
    }

    /// <summary>
    /// Gets a value indicating the calling conventions for this method.
    /// </summary>
    /// <returns>The <see cref="CallingConventions"/> for this method.</returns>
    public CallingConventions CallingConvention
    {
        get
        {
            return Origin.CallingConvention;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the generic method contains unassigned generic type parameters.
    /// </summary>
    /// <returns>true if the current <see cref="MethodBase"/> object represents a generic method that contains unassigned generic type parameters; otherwise, false.</returns>
    public bool ContainsGenericParameters
    {
        get
        {
            return Origin.ContainsGenericParameters;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the method is abstract.
    /// </summary>
    /// <returns>true if the method is abstract; otherwise, false.</returns>
    public bool IsAbstract
    {
        get
        {
            return Origin.IsAbstract;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="MethodAttributes.Assembly"/>; that is, the method or constructor is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.
    /// </summary>
    /// <returns>true if the visibility of this method or constructor is exactly described by <see cref="MethodAttributes.Assembly"/>; otherwise, false.</returns>
    public bool IsAssembly
    {
        get
        {
            return Origin.IsAssembly;
        }
    }

#if !NET35_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether (undocumented).
    /// </summary>
    public bool IsConstructedGenericMethod
    {
        get
        {
            return Origin.IsConstructedGenericMethod;
        }
    }
#endif

    /// <summary>
    /// Gets a value indicating whether the method is a constructor.
    /// </summary>
    /// <returns>true if this method is a constructor represented by a <see cref="OriginConstructorInfo"/> object (see note in Remarks about <see cref="ConstructorBuilder"/> objects); otherwise, false.</returns>
    public bool IsConstructor
    {
        get
        {
            return Origin.IsConstructor;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the visibility of this method or constructor is described by <see cref="MethodAttributes.Family"/>; that is, the method or constructor is visible only within its class and derived classes.
    /// </summary>
    /// <returns>true if access to this method or constructor is exactly described by <see cref="MethodAttributes.Family"/>; otherwise, false.</returns>
    public bool IsFamily
    {
        get
        {
            return Origin.IsFamily;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the visibility of this method or constructor is described by <see cref="MethodAttributes.FamANDAssem"/>; that is, the method or constructor can be called by derived classes, but only if they are in the same assembly.
    /// </summary>
    /// <returns>true if access to this method or constructor is exactly described by <see cref="MethodAttributes.FamANDAssem"/>; otherwise, false.</returns>
    public bool IsFamilyAndAssembly
    {
        get
        {
            return Origin.IsFamilyAndAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="MethodAttributes.FamORAssem"/>; that is, the method or constructor can be called by derived classes wherever they are, and by classes in the same assembly.
    /// </summary>
    /// <returns>true if access to this method or constructor is exactly described by <see cref="MethodAttributes.FamORAssem"/>; otherwise, false.</returns>
    public bool IsFamilyOrAssembly
    {
        get
        {
            return Origin.IsFamilyOrAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this method is final.
    /// </summary>
    /// <returns>true if this method is final; otherwise, false.</returns>
    public bool IsFinal
    {
        get
        {
            return Origin.IsFinal;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the method is generic.
    /// </summary>
    /// <returns>true if the current <see cref="MethodBase"/> represents a generic method; otherwise, false.</returns>
    public bool IsGenericMethod
    {
        get
        {
            return Origin.IsGenericMethod;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the method is a generic method definition.
    /// </summary>
    /// <returns>true if the current <see cref="MethodBase"/> object represents the definition of a generic method; otherwise, false.</returns>
    public bool IsGenericMethodDefinition
    {
        get
        {
            return Origin.IsGenericMethodDefinition;
        }
    }

    /// <summary>
    /// Gets a value indicating whether only a member of the same kind with exactly the same signature is hidden in the derived class.
    /// </summary>
    /// <returns>true if the member is hidden by signature; otherwise, false.</returns>
    public bool IsHideBySig
    {
        get
        {
            return Origin.IsHideBySig;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this member is private.
    /// </summary>
    /// <returns>true if access to this method is restricted to other members of the class itself; otherwise, false.</returns>
    public bool IsPrivate
    {
        get
        {
            return Origin.IsPrivate;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this is a public method.
    /// </summary>
    /// <returns>true if this method is public; otherwise, false.</returns>
    public bool IsPublic
    {
        get
        {
            return Origin.IsPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current method or constructor is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations.
    /// </summary>
    /// <returns>true if the current method or constructor is security-critical or security-safe-critical at the current trust level; false if it is transparent.</returns>
    public bool IsSecurityCritical
    {
        get
        {
            return Origin.IsSecurityCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current method or constructor is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.
    /// </summary>
    /// <returns>true if the method or constructor is security-safe-critical at the current trust level; false if it is security-critical or transparent.</returns>
    public bool IsSecuritySafeCritical
    {
        get
        {
            return Origin.IsSecuritySafeCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current method or constructor is transparent at the current trust level, and therefore cannot perform critical operations.
    /// </summary>
    /// <returns>true if the method or constructor is security-transparent at the current trust level; otherwise, false.</returns>
    public bool IsSecurityTransparent
    {
        get
        {
            return Origin.IsSecurityTransparent;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this method has a special name.
    /// </summary>
    /// <returns>true if this method has a special name; otherwise, false.</returns>
    public bool IsSpecialName
    {
        get
        {
            return Origin.IsSpecialName;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the method is static.
    /// </summary>
    /// <returns>true if this method is static; otherwise, false.</returns>
    public bool IsStatic
    {
        get
        {
            return Origin.IsStatic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the method is virtual.
    /// </summary>
    /// <returns>true if this method is virtual; otherwise, false.</returns>
    public bool IsVirtual
    {
        get
        {
            return Origin.IsVirtual;
        }
    }

    /// <summary>
    /// Gets a handle to the internal metadata representation of a method.
    /// </summary>
    /// <returns>A <see cref="RuntimeMethodHandle"/> object.</returns>
    public RuntimeMethodHandle MethodHandle
    {
        get
        {
            return Origin.MethodHandle;
        }
    }

    /// <summary>
    /// Gets the <see cref="MethodImplAttributes"/> flags that specify the attributes of a method implementation.
    /// </summary>
    /// <returns>The method implementation flags.</returns>
    public MethodImplAttributes MethodImplementationFlags
    {
        get
        {
            return Origin.MethodImplementationFlags;
        }
    }

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>true if obj equals the type and value of this instance; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        return obj is MethodBase AsMethodBase && Origin.Equals(AsMethodBase.Origin);
    }

    /// <summary>
    /// Returns a <see cref="MethodBase"/> object representing the currently executing method.
    /// </summary>
    /// <returns><see cref="GetCurrentMethod"/> is a static method that is called from within an executing method and that returns information about that method. A <see cref="MethodBase"/> object representing the currently executing method.</returns>
    /// <exception cref="TargetException">This member was invoked with a late-binding mechanism.</exception>
    /// <exception cref="NullReferenceException">Unable to get current method.</exception>
    public static MethodBase GetCurrentMethod()
    {
        StackTrace Trace = new();
        StackFrame Frame = Trace.GetFrame(1) ?? throw new NullReferenceException("Unable to get current method.");
        OriginMethodBase Method = Frame?.GetMethod() ?? throw new NullReferenceException("Unable to get current method.");

        return new MethodBase(Method);
    }

    /// <summary>
    /// Returns an array of <see cref="Type"/> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
    /// <exception cref="NotSupportedException">The current object is a <see cref="OriginConstructorInfo"/>. Generic constructors are not supported in the .NET Framework version 2.0. This exception is the default behavior if this method is not overridden in a derived class.</exception>
    public virtual Type[] GetGenericArguments()
    {
        return Type.GetList(Origin.GetGenericArguments()).ToArray();
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
    /// When overridden in a derived class, gets a <see cref="MethodBody"/> object that provides access to the MSIL stream, local variables, and exceptions for the current method.
    /// </summary>
    /// <returns>A <see cref="MethodBody"/> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</returns>
    /// <exception cref="InvalidOperationException">This method is invalid unless overridden in a derived class.</exception>
    /// <exception cref="NullReferenceException">Method doesn't have a method body.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("Trimming may change method bodies. For example it can change some instructions, remove branches or local variables.")]
#endif
    public MethodBody GetMethodBody()
    {
        return Origin.GetMethodBody() ?? throw new NullReferenceException("Method doesn't have a method body.");
    }

    /// <summary>
    /// Gets method information by using the method's internal metadata representation (handle).
    /// </summary>
    /// <param name="handle">The method's handle.</param>
    /// <returns>A <see cref="MethodBase"/> containing information about the method.</returns>
    /// <exception cref="ArgumentException">handle is invalid.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
    {
        return new MethodBase(OriginMethodBase.GetMethodFromHandle(handle) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Gets a <see cref="MethodBase"/> object for the constructor or method represented by the specified handle, for the specified generic type.
    /// </summary>
    /// <param name="handle">A handle to the internal metadata representation of a constructor or method.</param>
    /// <param name="declaringType">A handle to the generic type that defines the constructor or method.</param>
    /// <returns>A <see cref="MethodBase"/> object representing the method or constructor specified by handle, in the generic type specified by <paramref name="declaringType"/>.</returns>
    /// <exception cref="ArgumentException">handle is invalid.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
    {
        return new MethodBase(OriginMethodBase.GetMethodFromHandle(handle, declaringType) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="MethodImplAttributes"/> flags.
    /// </summary>
    /// <returns>The <see cref="MethodImplAttributes"/> flags.</returns>
    public MethodImplAttributes GetMethodImplementationFlags()
    {
        return Origin.GetMethodImplementationFlags();
    }

    /// <summary>
    /// When overridden in a derived class, gets the parameters of the specified method or constructor.
    /// </summary>
    /// <returns>An array of type <see cref="OriginParameterInfo"/> containing information that matches the signature of the method (or constructor) reflected by this <see cref="MethodBase"/> instance.</returns>
    public OriginParameterInfo[] GetParameters()
    {
        return Origin.GetParameters();
    }

    /// <summary>
    /// Invokes the method or constructor represented by the current instance, using the specified parameters.
    /// </summary>
    /// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see cref="Type.Void"/> or an instance of the class that defines the constructor.</param>
    /// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, parameters should be empty. If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
    /// <returns>An object containing the return value of the invoked method, or <see cref="Type.Void"/> in the case of a constructor.</returns>
    /// <exception cref="TargetException">The <paramref name="obj"/> parameter is <see cref="Type.Void"/> and the method is not static. -or- The method is not declared or inherited by the class of <paramref name="obj"/>. -or- A static constructor is invoked, and <paramref name="obj"/> is neither <see cref="Type.Void"/> nor an instance of the class that declared the constructor. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="ArgumentException">The elements of the <paramref name="parameters"/> array do not match the signature of the method or constructor reflected by this instance.</exception>
    /// <exception cref="TargetInvocationException">The invoked method or constructor throws an exception. -or- The current instance is a <see cref="DynamicMethod"/> that contains unverifiable code. See the "Verification" section in Remarks for <see cref="DynamicMethod"/>.</exception>
    /// <exception cref="TargetParameterCountException">The <paramref name="parameters"/> array does not have the correct number of arguments.</exception>
    /// <exception cref="MethodAccessException">The caller does not have permission to execute the method or constructor that is represented by the current instance. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="Type.ContainsGenericParameters"/> property returns true for the declaring type.</exception>
    /// <exception cref="NotSupportedException">The current instance is a <see cref="MethodBuilder"/>.</exception>
    public object Invoke(object obj, object[] parameters)
    {
        return Origin.Invoke(obj != Type.Void ? obj : null, parameters.Length > 0 ? parameters : null) ?? Type.Void;
    }

    /// <summary>
    /// When overridden in a derived class, invokes the reflected method or constructor with the given parameters.
    /// </summary>
    /// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see cref="Type.Void"/> or an instance of the class that defines the constructor.</param>
    /// <param name="invokeAttr">A bitmask that is a combination of 0 or more bit flags from <see cref="BindingFlags"/>. If <paramref name="binder"/> is <see cref="Assembly.DefaultBinder"/>, this parameter is assigned the value <see cref="BindingFlags.Default"/>; thus, whatever you pass in is ignored.</param>
    /// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="MemberInfo"/> objects via reflection.</param>
    /// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, parameters should be empty. If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
    /// <param name="culture">An instance of <see cref="CultureInfo"/> used to govern the coercion of types. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired. (This is necessary to convert a string that represents 1000 to a <see cref="double"/> value, for example, since 1000 is represented differently by different cultures.)</param>
    /// <returns>An <see cref="object"/> containing the return value of the invoked method, or <see cref="Type.Void"/> in the case of a constructor, or <see cref="Type.Void"/> if the method's return type is void. Before calling the method or constructor, Invoke checks to see if the user has access permission and verifies that the parameters are valid.</returns>
    /// <exception cref="TargetException">The <paramref name="obj"/> parameter is <see cref="Type.Void"/> and the method is not static. -or- The method is not declared or inherited by the class of <paramref name="obj"/>. -or- A static constructor is invoked, and <paramref name="obj"/> is neither <see cref="Type.Void"/> nor an instance of the class that declared the constructor.</exception>
    /// <exception cref="ArgumentException">The type of the <paramref name="parameters"/> parameter does not match the signature of the method or constructor reflected by this instance.</exception>
    /// <exception cref="TargetParameterCountException">The <paramref name="parameters"/> array does not have the correct number of arguments.</exception>
    /// <exception cref="TargetInvocationException">The invoked method or constructor throws an exception.</exception>
    /// <exception cref="MethodAccessException">The caller does not have permission to execute the method or constructor that is represented by the current instance.</exception>
    /// <exception cref="InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="Type.ContainsGenericParameters"/> property returns true for the declaring type.</exception>
    public object Invoke(object obj, BindingFlags invokeAttr, OriginBinder binder, object[] parameters, CultureInfo culture)
    {
        return Origin.Invoke(obj != Type.Void ? obj : null, invokeAttr, binder != Assembly.DefaultBinder ? binder : null, parameters.Length > 0 ? parameters : null, culture) ?? Type.Void;
    }

    /// <summary>
    /// Indicates whether two <see cref="MethodBase"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(MethodBase left, MethodBase right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="MethodBase"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(MethodBase left, MethodBase right)
    {
        return left.Origin != right.Origin;
    }
}
