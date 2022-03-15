namespace MurderousPursuitHack.Drawing
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class WindowStyle
    {
        public GUIStyle Button { get; private set; }

        public GUIStyle Expander { get; private set; }

        public GUIStyle Label { get; private set; }

        public GUIStyle Toggle { get; private set; }

        public GUIStyle Window { get; private set; }

        public GUIStyle HorizontalSlider { get; private set; }

        public GUIStyle RedSlider { get; private set; }

        public GUIStyle GreenSlider { get; private set; }

        public GUIStyle BlueSlider { get; private set; }

        public GUIStyle WhiteSlider { get; private set; }

        public GUIStyle Thumb { get; private set; }

        private readonly List<Texture2D> textures = new List<Texture2D>();

        public void Init()
        {
            InitStyles();
        }

        public void OnDestroy()
        {
            foreach (Texture2D texture in textures)
            {
                GameObject.Destroy(texture);
            }
        }

        private void InitStyles()
        {
            CreateButtonStyle();
            CreateExpanderStyle();
            CreateLabelStyle();
            CreateToggleStyle();
            CreateWindowStyle();
            CreateSliderStyle();
            CreateRedSlider();
            CreateGreenSlider();
            CreateBlueSlider();
            CreateWhiteSlider();
            CreateThumbStyle();
        }

        private void CreateWhiteSlider()
        {
            WhiteSlider = new GUIStyle(GUI.skin.horizontalSlider);
            WhiteSlider.normal.background = CreateTexture(2, 2, Color.white);
        }

        private void CreateBlueSlider()
        {
            BlueSlider = new GUIStyle(GUI.skin.horizontalSlider);
            BlueSlider.normal.background = CreateTexture(2, 2, Color.blue);
        }

        private void CreateGreenSlider()
        {
            GreenSlider = new GUIStyle(GUI.skin.horizontalSlider);
            GreenSlider.normal.background = CreateTexture(2, 2, Color.green);
        }

        private void CreateRedSlider()
        {
            RedSlider = new GUIStyle(GUI.skin.horizontalSlider);
            RedSlider.normal.background = CreateTexture(2, 2, Color.red);
        }

        private void CreateButtonStyle()
        {
            Button = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold
            };
            Button.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.8f, 1f));
            Button.hover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 1f, 1f));
        }

        private void CreateExpanderStyle()
        {
            Expander = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold
            };
            Expander.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.55f, 1f));
            Expander.onNormal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.55f, 1f));

            // brigther
            Expander.active.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            Expander.onActive.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            Expander.hover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            Expander.onHover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
        }

        private void CreateLabelStyle()
        {
            Label = new GUIStyle(GUI.skin.label);
        }

        private void CreateToggleStyle()
        {
            Toggle = new GUIStyle(GUI.skin.toggle)
            {
                fontStyle = FontStyle.Bold
            };
        }

        private void CreateWindowStyle()
        {
            Window = new GUIStyle(GUI.skin.window);
            Window.normal.background = CreateTexture(2, 2, Color.black);
            Window.onNormal.background = CreateTexture(2, 2, Color.black);
            Window.active.background = CreateTexture(2, 2, Color.black);
            Window.onActive.background = CreateTexture(2, 2, Color.black);
        }

        private void CreateSliderStyle()
        {
            HorizontalSlider = new GUIStyle(GUI.skin.horizontalSlider);
            HorizontalSlider.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.8f, 1f));
        }

        private void CreateThumbStyle()
        {
            Thumb = new GUIStyle(GUI.skin.horizontalSliderThumb);
            Thumb.normal.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
        }

        private Texture2D CreateTexture(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            textures.Add(result);
            return result;
        }
    }
}
