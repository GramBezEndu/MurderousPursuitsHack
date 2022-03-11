namespace MurderousPursuitHack.Drawing
{
    using System;
    using UnityEngine;

    public static class DrawingHelper
    {
        public static string DisplayKeybind(string action, KeyCode keycode)
        {
            if (keycode == KeyCode.None)
            {
                return action;
            }
            else
            {
                return String.Format("[{0}] {1}", keycode, action);
            }
        }
    }
}
