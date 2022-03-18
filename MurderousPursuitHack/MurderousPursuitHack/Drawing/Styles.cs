namespace MurderousPursuitHack.Drawing
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Styles
    {
        public GUIStyle Button { get; private set; }

        public GUIStyle Box { get; private set; }

        public GUIStyle Expander { get; private set; }

        public GUIStyle SecondaryExpander { get; private set; }

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
            Window = WindowStyle();
            Box = BoxStyle();
            Button = ButtonStyle();
            Expander = ExpanderStyle();
            Label = LabelStyle();
            Toggle = ToggleStyle();
            HorizontalSlider = HorizontalSliderStyle();
            Thumb = ThumbStyle();
            RedSlider = RedSliderStyle();
            GreenSlider = GreenSliderStyle();
            BlueSlider = BlueSliderStyle();
            WhiteSlider = WhiteSliderStyle();
        }

        private GUIStyle BoxStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.box)
            {
                fontStyle = FontStyle.Bold
            };
            style.normal.background = CreateTexture(2, 2, new Color(0.37f, 0.37f, 0.37f, 1f));
            return style;
        }

        private GUIStyle WhiteSliderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.horizontalSlider);
            style.normal.background = CreateTexture(2, 2, Color.white);
            return style;
        }

        private GUIStyle BlueSliderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.horizontalSlider);
            style.normal.background = CreateTexture(2, 2, Color.blue);
            return style;
        }

        private GUIStyle GreenSliderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.horizontalSlider);
            style.normal.background = CreateTexture(2, 2, new Color(0f, 0.8f, 0f, 1f));
            return style;
        }

        private GUIStyle RedSliderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.horizontalSlider);
            style.normal.background = CreateTexture(2, 2, Color.red);
            return style;
        }

        private GUIStyle ButtonStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold
            };
            style.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.8f, 1f));
            style.hover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 1f, 1f));
            return style;
        }

        private GUIStyle ExpanderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold
            };
            style.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.55f, 1f));
            style.onNormal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.55f, 1f));

            // brigther
            style.active.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            style.onActive.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            style.hover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            style.onHover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            return style;
        }

        private GUIStyle LabelStyle()
        {
            return new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold
            };
        }

        private GUIStyle ToggleStyle()
        {
            return new GUIStyle(GUI.skin.toggle)
            {
                fontStyle = FontStyle.Bold
            };
        }

        private GUIStyle WindowStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.window)
            {
                fontSize = 14
            };
            Color color = new Color(0.25f, 0.25f, 0.25f, 1f);
            style.normal.background = CreateTexture(2, 2, color);
            style.onNormal.background = CreateTexture(2, 2, color);
            style.active.background = CreateTexture(2, 2, color);
            style.onActive.background = CreateTexture(2, 2, color);
            return style;
        }

        private GUIStyle HorizontalSliderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.horizontalSlider);
            style.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.8f, 1f));
            return style;
        }

        private GUIStyle ThumbStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.horizontalSliderThumb);
            style.normal.background = CreateTexture(2, 2, new Color(0f, 0.75f, 0.75f, 1f));
            return style;
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
