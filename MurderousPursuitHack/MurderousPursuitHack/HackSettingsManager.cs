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
        //Window
        public Rect WindowRect = new Rect(10, 10, 200, 30);
        public bool ShowOptions = false;
        Text wh;

        public void OnGUI()
        {
            WindowRect = GUILayout.Window(0, WindowRect, DoMyWindow, "Loaded successfully", GUILayout.Width(200), GUILayout.MaxHeight(100));
        }

        public void Start()
        {
            //Application.Quit();
            //wh = GetComponent<Text>();
            //wh.text = "TEST";
        }

        public void Update()
        {

        }

        public void OnEnable()
        {

        }

        private void DoMyWindow(int windowID)
        {
            //GUILayout.BeginVertical();
            //ShowOptions = GUILayout.Toggle(ShowOptions, "Show options");
            //GUI.TextArea(new Rect(0, 0, 50, 100), "TEST");
            //GUILayout.TextArea("SADASD");
            //GUILayout.Label("Wallhack");
            //GUILayout.Label("Wallhack");
            //GUILayout.Label("Wallhack");
            //GUILayout.Label("Wallhack");
            //if (ShowOptions)
            //{
            //    GUILayout.Label("Wallhack");
            //    //WallhackManager.Draw();
            //}
            //GUILayout.EndVertical();

            // Make a very long rect that is 20 pixels tall.
            // This will make the window be resizable by the top
            // title bar - no matter how wide it gets.
            //GUI.DragWindow(new Rect(0, 0, 10000, 200));
            //GUI.DragWindow();
        }
    }
}
