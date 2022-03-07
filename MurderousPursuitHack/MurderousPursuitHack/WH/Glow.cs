namespace MurderousPursuitHack.WH
{
    using BG.Utils;
    using ProjectX.Characters;
    using ProjectX.Player;
    using System;
    using UnityEngine;

    public class Glow : MonoBehaviour
    {
        Material neutralGlow;

        Material hunterGlow;

        Material quarryGlow;

        public void Start()
        {
            neutralGlow = CreateNeutralMaterial();
            hunterGlow = CreateHunterMaterial(neutralGlow);
            quarryGlow = CreateQuarryMaterial(neutralGlow);
        }

        public void Update()
        {
            foreach (PlayerInfo playerInfo in GameInfoManager.Instance.Players)
            {
                ApplyGlow(playerInfo);
            }
        }

        private Material CreateNeutralMaterial()
        {
            Material neutralMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };
            // Good looking glow effect:
            neutralMaterial.SetInt("_SrcBlend", 5);
            neutralMaterial.SetInt("_DstBlend", 10);
            neutralMaterial.SetInt("_Cull", 0);
            neutralMaterial.SetInt("_ZTest", 8); // Render through walls.
            neutralMaterial.SetInt("_ZWrite", 0);
            neutralMaterial.SetColor("_Color", new Color(0.63f, 0f, 0.63f, 1f));

            return neutralMaterial;
        }

        private Material CreateHunterMaterial(Material neutral)
        {
            var newMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };
            newMaterial.CopyPropertiesFromMaterial(neutral);
            newMaterial.SetColor("_Color", new Color(0.55f, 0f, 0f, 1f));
            return newMaterial;
        }

        private Material CreateQuarryMaterial(Material neutral)
        {
            var newMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };
            newMaterial.CopyPropertiesFromMaterial(neutral);
            newMaterial.SetColor("_Color", new Color(0f, 0.45f, 0.65f, 1f));
            return newMaterial;
        }

        private void ApplyGlow(PlayerInfo playerInfo)
        {
            XPlayer player = playerInfo.Player;
            if (player.isLocalPlayer)
            {
                return;
            }

            Renderer[] allRenderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < allRenderers.Length; i++)
            {
                Renderer renderer = allRenderers[i];
                if (playerInfo.IsHunterForLocal)
                {
                    renderer.material = hunterGlow;

                    var shared = renderer.sharedMaterials;
                    for (int j = 0; j < shared.Length; j++)
                    {
                        shared[j] = hunterGlow;
                    }
                    renderer.sharedMaterials = shared;
                }
                else if (playerInfo.IsQuarryForLocal)
                {
                    renderer.material = quarryGlow;

                    var shared = renderer.sharedMaterials;
                    for (int j = 0; j < shared.Length; j++)
                    {
                        shared[j] = quarryGlow;
                    }
                    renderer.sharedMaterials = shared;
                }
                else
                {
                    renderer.material = neutralGlow;

                    var shared = renderer.sharedMaterials;
                    for (int j = 0; j < shared.Length; j++)
                    {
                        shared[j] = neutralGlow;
                    }
                    renderer.sharedMaterials = shared;
                }
            }
        }
    }
}
