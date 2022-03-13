namespace MurderousPursuitHack.Drawing
{
    using System.Collections.Generic;
    using UnityEngine;

    public class WindowBuilder
    {
        private Vector2 currentElementPosition;

        private readonly float elementsMarginY = 5f;

        private readonly List<Section> sections = new List<Section>();

        private int currentSectionIndex = -1;

        private bool initialized;

        public WindowBuilder(Vector2 position, Vector2 windowSize, float elementHeight)
        {
            Position = position;
            Size = windowSize;
            ElementHeight = elementHeight;
            Style = new WindowStyle();
        }

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public float ElementHeight { get; set; }

        public WindowStyle Style { get; private set; }

        public void Start()
        {
            if (!initialized)
            {
                Style.Init();
                initialized = true;
            }

            ResetPosition();
            currentSectionIndex = -1;
        }

        public void OnDestroy()
        {
            Style.OnDestroy();
        }

        public void CreateWindow(GUI.WindowFunction windowFunction, int windowID, string name)
        {
            GUI.Window(windowID, new Rect(Position, Size), windowFunction, name, Style.Window);
        }

        public void StartSection(string name)
        {
            int index = sections.FindIndex(x => x.Name == name);
            if (index == -1)
            {
                Section section = new Section();
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
                return GUI.Button(NextRect(0.8f), message, Style.Button);
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
                return GUI.Toggle(NextRect(), value, message, Style.Toggle);
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
                return (int)GUI.HorizontalSlider(NextRect(), current, leftValue, rightValue, Style.HorizontalSlider, Style.Thumb);
            }
            else
            {
                return current;
            }
        }

        public bool Expander(string message, bool value)
        {
            return GUI.Toggle(NextRect(0.98f), value, message, Style.Expander);
        }

        public void Label(string message)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                GUI.Label(NextRect(), message, Style.Label);
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
            return new Rect(posX, currentElementPosition.y, Size.x * widthElementScale, ElementHeight);
        }

        private void ResetPosition()
        {
            currentElementPosition = new Vector2(0f, 0f);
        }
    }
}