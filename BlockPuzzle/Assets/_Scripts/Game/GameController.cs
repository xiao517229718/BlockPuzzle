using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public class GameController : MonoBehaviour
    {
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
            ShapManager.currentIndex = 3;
            CreatShap.Creat(shaps, creatPos, out currentNotUseShap);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
