using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public class GameController : MonoBehaviour
    {
        public static bool pause = false;
        /// <summary>
        /// 形状生成点
        /// </summary>
        public List<Transform> creatTrans = new List<Transform>();
        private Transform shaps = null;
        /// <summary>
        /// 形状初始生成位置
        /// </summary>
        private List<Vector3> creatPos = new List<Vector3>();
        /// <summary>
        /// 当前还没有使用的形状 生成和放置了一个只后需要判断一下里面的形状是否还开放置
        /// </summary>
        private List<List<List<int>>> currentNotUseShap = new List<List<List<int>>>();
        void Start()
        {
            shaps = GameObject.Find("Shaps").transform;
            for (int i = 0; i < creatTrans.Count; i++)
            {
                creatPos.Add(creatTrans[i].position);
            }
            StartGame();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 判断是否移动 
        /// </summary>
        public static bool JudgeMove(GameObject detection, GameObject elem)
        {

            DetectionPos dp = detection.GetComponent<DetectionPos>();
            int posx = dp.pos_x;
            int posy = dp.pos_y;
            bool move = MapController.Fill(posx, posy, elem);
            return move;
        }
        /// <summary>
        /// 移动完成后 检测是否需要消除
        /// </summary>
        /// <returns></returns>
        public static void Check()
        {
            MapController.Check();
        }
        public void StartGame()
        {
            //ShapManager.currentIndex = 3;
            CreatShap.Creat(shaps, creatPos, out currentNotUseShap);
        }
    }
}
