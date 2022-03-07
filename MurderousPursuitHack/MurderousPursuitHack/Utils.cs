using System.Reflection;

namespace MurderousPursuitHack
{
    public static class Utils
    {
        public static readonly BindingFlags FieldGetFlags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance;
    }
}
