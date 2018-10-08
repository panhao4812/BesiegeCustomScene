using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace BesiegeCustomScene
{
    class WaterMod : MonoBehaviour
    {

        void Start()
        {

        }
        void OnDisable()
        {
            ClearWater();
        }
        void OnDestroy()
        {
            ClearWater();
        }
        /// ////////////////////////      
        private GameObject[] Mwater;
        private int WaterSize = 0;
        public void ReadScene(string SceneName)
        {
            WaterSize = 0;
            try
            {
                //  GeoTools.Log(Application.dataPath);
                if (!File.Exists(GeoTools.ScenePath + SceneName + ".txt"))
                {
                    GeoTools.Log("Error! Scene File not exists!");
                    return;
                }
                StreamReader srd = File.OpenText(GeoTools.ScenePath + SceneName + ".txt");
                while (srd.Peek() != -1)
                {
                    string str = srd.ReadLine();
                    string[] chara = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (chara.Length > 2)
                    {
                        #region Water
                        if (chara[0] == "MWater" || chara[0] == "Mwater")
                        {
                            if (chara[1] == "size")
                            {
                                this.WaterSize = Convert.ToInt32(chara[2]);
                                LoadWater();
                            }
                            if (chara[1] == "watertemp")
                            {
                                if (Convert.ToInt32(chara[2]) == 0)
                                {
                                    gameObject.GetComponent<Prop>().WaterTemp.SetActive(false);
                                }
                                else
                                {
                                    gameObject.GetComponent<Prop>().WaterTemp.SetActive(true);
                                }
                            }
                        }
                        else if (chara[0] == "Water")
                        {
                            int i = Convert.ToInt32(chara[1]);
                            if (chara[2] == "mesh")
                            {
                                Mwater[i].GetComponent<MeshFilter>().mesh = GeoTools.MeshFromObj(chara[3]);
                            }
                            else if (chara[2] == "wmesh")
                            {
                                Mwater[i].GetComponent<MeshFilter>().mesh = GeoTools.WMeshFromObj(chara[3]);
                            }
                            else if (chara[2] == "scale")
                            {
                                Mwater[i].transform.localScale = new Vector3(
                               Convert.ToSingle(chara[3]),
                               Convert.ToSingle(chara[4]),
                               Convert.ToSingle(chara[5]));
                            }
                            else if (chara[2] == "location")
                            {
                                Mwater[i].transform.localPosition = new Vector3(
                               Convert.ToSingle(chara[3]),
                               Convert.ToSingle(chara[4]),
                               Convert.ToSingle(chara[5]));
                            }
                            else if (chara[2] == "meshcollider")
                            {
                                Mwater[i].GetComponent<MeshCollider>().sharedMesh = GeoTools.MeshFromObj(chara[3]);
                                Mwater[i].GetComponent<MeshCollider>().convex = true;
                                Mwater[i].GetComponent<MeshCollider>().isTrigger = true;
                            }
                            else if (chara[2] == "wmeshcollider")
                            {
                                Mwater[i].GetComponent<MeshCollider>().sharedMesh = GeoTools.WMeshFromObj(chara[3]);
                                Mwater[i].GetComponent<MeshCollider>().convex = true;
                                Mwater[i].GetComponent<MeshCollider>().isTrigger = true;
                            }
                        }
                        #endregion
                    }
                }
                srd.Close();
                GeoTools.Log("ReadWater Completed!");
            }
            catch (Exception ex)
            {
                GeoTools.Log("Error! ReadWater Failed!");
                GeoTools.Log(ex.ToString());
                return;
            }
        }
        public void LoadWater()
        {
            try
            {
                ClearWater();
                if (WaterSize <= 0) return;
                if (this.gameObject.GetComponent<Prop>().TileTemp == null) return;              
                Mwater = new GameObject[WaterSize];
                for (int i = 0; i < Mwater.Length; i++)
                {
                    Mwater[i] = Instantiate(gameObject.GetComponent<Prop>().TileTemp);
                    Mwater[i].name = "water" + i.ToString();
                    Mwater[i].SetActive(true);
                    Mwater[i].transform.localScale = new Vector3(1, 1, 1);
                    Mwater[i].transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                GeoTools.Log("LoadWater Failed");
                GeoTools.Log(ex.ToString());
            }
        }
        public void ClearWater()
        {

            ClearFloater();
            if (Mwater == null) return;
            if (Mwater.Length <= 0) return;
            if (WaterSize > 0) GeoTools.Log("ClearWater");
            gameObject.GetComponent<Prop>().WaterTemp.SetActive(false);
            try
            {
                Destroy(GameObject.Find("WaterTempReflectionMain Camera"));
            }
            catch { }
            for (int i = 0; i < Mwater.Length; i++)
            {
                Destroy(Mwater[i]);
            }
        }
        public void LoadFloater()
        {
            try
            {
                MyBlockInfo[] infoArray = UnityEngine.Object.FindObjectsOfType<MyBlockInfo>();
                foreach (MyBlockInfo info in infoArray)
                {
                    if (info.gameObject.GetComponent<Floater>() == null)
                    {
                        info.gameObject.AddComponent<Floater>();
                    }
                }
            }
            catch
            {
                GeoTools.Log("Error! LoadFloater Failed");
            }
        }
        public void ClearFloater()
        {
            try
            {
                bool sign = false;
                MyBlockInfo[] infoArray = UnityEngine.Object.FindObjectsOfType<MyBlockInfo>();
                foreach (MyBlockInfo info in infoArray)
                {
                    if (info.gameObject.GetComponent<Floater>() != null)
                    {
                        Destroy(info.gameObject.GetComponent<Floater>());
                        sign = true;
                    }
                }
                if (sign) GeoTools.Log("ClearFloater");
            }
            catch
            {
                GeoTools.Log("Error! ClearFloater Failed");
            }
        }
    }
}
