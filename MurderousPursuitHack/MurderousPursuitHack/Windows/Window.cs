namespace MurderousPursuitHack.Windows
{
    using MurderousPursuitHack.Drawing;
    using System;
    using UnityEngine;

    public class Window : MonoBehaviour
    {
        public event EventHandler OnVisibleChanged;

        private static int GlobalId = 1;

        private bool visible = true;

        public int Id { get; private set; }

        public Vector2 Position { get; set; } = new Vector2(170, 260);

        public Vector2 Size { get; set; } = new Vector2(300, 460);

        public float ElementHeight { get; set; } = 30f;

        public float ElementsMarginY { get; set; } = 5f;

        public string Name { get; set; } = "UNNAMED";

        public bool Visible
        {
            get => visible;
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnVisibleChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public WindowBuilder Builder { get; private set; }

        public virtual void Start()
        {
            Id = GlobalId;
            GlobalId++;
            Builder = new WindowBuilder(this);
        }

        public void OnGUI()
        {
            if (Visible)
            {
                Builder.CreateWindow(CreateElements, Id);
            }
        }

        protected virtual void CreateElements(int windowID)
        {
            Builder.Start();
            Builder.StartSection("EMPTY", 35f);
            Builder.EndSection();
        }
    }
}
