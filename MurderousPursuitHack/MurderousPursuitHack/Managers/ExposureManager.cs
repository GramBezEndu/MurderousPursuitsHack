namespace MurderousPursuitHack.Managers
{
    using System.Collections.Generic;
    using UnityEngine;
    using static ProjectX.CoreGameLoop.ExposureManager;

    public class ExposureManager : MonoBehaviour
    {
        private Dictionary<uint, PlayerExposure> exposure;

        public void Update()
        {
            if (!HackManager.Instance.InGame)
            {
                return false;
            }

            if (Settings.ZeroExposure && HackManager.Instance.IsHost)
            {
                UpdateExposureDictionary();
                exposure[HackManager.Instance.LocalPlayerId].exposure = 0f;
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
