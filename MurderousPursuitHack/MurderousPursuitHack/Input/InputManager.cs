namespace MurderousPursuitHack.Input
{
    using MurderousPursuitHack.Skins;
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public Keybindings Keybindings { get; set; } = new Keybindings();

        public void Start()
        {
            Instance = this;
        }

        public void Update()
        {
            if (Input.GetKeyDown(Keybindings.CheatWindow))
            {
                HackSettingsManager.WindowHidden = !HackSettingsManager.WindowHidden;
            }

            if (Input.GetKeyDown(Keybindings.Wallhack))
            {
                HackSettingsManager.WallhackEnabled = !HackSettingsManager.WallhackEnabled;
            }

            if (Input.GetKeyDown(Keybindings.DebugInfo))
            {
                HackSettingsManager.DebugInfo = !HackSettingsManager.DebugInfo;
            }

            if (Input.GetKeyDown(Keybindings.ChangeSkin))
            {
                SkinsHelper.ChangeSkin();
            }

            if (Input.GetKeyDown(Keybindings.TeleportToQuarry))
            {
                TeleportManager.TeleportToQuarry();
            }

            if (Input.GetKeyDown(Keybindings.TeleportToAnyHunter))
            {
                TeleportManager.TeleportToAnyHunter();
            }
        }
    }
}
