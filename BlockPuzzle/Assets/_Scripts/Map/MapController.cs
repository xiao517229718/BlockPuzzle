using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            }

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.LogError($"开始计算时间{Time.realtimeSinceStartup}");
                bool canPut = MapHelper.HasSuitablePos(ShapList.GetShapList(shapIndex));
                Debug.LogError($"结束计算时间{Time.realtimeSinceStartup}");
                if (canPut) Debug.LogWarning("能放置");
                else Debug.LogWarning("不能放置");
            }
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
                    poses.Add(new Vector2(m_InitPos.x + i * m_Spece, m_InitPos.y + j * m_Spece));
                    GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "ShapDetection"), DetectionParent) as GameObject;
                    DetectionPos dp = go.GetComponent<DetectionPos>();
                    dp.pos_x = j;
                    dp.pos_y = i;
                    go.transform.localPosition = new Vector3(m_InitPos.x - j * m_Spece, m_InitPos.y - i * m_Spece, 0.1f);
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
        }
#endif
        #endregion
    }
}