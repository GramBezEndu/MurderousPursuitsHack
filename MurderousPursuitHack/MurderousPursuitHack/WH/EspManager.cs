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
            if (Event.current.type != EventType.Repaint)
            {
                // Optimization: repaint ESP only once per frame
                return;
            }

            if (Settings.EspEnabled)
            {
                DrawWallhack();
            }
        }

        private void DrawWallhack()
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

        private void DrawPlayer(PlayerData playerInfo)
        {
            SetGuiColorBasedOnPlayerType(playerInfo);

            string name = "PLAYER ";
            if (playerInfo.IsBot)
            {
                name = "BOT ";
            }
            else
            {
                if (LobbyNetworkManager.Instance != null)
                {
                    string nickname = LobbyNetworkManager.Instance.GetPlayerName(playerInfo.PlayerId);
                    if (nickname != string.Empty)
                    {
                        name = nickname;
                    }
                }
            }

            GUI.Label(
                new Rect(playerInfo.OnScreenPosition.x, Screen.height - playerInfo.OnScreenPosition.y, 150, 50),
                name + FormatDistance(DistanceToLocalPlayer(playerInfo)));
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
