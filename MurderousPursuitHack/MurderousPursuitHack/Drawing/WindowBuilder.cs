﻿namespace MurderousPursuitHack.Drawing
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

        private WindowStyle style;

        private List<Section> sections = new List<Section>();

        private int currentSectionIndex = -1;

        private uint elementsCount = 0;

        private bool initialized;

        public WindowBuilder(Vector2 position, Vector2 windowSize, float elementHeight) 
        {
            Position = position;
            Size = windowSize;
            ElementHeight = elementHeight;
            style = new WindowStyle();
        }

        public void StartElements()
        {
            if (!initialized)
            {
                style.Init();
                initialized = true;
            }

            ResetPosition();
            currentSectionIndex = -1;
            elementsCount = 0;
        }

        public void StartSection(string name)
        {
            int index = sections.FindIndex(x => x.Name == name);
            if (index == -1)
            {
                var section = new Section();
                section.Name = name;
                section.Expanded = Expander(name, section.Expanded);
                sections.Add(section);
                currentSectionIndex = sections.Count - 1;
            }
            else
            {
                sections[index].Expanded = Expander(name, sections[index].Expanded);
                currentSectionIndex = index;
            }
        }

        public void EndSection()
        {
            currentSectionIndex = -1;
        }

        public bool Button(string message)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                return GUI.Button(NextRect(0.8f), message, style.Button);
            }
            else
            {
                return false;
            }
        }

        public bool Toggle(bool value, string message)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                return GUI.Toggle(NextRect(), value, message);
            }
            else
            {
                // Returns previous value
                return value;
            }
        }

        public int Slider(int current, int leftValue, int rightValue)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                return (int)GUI.HorizontalSlider(NextRect(), current, leftValue, rightValue);
            }
            else
            {
                return current;
            }
        }

        public bool Expander(string message, bool value)
        {
            return GUI.Toggle(NextRect(0.98f), value, message, style.Expander);
        }

        public void Label(string message)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                GUI.Label(NextRect(), message, style.Label);
            }
        }

        public void StartDisabled()
        {
            GUI.enabled = false;
        }

        public void EndDisabled()
        {
            GUI.enabled = true;
        }

        private Rect NextRect(float widthElementScale = 0.95f)
        {
            currentElementPosition.y += ElementHeight + elementsMarginY;
            float windowCentreX = Size.x / 2f;
            float halfElementWidth = Size.x * widthElementScale / 2f;
            float posX = windowCentreX - halfElementWidth;
            elementsCount++;
            return new Rect(posX, currentElementPosition.y, Size.x * widthElementScale, ElementHeight);
        }

        private void ResetPosition()
        {
            currentElementPosition = new Vector2(0f, 0f);
        }
    }
}