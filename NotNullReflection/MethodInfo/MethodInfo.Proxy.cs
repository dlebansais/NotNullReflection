namespace NotNullReflection;

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MemberTypes = System.Reflection.MemberTypes;
using NotSupportedException = System.NotSupportedException;
using NotImplementedException = System.NotImplementedException;
using InvalidOperationException = System.InvalidOperationException;
using ArgumentException = System.ArgumentException;
using OriginParameterInfo = System.Reflection.ParameterInfo;
using ICustomAttributeProvider = System.Reflection.ICustomAttributeProvider;
using Delegate = System.Delegate;

/// <summary>
/// Discovers the attributes of a method and provides access to method metadata.
/// </summary>
public partial class MethodInfo : MethodBase
{
    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is a method.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is a method.</returns>
    public override MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Gets a <see cref="OriginParameterInfo"/> object that contains information about the return type of the method, such as whether the return type has custom modifiers.
    /// </summary>
    /// <returns>A <see cref="OriginParameterInfo"/> object that contains information about the return type.</returns>
    /// <exception cref="NotImplementedException">This method is not implemented.</exception>
    public OriginParameterInfo ReturnParameter
    {
        get
        {
            return Origin.ReturnParameter;
        }
    }

    /// <summary>
    /// Gets the return type of this method.
    /// </summary>
    /// <returns>The return type of this method.</returns>
    public virtual Type ReturnType
    {
        get
        {
            return new Type(Origin.ReturnType);
        }
    }

    /// <summary>
    /// Gets the custom attributes for the return type.
    /// </summary>
    /// <returns>An <see cref="ICustomAttributeProvider"/> object representing the custom attributes for the return type.</returns>
    public ICustomAttributeProvider ReturnTypeCustomAttributes
    {
        get
        {
            return Origin.ReturnTypeCustomAttributes;
        }
    }

    /// <summary>
    /// Creates a delegate of the specified type from this method.
    /// </summary>
    /// <param name="delegateType">The type of the delegate to create.</param>
    /// <returns>The delegate for this method.</returns>
    public Delegate CreateDelegate(Type delegateType)
    {
        return Origin.CreateDelegate(delegateType.Origin);
    }

    /// <summary>
    /// Creates a delegate of the specified type with the specified target from this method.
    /// </summary>
    /// <param name="delegateType">The type of the delegate to create.</param>
    /// <param name="target">The object targeted by the delegate.</param>
    /// <returns>The delegate for this method.</returns>
    public Delegate CreateDelegate(Type delegateType, object target)
    {
        return Origin.CreateDelegate(delegateType.Origin, target);
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Creates a delegate of type <typeparamref name="T"/> from this method.
    /// </summary>
    /// <typeparam name="T">The type of the delegate to create.</typeparam>
    /// <returns>The delegate for this method.</returns>
    public T CreateDelegate<T>()
        where T : Delegate
    {
        return Origin.CreateDelegate<T>();
    }

    /// <summary>
    /// Creates a delegate of type T with the specified target from this method.
    /// </summary>
    /// <typeparam name="T">The type of the delegate to create.</typeparam>
    /// <param name="target">The object targeted by the delegate.</param>
    /// <returns>The delegate for this method.</returns>
    public T CreateDelegate<T>(object target)
        where T : Delegate
    {
        return Origin.CreateDelegate<T>(target);
    }
#endif

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>true if <paramref name="obj"/> equals the type and value of this instance; otherwise, false.</returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        return obj is MethodInfo AsMethodInfo ? Origin.Equals(AsMethodInfo.Origin) : false;
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="MethodInfo"/> object for the method on the direct or indirect base class in which the method represented by this instance was first declared.
    /// </summary>
    /// <returns>A <see cref="MethodInfo"/> object for the first implementation of this method.</returns>
    public MethodInfo GetBaseDefinition()
    {
        return new MethodInfo(Origin.GetBaseDefinition());
    }

    /// <summary>
    /// Returns an array of <see cref="Type"/> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
    /// <exception cref="NotSupportedException">This method is not supported.</exception>
    public override Type[] GetGenericArguments()
    {
        return Type.GetList(Origin.GetGenericArguments()).ToArray();
    }

    /// <summary>
    /// Returns a <see cref="MethodInfo"/> object that represents a generic method definition from which the current method can be constructed.
    /// </summary>
    /// <returns>A <see cref="MethodInfo"/> object representing a generic method definition from which the current method can be constructed.</returns>
    /// <exception cref="InvalidOperationException">The current method is not a generic method. That is, <see cref="MethodBase.IsGenericMethod"/> returns false.</exception>
    /// <exception cref="NotSupportedException">This method is not supported.</exception>
    public MethodInfo GetGenericMethodDefinition()
    {
        return new MethodInfo(Origin.GetGenericMethodDefinition());
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
    /// Substitutes the elements of an array of types for the type parameters of the current generic method definition, and returns a <see cref="MethodInfo"/> object representing the resulting constructed method.
    /// </summary>
    /// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic method definition.</param>
    /// <returns>A <see cref="MethodInfo"/> object that represents the constructed method formed by substituting the elements of typeArguments for the type parameters of the current generic method definition.</returns>
    /// <exception cref="InvalidOperationException">The current <see cref="MethodInfo"/> does not represent a generic method definition. That is, <see cref="MethodBase.IsGenericMethodDefinition"/> returns false.</exception>
    /// <exception cref="ArgumentException">The number of elements in <paramref name="typeArguments"/> is not the same as the number of type parameters of the current generic method definition. -or- An element of <paramref name="typeArguments"/> does not satisfy the constraints specified for the corresponding type parameter of the current generic method definition.</exception>
    /// <exception cref="NotSupportedException">This method is not supported.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("If some of the generic arguments are annotated (either with DynamicallyAccessedMembersAttribute, or generic constraints), trimming can't validate that the requirements of those annotations are met.")]
#endif
    public MethodInfo MakeGenericMethod(params Type[] typeArguments)
    {
        return new MethodInfo(Origin.MakeGenericMethod(Type.GetOriginList(typeArguments).ToArray()));
    }

    /// <summary>
    /// Indicates whether two <see cref="MethodInfo"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(MethodInfo left, MethodInfo right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="MethodInfo"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(MethodInfo left, MethodInfo right)
    {
        return left.Origin != right.Origin;
    }
}
