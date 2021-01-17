using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Text;
namespace BlockPuzzle
{
    public class MapHelper
    {
        /// <summary>
        /// 地图横向值
        /// </summary>
        public static readonly int HorizontalCount = 8;
        /// <summary>
        /// 地图竖向值
        /// </summary>
        public static readonly int virticalCount = 8;
        /// <summary>
        /// 检测体生产间隔
        /// </summary>
        public static readonly float interval = 0.19f;
        /// <summary>
        /// 游戏运行时 地图信息
        /// </summary>
        public static List<List<MapSingleInfo>> _mapInfo = new List<List<MapSingleInfo>>();
        public static List<List<Vector2>> _worldPos = new List<List<Vector2>>();
        /// <summary>
        /// 存放游戏物体 用于消除
        /// </summary>
        public static GameObject[,] objcts = new GameObject[8, 8];
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitMap()
        {
            try
            {
                ShapManager.currentIndex = int.Parse(GameInfoSaveManager.GetAttributeInfo("currentIndex"));
            }
            catch
            {
                ShapManager.currentIndex = 0;
            }
            Debug.Log("获取生成进度");
            Map map = GameInfoSaveManager.GetClassInfo<Map>("BlockPuzzleMap");
            if (map == null)
            {

                SetDefaultMap();
                Debug.Log("设置默认地图信息");
            }
            else
            {
                _mapInfo = map._mapInfo;
                Debug.Log("读取已储存的地图信息");
            }

        }
        /// <summary>
        /// 获取保存到本地持久的地图
        /// </summary>
        /// <returns></returns>
        public static List<List<MapSingleInfo>> GetMap()
        {
            InitMap();
            return _mapInfo;
        }

