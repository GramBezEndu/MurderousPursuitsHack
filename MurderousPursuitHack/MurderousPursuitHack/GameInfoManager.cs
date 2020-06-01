using ProjectX.Abilities;
using ProjectX.CoreGameLoop;
using ProjectX.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MurderousPursuitHack
{
    public class GameInfoManager : MonoBehaviour
    {
        private static GameInfoManager instance;
        public static GameInfoManager Instance { get { return instance; } }

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public List<PlayerInfo> Players = new List<PlayerInfo>(8);

        private uint LocalPlayerId;
        private List<uint> CurrentHunters = new List<uint>();
        private uint CurrentQuarry;
        public static readonly BindingFlags FieldGetFlags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance;
        private Dictionary<uint, ExposureManager.PlayerExposure> exposure;

        public void Start()
        {
            UpdateExposureDictionary();
        }

        public void Update()
        {
            UpdateExposureDictionary();
            if (PlayerManager.Instance != null && GameManager.Instance != null && GameManager.Instance.QuarryManager != null && ExposureManager.Instance != null && GameManager.Instance.IsGameRunning())
            {
                LocalPlayerId = PlayerManager.Instance.GetLocalPlayer().PlayerID;
                UpdateHunterList();
                UpdateQuarryList();
                Players.Clear();
                foreach (var p in PlayerManager.Instance.thePlayers.Values)
                {
                    UpdatePlayerInfo(p);
                }

                //0f -> set base speed values (they were not set yet)
                if (PlayerInfo.defaultRunMoveSpeed == 0f)
                {
                    PlayerInfo.defaultRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultRunMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.defaultFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultFastWalkMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));

                    PlayerInfo.nimbleRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleRunMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.nimbleFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleFastWalkMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));

                    PlayerInfo.runMoveSpeed = (float)(typeof(XCharacterMovement).GetField("runMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.fastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("fastWalkMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.walkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("walkMoveSpeed", FieldGetFlags).GetValue(Players[0].CharacterMovement));
                }

                //TODO: Could be event trigerred (better performance)
                //Update speedhack value
                if (HackSettingsManager.Speedhack)
                {
                    var local = Players.Find(x => x.IsLocalPlayer);
                    (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.defaultRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.defaultFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.nimbleRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.nimbleFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("runMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.runMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.fastWalkMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("walkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.walkMoveSpeed);
                }
                //Disable speedhack -> restore values
                else
                {
                    var local = Players.Find(x => x.IsLocalPlayer);
                    (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.defaultRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.defaultFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.nimbleRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.nimbleFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("runMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.runMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.fastWalkMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("walkMoveSpeed", FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.walkMoveSpeed);
                }

                if (HackSettingsManager.ZeroExposure)
                    exposure[LocalPlayerId].exposure = 0f;

                //Input
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    TeleportManager.TeleportLocalPlayerToQuarry();
                }
                else if(Input.GetKeyDown(KeyCode.Keypad2))
                {
                    TeleportManager.TeleportLocalPlayerToHunter();
                }
                else if (Input.GetKeyDown(KeyCode.F6))
                {
                    AbilityManager.StartAbility<XPlacePieBomb>();
                }
                else if (Input.GetKeyDown(KeyCode.F7))
                {
                    AbilityManager.StartAbility<XFlash>();
                }
                else if (Input.GetKeyDown(KeyCode.F8))
                {
                    AbilityManager.StartAbility<XDisrupt>();
                }
            }
        }

        private void UpdateQuarryList()
        {
            CurrentQuarry = GameManager.Instance.QuarryManager.GetQuarryForPlayer(LocalPlayerId);
        }

        private void UpdateHunterList()
        {
            CurrentHunters = GameManager.Instance.QuarryManager.GetHuntersForPlayer(LocalPlayerId);
        }

        private void UpdatePlayerInfo(XPlayer p)
        {
            XCharacterMovement xCharacterMovement = (XCharacterMovement)
                (typeof(XPlayer).GetField("characterMovement", FieldGetFlags).GetValue(p));
            Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", FieldGetFlags).GetValue(xCharacterMovement));
            var collider = xCharacterMovement.GetComponent<Collider>();
            var currentPlayer = new PlayerInfo()
            {
                Name = p.name,
                IsLocalPlayer = xCharacterMovement.isLocalPlayer,
                IsHunterForLocal = CurrentHunters.Contains(p.PlayerID) ? true : false,
                IsQuarryForLocal = p.PlayerID == CurrentQuarry ? true : false,
                //Note: use characterTransform.position (not transform.position)
                Position = characterTransform.position,
                Velocity = xCharacterMovement.Velocity,
                Size = collider.bounds.size,
                CharacterMovement = xCharacterMovement,
                CharacterAbilities = xCharacterMovement.Abilities,
                PieBombAbility = (XPlacePieBomb)Array.Find(xCharacterMovement.Abilities.Abilities, x => x is XPlacePieBomb),
                Flash = (XFlash)Array.Find(xCharacterMovement.Abilities.Abilities, x => x is XFlash),
                PlayerPerk = p.PlayerPerk,
                Collider = collider,
            };
            currentPlayer.OnScreenPosition = Camera.main.WorldToScreenPoint(currentPlayer.Position);
            Players.Add(currentPlayer);
        }

        private void UpdateExposureDictionary()
        {
            if (ExposureManager.Instance != null)
                exposure = (Dictionary<uint, ExposureManager.PlayerExposure>)(typeof(ExposureManager)).GetField("exposure", FieldGetFlags).GetValue(ExposureManager.Instance);
        }
    }
}
