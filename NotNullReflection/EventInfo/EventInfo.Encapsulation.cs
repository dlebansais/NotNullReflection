namespace NotNullReflection;

using System.Linq;
using System.Security;
using ArgumentException = System.ArgumentException;
using Delegate = System.Delegate;
using EventAttributes = System.Reflection.EventAttributes;
using Exception = System.Exception;
using InvalidOperationException = System.InvalidOperationException;
using MemberAccessException = System.MemberAccessException;
using MemberTypes = System.Reflection.MemberTypes;
using MethodAccessException = System.MethodAccessException;
using NotImplementedException = System.NotImplementedException;
using NullReferenceException = System.NullReferenceException;
using TargetException = System.Reflection.TargetException;

/// <summary>
/// Discovers the attributes of an event and provides access to event metadata.
/// </summary>
public partial class EventInfo
{
    /// <summary>
    /// Gets the <see cref="MethodInfo"/> object for the <see cref="AddEventHandler(object,Delegate)"/> method of the event, including non-public methods.
    /// </summary>
    /// <returns>The <see cref="MethodInfo"/> object for the <see cref="AddEventHandler(object,Delegate)"/> method.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public virtual MethodInfo AddMethod
    {
        get
        {
            return MethodInfo.CreateNew(Origin.AddMethod ?? throw new NullReferenceException("Method not found."));
        }
    }

    /// <summary>
    /// Gets the attributes for this event.
    /// </summary>
    /// <returns>The read-only attributes for this event.</returns>
    public EventAttributes Attributes
    {
        get
        {
            return Origin.Attributes;
        }
    }

