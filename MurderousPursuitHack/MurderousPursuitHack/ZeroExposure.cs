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
    public class ZeroExposure : MonoBehaviour
    {
        private static ZeroExposure instance;
        public static ZeroExposure Instance { get { return instance; } }

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private Dictionary<uint, PlayerExposure> exposure;

        public void Update()
        {
            UpdateExposureDictionary();
            if (HackSettingsManager.ZeroExposure)
                exposure[GameInfoManager.Instance.LocalPlayerId].exposure = 0f;
        }

        private void UpdateExposureDictionary()
        {
            if (ExposureManager.Instance != null)
                exposure = (Dictionary<uint, PlayerExposure>)(typeof(ExposureManager)).GetField("exposure", GameInfoManager.FieldGetFlags).GetValue(ExposureManager.Instance);
        }
    }
}
