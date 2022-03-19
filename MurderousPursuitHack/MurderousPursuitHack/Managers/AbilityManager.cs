namespace MurderousPursuitHack.Managers
{
    using ProjectX.Abilities;
    using ProjectX.Player;
    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public class AbilityManager : MonoBehaviour
    {
        public static AbilityManager Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public bool StartAbility<LoadoutAbility>()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData playerData = HackManager.Instance.Players[HackManager.Instance.LocalPlayerId];
            if (playerData == null)
            {
                return false;
            }

            ProjectX.Abilities.LoadoutAbility ability =
                (ProjectX.Abilities.LoadoutAbility)Array.Find(playerData.CharacterMovement.Abilities.Abilities, x => typeof(LoadoutAbility).Equals(x.GetType()));
            ability.enabled = true;
            ability.ResetCooldown();
            playerData.CharacterAbilities.ClientTryStartAbility(ability, true);
            // TODO: Reset cooldown when ability ends
            ability.ResetCooldown();
            return true;
        }

        public bool StartAttack(XPlayer victim)
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData playerData = HackManager.Instance.Players[HackManager.Instance.LocalPlayerId];
            if (playerData == null)
            {
                return false;
            }

            ProjectX.Abilities.XPlayerAttack attack =
                (ProjectX.Abilities.XPlayerAttack)Array.Find
                (playerData.CharacterMovement.Abilities.Abilities, x => typeof(ProjectX.Abilities.XPlayerAttack).Equals(x.GetType()));
            attack.SetVictim(victim.gameObject);
            attack.CaughtBySecurity = false;
            attack.SetDirection(ProjectX.Abilities.AttackDirectionState.AttackFromBack);
            if (playerData.IsHunterForLocal)
            {
                attack.SetStun(true);
            }
            else
            {
                attack.SetStun(false);
            }
            attack.SetRiposte(false);

            attack.StartAttack();
            attack.SetFieldValue("isWaitingForAttackAnim", false);
            attack.SetFieldValue("isFinishingAttack", false);
            // 0 is equal to AttackState.None
            attack.SetFieldValue("attackState", 0);
            MethodInfo methodInfo =
                typeof(ProjectX.Abilities.XPlayerAttack).GetMethod("AbilityStopped", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(attack, new object[] { });
            return true;
        }

        public bool ToggleFlyhack()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            PlayerData playerData = HackManager.Instance.Players[HackManager.Instance.LocalPlayerId];
            if (playerData == null)
            {
                return false;
            }

            XPlayerFly fly = (XPlayerFly)Array.Find(playerData.CharacterMovement.Abilities.Abilities, x => typeof(XPlayerFly).Equals(x.GetType()));
            fly.enabled = true;
            fly.SetFieldValue("speedNormal", 4f);
            fly.SetFieldValue("speedFast", 10f);

            if (fly.IsActive == false)
            {
                playerData.CharacterAbilities.ClientTryStartAbility(fly, false);
            }
            else
            {
                playerData.CharacterAbilities.ClientTryStopAbility(fly, true);
            }

            return true;
        }

    }
}
