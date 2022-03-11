namespace MurderousPursuitHack.Input
{
    using MurderousPursuitHack.Skins;
    using ProjectX.Abilities;
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
            UpdateKeys();

            if (GameInfoManager.Instance.IsHost)
            {
                UpdateHostFeaturesKeys();
            }
        }

        private void UpdateKeys()
        {
            if (Input.GetKeyDown(Keybindings.CheatWindow))
            {
                Settings.CheatsWindow = !Settings.CheatsWindow;
            }

            if (Input.GetKeyDown(Keybindings.Chams))
            {
                Settings.ChamsEnabled = !Settings.ChamsEnabled;
            }

            if (Input.GetKeyDown(Keybindings.Esp))
            {
                Settings.EspEnabled = !Settings.EspEnabled;
            }

            if (Input.GetKeyDown(Keybindings.DebugInfo))
            {
                Settings.DebugWindow = !Settings.DebugWindow;
            }

            if (Input.GetKeyDown(Keybindings.ChangeSkin))
            {
                SkinsHelper.ClientSwitchSkin();
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

        private void UpdateHostFeaturesKeys()
        {
            if (Input.GetKeyDown(Keybindings.ZeroExposure))
            {
                Settings.ZeroExposure = !Settings.ZeroExposure;
            }

            if (Input.GetKeyDown(Keybindings.PieBomb))
            {
                Managers.AbilityManager.StartAbility<XPlacePieBomb>();
            }

            if (Input.GetKeyDown(Keybindings.Flash))
            {
                Managers.AbilityManager.StartAbility<XFlash>();
            }

            if (Input.GetKeyDown(Keybindings.Disrupt))
            {
                Managers.AbilityManager.StartAbility<XDisrupt>();
            }
        }
    }
}
