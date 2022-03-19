namespace MurderousPursuitHack.Misc
{
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Movement;
    using System.Linq;
    using UnityEngine;

    public class AutoKill : MonoBehaviour
    {
        public static AutoKill Instance { get; private set; }

        private float lastCache = 0f;

        public void Awake()
        {
            enabled = false;
            Instance = this;
        }

        public void Update()
        {
            if (Time.time >= lastCache)
            {
                lastCache = Time.time + 3f;

                ProjectX.Player.XPlayer localPlayer = HackManager.Instance.LocalPlayer;
                if (localPlayer == null)
                {
                    return;
                }

                PlayerData target = GetTarget();
                if (target == null)
                {
                    return;
                }
                Teleports.TeleportPlayerToLocal(target, true);
            }
        }

        private PlayerData GetTarget()
        {
            if (!HackManager.Instance.InGame)
            {
                return null;
            }

            PlayerData target = HackManager.Instance.Players[HackManager.Instance.CurrentQuarry];
            if (target == null)
            {
                target = Teleports.GetClosestHunter();
                if (target == null)
                {
                    return null;
                }
                else
                {
                    return target;
                }
            }
            else
            {
                return target;
            }
        }
    }
}
