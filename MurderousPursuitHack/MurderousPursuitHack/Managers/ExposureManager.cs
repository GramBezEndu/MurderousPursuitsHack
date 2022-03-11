namespace MurderousPursuitHack
{
    using System.Collections.Generic;
    using UnityEngine;
    using static ProjectX.CoreGameLoop.ExposureManager;

    public class ExposureManager : MonoBehaviour
    {
        private Dictionary<uint, PlayerExposure> exposure;

        public void Update()
        {
            if (Settings.ZeroExposure && GameInfoManager.Instance.IsHost)
            {
                UpdateExposureDictionary();
                exposure[GameInfoManager.Instance.LocalPlayerId].exposure = 0f;
            }
        }

        private void UpdateExposureDictionary()
        {
            if (ProjectX.CoreGameLoop.ExposureManager.Instance != null)
            {
                exposure = (Dictionary<uint, PlayerExposure>)(ProjectX.CoreGameLoop.ExposureManager.Instance.GetFieldValue("exposure"));
            }
        }
    }
}
