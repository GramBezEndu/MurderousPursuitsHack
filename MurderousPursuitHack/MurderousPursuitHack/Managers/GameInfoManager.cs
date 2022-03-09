namespace MurderousPursuitHack
{
    using BG.UI;
    using BG.Utils;
    using ProjectX.Player;
    using ProjectX.UI.HUD;
    using System;
    using System.Collections;
    using System.Collections.Generic;
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

        public List<PlayerData> Players = new List<PlayerData>(8);

        public XPlayer LocalPlayer;

        public uint LocalPlayerId;

        public QuarryLocatorBarHUD QuarryBar;

        public uint? CurrentQuarry;

        public HunterHUD HunterHUD;

        public List<uint> HunterIDs = new List<uint>();

        private float lastCache;

        public void Update()
        {
            if (Singleton<PlayerManager>.Instance != null)
            {
                LocalPlayer = Singleton<PlayerManager>.Instance.GetLocalPlayer();
                if (LocalPlayer == null)
                {
                    return;
                }

                LocalPlayerId = LocalPlayer.PlayerID;
                if (NeedToUpdateCachedObjects())
                {
                    FindAndCacheObjects();
                }

                UpdateHunterList();
                UpdateCurrentQuarry();
                Players = CreatePlayerDataList();
            }
        }

        private void FindAndCacheObjects()
        {
            lastCache = Time.time + 1f;
            HunterHUD = UnityEngine.Object.FindObjectOfType<HunterHUD>();
            QuarryBar = UnityEngine.Object.FindObjectOfType<QuarryLocatorBarHUD>();
        }

        private List<PlayerData> CreatePlayerDataList()
        {
            List<PlayerData> players = new List<PlayerData>();
            if (Singleton<PlayerManager>.Instance.thePlayers == null)
            {
                return players;
            }

            foreach (XPlayer player in Singleton<PlayerManager>.Instance.thePlayers.Values)
            {
                players.Add(CreatePlayerData(player));
            }

            return players;
        }

        private void UpdateCurrentQuarry()
        {
            if (QuarryBar != null)
            {
                CurrentQuarry = (uint)(QuarryBar.GetFieldValue("quarryID"));
            }
            else
            {
                CurrentQuarry = null;
            }
        }

        private void UpdateHunterList()
        {
            HunterIDs.Clear();
            if (HunterHUD != null)
            {
                object hunterDetails = HunterHUD.GetFieldValue("hunterDetails");
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

        private PlayerData CreatePlayerData(XPlayer player)
        {
            // Note: Need to be "XPlayer" type
            XCharacterMovement xCharacterMovement = (XCharacterMovement)(typeof(XPlayer).GetField("characterMovement", ReflectionHelper.FieldGetFlags).GetValue(player));
            Transform characterTransform = (Transform)(xCharacterMovement.GetFieldValue("characterTransform"));
            Collider collider = xCharacterMovement.GetComponent<Collider>();
            PlayerData playerData = new PlayerData()
            {
                PlayerId = player.PlayerID,
                Player = player,
                DisplayName = player.name,
                IsLocalPlayer = xCharacterMovement.isLocalPlayer,
                IsAlive = player.IsAlive,
                IsBot = Singleton<PlayerManager>.Instance.BotIDs.Contains(player.PlayerID) ? true : false,
                IsHunterForLocal = HunterIDs.Contains(player.PlayerID) ? true : false,
                IsQuarryForLocal = player.PlayerID == CurrentQuarry ? true : false,
                // Note: use characterTransform.position (not transform.position)
                Position = characterTransform.position,
                Velocity = xCharacterMovement.Velocity,
                Size = collider.bounds.size,
                CharacterMovement = xCharacterMovement,
                CharacterAbilities = xCharacterMovement.Abilities,
                PlayerPerk = player.PlayerPerk,
                Collider = collider,
            };
            // Note: Caching camera might improve performance but it will add additional checks
            playerData.OnScreenPosition = Camera.main.WorldToScreenPoint(playerData.Position);
            return playerData;
        }

        private bool NeedToUpdateCachedObjects()
        {
            return Time.time >= lastCache;
        }
    }
}
