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
            (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.defaultRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.defaultFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.nimbleRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.nimbleFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("runMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.runMoveSpeed);
            (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.fastWalkMoveSpeed);
            (typeof(XCharacterMovement)).GetField("walkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * PlayerInfo.walkMoveSpeed);
        }

        private static void SetBaseValues()
        {
            if (GameInfoManager.Instance != null)
            {
                if (GameInfoManager.Instance.Players != null)
                {
                    if (GameInfoManager.Instance.Players.Count > 0)
                    {
                        PlayerInfo.defaultRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultRunMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.defaultFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultFastWalkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));

                        PlayerInfo.nimbleRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleRunMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.nimbleFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleFastWalkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));

                        PlayerInfo.runMoveSpeed = (float)(typeof(XCharacterMovement).GetField("runMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.fastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("fastWalkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        PlayerInfo.walkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("walkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                    }
                }
            }
        }

        public static void DisableSpeedhack()
        {
            var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
            (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.defaultRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.defaultFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.nimbleRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.nimbleFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("runMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.runMoveSpeed);
            (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.fastWalkMoveSpeed);
            (typeof(XCharacterMovement)).GetField("walkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, PlayerInfo.walkMoveSpeed);
        }
    }
}