        /// <summary>
        /// 保存地图信息到本地持久
        /// </summary>
        public static void SaveMap()
        {
            if (_mapInfo.Count == 0) return;
            Map map = new Map();
            map._mapInfo = _mapInfo;
            GameInfoSaveManager.SaveClassInfo<Map>("BlockPuzzleMap", map);
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="mapInfo"></param>
        public static void SaveMap(List<List<int>> mapInfo)
        {
            GameInfoSaveManager.StartGetSaveInfo();
            if (mapInfo.Count != virticalCount)
            {
                Debug.LogWarning("地图高度不正确");
                return;
            }
            if (mapInfo[0].Count != HorizontalCount)
            {
                Debug.LogWarning("地图宽度不正确");
                return;
            }
            if (_mapInfo.Count == 0) GetMap();//获取位置信息 x y 坐标
            List<List<MapSingleInfo>> InspectormapInfo = new List<List<MapSingleInfo>>();
            for (int i = 0; i < mapInfo.Count; i++)
            {
                List<MapSingleInfo> temp = new List<MapSingleInfo>();
                for (int j = 0; j < mapInfo[i].Count; j++)
                {
                    MapSingleInfo elem = SetElemInfo(_mapInfo[i][j].PosX, _mapInfo[i][j].Posy.ToString(), ColorType.ColorWood, i, j, mapInfo[i][j]);
                    temp.Add(elem);
                }
                InspectormapInfo.Add(temp);
            }
            Map map = new Map();
            map._mapInfo = InspectormapInfo;
            _mapInfo = InspectormapInfo;
            string currentIndex = ShapManager.currentIndex.ToString();
            GameInfoSaveManager.SaveAttributeInfo("currentIndex", currentIndex);
            GameInfoSaveManager.SaveClassInfo<Map>("BlockPuzzleMap", map);
            GameInfoSaveManager.EndSetSaveInfo();
        }
        /// <summary>
        /// 鼠标松手 能否放置到地图上
        /// </summary>
        /// <returns></returns>
        public static bool CurrentCanPut()
        {
            bool canput = false;
            return canput;
        }
        /// <summary>
        /// 传入方块类型 判断地图中是否还可以放置
        /// </summary>
        /// <param name="elemType">传入的形状</param>
        /// <returns></returns>

        public static bool HasSuitablePos(List<List<int>> elemMap)
        { //tode
          //传入形状的行数
            List<List<int>> newelem = new List<List<int>>();
            for (int i = elemMap.Count - 1; i >= 0; i--)
            {

                List<int> newe = new List<int>();
                for (int j = 0; j < elemMap[i].Count; j++)
                {
                    newe.Add(elemMap[i][j]);
                }
                newelem.Add(newe);
            }
            elemMap = newelem;

            List<List<int>> objs = new List<List<int>>();

            for (int i = 0; i < objcts.GetLength(0); i++)
            {
                List<int> ele = new List<int>();
                for (int j = 0; j < objcts.GetLength(1); j++)
                {
                    if (objcts[i, j] != null)
                    {
                        ele.Add(1);

                    }
                    else
                        ele.Add(0);
                }
                objs.Add(ele);
            }


            int verticalCount = elemMap.Count;
            //传入形状的列数
            int horizontalCount = elemMap[0].Count;
            for (int i = 0; i < _mapInfo.Count; i++)
            {
                for (int j = 0; j < _mapInfo[i].Count; j++)
                {
                    if (_mapInfo[i][j].isFill == 0)
                    {//获取一个没有被填充的元素 从此处截取一个传入的形状 
                        List<List<int>> temp = new List<List<int>>();
                        for (int l = 0; l < verticalCount; l++)
                        {
                            List<int> horielem = new List<int>();
                            for (int k = 0; k < horizontalCount; k++)
                            {
                                if ((i + l) > 7 || (j + k) > 7)
                                {
                                    break;
                                }
                                horielem.Add(_mapInfo[i + l][j + k].isFill);
                            }
                            temp.Add(horielem);
                        }
                        if (IsSuitable(elemMap, temp)) //可以放置
                        {

                            for (int h = 0; h < elemMap.Count; h++)
                            {
                                for (int k = 0; k < elemMap[h].Count; k++)
                                {
                                    //Debug.LogError(JsonMapper.ToJson(elemMap.ToArray()) + "   elemMap");
                                    //Debug.LogError(JsonMapper.ToJson(temp.ToArray()) + "  temp");
                                }
                            }
                            return true;
                        }
                        else
                        {//不可放置

                            //Debug.LogError($"i:{i} j:{j}");
                            string elemS = JsonMapper.ToJson(elemMap.ToArray());
                            string tempS = JsonMapper.ToJson(_mapInfo.ToArray());
                            continue;
                        }
                    }
                }

            }
            return false;
        }
        /// <summary>
        /// 设置地图所有单项的本地坐标
        /// </summary>
        public static void SetElePoses(List<List<Vector2>> localPoses)
        {
            _worldPos = localPoses;
        }

        #region 内部函数
        private static bool IsSuitable(List<List<int>> shap, List<List<int>> tem)
        {

            //bool suitable = true;
            if (shap.Count != tem.Count)
            {
                return false;
            }
            for (int i = 0; i < shap.Count; i++)
            {
                if (shap[i].Count != tem[i].Count)
                {
                    return false;
                }
            }
            for (int i = 0; i < shap.Count; i++)
            {
                for (int j = 0; j < shap[i].Count; j++)
                {
                    if (shap[i][j] == 1)
                    {
                        if (tem[i][j] == 1) //对应的位置 已经被占据
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 设置默认地图 全是空
        /// </summary>
        private static List<List<MapSingleInfo>> SetDefaultMap()
        {
            if (_worldPos.Count == 0)
            {
                Debug.LogWarning("初始化地图位置失败 运行挂载Mapcontroller脚本的场景也许可以解决此问题");
                return null;
            }

            for (int i = 0; i < virticalCount; i++)
            {
                List<MapSingleInfo> horizontalMap = new List<MapSingleInfo>();
                for (int j = 0; j < HorizontalCount; j++)
                {
                    Debug.Log("save：：" + _worldPos[i][j].x.ToString() + _worldPos[i][j].y.ToString());
                    MapSingleInfo elem = SetElemInfo(_worldPos[i][j].x.ToString(), _worldPos[i][j].y.ToString(), ColorType.ColorWood, i, j, 0);
                    horizontalMap.Add(elem);
                }
                _mapInfo.Add(horizontalMap);
            }
            return _mapInfo;
        }
        /// <summary>
        /// json 不能存方法 不能用构造函数
        /// </summary>
        /// <param name="PosX">世界坐标x</param>
        /// <param name="PosY">世界左边y</param>
        /// <param name="colorType">颜色类型</param>
        /// <param name="mapIndex">地图索引 x</param>
        /// <param name="mapIndey">地图索引 y</param>
        /// <returns></returns>
        private static MapSingleInfo SetElemInfo(string PosX, string PosY, ColorType colorType, int mapIndex, int mapIndey, int isfill)
        {
            MapSingleInfo elem = new MapSingleInfo();
            elem.PosX = PosX;
            elem.Posy = PosY;
            elem.colorType = colorType;
            elem.mapIndex = mapIndex;
            elem.mapIndey = mapIndey;
            elem.isFill = isfill;
            return elem;
        }
        #endregion
    }
}