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
        public static readonly float interval = 0.2f;
        /// <summary>
        /// 游戏运行时 地图信息
        /// </summary>
        public static List<List<MapSingleInfo>> _mapInfo = new List<List<MapSingleInfo>>();
        public static List<List<Vector2>> _worldPos = new List<List<Vector2>>();
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitMap()
        {
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
            if (_mapInfo.Count == 0) GetMap();
            List<List<MapSingleInfo>> InspectormapInfo = new List<List<MapSingleInfo>>();
            for (int i = 0; i < mapInfo.Count; i++)
            {
                List<MapSingleInfo> temp = new List<MapSingleInfo>();
                for (int j = 0; j < mapInfo[0].Count; j++)
                {
                    Debug.Log(mapInfo[i][j]);
                    MapSingleInfo elem = SetElemInfo(_mapInfo[i][j].PosX, _mapInfo[i][j].Posy.ToString(), ColorType.ColorWood, i, j, mapInfo[i][j]);
                    temp.Add(elem);
                }
                InspectormapInfo.Add(temp);
            }
            Map map = new Map();
            map._mapInfo = InspectormapInfo;
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
            int verticalCount = elemMap.Count;
            //传入形状的列数
            int horizontalCount = elemMap[0].Count;
            Debug.Log($"宽度 ：{horizontalCount} 高度 :{verticalCount}");
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
                                horielem.Add(_mapInfo[i + l][j + k].isFill);
                            }
                            temp.Add(horielem);
                        }
                        if (IsSuitable(elemMap, temp)) //可以放置
                        {
                            return true;
                        }
                        else
                        {//不可放置
                            continue;
                        }
                        //#region 打印日志
                        //for (int m = 0; m < temp.Count; m++)
                        //{
                        //    for (int n = 0; n < temp[m].Count; m++)
                        //    {
                        //        Debug.LogError($"{temp[m][n]} 获取的临时表  m:{m} n:{n}");
                        //    }
                        //}

                        //#endregion
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
            bool suitable = true;
            if (shap.Count != tem.Count)
            {
                Debug.LogWarning("高度不一样");
                suitable = false;
                return suitable;
            }
            if (shap[0].Count != tem[0].Count)
            {
                Debug.LogWarning("宽度不一样");
                suitable = false;
                return suitable;
            }
            for (int i = 0; i < shap.Count; i++)
            {
                for (int j = 0; j < shap[i].Count; j++)
                {
                    if (shap[i][j] == 1)
                    {
                        if (tem[i][j] == 1) //对应的位置 已经被占据
                        {
                            suitable = false;
                            return suitable;
                        }
                    }
                }
            }
            return suitable;
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
                    MapSingleInfo elem = SetElemInfo(_worldPos[i][j].x.ToString(), _worldPos[i][j].y.ToString(), ColorType.ColorWood, i, j, 1);
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