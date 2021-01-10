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
        private static List<int> currentNotUseShap = new List<int>();
        /// <summary>
        /// 当前拖拽的形状
        /// </summary>
        public static int currentDrage = 0;
        private static bool creatNext = false;
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.gameObject.AddComponent<Shap>();
            }
            if (creatNext)
            {
                StartGame();
                creatNext = false;
            }
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
            if (move)
            {
                dp.InPosObj = elem;
            }
            return move;
        }
        /// <summary>
        /// 移动完成后 检测是否需要消除
        /// </summary>
        /// <returns></returns>
        public static void Check(int shapIndex)
        {
            UseShap(shapIndex);
            MapController.Check();
        }
        public void StartGame()
        {
            //ShapManager.currentIndex = 3;
            CreatShap.Creat(shaps, creatPos, out currentNotUseShap);
        }
        /// <summary>
        /// 使用了一个形状 1：移除一个形状 2  判断剩下的是能满足填充
        /// </summary>
        /// <param name="shapIndex"></param>
        public static void UseShap(int shapIndex)
        {
            if (currentNotUseShap.Contains(shapIndex))
            {
                currentNotUseShap.Remove(shapIndex);
                resShapCheck();
            }
            else
            {

            }
        }
        private static void resShapCheck()
        {
            if (currentNotUseShap.Count == 0)
            {
                creatNext = true;
                return;
            }
            else
            {
                bool haveSeat = false;
                for (int i = 0; i < currentNotUseShap.Count; i++)
                {
                    List<List<int>> shapArry = ShapList.GetShapList(currentNotUseShap[i]);
                    bool isgameOver = MapHelper.HasSuitablePos(shapArry);
                    if (isgameOver)
                    { //还能放置
                        haveSeat = isgameOver;
                    }
                    else
                    {

                    }
                }
                if (haveSeat == true)
                {
                    Debug.LogError("还能游戏");

                }
                else
                {
                    Debug.LogError("游戏结束");
                }
            }
        }
    }
}
