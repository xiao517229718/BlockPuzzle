using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    /// <summary>
    /// 单个物体射线检测获取位置
    /// </summary>
    public class ElemDetection : MonoBehaviour
    {
        private Vector3 outPoint = Vector3.zero;
        private void Awake()
        {
        }
        private void Update()
        {
            RayCast();
        }
        public void RayCast()
        {
            try
            {
                RaycastHit hitInfo = new RaycastHit();
                Physics.Raycast(transform.position, transform.forward, out hitInfo, 5);
                {
                    outPoint = hitInfo.point;
                }
            }
            catch { }

        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, outPoint);
        }
    }
}



