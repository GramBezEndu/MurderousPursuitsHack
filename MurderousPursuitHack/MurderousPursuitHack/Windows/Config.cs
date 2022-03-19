namespace MurderousPursuitHack.Windows
{
    public class Config : Window
    {
        public static Config Instance { get; private set; }

        public override void Awake()
        {
            base.Awake();
            Name = "CONFIG";
            Instance = this;
        }
    }
}
