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
using static ProjectX.CoreGameLoop.ExposureManager;

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

        public static readonly BindingFlags FieldGetFlags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance;
        public List<PlayerInfo> Players = new List<PlayerInfo>(8);
        public uint LocalPlayerId;
        private List<uint> CurrentHunters = new List<uint>();
        private uint CurrentQuarry;

        public void Update()
        {
            LocalPlayerId = PlayerManager.Instance.GetLocalPlayer().PlayerID;
            UpdateHunterList();
            UpdateQuarryList();
            Players.Clear();
            foreach (var p in PlayerManager.Instance.thePlayers.Values)
            {
                UpdatePlayerInfo(p);
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
                PlayerPerk = p.PlayerPerk,
                Collider = collider,
            };
            currentPlayer.OnScreenPosition = Camera.main.WorldToScreenPoint(currentPlayer.Position);
            Players.Add(currentPlayer);
        }
    }
}
