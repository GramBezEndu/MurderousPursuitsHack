namespace MurderousPursuitHack.Drawing
{
    using MurderousPursuitHack.Windows;
    using System.Collections.Generic;
    using UnityEngine;

    public class WindowBuilder
    {
        public Vector2 CurrentElementPosition;

        private readonly Window window;

        private readonly List<Section> sections = new List<Section>();

        private int currentSectionIndex = -1;

        private bool initialized;

        private int elementsCount;

        public WindowStyle Style { get; private set; }

        public WindowBuilder(Window window)
        {
            this.window = window;
            Style = new WindowStyle();
        }

        public void Start()
        {
            if (!initialized)
            {
                Style.Init();
                initialized = true;
            }

            ResetPosition();
            elementsCount = 0;
            currentSectionIndex = -1;
        }

        public void OnDestroy()
        {
            Style.OnDestroy();
        }

        public void CreateWindow(GUI.WindowFunction windowFunction, int windowID)
        {
            GUI.Window(windowID, new Rect(window.Position, window.Size), windowFunction, window.Name, Style.Window);
        }

        public void StartSection(string name)
        {
            int index = sections.FindIndex(x => x.Name == name);
            if (index == -1)
            {
                Section section = new Section
                {
                    Name = name
                };
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

        private Rect NextRect(float widthElementScale = 0.96f)
        {
            if (elementsCount > 0 || (elementsCount == 0 && window.Name != string.Empty))
            {
                CurrentElementPosition.y += window.ElementHeight + window.ElementsMarginY;
            }

            elementsCount++;
            float windowCentreX = window.Size.x / 2f;
            float halfElementWidth = window.Size.x * widthElementScale / 2f;
            float posX = windowCentreX - halfElementWidth;
            return new Rect(posX, CurrentElementPosition.y, window.Size.x * widthElementScale, window.ElementHeight);
        }

        private void ResetPosition()
        {
            CurrentElementPosition = new Vector2(0f, 0f);
        }
    }
}
