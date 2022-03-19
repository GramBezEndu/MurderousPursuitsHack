namespace MurderousPursuitHack.Drawing
{
    using MurderousPursuitHack.Windows;
    using UnityEngine;

    public class ColorPreview
    {
        public ColorPreview(ColorData colorData)
        {
            ColorData = colorData;
            Texture = CreateTexture(2, 2, Color.white);
        }

        public bool Initialized { get; private set; }

        public ColorData ColorData { get; private set; }

        public Texture2D Texture { get; private set; }

        public GUIStyle Button { get; private set; }

        public void Initialize()
        {
            if (!Initialized)
            {
                Button = ButtonStyle();
                Initialized = true;
            }
        }

        private GUIStyle ButtonStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.background = CreateTexture(2, 2, Color.white);
            style.active.background = CreateTexture(2, 2, Color.white);
            style.hover.background = CreateTexture(2, 2, new Color(0.85f, 0.85f, 0.85f, 1f));
            return style;
        }

        // TODO: Refactoring - remove duplicated method
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
