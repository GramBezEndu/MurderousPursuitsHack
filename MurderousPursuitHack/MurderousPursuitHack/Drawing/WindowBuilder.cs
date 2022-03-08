namespace MurderousPursuitHack.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;

    public class WindowBuilder
    {
        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public float ElementHeight { get; set; }

        private Vector2 currentElementPosition;

        private float elementsMarginY = 5f;

        private GUIStyle buttonStyle = null;

        private GUIStyle expanderStyle = null;

        private GUIStyle labelStyle = null;

        public WindowBuilder(Vector2 position, Vector2 windowSize, float elementHeight) 
        {
            Position = position;
            Size = windowSize;
            ElementHeight = elementHeight;
        }

        public void StartElements()
        {
            InitStyles();
            ResetPosition();
        }

        public bool Button(string message)
        {
            return GUI.Button(NextRect(), message, buttonStyle);
        }

        public bool Toggle(bool value, string message)
        {
            return GUI.Toggle(NextRect(), value, message);
        }

        public void Expander(string message)
        {
            GUI.Toggle(NextRect(0.98f), true, message, expanderStyle);
        }

        public void Label(string message)
        {
            GUI.Label(NextRect(), message, labelStyle);
        }

        public Rect NextRect(float widthElementScale = 0.95f)
        {
            currentElementPosition.y += ElementHeight + elementsMarginY;
            float windowCentreX = Size.x / 2f;
            float halfElementWidth = Size.x * widthElementScale / 2f;
            float posX = windowCentreX - halfElementWidth;
            return new Rect(posX, currentElementPosition.y, Size.x * widthElementScale, ElementHeight);
        }

        public void EndElements()
        {
            ResetPosition();
        }

        public void StartDisabledSection()
        {
            GUI.enabled = false;
        }

        public void EndDisabledSection()
        {
            GUI.enabled = true;
        }

        private void ResetPosition()
        {
            currentElementPosition = new Vector2(0f, 0f);
        }

        private void InitStyles()
        {
            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.button);
                //buttonStyle.normal.background = MakeTex(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
                //buttonStyle.hover.background = MakeTex(2, 2, new Color(0f, 0.6f, 0.6f, 1f));
            }

            if (expanderStyle == null)
            {
                expanderStyle = new GUIStyle(GUI.skin.button);
                expanderStyle.normal.background = MakeTex(2, 2, new Color(0f, 0.55f, 0.55f, 1f));
                expanderStyle.hover.background = MakeTex(2, 2, new Color(0f, 0.4f, 0.4f, 1f));
            }

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(GUI.skin.label);
                //labelStyle.normal.background = MakeTex(2, 2, new Color(0.7f, 0.7f, 0.7f, 1f));
            }
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}