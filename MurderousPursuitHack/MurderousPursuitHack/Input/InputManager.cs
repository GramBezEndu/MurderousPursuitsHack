namespace MurderousPursuitHack.Input
{
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Misc;
    using MurderousPursuitHack.Movement;
    using MurderousPursuitHack.Visuals;
    using MurderousPursuitHack.Windows;
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
            if (!HackManager.Instance.InGame)
            {
                return;
            }

            if (HackManager.Instance.IsHost)
            {
                UpdateHostFeaturesKeys();
            }
        }

        private void UpdateKeys()
        {
            if (Input.GetKeyDown(Keybindings.CheatWindow))
            {
                SettingsWindow.Instance.Visible = !SettingsWindow.Instance.Visible;
            }

            if (Input.GetKeyDown(Keybindings.Chams))
            {
                Settings.Current.QuarryChams = !Settings.Current.QuarryChams;
            }

            if (Input.GetKeyDown(Keybindings.PlayerESP))
            {
                Settings.Current.EspEnabled = !Settings.Current.EspEnabled;
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

            if (Input.GetKeyDown(Keybindings.FlyHack))
            {
                Managers.AbilityManager.Instance.ToggleFlyhack();
            }

            if (Input.GetKeyDown(Keybindings.FreezeAnimation))
            {
                Animations.ToggleAnimationFreeze();
            }

            if (Input.GetKeyDown(Keybindings.AutoKill))
            {
                AutoKill.Instance.enabled = !AutoKill.Instance.enabled;
            }
        }

        private void UpdateHostFeaturesKeys()
        {
            if (Input.GetKeyDown(Keybindings.ZeroExposure))
            {
                Settings.Current.ZeroExposure = !Settings.Current.ZeroExposure;
            }

            if (Input.GetKeyDown(Keybindings.PieBomb))
            {
                Managers.AbilityManager.Instance.StartAbility<XPlacePieBomb>();
            }

            if (Input.GetKeyDown(Keybindings.Flash))
            {
                Managers.AbilityManager.Instance.StartAbility<XFlash>();
            }

            if (Input.GetKeyDown(Keybindings.Disrupt))
            {
                Managers.AbilityManager.Instance.StartAbility<XDisrupt>();
            }
        }
    }
}
