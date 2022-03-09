namespace MurderousPursuitHack.Drawing
{
    using UnityEngine;

    public class WindowStyle
    {
        public GUIStyle Button { get; private set; }

        public GUIStyle Expander { get; private set; }

        public GUIStyle Label { get; private set; }

        public void Init()
        {
            InitStyles();
        }

        private void InitStyles()
        {
            CreateButtonStyle();
            CreateExpanderStyle();
            CreateLabelStyle();
        }

        private void CreateButtonStyle()
        {
            Button = new GUIStyle(GUI.skin.button);
            Button.normal.background = CreateTexture(2, 2, new Color(0f, 0.55f, 0.8f, 1f));
            Button.hover.background = CreateTexture(2, 2, new Color(0f, 0.75f, 1f, 1f));
        }

        private void CreateExpanderStyle()
        {
            Expander = new GUIStyle(GUI.skin.button);
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
            return result;
        }
    }
}
