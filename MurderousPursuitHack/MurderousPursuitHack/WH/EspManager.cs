﻿namespace MurderousPursuitHack
{
    using MurderousPursuitHack.WH;
    using ProjectX;
    using UnityEngine;

    class EspManager : MonoBehaviour
    {
        public EspColors WallhackColors { get; set; } = new EspColors()
        {
            Quarry = Color.cyan,
            Hunter = Color.red,
            Neutral = Color.magenta,
        };

        public void OnGUI()
        {
            if (HackSettingsManager.EspEnabled)
            {
                DrawWallhack();
            }
        }

        private void DrawWallhack()
        {
            foreach (var p in GameInfoManager.Instance.Players)
            {
                //Unity returns Vector3.zero when out off screen
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
            //Bring back old color
            GUI.color = Color.yellow;
        }

        private void DrawPlayer(PlayerInfo playerInfo)
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
                    var nickname = LobbyNetworkManager.Instance.GetPlayerName(playerInfo.PlayerId);
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

        private void SetGuiColorBasedOnPlayerType(PlayerInfo p)
        {
            if (p.IsHunterForLocal)
            {
                GUI.color = WallhackColors.Hunter;
            }
            else if (p.IsQuarryForLocal)
            {
                GUI.color = WallhackColors.Quarry;
            }
            else
            {
                GUI.color = WallhackColors.Neutral;
            }
        }

        private int DistanceToLocalPlayer(PlayerInfo p)
        {
            return (int)Vector3.Distance(GameInfoManager.Instance.LocalPlayer.transform.position, p.Position);
        }

        private string FormatDistance(int distance)
        {
            return string.Format("[{0}]", distance.ToString());
        }
    }
}