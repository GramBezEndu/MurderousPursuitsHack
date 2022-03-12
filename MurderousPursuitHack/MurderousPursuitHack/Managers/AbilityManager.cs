namespace MurderousPursuitHack.Managers
{
    using ProjectX.Player;
    using System;
    using System.Reflection;

    public static class AbilityManager
    {
        public static bool StartAbility<LoadoutAbility>()
        {
            if (HackManager.Instance == null)
            {
                return false;
            }

            PlayerData playerData = HackManager.Instance.Players.Find(x => x.IsLocalPlayer);
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

        public static bool StartAttack(XPlayer victim)
        {
            if (HackManager.Instance == null)
            {
                return false;
            }

            PlayerData playerData = HackManager.Instance.Players.Find(x => x.IsLocalPlayer);
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
    }
}
