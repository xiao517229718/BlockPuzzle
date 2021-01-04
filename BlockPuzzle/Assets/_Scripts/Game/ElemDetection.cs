using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

        }
        public void RayCast()
        {
            try
            {
                RaycastHit hitInfo = new RaycastHit();
                Physics.Raycast(transform.position, transform.forward, out hitInfo, 5);
                {
                    outPoint = hitInfo.point;
                    if (hitInfo.collider.gameObject.tag == "Detection")//获取到对象了 
                    {

                        GameObject target = hitInfo.collider.gameObject;
                        if (GameController.JudgeMove(hitInfo.collider.gameObject, gameObject))
                        { //如果没有填充
                            transform.SetParent(transform.parent.parent);
                            transform.DOMove(target.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
{
    GameController.Check();
});
                        };
                    }
                }
            }
            catch { }

        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, outPoint);
        }
        /// <summary>
        /// 鼠标抬起的时候调用
        /// </summary>
        public void MouseUp()
        {
            RayCast();
        }
    }
}



