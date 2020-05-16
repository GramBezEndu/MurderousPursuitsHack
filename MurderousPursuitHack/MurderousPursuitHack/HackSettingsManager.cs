using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MurderousPursuitHack
{
    class HackSettingsManager : MonoBehaviour
    {
        public static bool WallhackEnabled = true;
        public static bool LocalPlayerInfo = false;
        public static bool DebugInfo = false;

        Vector2 startingPos = new Vector2(10f, 100f);
        Vector2 size = new Vector2(300, 150);
        const float yMargin = 25f;

        public void OnGUI()
        {
            GUI.Window(0, new Rect(startingPos, size), DoMyWindow, "Cheat manager");
        }

        private void DoMyWindow(int windowID)
        {
            //Position related to window
            WallhackEnabled = GUI.Toggle(new Rect(startingPos.x, yMargin, size.x, yMargin), WallhackEnabled, " Toggle Wallhack [F1]");
            LocalPlayerInfo = GUI.Toggle(new Rect(startingPos.x, 2 * yMargin, size.x, yMargin), LocalPlayerInfo, " Local player info [F2]");
            DebugInfo = GUI.Toggle(new Rect(startingPos.x, 3 * yMargin, size.x, yMargin), DebugInfo, " Debug info [F3]");
        }

        public void Start()
        {
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                WallhackEnabled = !WallhackEnabled;
            else if (Input.GetKeyDown(KeyCode.F2))
                LocalPlayerInfo = !LocalPlayerInfo;
            else if (Input.GetKeyDown(KeyCode.F3))
                DebugInfo = !DebugInfo;
        }

        public void OnEnable()
        {

        }
    }
}
