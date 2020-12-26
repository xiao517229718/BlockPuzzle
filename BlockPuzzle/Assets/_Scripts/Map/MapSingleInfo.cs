using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlockPuzzle
{
    public enum ColorType
    {
        /// <summary>
        /// 木头色
        /// </summary>
        ColorWood,
        /// <summary>
        /// 红色
        /// </summary>
        ColorRed,
    }
    [System.Serializable]
    public class MapSingleInfo
    {
        /// <summary>
        /// json 不i能存储vector3 所以 用字符串
        /// </summary>
        public string PosX = string.Empty;
        public string Posy = string.Empty;
        public ColorType colorType = ColorType.ColorWood;
        /// <summary>
        /// 地图索引行  从左上角开始计算 
        /// </summary>
        public int mapIndex = 0;
        /// <summary>
        /// 地图索引列 从左上角开始计算 
        /// </summary>
        public int mapIndey = 0;
        /// <summary>
        /// 是否被填充 0 没有填充 1 被填充
        /// </summary>
        public int isFill = 0;
    }
}