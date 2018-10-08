using PluginManager.Plugin;
using UnityEngine;

namespace BesiegeCustomScene
{
    /*
    public class BesiegeModLoader : spaar.ModLoader.Mod
    {
        public override string Name { get { return "BesiegeCustomScene"; } }
        public override string DisplayName { get { return "BesiegeCustomScene"; } }
        public override string BesiegeVersion { get { return "v0.42"; } }
        public override string Author { get { return "zian1"; } }
        public override Version Version { get { return new Version("10.1"); } }
        public override bool CanBeUnloaded { get { return true; } }
        public GameObject temp;
        public override void OnLoad()
        {         
            temp = new GameObject(); temp.name = "BesiegeCustomScene_v10";
            temp.AddComponent<SceneUI>();
            temp.AddComponent<TimeUI>();
            temp.AddComponent<MeshMod>();
            temp.AddComponent<TriggerMod>();
            temp.AddComponent<SnowMod>();
            temp.AddComponent<CloudMod>();
            temp.AddComponent<WaterMod>();
            temp.AddComponent<Prop>();
            UnityEngine.Object.DontDestroyOnLoad(temp);
        }
        public override void OnUnload()
        {
            UnityEngine.Object.Destroy(temp);
        }          
    }  
    //*/
    [OnGameInit]
    public class BesiegeModLoader : MonoBehaviour
    {
        public GameObject Scene;

        string DisplayName = "Besiege Custom Scene";

        string Version = "1.10.5";

        public BesiegeModLoader() { }

        private void Start()
        {
            Scene = new GameObject();
            Scene.name = string.Format("{0} {1}", DisplayName, Version);
            Scene.AddComponent<SceneUI>();
            Scene.AddComponent<TimeUI>();
            Scene.AddComponent<MeshMod>();
            Scene.AddComponent<TriggerMod>();
            Scene.AddComponent<SnowMod>();
            Scene.AddComponent<CloudMod>();
            Scene.AddComponent<WaterMod>();
            Scene.AddComponent<Prop>();
            UnityEngine.Object.DontDestroyOnLoad(Scene);
        }
    }
}
