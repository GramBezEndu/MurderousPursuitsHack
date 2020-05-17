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
        public List<PlayerInfo> Players = new List<PlayerInfo>(8);
        public bool IsGameInProgress
        {
            get
            {
                if (gameManager != null)
                {
                    return gameManager.IsGameRunning();
                }
                return false;
            }
        }

        private uint LocalPlayerId;
        private List<uint> CurrentHunters = new List<uint>();
        private uint CurrentQuarry;
        private readonly BindingFlags fieldGet = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance;
        private PlayerManager playerManager;
        private QuarryManager quarryManager;
        private GameManager gameManager;
        private ExposureManager exposureManager;
        private Dictionary<uint, ExposureManager.PlayerExposure> exposure;

        public void Start()
        {
            FindReferences();
        }

        public void Update()
        {
            FindReferences();
            if (playerManager != null && gameManager != null && quarryManager != null && exposureManager != null && gameManager.IsGameRunning())
            {
                LocalPlayerId = playerManager.GetLocalPlayer().PlayerID;
                UpdateHunterList();
                UpdateQuarryList();
                Players.Clear();
                foreach (var p in playerManager.thePlayers.Values)
                {
                    UpdatePlayerInfo(p);
                }

                //0f -> set base speed values (they were not set yet)
                if (PlayerInfo.defaultRunMoveSpeed == 0f)
                {
                    PlayerInfo.defaultRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultRunMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.defaultFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultFastWalkMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));

                    PlayerInfo.nimbleRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleRunMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.nimbleFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleFastWalkMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));

                    PlayerInfo.runMoveSpeed = (float)(typeof(XCharacterMovement).GetField("runMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.fastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("fastWalkMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));
                    PlayerInfo.walkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("walkMoveSpeed", fieldGet).GetValue(Players[0].CharacterMovement));
                }

                //TODO: Could be event trigerred (better performance)
                //Update speedhack value
                if (HackSettingsManager.Speedhack)
                {
                    var local = Players.Find(x => x.IsLocalPlayer);
                    (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.defaultRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.defaultFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.nimbleRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.nimbleFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("runMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.runMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.fastWalkMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("walkMoveSpeed", fieldGet).SetValue(local.CharacterMovement,
                        HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.walkMoveSpeed);
                }
                //Disable speedhack -> restore values
                else
                {
                    var local = Players.Find(x => x.IsLocalPlayer);
                    (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.defaultRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.defaultFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.nimbleRunMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.nimbleFastWalkMoveSpeed);

                    (typeof(XCharacterMovement)).GetField("runMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.runMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.fastWalkMoveSpeed);
                    (typeof(XCharacterMovement)).GetField("walkMoveSpeed", fieldGet).SetValue(local.CharacterMovement, PlayerInfo.walkMoveSpeed);
                }

                if (HackSettingsManager.ZeroExposure)
                    exposure[LocalPlayerId].exposure = 0f;

                //Input
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    TeleportLocalPlayerToQuarry();
                }
                else if(Input.GetKeyDown(KeyCode.Keypad2))
                {
                    TeleportLocalPlayerToHunter();
                }
                else if (Input.GetKeyDown(KeyCode.F6))
                {
                    StartPlacePieBomb();
                }
                else if (Input.GetKeyDown(KeyCode.F7))
                {
                    Flash();
                }
            }
        }

        private void UpdateQuarryList()
        {
            CurrentQuarry = gameManager.QuarryManager.GetQuarryForPlayer(LocalPlayerId);
        }

        private void UpdateHunterList()
        {
            CurrentHunters = gameManager.QuarryManager.GetHuntersForPlayer(LocalPlayerId);
        }

        private void UpdatePlayerInfo(XPlayer p)
        {
            XCharacterMovement xCharacterMovement = (XCharacterMovement)
                (typeof(XPlayer).GetField("characterMovement", fieldGet).GetValue(p));
            Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", fieldGet).GetValue(xCharacterMovement));
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
                /*(XPlacePieBomb)xCharacterMovement.Abilities.Abilities.SingleOrDefault(x => x is XPlacePieBomb),*/
                PlayerPerk = p.PlayerPerk,
                Collider = collider,
            };
            currentPlayer.OnScreenPosition = Camera.main.WorldToScreenPoint(currentPlayer.Position);
            Players.Add(currentPlayer);
        }

        private void FindReferences()
        {
            playerManager = PlayerManager.Instance;
            gameManager = GameManager.Instance;
            quarryManager = gameManager.QuarryManager;
            exposureManager = ExposureManager.Instance;
            exposure = (Dictionary<uint, ExposureManager.PlayerExposure>)(typeof(ExposureManager)).GetField("exposure", fieldGet).GetValue(exposureManager);
        }

        private void TeleportLocalPlayer(Vector3 pos)
        {
            var local = Players.Find(x => x.IsLocalPlayer);
            if (local != null)
            {
                Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", fieldGet).GetValue(local.CharacterMovement));
                characterTransform.position = pos;
            }
        }

        public void TeleportLocalPlayerToQuarry()
        {
            var target = Players.Find(x => x.IsQuarryForLocal);
            if (target != null)
                TeleportLocalPlayer(target.Position);
        }

        public void TeleportLocalPlayerToHunter()
        {
            var target = Players.Find(x => x.IsHunterForLocal);
            if (target != null)
                TeleportLocalPlayer(target.Position);
        }

        public void StartPlacePieBomb()
        {
            var local = Players.Find(x => x.IsLocalPlayer);
            if (local != null)
            {
                //Enable activity otherwise it won't start if it was not equipped
                local.PieBombAbility.enabled = true;
                local.CharacterAbilities.ClientTryStartAbility(local.PieBombAbility, true);
                local.PieBombAbility.ResetCooldown();
            }
        }

        public void Flash()
        {
            var local = Players.Find(x => x.IsLocalPlayer);
            if (local != null)
            {
                local.Flash.enabled = true;
                local.CharacterAbilities.ClientTryStartAbility(local.Flash, true);
                local.Flash.ResetCooldown();
            }
        }
    }
}
