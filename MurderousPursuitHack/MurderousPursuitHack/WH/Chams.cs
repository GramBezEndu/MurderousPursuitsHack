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
            Settings settings = Settings.Current;
            hunterChams = CloneMaterial(neutralChams, settings.HunterGlow.Color);
            quarryChams = CloneMaterial(neutralChams, settings.QuarryGlow.Color);
            localPlayerChams = CloneMaterial(neutralChams, settings.LocalGlow.Color);

            SubscribeToChamsSettingsChange();
        }

        private void SubscribeToChamsSettingsChange()
        {
            Settings settings = Settings.Current;
            settings.OnQuarryChamsDisabled += (o, e) => ClearQuarryChams();
            settings.OnLocalChamsDisabled += (o, e) => ClearLocalChams();
            settings.OnHunterChamsDisabled += (o, e) => ClearHunterChams();
            settings.OnNeutralChamsDisabled += (o, e) => ClearNeutralChams();

            settings.NeutralGlow.OnColorChanged += (o, e) => neutralChams.SetColor("_Color", settings.NeutralGlow.Color);
            settings.LocalGlow.OnColorChanged += (o, e) => localPlayerChams.SetColor("_Color", settings.LocalGlow.Color);
            settings.QuarryGlow.OnColorChanged += (o, e) => quarryChams.SetColor("_Color", settings.QuarryGlow.Color);
            settings.HunterGlow.OnColorChanged += (o, e) => hunterChams.SetColor("_Color", settings.HunterGlow.Color);
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
            material.SetColor("_Color", Settings.Current.NeutralGlow.Color);

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
            // TODO: Handle cases -> Quarry becoming neutral player and neutral chams disabled
            XPlayer player = playerInfo.Player;
            if (player.isLocalPlayer && Settings.Current.LocalChams == false)
            {
                return;
            }

            if (playerInfo.IsHunterForLocal && Settings.Current.HunterChams == false)
            {
                return;
            }

            if (playerInfo.IsQuarryForLocal && Settings.Current.QuarryChams == false)
            {
                return;
            }

            if (Settings.Current.NeutralChams == false && (!playerInfo.IsQuarryForLocal && !playerInfo.IsHunterForLocal))
            {
                return;
            }

            Renderer[] allRenderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < allRenderers.Length; i++)
            {
                ApplyChams(playerInfo, allRenderers, i);
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

        private void ClearQuarryChams()
        {
            foreach (PlayerData playerData in HackManager.Instance.Players)
            {
                XPlayer player = playerData.Player;
                if (playerData.IsQuarryForLocal)
                {
                    Skins.RestoreSkin(player);
                }
            }
        }

        private void ClearHunterChams()
        {
            foreach (PlayerData playerData in HackManager.Instance.Players)
            {
                XPlayer player = playerData.Player;
                if (playerData.IsHunterForLocal)
                {
                    Skins.RestoreSkin(player);
                }
            }
        }

        private void ClearNeutralChams()
        {
            foreach (PlayerData playerData in HackManager.Instance.Players)
            {
                XPlayer player = playerData.Player;
                if (!playerData.IsHunterForLocal && !playerData.IsHunterForLocal)
                {
                    Skins.RestoreSkin(player);
                }
            }
        }

        private void ClearLocalChams()
        {
            XPlayer player = HackManager.Instance.LocalPlayer;
            Skins.RestoreSkin(player);
        }
    }
}
