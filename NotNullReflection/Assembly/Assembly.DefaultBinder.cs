namespace NotNullReflection;

using System.Globalization;
using NotSupportedException = System.NotSupportedException;
using OriginType = System.Type;
using OriginMethodBase = System.Reflection.MethodBase;
using BindingFlags = System.Reflection.BindingFlags;
using OriginParameterModifier = System.Reflection.ParameterModifier;
using OriginFieldInfo = System.Reflection.FieldInfo;
using OriginPropertyInfo = System.Reflection.PropertyInfo;
using OriginBinder = System.Reflection.Binder;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    private class UnusedBinder : OriginBinder
    {
        public UnusedBinder() { }
        public override OriginFieldInfo BindToField(BindingFlags bindingAttr, OriginFieldInfo[] match, object value, CultureInfo? culture) { throw new NotSupportedException(); }
        public override OriginMethodBase BindToMethod(BindingFlags bindingAttr, OriginMethodBase[] match, ref object?[] args, OriginParameterModifier[]? modifiers, CultureInfo? culture, string[]? names, out object? state) { throw new NotSupportedException(); }
        public override object ChangeType(object value, OriginType type, CultureInfo? culture) { throw new NotSupportedException(); }
        public override void ReorderArgumentArray(ref object?[] args, object state) { throw new NotSupportedException(); }
        public override OriginMethodBase? SelectMethod(BindingFlags bindingAttr, OriginMethodBase[] match, OriginType[] types, OriginParameterModifier[]? modifiers) { throw new NotSupportedException(); }
        public override OriginPropertyInfo? SelectProperty(BindingFlags bindingAttr, OriginPropertyInfo[] match, OriginType? returnType, OriginType[]? indexes, OriginParameterModifier[]? modifiers) { throw new NotSupportedException(); }
    }

    /// <summary>
    /// Gets the default binder object.
    /// </summary>
    public static OriginBinder DefaultBinder { get; } = new UnusedBinder();
}
