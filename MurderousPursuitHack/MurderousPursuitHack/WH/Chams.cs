namespace MurderousPursuitHack.WH
{
    using ProjectX.Player;
    using System;
    using UnityEngine;

    public class Chams : MonoBehaviour
    {
        Material neutralGlow;

        Material hunterGlow;

        Material quarryGlow;

        Material localPlayerGlow;

        private readonly bool drawLocalPlayerChams = true;

        public void Start()
        {
            neutralGlow = CreateNeutralMaterial();
            hunterGlow = CreateHunterMaterial(neutralGlow);
            quarryGlow = CreateQuarryMaterial(neutralGlow);
            localPlayerGlow = CreateLocalPlayerMaterial(neutralGlow);
        }

        public void Update()
        {
            foreach (PlayerData playerInfo in GameInfoManager.Instance.Players)
            {
                UpdateGlow(playerInfo);
            }
        }

        public void OnDestroy()
        {
            GameObject.Destroy(neutralGlow);
            GameObject.Destroy(hunterGlow);
            GameObject.Destroy(quarryGlow);
            GameObject.Destroy(localPlayerGlow);
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

        private Material CreateLocalPlayerMaterial(Material neutral)
        {
            var newMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };
            newMaterial.CopyPropertiesFromMaterial(neutral);
            newMaterial.SetColor("_Color", Color.white);
            return newMaterial;
        }

        private Material CreateQuarryMaterial(Material neutral)
        {
            var newMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };
            newMaterial.CopyPropertiesFromMaterial(neutral);
            newMaterial.SetColor("_Color", new Color(0f, 0.5f, 0.7f, 1f));
            return newMaterial;
        }

        private void UpdateGlow(PlayerData playerInfo)
        {
            XPlayer player = playerInfo.Player;
            if (player.isLocalPlayer && drawLocalPlayerChams == false)
            {
                return;
            }

            Renderer[] allRenderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < allRenderers.Length; i++)
            {
                if (Settings.ChamsEnabled)
                {
                    ApplyChams(playerInfo, allRenderers, i);
                }
                else
                {
                    ClearChams(allRenderers, i);
                }
            }
        }

        private void ApplyChams(PlayerData playerInfo, Renderer[] allRenderers, int i)
        {
            Renderer renderer = allRenderers[i];
            if (playerInfo.IsHunterForLocal)
            {
                ApplyMaterial(renderer, hunterGlow);
            }
            else if (playerInfo.IsQuarryForLocal)
            {
                ApplyMaterial(renderer, quarryGlow);
            }
            else if (playerInfo.IsLocalPlayer)
            {
                ApplyMaterial(renderer, localPlayerGlow);
            }
            else
            {
                ApplyMaterial(renderer, neutralGlow);
            }
        }

        private void ClearChams(Renderer[] allRenderers, int i)
        {
            // TODO: Finish
            return;

            Renderer renderer = allRenderers[i];

            renderer.material = neutralGlow;

            var shared = renderer.sharedMaterials;
            for (int j = 0; j < shared.Length; j++)
            {
                shared[j] = neutralGlow;
            }
            renderer.sharedMaterials = shared;
        }

        // TODO: Refactor, maybe extension method
        private void ApplyMaterial(Renderer renderer, Material material)
        {
            renderer.material = material;

            Material[] shared = renderer.sharedMaterials;
            for (int j = 0; j < shared.Length; j++)
            {
                shared[j] = material;
            }
            renderer.sharedMaterials = shared;
        }
    }
}
