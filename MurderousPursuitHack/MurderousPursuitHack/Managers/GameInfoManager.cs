namespace MurderousPursuitHack
{
    using BG.UI;
    using BG.Utils;
    using ProjectX.Characters;
    using ProjectX.CoreGameLoop;
    using ProjectX.Player;
    using ProjectX.UI.HUD;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public class GameInfoManager : MonoBehaviour
    {
        // Known singletons:
        // Singleton<PlayerManager>
        // Singleton<GameDataManager>

        public static GameInfoManager Instance { get; private set; }

        public void Start()
        {
            Instance = this;
        }

        public List<PlayerInfo> Players = new List<PlayerInfo>(8);

        public XPlayer LocalPlayer;

        public uint LocalPlayerId;

        public QuarryLocatorBarHUD QuarryBar;

        public uint? CurrentQuarry;

        public HunterHUD HunterHUD;

        public List<uint> HunterIDs = new List<uint>();

        public void Update()
        {
            if (Singleton<PlayerManager>.Instance != null)
            {
                Players.Clear();
                LocalPlayer = Singleton<PlayerManager>.Instance.GetLocalPlayer();
                if (LocalPlayer != null)
                {
                    LocalPlayerId = LocalPlayer.PlayerID;
                    if (Singleton<PlayerManager>.Instance.PlayerCount > 0)
                    {
                        HunterHUD = UnityEngine.Object.FindObjectOfType<HunterHUD>();
                        UpdateHunterList();

                        foreach (var p in Singleton<PlayerManager>.Instance.thePlayers.Values)
                        {
                            UpdatePlayerInfo(p);
                        }

                        QuarryBar = UnityEngine.Object.FindObjectOfType<QuarryLocatorBarHUD>();
                        UpdateCurrentQuarry();
                    }
                }
            }
        }

        private void UpdateCurrentQuarry()
        {
            if (LocalPlayer != null && QuarryBar != null)
            {
                CurrentQuarry = (uint)(QuarryBar.GetType().GetField("quarryID", Utils.FieldGetFlags).GetValue(QuarryBar));
            }
            else
            {
                CurrentQuarry = null;
            }
        }

        private void UpdateHunterList()
        {
            HunterIDs.Clear();
            if (LocalPlayer != null && HunterHUD != null)
            {
                object hunterDetails = (HunterHUD.GetType().GetField("hunterDetails", Utils.FieldGetFlags).GetValue(HunterHUD));
                int objIndex = 0;
                if (hunterDetails != null)
                {
                    foreach (object obj in (hunterDetails as IEnumerable))
                    {
                        Type objType = obj.GetType();
                        HunterIDs.Add((uint)(objType.GetField("hunterID").GetValue(obj)));
                        objIndex++;
                    }
                }
            }
        }

        private void UpdatePlayerInfo(XPlayer p)
        {
            XCharacterMovement xCharacterMovement = (XCharacterMovement)
                (typeof(XPlayer).GetField("characterMovement", Utils.FieldGetFlags).GetValue(p));
            Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", Utils.FieldGetFlags).GetValue(xCharacterMovement));
            var collider = xCharacterMovement.GetComponent<Collider>();
            var playerInfo = new PlayerInfo()
            {
                PlayerId = p.PlayerID,
                Player = p,
                DisplayName = p.name,
                IsLocalPlayer = xCharacterMovement.isLocalPlayer,
                IsAlive = p.IsAlive,
                IsBot = Singleton<PlayerManager>.Instance.BotIDs.Contains(p.PlayerID) ? true : false,
                IsHunterForLocal = HunterIDs.Contains(p.PlayerID) ? true : false,
                IsQuarryForLocal = p.PlayerID == CurrentQuarry ? true : false,
                //Note: use characterTransform.position (not transform.position)
                Position = characterTransform.position,
                Velocity = xCharacterMovement.Velocity,
                Size = collider.bounds.size,
                CharacterMovement = xCharacterMovement,
                CharacterAbilities = xCharacterMovement.Abilities,
                PlayerPerk = p.PlayerPerk,
                Collider = collider,
            };
            playerInfo.OnScreenPosition = Camera.main.WorldToScreenPoint(playerInfo.Position);
            Players.Add(playerInfo);
        }
    }
}
