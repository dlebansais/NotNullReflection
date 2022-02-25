namespace NotNullReflection;

using System.Globalization;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using NotSupportedException = System.NotSupportedException;
using OriginFieldInfo = System.Reflection.FieldInfo;
using OriginMethodBase = System.Reflection.MethodBase;
using OriginPropertyInfo = System.Reflection.PropertyInfo;
using OriginType = System.Type;
using ParameterModifier = System.Reflection.ParameterModifier;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    private class UnusedBinder : Binder
    {
        public UnusedBinder() { }
        public override OriginFieldInfo BindToField(BindingFlags bindingAttr, OriginFieldInfo[] match, object value, CultureInfo? culture) { throw new NotSupportedException(); }
        public override OriginMethodBase BindToMethod(BindingFlags bindingAttr, OriginMethodBase[] match, ref object?[] args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? names, out object? state) { throw new NotSupportedException(); }
        public override object ChangeType(object value, OriginType type, CultureInfo? culture) { throw new NotSupportedException(); }
        public override void ReorderArgumentArray(ref object?[] args, object state) { throw new NotSupportedException(); }
        public override OriginMethodBase? SelectMethod(BindingFlags bindingAttr, OriginMethodBase[] match, OriginType[] types, ParameterModifier[]? modifiers) { throw new NotSupportedException(); }
        public override OriginPropertyInfo? SelectProperty(BindingFlags bindingAttr, OriginPropertyInfo[] match, OriginType? returnType, OriginType[]? indexes, ParameterModifier[]? modifiers) { throw new NotSupportedException(); }
    }

    /// <summary>
    /// Gets the default binder object.
    /// </summary>
    public static Binder DefaultBinder { get; } = new UnusedBinder();
}
