using ProjectX.CoreGameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static ProjectX.CoreGameLoop.ExposureManager;

namespace MurderousPursuitHack
{
    public class ExposureManager : MonoBehaviour
    {
        private Dictionary<uint, PlayerExposure> exposure;

        public void Update()
        {
            UpdateExposureDictionary();
            if (HackSettingsManager.ZeroExposure)
            {
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
