namespace MurderousPursuitHack.WH
{
    using MurderousPursuitHack.Managers;
    using ProjectX;
    using UnityEngine;

    public class EspManager : MonoBehaviour
    {
        public EspColors Colors { get; set; } = new EspColors()
        {
            Quarry = Color.cyan,
            Hunter = Color.red,
            Neutral = Color.magenta,
        };

        public void OnGUI()
        {
            if (!HackManager.Instance.InGame)
            {
                return;
            }

            if (Event.current.type != EventType.Repaint)
            {
                // Optimization: repaint ESP only once per frame
                return;
            }

            if (Settings.Current.EspEnabled)
            {
                DrawPlayerESP();
            }
        }

        private void DrawPlayerESP()
        {
            foreach (PlayerData p in HackManager.Instance.Players)
            {
                // Unity returns Vector3.zero when out off screen
                if (p.OnScreenPosition == Vector3.zero)
                {
                    continue;
                }

                if (p.OnScreenPosition.z < 0)
                {
                    continue;
                }

                if (p.IsLocalPlayer == true)
                {
                    continue;
                }

                if (!p.IsAlive)
                {
                    continue;
                }

                DrawPlayer(p);
            }
        }

        private void DrawPlayer(PlayerData playerData)
        {
            SetGuiColorBasedOnPlayerType(playerData);

            GUI.Label(
                new Rect(playerData.OnScreenPosition.x, Screen.height - playerData.OnScreenPosition.y, 150, 50),
                playerData.DisplayName + " " + FormatDistance(DistanceToLocalPlayer(playerData)));
        }

        private void SetGuiColorBasedOnPlayerType(PlayerData p)
        {
            if (p.IsHunterForLocal)
            {
                GUI.color = Colors.Hunter;
            }
            else if (p.IsQuarryForLocal)
            {
                GUI.color = Colors.Quarry;
            }
            else
            {
                GUI.color = Colors.Neutral;
            }
        }

        private int DistanceToLocalPlayer(PlayerData p)
        {
            return (int)Vector3.Distance(HackManager.Instance.LocalPlayer.transform.position, p.Position);
        }

        private string FormatDistance(int distance)
        {
            return string.Format("[{0}]", distance.ToString());
        }
    }
}
