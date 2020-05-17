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
        public static bool WindowHidden = false;
        public static bool WallhackEnabled = true;
        public static bool LocalPlayerInfo = false;
        public static bool DebugInfo = false;
        public static bool Speedhack = false;
        public static float[] SpeedhackMultipliers;
        public static int CurrentMultiplier = 3; //1f

        Vector2 startingPos = new Vector2(10f, 100f);
        Vector2 size = new Vector2(300, 320);
        const float yMargin = 25f;

        public void OnGUI()
        {
            if (!WindowHidden)
                GUI.Window(0, new Rect(startingPos, size), DoMyWindow, "Cheat manager");
        }

        /// <summary>
        /// Note: remember it positions each component related to window position
        /// </summary>
        /// <param name="windowID"></param>
        private void DoMyWindow(int windowID)
        {
            GUI.Label(new Rect(startingPos.x, yMargin, size.x - 2 * startingPos.x, yMargin), "Hide/Show window [F1]");

            GUI.Label(new Rect(startingPos.x, 2 * yMargin, size.x - 2 * startingPos.x, yMargin), "VISUALS");
            WallhackEnabled = GUI.Toggle(new Rect(startingPos.x, 3 * yMargin, size.x - 2 * startingPos.x, yMargin), WallhackEnabled, " Wallhack [F2]");

            GUI.Label(new Rect(startingPos.x, 4 * yMargin, size.x - 2 * startingPos.x, yMargin), "DEBUG");
            LocalPlayerInfo = GUI.Toggle(new Rect(startingPos.x, 5 * yMargin, size.x - 2 * startingPos.x, yMargin), LocalPlayerInfo, " Local player info [F3]");
            DebugInfo = GUI.Toggle(new Rect(startingPos.x, 6 * yMargin, size.x - 2 * startingPos.x, yMargin), DebugInfo, " Debug info [F4]");

            GUI.Label(new Rect(startingPos.x, 7 * yMargin, size.x - 2 * startingPos.x, yMargin), "TELEPORTS");
            GUI.Button(new Rect(startingPos.x, 8 * yMargin, size.x - 2 * startingPos.x, yMargin), "Teleport to quarry [Num1]");
            GUI.Button(new Rect(startingPos.x, 9 * yMargin, size.x - 2 * startingPos.x, yMargin), "Teleport to any hunter [Num2]");

            GUI.Label(new Rect(startingPos.x, 10 * yMargin, size.x - 2 * startingPos.x, yMargin), "SPEED HACK");
            Speedhack = GUI.Toggle(new Rect(startingPos.x, 11 * yMargin, size.x * 0.4f, yMargin), Speedhack, String.Format("Speedhack: {0}", Math.Round(SpeedhackMultipliers[CurrentMultiplier], 3)));
            CurrentMultiplier = (int)GUI.HorizontalSlider(new Rect(startingPos.x + size.x * 0.45f, 11 * yMargin + 5, size.x * 0.4f, yMargin),
                CurrentMultiplier, 0, SpeedhackMultipliers.Length - 1);
        }

        public void Start()
        {
            SpeedhackMultipliers = GeneratePossibleMultipliers();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                WindowHidden = !WindowHidden;
            else if (Input.GetKeyDown(KeyCode.F2))
                WallhackEnabled = !WallhackEnabled;
            else if (Input.GetKeyDown(KeyCode.F3))
                LocalPlayerInfo = !LocalPlayerInfo;
            else if (Input.GetKeyDown(KeyCode.F4))
                DebugInfo = !DebugInfo;
        }

        public void OnEnable()
        {

        }

        private float[] GeneratePossibleMultipliers()
        {
            float min = 0.4f;
            float max = 5f;
            float step = 0.2f;
            List<float> results = new List<float>();
            while(min < max)
            {
                results.Add(min);
                min += step;
            }
            return results.ToArray();
        }
    }
}
