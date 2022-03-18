namespace MurderousPursuitHack.Drawing
{
    using MurderousPursuitHack.Windows;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;

    public class ColorPreview
    {
        public ColorPreview(ColorData colorData)
        {
            ColorData = colorData;
            Texture = CreateTexture(2, 2, Color.white);
        }

        public ColorData ColorData { get; private set; }

        public Texture2D Texture { get; private set; }

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
