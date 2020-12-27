using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public class DetectionPos : MonoBehaviour
    {
        /// <summary>
        /// 放在检测位置的游戏物体
        /// </summary>
        public GameObject InPosObj = null;
        /// <summary>
        /// 在各格子中的坐标x
        /// </summary>
        public int pos_x;
        /// <summary>
        /// 在各格子中的坐标y
        /// </summary>
        public int pos_y;
    }
}