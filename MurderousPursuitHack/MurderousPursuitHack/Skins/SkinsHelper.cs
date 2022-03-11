namespace MurderousPursuitHack.Skins
{
    using BG.Utils;
    using Opsive.ThirdPersonController;
    using ProjectX.Characters;
    using ProjectX.Player;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class SkinsHelper
    {
        public static void ClientSwitchSkin()
        {
            XPlayer player = GameInfoManager.Instance.LocalPlayer;
            if (player != null)
            {
                List<CharacterType> allTypes = Singleton<CharacterManager>.Instance.GetAvailableCharacterTypes(true);
                CharacterType randomType = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
                int randomCharacterSkin = Singleton<CharacterManager>.Instance.GetRandomCharacterSkin(randomType, true);
                BaseCharacter baseCharacter = Utility.GetComponentForType<BaseCharacter>(player.gameObject);
                baseCharacter.ClientChangeCharacter(randomType, randomCharacterSkin);
            }
        }

        public static void RestoreSkin(XPlayer player)
        {
            if (player != null)
            {
                BaseCharacter baseCharacter = Utility.GetComponentForType<BaseCharacter>(player.gameObject);
                MethodInfo methodInfo = typeof(BaseCharacter).GetMethod("ClientCreateCharacter", BindingFlags.Instance | BindingFlags.NonPublic);
                methodInfo.Invoke(baseCharacter, new object[] { baseCharacter.CharacterType, true, false });
            }
        }
    }
}
