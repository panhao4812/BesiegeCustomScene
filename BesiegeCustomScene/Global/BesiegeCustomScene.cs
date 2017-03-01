using spaar;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace BesiegeCustomScene
{

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
}
