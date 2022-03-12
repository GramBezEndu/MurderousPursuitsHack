namespace MurderousPursuitHack.Visuals
{
    using BG.Utils;
    using MurderousPursuitHack.Managers;
    using Opsive.ThirdPersonController;
    using ProjectX.Characters;
    using ProjectX.Player;
    using System.Collections.Generic;
    using System.Reflection;

    public static class Skins
    {
        public static bool ChangeLocalPlayerSkin()
        {
            XPlayer player = HackManager.Instance.LocalPlayer;
            if (player == null)
            {
                return false;
            }

            List<CharacterType> allTypes = Singleton<CharacterManager>.Instance.GetAvailableCharacterTypes(true);
            CharacterType randomType = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
            int randomCharacterSkin = Singleton<CharacterManager>.Instance.GetRandomCharacterSkin(randomType, true);
            BaseCharacter baseCharacter = Utility.GetComponentForType<BaseCharacter>(player.gameObject);
            baseCharacter.ClientChangeCharacter(randomType, randomCharacterSkin);
            return true;
        }

        public static bool RestoreSkin(XPlayer player)
        {
            if (player == null)
            {
                return false;
            }

            BaseCharacter baseCharacter = Utility.GetComponentForType<BaseCharacter>(player.gameObject);
            MethodInfo methodInfo = typeof(BaseCharacter).GetMethod("ClientCreateCharacter", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(baseCharacter, new object[] { baseCharacter.CharacterType, true, false });
            return true;
        }
    }
}
