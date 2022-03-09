namespace MurderousPursuitHack.Managers
{
    using System;

    public static class AbilityManager
    {
        public static void StartAbility<LoadoutAbility>()
        {
            if (GameInfoManager.Instance != null)
            {
                PlayerData playerData = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
                if (playerData != null)
                {
                    ProjectX.Abilities.LoadoutAbility ability = 
                        (ProjectX.Abilities.LoadoutAbility)Array.Find(playerData.CharacterMovement.Abilities.Abilities, x => typeof(LoadoutAbility).Equals(x.GetType()));
                    ability.enabled = true;
                    ability.ResetCooldown();
                    playerData.CharacterAbilities.ClientTryStartAbility(ability, true);
                    // TODO: Reset cooldown when ability ends
                    ability.ResetCooldown();
                }
            }
        }
    }
}
