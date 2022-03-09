namespace MurderousPursuitHack.Drawing
{
    using System;
    using UnityEngine;

    public static class DrawingHelper
    {
        public static string DisplayKeybind(string action, KeyCode keycode)
        {
            return String.Format("[{0}] {1}", keycode, action);
        }
    }
}