    /// <summary>
    /// Gets the <see cref="Type"/> object of the underlying event-handler delegate associated with this event.
    /// </summary>
    /// <returns>A read-only <see cref="Type"/> object representing the delegate event handler.</returns>
    /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
    /// <exception cref="NullReferenceException">Type not found.</exception>
    public Type EventHandlerType
    {
        get
        {
            return Type.CreateNew(Origin.EventHandlerType ?? throw new NullReferenceException("Type not found."));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the event is multicast.
    /// </summary>
    /// <returns><see langword="true"/> if the delegate is an instance of a multicast delegate; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
    public virtual bool IsMulticast
    {
        get
        {
            return Origin.IsMulticast;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="EventInfo"/> has a name with a special meaning.
    /// </summary>
    /// <returns><see langword="true"/> if this event has a special name; otherwise, <see langword="false"/>.</returns>
    public bool IsSpecialName
    {
        get
        {
            return Origin.IsSpecialName;
        }
    }

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is an event.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is an event.</returns>
    public override MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Gets the method that is called when the event is raised, including non-public methods.
    /// </summary>
    /// <returns>The method that is called when the event is raised.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo RaiseMethod
    {
        get
        {
            return MethodInfo.CreateNew(Origin.RaiseMethod ?? throw new NullReferenceException("Method not found."));
        }
    }

    /// <summary>
    /// Gets the <see cref="MethodInfo"/> object for removing a method of the event, including non-public methods.
    /// </summary>
    /// <returns>The <see cref="MethodInfo"/> object for removing a method of the event.</returns>
    public MethodInfo RemoveMethod
    {
        get
        {
            return MethodInfo.CreateNew(Origin.RemoveMethod ?? throw new NullReferenceException("Method not found."));
        }
    }

    /// <summary>
    /// Adds an event handler to an event source.
    /// </summary>
    /// <param name="target">The event source.</param>
    /// <param name="handler">Encapsulates a method or methods to be invoked when the event is raised by the target.</param>
    /// <exception cref="InvalidOperationException">The event does not have a public add accessor.</exception>
    /// <exception cref="ArgumentException">The handler that was passed in cannot be used.</exception>
    /// <exception cref="MethodAccessException">The caller does not have access permission to the member. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="TargetException">The <paramref name="target"/> parameter is <see cref="Type.Void"/> and the event is not static. -or- The <see cref="EventInfo"/> is not declared on the target. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    public void AddEventHandler(object target, Delegate handler)
    {
        Origin.AddEventHandler(target != Type.Void ? target : null, handler);
    }

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns><see langword="true"/> if <paramref name="obj"/> equals the type and value of this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object obj)
    {
        return obj is EventInfo AsEventInfo && Origin.Equals(AsEventInfo.Origin);
    }

    /// <summary>
    /// Returns the method used to add an event handler delegate to the event source.
    /// </summary>
    /// <returns>A <see cref="MethodInfo"/> object representing the method used to add an event handler delegate to the event source.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetAddMethod()
    {
        return MethodInfo.CreateNew(Origin.GetAddMethod() ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the <see cref="MethodInfo"/> object for the <see cref="AddEventHandler(object,Delegate)"/> method of the event, specifying whether to return non-public methods.
    /// </summary>
    /// <param name="nonPublic"><see langword="true"/> if non-public methods can be returned; otherwise, <see langword="false"/>.</param>
    /// <returns>A <see cref="MethodInfo"/> object representing the method used to add an event handler delegate to the event source.</returns>
    /// <exception cref="MethodAccessException"><paramref name="nonPublic"/> is <see langword="true"/>, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetAddMethod(bool nonPublic)
    {
        return MethodInfo.CreateNew(Origin.GetAddMethod(nonPublic) ?? throw new NullReferenceException("Method not found."));
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
    /// Returns the public methods that have been associated with an event in metadata using the .other directive.
    /// </summary>
    /// <returns>An array representing the public methods that have been associated with the event in metadata by using the .other directive. If there are no such public methods, an empty array is returned.</returns>
    public MethodInfo[] GetOtherMethods()
    {
        return MethodInfo.GetList(Origin.GetOtherMethods()).ToArray();
    }

    /// <summary>
    /// Returns the methods that have been associated with the event in metadata using the .other directive, specifying whether to include non-public methods.
    /// </summary>
    /// <param name="nonPublic"><see langword="true"/> to include non-public methods; otherwise, <see langword="false"/>.</param>
    /// <returns>An array representing methods that have been associated with an event in metadata by using the .other directive. If there are no methods matching the specification, an empty array is returned.</returns>
    /// <exception cref="NotImplementedException">This method is not implemented.</exception>
    public MethodInfo[] GetOtherMethods(bool nonPublic)
    {
        return MethodInfo.GetList(Origin.GetOtherMethods(nonPublic)).ToArray();
    }

    /// <summary>
    /// Returns the method that is called when the event is raised.
    /// </summary>
    /// <returns>The method that is called when the event is raised.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetRaiseMethod()
    {
        return MethodInfo.CreateNew(Origin.GetRaiseMethod() ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// When overridden in a derived class, returns the method that is called when the event is raised, specifying whether to return non-public methods.
    /// </summary>
    /// <param name="nonPublic"><see langword="true"/> if non-public methods can be returned; otherwise, <see langword="false"/>.</param>
    /// <returns>A <see cref="MethodInfo"/> object that was called when the event was raised.</returns>
    /// <exception cref="MethodAccessException"><paramref name="nonPublic"/> is <see langword="true"/>, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetRaiseMethod(bool nonPublic)
    {
        return MethodInfo.CreateNew(Origin.GetRaiseMethod(nonPublic) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Returns the method used to remove an event handler delegate from the event source.
    /// </summary>
    /// <returns>A <see cref="MethodInfo"/> object representing the method used to remove an event handler delegate from the event source.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetRemoveMethod()
    {
        return MethodInfo.CreateNew(Origin.GetRemoveMethod() ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the <see cref="MethodInfo"/> object for removing a method of the event, specifying whether to return non-public methods.
    /// </summary>
    /// <param name="nonPublic"><see langword="true"/> if non-public methods can be returned; otherwise, <see langword="false"/>.</param>
    /// <returns>A <see cref="MethodInfo"/> object representing the method used to remove an event handler delegate from the event source.</returns>
    /// <exception cref="MethodAccessException"><paramref name="nonPublic"/> is <see langword="true"/>, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetRemoveMethod(bool nonPublic)
    {
        return MethodInfo.CreateNew(Origin.GetRemoveMethod(nonPublic) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Indicates whether two <see cref="EventInfo"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(EventInfo left, EventInfo right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="EventInfo"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(EventInfo left, EventInfo right)
    {
        return left.Origin != right.Origin;
    }

    /// <summary>
    /// Removes an event handler from an event source.
    /// </summary>
    /// <param name="target">The event source.</param>
    /// <param name="handler">The delegate to be disassociated from the events raised by target.</param>
    /// <exception cref="InvalidOperationException">The event does not have a public remove accessor.</exception>
    /// <exception cref="ArgumentException">The handler that was passed in cannot be used.</exception>
    /// <exception cref="TargetException">The <paramref name="target"/> parameter is <see cref="Type.Void"/> and the event is not static. -or- The <see cref="EventInfo"/> is not declared on the target. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="MethodAccessException">The caller does not have access permission to the member. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    public virtual void RemoveEventHandler(object target, Delegate handler)
    {
        Origin.RemoveEventHandler(target != Type.Void ? target : null, handler);
    }
}
