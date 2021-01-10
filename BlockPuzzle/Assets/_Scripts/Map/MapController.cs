using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BlockPuzzle
{
    [System.Serializable]
    public class IntMapInfo
    {
        public List<List<int>> allInfo = new List<List<int>>();
        public List<int> lineOne = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineTwo = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineThree = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineFour = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineFive = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineSix = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineSeven = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> lineEight = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        public IntMapInfo()
        {
            allInfo.Add(lineOne);
            allInfo.Add(lineTwo);
            allInfo.Add(lineThree);
            allInfo.Add(lineFour);
            allInfo.Add(lineFive);
            allInfo.Add(lineSix);
            allInfo.Add(lineSeven);
            allInfo.Add(lineEight);
        }
    }
    public class MapController : MonoBehaviour
    {
        [Tooltip("左上角起始位置")]
        public Transform leftUpStartPos = null;
        private Transform DetectionParent = null;
        [Tooltip("上下左右间隔")]
        private float m_Spece = 0.2f;
        private int m_xSize = 8;
        private int m_ySize = 8;
        private Vector3 m_InitPos = Vector3.zero;
        public IntMapInfo intMapInfo;
        [HideInInspector]
        public int shapIndex = 0;
        private Transform shaps = null;
        private static Dictionary<string, GameObject> current = new Dictionary<string, GameObject>();
        private
        void Start()
        {
            shaps = GameObject.Find("Shaps").transform;
            leftUpStartPos = GameObject.Find("ShapDetection").transform;
            DetectionParent = GameObject.Find("Detections").transform;
            Debug.Log(leftUpStartPos != null);
            if (leftUpStartPos != null)
            {
                m_xSize = MapHelper.HorizontalCount;
                m_ySize = MapHelper.virticalCount;
                m_Spece = MapHelper.interval;
                m_InitPos = leftUpStartPos.localPosition;
                InitPos();
                MapHelper.InitMap();
                CreatDefault();
            }

        }
        void CreatDefault()
        {
            for (int i = 0; i < MapHelper._mapInfo.Count; i++)
            {
                for (int j = 0; j < MapHelper._mapInfo[i].Count; j++)
                {
                    if (MapHelper._mapInfo[i][j].isFill == 1)
                    {
                        GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "SingekBlock")) as GameObject;
                        go.transform.SetParent(shaps);
                        go.transform.localPosition = new Vector3(float.Parse(MapHelper._mapInfo[i][j].PosX), float.Parse(MapHelper._mapInfo[i][j].Posy), 0);
                        go.name = i.ToString() + j.ToString();
                        MapHelper.objcts[i, j] = go;
                    }
                }
            }

        }
        void Update()
        {

        }
        private void OnDestroy()
        {
            SaveMapInfo();
        }
        /// <summary>
        /// 获取地图信息
        /// </summary>
        /// <returns></returns>
        public List<List<MapSingleInfo>> GetMapInfo()
        {
            if (MapHelper._mapInfo.Count == 0)
            {
                Debug.LogError("目前还没有生成地图 请先生成地图");
                return null;
            }
            return MapHelper._mapInfo;
        }
        /// <summary>
        /// 保存地图信息
        /// </summary>
        public void SaveMapInfo()
        {
            MapHelper.SaveMap();
        }

        /// <summary>
        /// 检测是否有消除的
        /// </summary>
        public static void Check()
        {

            int lineCount = MapHelper._mapInfo[0].Count;

            for (int i = 0; i < MapHelper._mapInfo.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < MapHelper._mapInfo[i].Count; j++)
                {
                    if (MapHelper._mapInfo[i][j].isFill == 0)
                    {
                        break;
                    }
                    else
                    {
                        count++;
                        if (count == MapHelper._mapInfo[i].Count)
                        {
                            ClearnLine(i, true);
                            List<GameObject> lineObjs = new List<GameObject>();
                            for (int h = 0; h < lineCount; h++)
                            {
                                lineObjs.Add(MapHelper.objcts[i, h]);
                                MapHelper.objcts[i, h] = null;
                            }
                            for (int h = 0; h < lineObjs.Count; h++)
                            {
                                DownElem(lineObjs[h]);
                            }

                        }
                    }
                }
            }
            for (int i = 0; i < MapHelper._mapInfo[0].Count; i++)
            {
                int verCount = 0;
                for (int j = 0; j < MapHelper._mapInfo.Count; j++)
                {
                    if (MapHelper._mapInfo[j][i].isFill == 0)
                    {
                        break;
                    }
                    else
                    {
                        verCount++;
                        if (verCount == MapHelper._mapInfo[i].Count)
                        {
                            ClearnLine(i, false);
                            List<GameObject> lineObjs = new List<GameObject>();
                            for (int h = 0; h < lineCount; h++)
                            {
                                lineObjs.Add(MapHelper.objcts[h, i]);
                                MapHelper.objcts[h, i] = null;
                            }
                            for (int h = 0; h < lineObjs.Count; h++)
                            {
                                DownElem(lineObjs[h]);
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置掉下
        /// </summary>
        /// <param name="go"></param>
        public static void DownElem(GameObject go)
        {
            go.AddComponent<Rigidbody>();
        }

        /// <summary>
        /// 当前拖拽物体是否应该填充
        /// </summary>
        /// <param name="fill"></param>
        public static void FillOrNot(bool fill)
        {
            if (fill)
            {
                foreach (var item in current)
                {
                    string[] elem = item.Key.Split('_');
                    int x = int.Parse(elem[0]);
                    int y = int.Parse(elem[1]);
                    MapHelper._mapInfo[x][y].isFill = 1;
                    MapHelper.objcts[x, y] = item.Value;
                }
            }
            else
            {
            }
            current.Clear();
        }

        /// <summary>
        /// 填充某个位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true  填充成功  false 填充失败</returns>
        public static bool Fill(int x, int y, GameObject go)
        {
            bool fill = false;
            if (MapHelper._mapInfo[x][y].isFill == 0)
            {
                string key = x.ToString() + '_' + y.ToString();
                if (!current.ContainsKey(key))
                    current.Add(key, go);
                fill = true;
            }
            else
            {
            }
            return fill;
        }
        /// <summary>
        /// 清除某一行
        /// </summary>
        /// <param name="x"></param>
        public static void ClearnLine(int x, bool lie)
        {
            Debug.LogError("满行？？");
            if (lie)
            {

                for (int i = 0; i < MapHelper._mapInfo[x].Count; i++)
                {
                    MapHelper._mapInfo[x][i].isFill = 0;
                }
            }
            else
            {

                for (int i = 0; i < MapHelper._mapInfo[x].Count; i++)
                {
                    MapHelper._mapInfo[i][x].isFill = 0;
                }
            }


        }
        /// <summary>
        /// 某个位置是否被填充
        /// </summary>
        /// <returns></returns>
        public bool isFilled(int x, int y)
        {
            bool isfill = false;

            return isfill;
        }
        /// <summary>
        /// 某个形状在地图中是否还有位置可放置
        /// </summary>
        /// <param name="shap"></param>
        /// <returns></returns>
        public bool havePos(int shap)
        {
            bool havepos = false;

            return havepos;
        }
        #region 内部函数
        /// <summary>
        /// 初始化单项检测的位置 
        /// </summary>
        private void InitPos()
        {
            List<List<Vector2>> mapPoses = new List<List<Vector2>>();
            for (int i = 0; i < m_xSize; i++)
            {
                List<Vector2> poses = new List<Vector2>();
                for (int j = 0; j < m_ySize; j++)
                {
                    poses.Add(new Vector2(m_InitPos.x + i * 0.2f, m_InitPos.y + j * 0.2f));
                    GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "ShapDetection"), DetectionParent) as GameObject;
                    DetectionPos dp = go.GetComponent<DetectionPos>();
                    dp.pos_x = j;
                    dp.pos_y = i;
                    //Text tex = go.transform.GetComponentInChildren<Text>();
                    //tex.text = ToString(i) + ToString(j);
                    go.transform.localPosition = new Vector3(m_InitPos.x - j * 0.2f, m_InitPos.y - i * 0.2f, 0.1f);
                }
                mapPoses.Add(poses);
            }
            MapHelper.SetElePoses(mapPoses);
        }
#if UNITY_EDITOR
        /// <summary>
        /// 编辑器保存地图信息
        /// </summary>
        public void InspectorSaveMapInfo()
        {
            MapHelper.SaveMap(intMapInfo.allInfo);
        }
        /// <summary>
        /// 地图全部填充
        /// </summary>
        public void AllToOneFill(int value)
        {
            for (int i = 0; i < intMapInfo.allInfo.Count; i++)
            {
                for (int j = 0; j < intMapInfo.allInfo[i].Count; j++)
                {
                    intMapInfo.allInfo[i][j] = value;
                }
            }
            InspectorSaveMapInfo();
            Debug.Log("设置成功");
        }
#endif
        #endregion
    }
}