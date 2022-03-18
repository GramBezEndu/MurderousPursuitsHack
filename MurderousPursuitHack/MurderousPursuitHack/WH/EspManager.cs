namespace MurderousPursuitHack.WH
{
    using MurderousPursuitHack.Managers;
    using UnityEngine;

    public class EspManager : MonoBehaviour
    {
        private GUIStyle quarry;

        private GUIStyle hunter;

        private GUIStyle neutral;

        public void Start()
        {
            quarry = CreateTextStyle(Colors.Quarry);
            hunter = CreateTextStyle(Colors.Hunter);
            neutral = CreateTextStyle(Colors.Neutral);
        }

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
            GUIStyle currentStyle = GetStyleBasedOnPlayerType(playerData);
            string display = playerData.DisplayName + " " + FormatDistance(DistanceToLocalPlayer(playerData));
            Vector2 size = currentStyle.CalcSize(new GUIContent(display));
            Vector2 position = new Vector2(playerData.OnScreenPosition.x - (size.x / 2f), Screen.height - playerData.OnScreenPosition.y);
            GUI.Label(new Rect(position, 2f * size), display, currentStyle);
        }

        private GUIStyle GetStyleBasedOnPlayerType(PlayerData p)
        {
            if (p.IsHunterForLocal)
            {
                return hunter;
            }
            else if (p.IsQuarryForLocal)
            {
                return quarry;
            }
            else
            {
                return neutral;
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

        private GUIStyle CreateTextStyle(Color color)
        {
            var style = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
            };
            style.normal.textColor = color;
            return style;
        }
    }
}
