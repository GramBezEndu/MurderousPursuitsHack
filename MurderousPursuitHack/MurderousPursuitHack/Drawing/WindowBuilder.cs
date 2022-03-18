namespace MurderousPursuitHack.Drawing
{
    using MurderousPursuitHack.Windows;
    using System.Collections.Generic;
    using UnityEngine;

    public class WindowBuilder
    {
        private readonly Window window;

        private readonly List<Section> sections = new List<Section>();

        private readonly float sectionIndent = 15f;

        private readonly float spacingAfterSection = 20f;

        private Vector2 cachedElementPosition;

        private int currentSectionIndex = -1;

        private bool initialized;

        private int elementsCount;

        public WindowBuilder(Window window)
        {
            this.window = window;
            Styles = new Styles();
        }

        public Vector2 CurrentElementPosition { get; set; }

        public Styles Styles { get; private set; }

        public void Start()
        {
            if (!initialized)
            {
                Styles.Init();
                initialized = true;
            }

            ResetPosition();
            elementsCount = 0;
            cachedElementPosition = Vector2.zero;
            currentSectionIndex = -1;
        }

        public void OnDestroy()
        {
            Styles.OnDestroy();
        }

        public void CreateWindow(GUI.WindowFunction windowFunction, int windowID)
        {
            GUI.Window(windowID, new Rect(window.Position, window.Size), windowFunction, window.Name, Styles.Window);
        }

        public void StartSection(string name, float height)
        {
            int index = sections.FindIndex(x => x.Name == name);
            Rect rectangle = NextRect(new Vector2(0.98f, 0.75f), Allignement.Center);
            GUI.Box(new Rect(CurrentElementPosition, new Vector2(window.Size.x * 0.99f, height)), name, Styles.Box);

            if (index == -1)
            {
                Section section = new Section
                {
                    Name = name
                };
                //section.Expanded = Expander(new Rect(rectangle.x, rectangle.y + 5f, rectangle.width, rectangle.height), name, section.Expanded);
                sections.Add(section);
                currentSectionIndex = sections.Count - 1;
            }
            else
            {
                //sections[index].Expanded = Expander(new Rect(rectangle.x, rectangle.y + 5f, rectangle.width, rectangle.height), name, sections[index].Expanded);
                currentSectionIndex = index;
            }
        }

        public void EndSection()
        {
            currentSectionIndex = -1;
            CurrentElementPosition = new Vector2(CurrentElementPosition.x, CurrentElementPosition.y + spacingAfterSection);
        }

        public bool Button(string message)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                return GUI.Button(NextRect(new Vector2(0.9f, 1f), Allignement.Center), message, Styles.Button);
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
                return GUI.Toggle(NextRect(), value, message, Styles.Toggle);
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
                return (int)GUI.HorizontalSlider(NextRect(elementWidthMultiplier), current, leftValue, rightValue, Styles.HorizontalSlider, Styles.Thumb);
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
                return (int)GUI.HorizontalSlider(NextRect(elementScale), current, leftValue, rightValue, sliderStyle, Styles.Thumb);
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
                GUI.Label(nextRect, label, Styles.Label);
                return (int)GUI.HorizontalSlider(
                    new Rect(nextRect.x + nextRect.width, nextRect.y + 5f, window.Size.x * 0.55f, window.ElementHeight * 0.55f),
                    current,
                    leftValue,
                    rightValue,
                    sliderStyle,
                    Styles.Thumb);
            }
            else
            {
                return current;
            }
        }

        public bool Expander(Rect rectangle, string message, bool value)
        {
            return GUI.Toggle(rectangle, value, message, Styles.Expander);
        }

        public void Label(string message)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                GUI.Label(NextRect(), message, Styles.Label);
            }
        }

        public bool ColorPreview(ColorPreview colorPreview)
        {
            if (currentSectionIndex == -1 || sections[currentSectionIndex].Expanded)
            {
                bool clicked;
                Vector2 colorPreviewSize = new Vector2(30f, 17f);
                Color cachedColor = GUI.color;
                GUI.color = colorPreview.ColorData.Color;
                clicked = GUI.Button(
                    new Rect(window.Size.x - (colorPreviewSize.x * 1.2f), CurrentElementPosition.y, colorPreviewSize.x, colorPreviewSize.y),
                    colorPreview.Texture,
                    colorPreview.Button);
                GUI.color = cachedColor;
                return clicked;
            }
            else
            {
                return false;
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
                CurrentElementPosition = new Vector2(0f, -window.ElementHeight);
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

        private Rect NextRect(float widthElementScale = 0.7f)
        {
            return NextRect(new Vector2(widthElementScale, 1f));
        }

        private Rect NextRect(Vector2 elementScale, Allignement allignement = Allignement.Left)
        {
            if (elementsCount > 0 || (elementsCount == 0 && window.Name != string.Empty))
            {
                CurrentElementPosition = new Vector2(
                    CurrentElementPosition.x,
                    CurrentElementPosition.y + (window.ElementHeight * elementScale.y) + window.ElementsMarginY);
            }

            if (currentSectionIndex != -1 && sections[currentSectionIndex].Expanded)
            {
                CurrentElementPosition = new Vector2(CurrentElementPosition.x + sectionIndent, CurrentElementPosition.y);
            }

            elementsCount++;
            Rect elementRectangle;
            switch (allignement)
            {
                case Allignement.Center:
                    float windowCentreX = window.Size.x / 2f;
                    float halfElementWidth = window.Size.x * elementScale.x / 2f;
                    float posX = windowCentreX - halfElementWidth;
                    elementRectangle = new Rect(posX, CurrentElementPosition.y, window.Size.x * elementScale.x, window.ElementHeight * elementScale.y);
                    break;
                case Allignement.Left:
                default:
                    elementRectangle = 
                        new Rect(CurrentElementPosition.x, CurrentElementPosition.y, window.Size.x * elementScale.x, window.ElementHeight * elementScale.y);
                    break;
            }

            if (currentSectionIndex != -1 && sections[currentSectionIndex].Expanded)
            {
                CurrentElementPosition = new Vector2(CurrentElementPosition.x - sectionIndent, CurrentElementPosition.y);
            }

            return elementRectangle;
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
