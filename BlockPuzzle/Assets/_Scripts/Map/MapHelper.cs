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
        /// 游戏运行时 地图信息
        /// </summary>
        public static List<List<MapSingleInfo>> _mapInfo = new List<List<MapSingleInfo>>();
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitMap()
        {
            GameInfoSaveManager.StartGetSaveInfo();
            Map map = GameInfoSaveManager.GetClassInfo<Map>("BlockPuzzleMap");
            if (map == null)
            {
                SetDefaultMap();
            }
            else
                _mapInfo = map._mapInfo;
        }
        /// <summary>
        /// 保存地图信息
        /// </summary>
        public static void SaveMap()
        {
            Map map = new Map();
            map._mapInfo = _mapInfo;
            GameInfoSaveManager.SaveClassInfo<Map>("BlockPuzzleMap", map);
        }
        /// <summary>
        /// 传入方块类型 判断地图中是否还可以放置
        /// </summary>
        /// <param name="elemType">传入的形状</param>
        /// <returns></returns>
        public static bool HasSuitable(List<List<MapSingleInfo>> elemMap)
        { //tode
            return false;
        }
        /// <summary>
        /// 设置默认地图 全是空
        /// </summary>
        private static void SetDefaultMap()
        {

        }
    }
}