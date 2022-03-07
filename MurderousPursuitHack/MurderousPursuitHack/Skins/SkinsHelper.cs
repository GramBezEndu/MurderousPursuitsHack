namespace MurderousPursuitHack.Skins
{
    using BG.Utils;
    using Opsive.ThirdPersonController;
    using ProjectX.Characters;
    using System.Collections.Generic;

    public static class SkinsHelper
    {
        public static void ChangeSkin()
        {
            ProjectX.Player.XPlayer localPlayer = GameInfoManager.Instance.LocalPlayer;
            if (localPlayer != null)
            {
                List<CharacterType> allTypes = Singleton<CharacterManager>.Instance.GetAvailableCharacterTypes(true);
                CharacterType randomType = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
                int randomCharacterSkin = Singleton<CharacterManager>.Instance.GetRandomCharacterSkin(randomType, true);
                BaseCharacter baseCharacter = Utility.GetComponentForType<BaseCharacter>(GameInfoManager.Instance.LocalPlayer.gameObject);
                baseCharacter.ClientChangeCharacter(randomType, randomCharacterSkin);
            }
        }
    }
}
