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

        public Vector2 Size { get; set; } = new Vector2(300, 480);

        public float ElementHeight { get; set; }

        private Vector2 currentElementPosition;

        //private readonly float yMargin = 25f;

        public WindowBuilder(Vector2 position, Vector2 windowSize, float elementHeight) 
        {
            Position = position;
            Size = windowSize;
            ElementHeight = elementHeight;
        }

        public void StartWindow()
        {
            ResetPosition();
        }

        public bool Button(string message)
        {
            return GUI.Button(NextRect(), message);
        }

        public bool Toggle(bool value, string message)
        {
            return GUI.Toggle(NextRect(), value, message);
        }

        public void Label(string message)
        {
            GUI.Label(NextRect(), message);
        }

        public Rect NextRect()
        {
            currentElementPosition.y += ElementHeight;
            return new Rect(currentElementPosition.x, currentElementPosition.y, Size.x * 0.9f, ElementHeight);
        }

        public void EndWindow()
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
            currentElementPosition = new Vector2(Position.x, 0f);
        }
    }
}