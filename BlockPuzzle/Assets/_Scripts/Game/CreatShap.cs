using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class CreatShap
    {
        public static List<List<int>> Creat(int shapIndex, Transform shapParent)
        {
            List<List<int>> shapArray = ShapList.GetShapList(shapIndex);
            GameObject singleParent = new GameObject();
            singleParent.name = "singleParent";
            Transform shapPosTemp = GameObject.Find("shapPosTemp").transform;
            List<Transform> allElem = new List<Transform>();
            float Pos_x = shapArray[0].Count;
            float Pos_y = shapArray.Count;
            for (int i = 0; i < shapArray.Count; i++)
            {
                for (int j = 0; j < shapArray[i].Count; j++)
                {
                    if (shapArray[i][j] == 1)
                    {


                        GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "SingekBlock")) as GameObject;
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
                //ElemDetection ed = allElem[i].GetComponent<ElemDetection>();
                //ed.RayCast();
            }
            singleParent.transform.SetParent(shapParent);
            return shapArray;
        }
    }
}