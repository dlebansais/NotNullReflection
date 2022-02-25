namespace NotNullReflection;

using System.Collections.Generic;
using MemberTypes = System.Reflection.MemberTypes;
using CustomAttributeData = System.Reflection.CustomAttributeData;
using NullReferenceException = System.NullReferenceException;
using InvalidOperationException = System.InvalidOperationException;
using NotImplementedException = System.NotImplementedException;
using TypeLoadException = System.TypeLoadException;
using OriginModule = System.Reflection.Module;

/// <summary>
/// Obtains information about the attributes of a member and provides access to member metadata.
/// </summary>
public abstract partial class MemberInfo
{
    /// <summary>
    /// Gets a collection that contains this member's custom attributes.
    /// </summary>
    /// <returns>A collection that contains this member's custom attributes.</returns>
    public IEnumerable<CustomAttributeData> CustomAttributes
    {
        get
        {
            return Origin.CustomAttributes;
        }
    }

    /// <summary>
    /// Gets the class that declares this member.
    /// </summary>
    /// <returns>The <see cref="Type"/> object for the class that declares this member.</returns>
    /// <exception cref="NullReferenceException">Declaring type doesn't exist.</exception>
    public virtual Type DeclaringType
    {
        get
        {
            return Type.CreateNew(Origin.DeclaringType ?? throw new NullReferenceException("Declaring type doesn't exist."));
        }
    }

#if NET5_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER
    /// <summary>
    /// Gets a value indicating whether this <see cref="MemberInfo"/> object is part of an assembly held in a collectible <see cref="System.Runtime.Loader.AssemblyLoadContext"/>.
    /// </summary>
    /// <returns>true if the <see cref="MemberInfo"/> is part of an assembly held in a collectible assembly load context; otherwise, false.</returns>
    public bool IsCollectible
    {
        get
        {
            return Origin.IsCollectible;
        }
    }
#endif

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating the type of the member - method, constructor, event, and so on.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating the type of member.</returns>
    public virtual MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Gets a value that identifies a metadata element.
    /// </summary>
    /// <returns>A value which, in combination with <see cref="Module"/>, uniquely identifies a metadata element.</returns>
    /// <exception cref="InvalidOperationException">The current <see cref="MemberInfo"/> represents an array method, such as Address, on an array type whose element type is a dynamic type that has not been completed. To get a metadata token in this case, pass the <see cref="MemberInfo"/> object to the System.Reflection.Emit.ModuleBuilder.GetMethodToken(System.Reflection.MethodInfo) method; or use the System.Reflection.Emit.ModuleBuilder.GetArrayMethodToken(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[]) method to get the token directly, instead of using the System.Reflection.Emit.ModuleBuilder.GetArrayMethod(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[]) method to get a System.Reflection.MethodInfo first.</exception>
    public int MetadataToken
    {
        get
        {
            return Origin.MetadataToken;
        }
    }

    /// <summary>
    /// Gets the module in which the type that declares the member represented by the current <see cref="MemberInfo"/> is defined.
    /// </summary>
    /// <returns>The <see cref="OriginModule"/> in which the type that declares the member represented by the current <see cref="MemberInfo"/> is defined.</returns>
    /// <exception cref="NotImplementedException">This method is not implemented.</exception>
    public OriginModule Module
    {
        get
        {
            return Origin.Module;
        }
    }

    /// <summary>
    /// Gets the name of the current member.
    /// </summary>
    /// <returns>A <see cref="string"/> containing the name of this member.</returns>
    public string Name
    {
        get
        {
            return Origin.Name;
        }
    }

    /// <summary>
    /// Gets the class object that was used to obtain this instance of <see cref="MemberInfo"/>.
    /// </summary>
    /// <returns>The Type object through which this <see cref="MemberInfo"/> object was obtained.</returns>
    /// <exception cref="NullReferenceException">Reflected type doesn't exist.</exception>
    public virtual Type ReflectedType
    {
        get
        {
            return Type.CreateNew(Origin.ReflectedType ?? throw new NullReferenceException("Reflected type doesn't exist."));
        }
    }

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>true if <paramref name="obj"/> equals the type and value of this instance; otherwise, false.</returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        return obj is MemberInfo AsMemberInfo && Origin.Equals(AsMemberInfo.Origin);
    }

    /// <summary>
    /// When overridden in a derived class, returns an array of all custom attributes applied to this member.
    /// </summary>
    /// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. This parameter is ignored for properties and events.</param>
    /// <returns>An array that contains all the custom attributes applied to this member, or an array with zero elements if no attributes are defined.</returns>
    /// <exception cref="InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
    /// <exception cref="TypeLoadException">A custom attribute type could not be loaded.</exception>
    public object[] GetCustomAttributes(bool inherit)
    {
        return Origin.GetCustomAttributes(inherit);
    }

    /// <summary>
    /// When overridden in a derived class, returns an array of custom attributes applied to this member and identified by <see cref="Type"/>.
    /// </summary>
    /// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
    /// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. This parameter is ignored for properties and events.</param>
    /// <returns>An array of custom attributes applied to this member, or an array with zero elements if no attributes assignable to <paramref name="attributeType"/> have been applied.</returns>
    /// <exception cref="TypeLoadException">A custom attribute type could not be loaded.</exception>
    /// <exception cref="InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
    public object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
        return Origin.GetCustomAttributes(attributeType.Origin, inherit);
    }

    /// <summary>
    /// Returns a list of <see cref="CustomAttributeData"/> objects representing data about the attributes that have been applied to the target member.
    /// </summary>
    /// <returns>A generic list of <see cref="CustomAttributeData"/> objects representing data about the attributes that have been applied to the target member.</returns>
    public IList<CustomAttributeData> GetCustomAttributesData()
    {
        return Origin.GetCustomAttributesData();
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        return Origin.GetHashCode();
    }

#if !NET35_OR_GREATER
    /// <summary>
    /// Undocumented.
    /// </summary>
    /// <param name="other">Undocumented parameter.</param>
    /// <returns>Undocumented return value.</returns>
    public bool HasSameMetadataDefinitionAs(MemberInfo other)
    {
        return Origin.HasSameMetadataDefinitionAs(other.Origin);
    }
#endif

    /// <summary>
    /// When overridden in a derived class, indicates whether one or more attributes of the specified type or of its derived types is applied to this member.
    /// </summary>
    /// <param name="attributeType">The type of custom attribute to search for. The search includes derived types.</param>
    /// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. This parameter is ignored for properties and events.</param>
    /// <returns>true if one or more instances of <paramref name="attributeType"/> or any of its derived types is applied to this member; otherwise, false.</returns>
    public bool IsDefined(Type attributeType, bool inherit)
    {
        return Origin.IsDefined(attributeType.Origin, inherit);
    }

    /// <summary>
    /// Indicates whether two <see cref="MemberInfo"/> objects are equal.
    /// </summary>
    /// <param name="left">The <see cref="MemberInfo"/> to compare to <paramref name="right"/>.</param>
    /// <param name="right">The <see cref="MemberInfo"/> to compare to <paramref name="left"/>.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise false.</returns>
    public static bool operator ==(MemberInfo left, MemberInfo right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="MemberInfo"/> objects are not equal.
    /// </summary>
    /// <param name="left">The <see cref="MemberInfo"/> to compare to <paramref name="right"/>.</param>
    /// <param name="right">The <see cref="MemberInfo"/> to compare to <paramref name="left"/>.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise false.</returns>
    public static bool operator !=(MemberInfo left, MemberInfo right)
    {
        return left.Origin != right.Origin;
    }
}
