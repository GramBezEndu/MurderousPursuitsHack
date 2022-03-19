namespace MurderousPursuitHack.Managers
{
    using BG.UI;
    using BG.Utils;
    using ProjectX;
    using ProjectX.Levels;
    using ProjectX.Player;
    using ProjectX.UI.HUD;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class HackManager : MonoBehaviour
    {
        public static HackManager Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public bool InGame { get; private set; }

        public bool IsHost { get; private set; }

        public List<PlayerData> Players { get; private set; } = new List<PlayerData>(8);

        public XPlayer LocalPlayer { get; private set; }

        public uint LocalPlayerId { get; private set; }

        public QuarryLocatorBarHUD QuarryBar { get; private set; }

        public uint? CurrentQuarry { get; private set; }

        public HunterHUD HunterHUD { get; private set; }

        public List<uint> HunterIDs { get; private set; } = new List<uint>();

        public List<uint> BotIDs { get; private set; } = new List<uint>();

        public void Update()
        {
            LevelType levelFromSceneName = Singleton<LevelManager>.Instance.GetLevelFromSceneName(SceneManager.GetActiveScene().name);
            InGame = levelFromSceneName > LevelType.None;

            if (!InGame)
            {
                ClearData();
                return;
            }

            if (Singleton<LevelManager>.Instance != null)
            {
                IsHost = Singleton<LevelManager>.Instance.IsHost;
            }
            else
            {
                IsHost = false;
            }

            if (Singleton<PlayerManager>.Instance != null)
            {
                LocalPlayer = Singleton<PlayerManager>.Instance.GetLocalPlayer();
                if (LocalPlayer == null)
                {
                    return;
                }

                LocalPlayerId = LocalPlayer.PlayerID;
                FindHUD();

                if (Singleton<PlayerManager>.Instance.BotIDs != null)
                {
                    BotIDs = Singleton<PlayerManager>.Instance.BotIDs;
                }

                UpdateHunterList();
                UpdateCurrentQuarry();
                Camera camera = Camera.main;
                Players = CreatePlayerDataList(camera);
            }
        }

        private void ClearData()
        {
            HunterHUD = null;
            QuarryBar = null;
            LocalPlayer = null;
            Players.Clear();
        }

        private void FindHUD()
        {
            GameObject canvasGameplayHUD = GameObject.Find("CanvasGameplayHUD");
            if (canvasGameplayHUD != null)
            {
                HunterHUD = canvasGameplayHUD.GetComponentInChildren<HunterHUD>();
                QuarryBar = canvasGameplayHUD.GetComponentInChildren<QuarryLocatorBarHUD>();
            }
        }

        private List<PlayerData> CreatePlayerDataList(Camera camera)
        {
            List<PlayerData> playerDataList = new List<PlayerData>();
            if (Singleton<PlayerManager>.Instance.thePlayers == null)
            {
                return playerDataList;
            }

            foreach (XPlayer player in Singleton<PlayerManager>.Instance.thePlayers.Values)
            {
                PlayerData playerData = CreatePlayerData(player, camera);
                if (playerData != null)
                {
                    playerDataList.Add(CreatePlayerData(player, camera));
                }
            }

            return playerDataList;
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

        private PlayerData CreatePlayerData(XPlayer player, Camera camera)
        {
            // Note: Need to be "XPlayer" type
            XCharacterMovement xCharacterMovement = (XCharacterMovement)(typeof(XPlayer).GetField("characterMovement", ReflectionHelper.FieldGetFlags).GetValue(player));
            if (xCharacterMovement == null)
            {
                return null;
            }

            Transform characterTransform = (Transform)(xCharacterMovement.GetFieldValue("characterTransform"));

            PlayerData playerData = new PlayerData()
            {
                PlayerId = player.PlayerID,
                Player = player,
                IsLocalPlayer = xCharacterMovement.isLocalPlayer,
                IsAlive = player.IsAlive,
                IsBot = BotIDs.Contains(player.PlayerID),
                IsHunterForLocal = HunterIDs.Contains(player.PlayerID),
                IsQuarryForLocal = player.PlayerID == CurrentQuarry,
                // Note: use characterTransform.position (not transform.position)
                Position = characterTransform.position,
                CharacterMovement = xCharacterMovement,
                CharacterAbilities = xCharacterMovement.Abilities,
                PlayerPerk = player.PlayerPerk,
            };

            playerData.DisplayName = SetDisplayName(playerData);
            playerData.OnScreenPosition = camera.WorldToScreenPoint(playerData.Position);
            return playerData;
        }

        private string SetDisplayName(PlayerData playerData)
        {
            string defaultName = "UNKNOWN";
            if (playerData.IsBot)
            {
                return "BOT";
            }
            else
            {
                if (LobbyNetworkManager.Instance != null)
                {
                    return LobbyNetworkManager.Instance.GetPlayerName(playerData.PlayerId);
                }
            }

            return defaultName;
        }
    }
}
