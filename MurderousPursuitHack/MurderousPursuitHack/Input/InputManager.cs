namespace MurderousPursuitHack.Input
{
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Movement;
    using MurderousPursuitHack.Visuals;
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
            if (!HackManager.Instance.InGame)
            {
                return;
            }

            UpdateKeys();

            if (HackManager.Instance.IsHost)
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
                Skins.ChangeLocalPlayerSkin();
            }

            if (Input.GetKeyDown(Keybindings.TeleportToQuarry))
            {
                Teleports.TeleportToQuarry();
            }

            if (Input.GetKeyDown(Keybindings.TeleportToClosestHunter))
            {
                Teleports.TeleportToClosestHunter();
            }

            if (Input.GetKeyDown(Keybindings.TeleportQuarryToLocal))
            {
                Teleports.TeleportQuarry();
            }

            if (Input.GetKeyDown(Keybindings.TeleportHunter))
            {
                Teleports.TeleportHunter();
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
