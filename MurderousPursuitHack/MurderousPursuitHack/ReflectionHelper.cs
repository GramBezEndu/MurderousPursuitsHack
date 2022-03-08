namespace MurderousPursuitHack
{
    using System.Reflection;

    public static class ReflectionHelper
    {
        public static readonly BindingFlags FieldGetFlags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance;

        public static object GetFieldValue(this object fromObject, string fieldName)
        {
            return fromObject.GetType().GetField(fieldName, FieldGetFlags).GetValue(fromObject);
        }

        public static void SetFieldValue(this object obj, string fieldName, object value)
        {
            obj.GetType().GetField(fieldName, FieldGetFlags).SetValue(obj, value);
        }
    }
}
