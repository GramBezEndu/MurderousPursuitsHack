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
                GetBaseValues();
            }

            //If base values still not set -> return 
            if (DefaultRunMoveSpeed == 0f)
            {
                return;
            }

            if (Settings.Speedhack)
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
            float multiplier = HackSettingsManager.SpeedhackMultipliers[HackSettingsManager.CurrentSpeedMultiplierIndex];

            local.CharacterMovement.SetFieldValue("defaultRunMoveSpeed", multiplier * DefaultRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("defaultFastWalkMoveSpeed", multiplier * DefaultFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleRunMoveSpeed", multiplier * NimbleRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleFastWalkMoveSpeed", multiplier * NimbleFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("runMoveSpeed", multiplier * RunMoveSpeed);
            local.CharacterMovement.SetFieldValue("fastWalkMoveSpeed", multiplier * FastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("walkMoveSpeed", multiplier * WalkMoveSpeed);
        }

        private void GetBaseValues()
        {
            if (GameInfoManager.Instance != null)
            {
                if (GameInfoManager.Instance.Players != null)
                {
                    if (GameInfoManager.Instance.Players.Count > 0)
                    {
                        XCharacterMovement characterMovement = GameInfoManager.Instance.Players[0].CharacterMovement;
                        DefaultRunMoveSpeed = (float)(characterMovement.GetFieldValue("defaultRunMoveSpeed"));
                        DefaultFastWalkMoveSpeed = (float)(characterMovement.GetFieldValue("defaultFastWalkMoveSpeed"));

                        NimbleRunMoveSpeed = (float)(characterMovement.GetFieldValue("nimbleRunMoveSpeed"));
                        NimbleFastWalkMoveSpeed = (float)(characterMovement.GetFieldValue("nimbleFastWalkMoveSpeed"));

                        RunMoveSpeed = (float)(characterMovement.GetFieldValue("runMoveSpeed"));
                        FastWalkMoveSpeed = (float)(characterMovement.GetFieldValue("fastWalkMoveSpeed"));
                        WalkMoveSpeed = (float)(characterMovement.GetFieldValue("walkMoveSpeed"));
                    }
                }
            }
        }

        public void DisableSpeedhack()
        {
            var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
            local.CharacterMovement.SetFieldValue("defaultRunMoveSpeed", DefaultRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("defaultFastWalkMoveSpeed", DefaultFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleRunMoveSpeed", NimbleRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleFastWalkMoveSpeed", NimbleFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("runMoveSpeed", RunMoveSpeed);
            local.CharacterMovement.SetFieldValue("fastWalkMoveSpeed", FastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("walkMoveSpeed", WalkMoveSpeed);
        }
    }
}
