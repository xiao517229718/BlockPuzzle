using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class CreatShap
    {
        /// <summary>
        /// 生成一个形状
        /// </summary>
        /// <param name="shapIndex">形状索引</param>
        /// <param name="shapParent">生成的形状外部父节点</param>
        /// <param name="shap">形状数组</param>
        /// <returns></returns>
        public static GameObject Creat(int shapIndex, Transform shapParent, out List<List<int>> shap)
        {
            shap = ShapList.GetShapList(shapIndex);
            GameObject singleParent = new GameObject();
            singleParent.name = "singleParent";
            Rigidbody rb = singleParent.AddComponent<Rigidbody>();
            rb.useGravity = false;
            Transform shapPosTemp = GameObject.Find("shapPosTemp").transform;
            List<Transform> allElem = new List<Transform>();
            float Pos_x = shap[0].Count;
            float Pos_y = shap.Count;
            for (int i = 0; i < shap.Count; i++)
            {
                for (int j = 0; j < shap[i].Count; j++)
                {
                    if (shap[i][j] == 1)
                    {
                        GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "BlockCube")) as GameObject;
                        go.transform.SetParent(singleParent.transform);
                        go.transform.localPosition = new Vector3(j * MapHelper.interval, -i * MapHelper.interval, -0.5f);
                        go.transform.SetParent(shapPosTemp);
                        go.name = i.ToString() + j.ToString();
                        allElem.Add(go.transform);
                        //  GameObject singleParent = new GameObject();
                    }
                }
            }
            singleParent.transform.position = new Vector3(Pos_x / 2, Pos_y / 2, 0f);
            for (int i = 0; i < allElem.Count; i++)
            {
                allElem[i].SetParent(singleParent.transform);
            }
            singleParent.transform.SetParent(shapParent);

            return singleParent;
        }

        /// <summary>
        /// 生成一组形状（三个）
        /// </summary>
        /// <param name="creatPos">生成的位置</param>
        /// <param name="shap">每个形状对应的数组</param>
        public static void Create(Transform shapParent, List<Vector3> creatPos,List<Transform> targetPos, out List<int> shap)
        {
            List<List<List<int>>> allCurrentCreatShaps = new List<List<List<int>>>();
            List<int> currentShaps = ShapManager.GetQuestionIndex();
            Transform shapPosTemp = GameObject.Find("shapPosTemp").transform;
            for (int i = 0; i < currentShaps.Count; i++)
            {
                List<List<int>> singleShap = new List<List<int>>();
                singleShap = ShapList.GetShapList(currentShaps[i]);
                allCurrentCreatShaps.Add(singleShap);
                GameObject singleParent = new GameObject();
                singleParent.name = "singleParent" + "_" + currentShaps[i].ToString();
                //singleParent.transform.localScale = Vector3.one;
                //Rigidbody rb = singleParent.AddComponent<Rigidbody>();
                Shap shapIndex = singleParent.AddComponent<Shap>();
                shapIndex.shapIndex = currentShaps[i];
                shapIndex.target = targetPos[i];
               // rb.useGravity = false;

                List<Transform> allElem = new List<Transform>();
                float Pos_x = singleShap[0].Count;
                float Pos_y = singleShap.Count;

                for (int j = 0; j < singleShap.Count; j++)
                {
                    for (int k = 0; k < singleShap[j].Count; k++)
                    {
                        if (singleShap[j][k] == 1)
                        {
                            GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "BlockCube")) as GameObject;
                            go.transform.SetParent(singleParent.transform);
                            go.transform.localPosition = new Vector3((k * MapHelper.interval) + 0.5f * MapHelper.interval, (j * MapHelper.interval) + 0.5f * MapHelper.interval, -0f);
                            go.transform.SetParent(shapPosTemp);
                            go.name = j.ToString() + k.ToString();
                            allElem.Add(go.transform);
                        }
                    }
                }
                singleParent.transform.position = new Vector3((Pos_x * MapHelper.interval) / 2, (Pos_y * MapHelper.interval) / 2, 0f);
                for (int j = 0; j < allElem.Count; j++)
                {
                    allElem[j].SetParent(singleParent.transform);
                }
                singleParent.transform.SetParent(shapParent);
                singleParent.transform.position = creatPos[i];
                BoxCollider collider = singleParent.AddComponent<BoxCollider>();
                // collider.center = new Vector3(0, (Pos_y * MapHelper.interval) / 2, 0);
                collider.size = new Vector3(Pos_x * MapHelper.interval, Pos_y * MapHelper.interval, MapHelper.interval);
                shapIndex.isMove = true;
                //rb.useGravity = true;
                //   singleParent.AddComponent(typeof(Shap));

                //  UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(singleParent, "添加力", "Force");
                //shapIndex.shapIndex = singleShap;
            }
            shap = currentShaps;
        }
    }
}