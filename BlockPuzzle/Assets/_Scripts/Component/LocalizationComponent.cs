using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class LocalizationComponent : MonoBehaviour
    {
        /// <summary>
        /// 游戏开始获取存储数据
        /// </summary>
        private void Awake()
        {
            GameInfoSaveManager.StartGetSaveInfo();
            MapHelper.InitMap();
        }
        /// <summary>
        /// 结束游戏 保存游戏数据
        /// </summary>
        private void OnDestroy()
        {
            GameInfoSaveManager.EndSetSaveInfo();
        }
        /// <summary>
        /// 保存地图信息
        /// </summary>
        /// <param name="map"></param>
        public void SaveMapInfo(List<List<MapSingleInfo>> map)
        {
            Map _map = new Map();
            _map._mapInfo = map;
            GameInfoSaveManager.SaveClassInfo<Map>("BlockPuzzleMap", _map);
        }
        /// <summary>
        /// 获取地图信息
        /// </summary>
        /// <returns></returns>
        public List<List<MapSingleInfo>> GetMapInfo()
        {
            Map map = GameInfoSaveManager.GetClassInfo<Map>("BlockPuzzleMap");
            return map._mapInfo;
        }
    }
}