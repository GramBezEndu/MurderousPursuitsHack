namespace MurderousPursuitHack.Movement
{
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Players;
    using ProjectX.Player;
    using System.Linq;
    using UnityEngine;

    public class SpeedhackManager : MonoBehaviour
    {
        // Default movement values
        private float defaultRunMoveSpeed;

        private float defaultFastWalkMoveSpeed;

        private float nimbleRunMoveSpeed;

        private float nimbleFastWalkMoveSpeed;

        private float runMoveSpeed;

        private float fastWalkMoveSpeed;

        private float walkMoveSpeed;

        private bool initialized;

        public void Update()
        {
            if (!HackManager.Instance.InGame)
            {
                return;
            }

            if (!initialized)
            {
                initialized = TryToInitialize();
            }

            if (!initialized)
            {
                return;
            }

            if (Settings.Current.Speedhack)
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
            PlayerData local = PlayersHelper.SafeGetLocalPlayer();
            float multiplier = Settings.Current.SpeedMultiplier;

            local.CharacterMovement.SetFieldValue("defaultRunMoveSpeed", multiplier * defaultRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("defaultFastWalkMoveSpeed", multiplier * defaultFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleRunMoveSpeed", multiplier * nimbleRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleFastWalkMoveSpeed", multiplier * nimbleFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("runMoveSpeed", multiplier * runMoveSpeed);
            local.CharacterMovement.SetFieldValue("fastWalkMoveSpeed", multiplier * fastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("walkMoveSpeed", multiplier * walkMoveSpeed);
        }

        public void DisableSpeedhack()
        {
            PlayerData local = PlayersHelper.SafeGetLocalPlayer();
            local.CharacterMovement.SetFieldValue("defaultRunMoveSpeed", defaultRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("defaultFastWalkMoveSpeed", defaultFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleRunMoveSpeed", nimbleRunMoveSpeed);
            local.CharacterMovement.SetFieldValue("nimbleFastWalkMoveSpeed", nimbleFastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("runMoveSpeed", runMoveSpeed);
            local.CharacterMovement.SetFieldValue("fastWalkMoveSpeed", fastWalkMoveSpeed);
            local.CharacterMovement.SetFieldValue("walkMoveSpeed", walkMoveSpeed);
        }

        private bool TryToInitialize()
        {
            return SetDefaultValues();
        }

        private bool SetDefaultValues()
        {
            if (HackManager.Instance == null)
            {
                return false;
            }

            if (HackManager.Instance.Players.Count == 0)
            {
                return false;
            }

            XCharacterMovement characterMovement = HackManager.Instance.Players.Values.First().CharacterMovement;
            defaultRunMoveSpeed = (float)(characterMovement.GetFieldValue("defaultRunMoveSpeed"));
            defaultFastWalkMoveSpeed = (float)(characterMovement.GetFieldValue("defaultFastWalkMoveSpeed"));

            nimbleRunMoveSpeed = (float)(characterMovement.GetFieldValue("nimbleRunMoveSpeed"));
            nimbleFastWalkMoveSpeed = (float)(characterMovement.GetFieldValue("nimbleFastWalkMoveSpeed"));

            runMoveSpeed = (float)(characterMovement.GetFieldValue("runMoveSpeed"));
            fastWalkMoveSpeed = (float)(characterMovement.GetFieldValue("fastWalkMoveSpeed"));
            walkMoveSpeed = (float)(characterMovement.GetFieldValue("walkMoveSpeed"));
            return true;
        }
    }
}
