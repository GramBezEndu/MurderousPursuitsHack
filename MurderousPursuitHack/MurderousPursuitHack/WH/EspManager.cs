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
            Settings settings = Settings.Current;
            quarry = CreateTextStyle(settings.QuarryEspColor.Color);
            hunter = CreateTextStyle(settings.HunterEspColor.Color);
            neutral = CreateTextStyle(settings.NeutralEspColor.Color);

            settings.QuarryEspColor.OnColorChanged += (o, e) => quarry.normal.textColor = settings.QuarryEspColor.Color;
            settings.HunterEspColor.OnColorChanged += (o, e) => hunter.normal.textColor = settings.HunterEspColor.Color;
            settings.NeutralEspColor.OnColorChanged += (o, e) => neutral.normal.textColor = settings.NeutralEspColor.Color;
        }

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

            DrawPlayerESP();
        }

        private void DrawPlayerESP()
        {
            foreach (PlayerData p in HackManager.Instance.Players.Values)
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

                Settings settings = Settings.Current;

                if (!settings.QuarryEsp && p.IsQuarryForLocal)
                {
                    continue;
                }

                if (!settings.HunterEsp && p.IsHunterForLocal)
                {
                    continue;
                }

                if (!settings.NeutralChams && (!p.IsHunterForLocal && !p.IsQuarryForLocal))
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
            return (int)Vector3.Distance(HackManager.Instance.LocalPlayer.transform.position, p.Transform.position);
        }

        private string FormatDistance(int distance)
        {
            return string.Format("[{0}]", distance.ToString());
        }

        private GUIStyle CreateTextStyle(Color color)
        {
            GUIStyle style = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
            };
            style.normal.textColor = color;
            return style;
        }
    }
}
