namespace MurderousPursuitHack.Drawing
{
    using MurderousPursuitHack.Windows;
    using System.Collections.Generic;
    using UnityEngine;

    public class WindowBuilder
    {
        public Vector2 CurrentElementPosition;

        private Vector2 cachedElementPosition;

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
            cachedElementPosition = Vector2.zero;
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

        public int Slider(int current, int leftValue, int rightValue, float elementWidthMultiplier = 0.96f)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                return (int)GUI.HorizontalSlider(NextRect(elementWidthMultiplier), current, leftValue, rightValue, Style.HorizontalSlider, Style.Thumb);
            }
            else
            {
                return current;
            }
        }

        public int Slider(int current, int leftValue, int rightValue, GUIStyle sliderStyle, Vector2 elementScale)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                return (int)GUI.HorizontalSlider(NextRect(elementScale), current, leftValue, rightValue, sliderStyle, Style.Thumb);
            }
            else
            {
                return current;
            }
        }

        public int LabelSlider(string label, int current, int leftValue, int rightValue, GUIStyle sliderStyle)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                Rect nextRect = NextRect(new Vector2(0.05f, 1f), Allignement.Left);
                GUI.Label(nextRect, label, Style.Label);
                return (int)GUI.HorizontalSlider(
                    new Rect(nextRect.x + nextRect.width, nextRect.y + 5f, window.Size.x * 0.55f, window.ElementHeight * 0.55f),
                    current,
                    leftValue,
                    rightValue,
                    sliderStyle,
                    Style.Thumb);
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

        public Vector2 StartScrollView(Vector2 scrollPosition, Vector2 viewSize)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                Rect nextRect = NextRect();
                cachedElementPosition = CurrentElementPosition + new Vector2(window.Size.x, 350f) - new Vector2(0f, window.ElementHeight);
                CurrentElementPosition = new Vector2(0f, - window.ElementHeight);
                return GUI.BeginScrollView(
                    new Rect(nextRect.position, new Vector2(window.Size.x, 350f)),
                    scrollPosition,
                    new Rect(Vector2.zero, new Vector2(viewSize.x, viewSize.y)));
            }
            else
            {
                return scrollPosition;
            }
        }

        public void EndScrollView()
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                CurrentElementPosition = cachedElementPosition;
                GUI.EndScrollView();
            }
        }

        private Rect NextRect(float widthElementScale = 0.96f)
        {
            return NextRect(new Vector2(widthElementScale, 1f));
        }

        private Rect NextRect(Vector2 elementScale, Allignement allignement = Allignement.Center)
        {
            if (elementsCount > 0 || (elementsCount == 0 && window.Name != string.Empty))
            {
                CurrentElementPosition.y += (window.ElementHeight * elementScale.y) + window.ElementsMarginY;
            }

            elementsCount++;
            switch(allignement)
            {
                case Allignement.Center:
                    float windowCentreX = window.Size.x / 2f;
                    float halfElementWidth = window.Size.x * elementScale.x / 2f;
                    float posX = windowCentreX - halfElementWidth;
                    return new Rect(posX, CurrentElementPosition.y, window.Size.x * elementScale.x, window.ElementHeight * elementScale.y);
                case Allignement.Left:
                default:
                    return new Rect(CurrentElementPosition.x + 10f, CurrentElementPosition.y, window.Size.x * elementScale.x, window.ElementHeight * elementScale.y);
            }
        }

        private void ResetPosition()
        {
            CurrentElementPosition = new Vector2(0f, 0f);
        }

        private enum Allignement
        {
            Center,
            Left,
        };
    }
}
