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
        private bool inGame;

        private GameObject canvasGameplayHUD;

        public static HackManager Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public bool InGame
        { 
            get => inGame;
            private set
            {
                if (inGame != value)
                {
                    inGame = value;
                    if (inGame)
                    {
                        StartCoroutine(FindHUD());
                    }
                }
            }
        }

        public const uint InvalidPlayerId = 0;

        public bool IsHost { get; private set; }

        public Dictionary<uint, PlayerData> Players { get; private set; } = new Dictionary<uint, PlayerData>();

        public XPlayer LocalPlayer { get; private set; }

        public uint LocalPlayerId { get; private set; }

        public QuarryLocatorBarHUD QuarryBar { get; private set; }

        public uint QuarryId { get; private set; }

        public HunterHUD HunterHUD { get; private set; }

        public List<PlayerData> AliveHunters { get; private set; } = new List<PlayerData>();

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

            IsHost = Singleton<LevelManager>.Instance.IsHost;

            if (Singleton<PlayerManager>.Instance != null)
            {
                LocalPlayer = Singleton<PlayerManager>.Instance.GetLocalPlayer();
                if (LocalPlayer == null)
                {
                    return;
                }

                LocalPlayerId = LocalPlayer.PlayerID;

                if (Singleton<PlayerManager>.Instance.BotIDs != null)
                {
                    BotIDs = Singleton<PlayerManager>.Instance.BotIDs;
                }

                HunterIDs = GetHunterIDs();
                QuarryId = GetQuarry();
                Camera camera = Camera.main;
                UpdatePlayersData(camera);
                AliveHunters = UpdateHuntersList();
            }
        }

        private List<PlayerData> UpdateHuntersList()
        {
            List<PlayerData> hunters = new List<PlayerData>();
            foreach (PlayerData playerData in Players.Values)
            {
                if (playerData.IsHunterForLocal && playerData.IsAlive)
                {
                    hunters.Add(playerData);
                }
            }

            return hunters;
        }

        private void ClearData()
        {
            canvasGameplayHUD = null;
            HunterHUD = null;
            QuarryBar = null;
            LocalPlayer = null;
            LocalPlayerId = InvalidPlayerId;
            Players.Clear();
            QuarryId = InvalidPlayerId;
            HunterIDs.Clear();
            AliveHunters.Clear();
        }

        private IEnumerator FindHUD()
        {
            while (HunterHUD == null || QuarryBar == null)
            {
                canvasGameplayHUD = GameObject.Find("CanvasGameplayHUD");
                if (canvasGameplayHUD != null)
                {
                    HunterHUD = canvasGameplayHUD.GetComponentInChildren<HunterHUD>();
                    QuarryBar = canvasGameplayHUD.GetComponentInChildren<QuarryLocatorBarHUD>();
                }

                yield return null;
            }
        }

        private void UpdatePlayersData(Camera camera)
        {
            if (Singleton<PlayerManager>.Instance.thePlayers == null)
            {
                return;
            }

            foreach (XPlayer player in Singleton<PlayerManager>.Instance.thePlayers.Values)
            {
                if (Players.ContainsKey(player.PlayerID))
                {
                    UpdatePlayerData(Players[player.PlayerID], player, camera);
                }
                else
                {
                    PlayerData playerData = CreatePlayerData(player, camera);
                    if (playerData == null)
                    {
                        return;
                    }

                    Players.Add(player.PlayerID, playerData);
                }
            }
        }

        private uint GetQuarry()
        {
            if (QuarryBar != null)
            {
                return (uint)(QuarryBar.GetFieldValue("quarryID"));
            }
            else
            {
                return 0;
            }
        }

        private List<uint> GetHunterIDs()
        {
            List<uint> hunters = new List<uint>();
            if (HunterHUD != null)
            {
                object hunterDetails = HunterHUD.GetFieldValue("hunterDetails");
                int objIndex = 0;
                if (hunterDetails != null)
                {
                    foreach (object obj in (hunterDetails as IEnumerable))
                    {
                        Type objType = obj.GetType();
                        hunters.Add((uint)(objType.GetField("hunterID").GetValue(obj)));
                        objIndex++;
                    }
                }
            }

            return hunters;
        }

        private void UpdatePlayerData(PlayerData playerData, XPlayer player, Camera camera)
        {
            playerData.IsAlive = player.IsAlive;
            playerData.IsHunterForLocal = HunterIDs.Contains(player.PlayerID);
            playerData.IsQuarryForLocal = player.PlayerID == QuarryId;
            playerData.PlayerPerk = player.PlayerPerk;
            playerData.DisplayName = SetDisplayName(playerData);
            playerData.OnScreenPosition = camera.WorldToScreenPoint(playerData.Transform.position);
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
                IsQuarryForLocal = player.PlayerID == QuarryId,
                // Note: use characterTransform.position (not transform.position)
                Transform = characterTransform,
                CharacterMovement = xCharacterMovement,
                CharacterAbilities = xCharacterMovement.Abilities,
                PlayerPerk = player.PlayerPerk,
            };

            playerData.DisplayName = SetDisplayName(playerData);
            playerData.OnScreenPosition = camera.WorldToScreenPoint(playerData.Transform.position);
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
