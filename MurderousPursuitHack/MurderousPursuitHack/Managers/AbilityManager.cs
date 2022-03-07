﻿using Opsive.ThirdPersonController.Abilities;
using ProjectX.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MurderousPursuitHack
{
    public static class AbilityManager
    {
        public static void StartAbility<LoadoutAbility>()
        {
            if (GameInfoManager.Instance != null)
            {
                var local = GameInfoManager.Instance.Players.Find(x => x.IsLocalPlayer);
                if (local != null)
                {
                    var ability = Array.Find(local.CharacterMovement.Abilities.Abilities, x => typeof(LoadoutAbility).Equals(x.GetType()));
                    ability.enabled = true;
                    local.CharacterAbilities.ClientTryStartAbility(ability, true);
                    if (ability is ProjectX.Abilities.LoadoutAbility loadoutAbility)
                        loadoutAbility.ResetCooldown();
                }
            }
        }
    }
}