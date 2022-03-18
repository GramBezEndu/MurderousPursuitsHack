namespace MurderousPursuitHack.WH
{
    using MurderousPursuitHack.Managers;
    using MurderousPursuitHack.Visuals;
    using ProjectX.Player;
    using UnityEngine;

    public class Chams : MonoBehaviour
    {
        private const string GlowPath = "Hidden/Internal-Colored";

        private const string DiffusePath = "Legacy Shaders/Self-Illumin/Diffuse";

        private const string OutlinePath = "Hidden/cc_frost";

        private Shader glowShader;

        private Material neutralChams;

        private Material hunterChams;

        private Material quarryChams;

        private Material localPlayerChams;

        public void Start()
        {
            glowShader = Shader.Find(GlowPath);
            neutralChams = CreateNeutralMaterial();
            hunterChams = CloneMaterial(neutralChams, Settings.Current.HunterChams.Color);
            quarryChams = CloneMaterial(neutralChams, Settings.Current.QuarryChams.Color);
            localPlayerChams = CloneMaterial(neutralChams, Color.magenta);
            Settings.Current.OnChamsDisabled += (o, e) => ClearChams();
            Settings.Current.OnLocalChamsDisabled += (o, e) => ClearLocalPlayerChams();
            Settings.Current.NeutralChams.OnColorChanged += (o, e) => neutralChams.SetColor("_Color", Settings.Current.NeutralChams.Color);
            Settings.Current.LocalChamsColor.OnColorChanged += (o, e) => localPlayerChams.SetColor("_Color", Settings.Current.LocalChamsColor.Color);
            Settings.Current.QuarryChams.OnColorChanged += (o, e) => quarryChams.SetColor("_Color", Settings.Current.QuarryChams.Color);
            Settings.Current.HunterChams.OnColorChanged += (o, e) => hunterChams.SetColor("_Color", Settings.Current.HunterChams.Color);
        }

        public void Update()
        {
            if (!HackManager.Instance.InGame)
            {
                return;
            }

            foreach (PlayerData playerInfo in HackManager.Instance.Players)
            {
                UpdateGlow(playerInfo);
            }
        }

        public void OnDestroy()
        {
            GameObject.Destroy(neutralChams);
            GameObject.Destroy(hunterChams);
            GameObject.Destroy(quarryChams);
            GameObject.Destroy(localPlayerChams);
        }

        private Material CreateNeutralMaterial()
        {
            Material material = new Material(glowShader)
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };
            // Good looking glow effect:
            material.SetInt("_SrcBlend", 5);
            material.SetInt("_DstBlend", 10);
            material.SetInt("_Cull", 0);
            material.SetInt("_ZTest", 8); // Render through walls.
            material.SetInt("_ZWrite", 0);
            material.SetColor("_Color", Settings.Current.NeutralChams.Color);

            return material;
        }

        private Material CloneMaterial(Material neutral, Color color)
        {
            Material material = new Material(neutral);
            material.SetColor("_Color", color);
            return material;
        }

        private void UpdateGlow(PlayerData playerInfo)
        {
            XPlayer player = playerInfo.Player;
            if (player.isLocalPlayer && Settings.Current.DrawLocalPlayerChams == false)
            {
                return;
            }

            Renderer[] allRenderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < allRenderers.Length; i++)
            {
                if (Settings.Current.ChamsEnabled)
                {
                    ApplyChams(playerInfo, allRenderers, i);
                }
            }
        }

        private void ApplyChams(PlayerData playerInfo, Renderer[] allRenderers, int i)
        {
            Renderer renderer = allRenderers[i];
            if (playerInfo.IsHunterForLocal)
            {
                ApplyMaterial(renderer, hunterChams);
            }
            else if (playerInfo.IsQuarryForLocal)
            {
                ApplyMaterial(renderer, quarryChams);
            }
            else if (playerInfo.IsLocalPlayer)
            {
                ApplyMaterial(renderer, localPlayerChams);
            }
            else
            {
                ApplyMaterial(renderer, neutralChams);
            }
        }

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

        private void ClearChams()
        {
            foreach (PlayerData playerData in HackManager.Instance.Players)
            {
                XPlayer player = playerData.Player;
                Skins.RestoreSkin(player);
            }
        }

        private void ClearLocalPlayerChams()
        {
            if (Settings.Current.ChamsEnabled)
            {
                XPlayer player = HackManager.Instance.LocalPlayer;
                Skins.RestoreSkin(player);
            }
        }
    }
}
