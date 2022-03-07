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
        // Default movement values
        public float DefaultRunMoveSpeed;

        public float DefaultFastWalkMoveSpeed;

        public float NimbleRunMoveSpeed;

        public float NimbleFastWalkMoveSpeed;

        public float RunMoveSpeed;

        public  float FastWalkMoveSpeed;

        public float WalkMoveSpeed;

        public void Update()
        {
            if (DefaultRunMoveSpeed == 0f)
            {
                SetBaseValues();
            }

            //If base values still not set -> return 
            if (DefaultRunMoveSpeed == 0f)
            {
                return;
            }

            if (HackSettingsManager.Speedhack)
            {
                EnableSpeedhack();
            }
            else
            {
                DisableSpeedhack();
            }
        }

        public void EnableSpeedhack()
        {
            var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
            (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * DefaultRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * DefaultFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * NimbleRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * NimbleFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("runMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * RunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * FastWalkMoveSpeed);
            (typeof(XCharacterMovement)).GetField("walkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement,
                HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex] * WalkMoveSpeed);
        }

        private void SetBaseValues()
        {
            if (GameInfoManager.Instance != null)
            {
                if (GameInfoManager.Instance.Players != null)
                {
                    if (GameInfoManager.Instance.Players.Count > 0)
                    {
                        DefaultRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultRunMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        DefaultFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("defaultFastWalkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));

                        NimbleRunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleRunMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        NimbleFastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("nimbleFastWalkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));

                        RunMoveSpeed = (float)(typeof(XCharacterMovement).GetField("runMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        FastWalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("fastWalkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                        WalkMoveSpeed = (float)(typeof(XCharacterMovement).GetField("walkMoveSpeed", Utils.FieldGetFlags).GetValue(GameInfoManager.Instance.Players[0].CharacterMovement));
                    }
                }
            }
        }

        public void DisableSpeedhack()
        {
            var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
            (typeof(XCharacterMovement)).GetField("defaultRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, DefaultRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("defaultFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, DefaultFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("nimbleRunMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, NimbleRunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("nimbleFastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, NimbleFastWalkMoveSpeed);

            (typeof(XCharacterMovement)).GetField("runMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, RunMoveSpeed);
            (typeof(XCharacterMovement)).GetField("fastWalkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, FastWalkMoveSpeed);
            (typeof(XCharacterMovement)).GetField("walkMoveSpeed", Utils.FieldGetFlags).SetValue(local.CharacterMovement, WalkMoveSpeed);
        }
    }
}
