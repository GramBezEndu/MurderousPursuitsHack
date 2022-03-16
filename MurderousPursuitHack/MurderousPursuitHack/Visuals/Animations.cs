namespace MurderousPursuitHack.Visuals
{
    using MurderousPursuitHack.Managers;
    using UnityEngine;

    public static class Animations
    {
        public static bool ToggleAnimationFreeze()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            ProjectX.Player.XPlayer localPlayer = HackManager.Instance.LocalPlayer;
            if (localPlayer == null)
            {
                return false;
            }

            Animator animator = localPlayer.gameObject.GetComponent<Animator>();
            if (animator == null)
            {
                return false;
            }

            animator.enabled = !animator.enabled;
            return true;
        }
    }
}
