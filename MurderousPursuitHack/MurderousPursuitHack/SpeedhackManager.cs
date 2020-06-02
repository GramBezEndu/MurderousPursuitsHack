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
    public class SpeedhackManager : MonoBehaviour
    {
        private static SpeedhackManager instance;
        public static SpeedhackManager Instance { get { return instance; } }

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Update()
        {
            if (PlayerInfo.defaultRunMoveSpeed == 0f)
                SetBaseValues();
            //If base values still not set -> return 
            if (PlayerInfo.defaultRunMoveSpeed == 0f)
                return;
            if (HackSettingsManager.Speedhack)
                EnableSpeedhack();
            else
                DisableSpeedhack();
        }

        public void EnableSpeedhack()
        {
            var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
            (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.defaultRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.defaultFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.nimbleRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.nimbleFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("runMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.runMoveSpeed);
            (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.fastWalkMoveSpeed);
            (typeof(XCharacterMovement)).GetField("walkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentMultiplier] * PlayerInfo.walkMoveSpeed);
        }

        private static void SetBaseValues()
        {
            if (GameInfoManager.Instance != null)
            {
                if (GameInfoManager.Instance.Players != null)
                {
                    if (GameInfoManager.Instance.Players.Count > 0)
                    {
                        PlayerInfo.defaultRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultRunMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.defaultFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultFastWalkMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));

                        PlayerInfo.nimbleRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleRunMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.nimbleFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleFastWalkMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));

                        PlayerInfo.runMoveSpeed = (float)(typeof(XCharacterMovement).GetField("runMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.fastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("fastWalkMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.walkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("walkMoveSpeed", GameInfoManager.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                    }
                }
            }
        }

        public static void DisableSpeedhack()
        {
            var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
            (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.defaultRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.defaultFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.nimbleRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.nimbleFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("runMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.runMoveSpeed);
            (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.fastWalkMoveSpeed);
            (typeof(XCharacterMovement)).GetField("walkMoveSpeed", GameInfoManager.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.walkMoveSpeed);
        }
    }
}
