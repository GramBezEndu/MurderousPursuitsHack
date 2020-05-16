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
        public List<PlayerInfo> PlayerInfo = new List<PlayerInfo>(8);
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

        public void Start()
        {
            FindReferences();
        }

        public void Update()
        {
            FindReferences();
            if (playerManager != null && gameManager != null && quarryManager != null && gameManager.IsGameRunning())
            {
                LocalPlayerId = playerManager.GetLocalPlayer().PlayerID;
                UpdateHunterList();
                UpdateQuarryList();
                PlayerInfo.Clear();
                foreach (var p in playerManager.thePlayers.Values)
                {
                    UpdatePlayerInfo(p);
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
                //Note: use characterTransform.position (not transform.position [maybe the other one will work too, dunno])
                Position = characterTransform.position,
                Velocity = xCharacterMovement.Velocity,
                Size = collider.bounds.size,
                CharacterAbilities = xCharacterMovement.Abilities,
                Collider = collider,
            };
            currentPlayer.OnScreenPosition = Camera.main.WorldToScreenPoint(currentPlayer.Position);
            PlayerInfo.Add(currentPlayer);
        }

        private void FindReferences()
        {
            playerManager = PlayerManager.Instance;
            gameManager = GameManager.Instance;
            quarryManager = gameManager.QuarryManager;
        }
    }
}
