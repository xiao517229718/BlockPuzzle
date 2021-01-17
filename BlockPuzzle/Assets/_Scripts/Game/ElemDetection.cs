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
        private GameObject target;
        private Vector3 outPoint = Vector3.zero;
        private void Awake()
        {
        }
        private void Update()
        {

        }
        public bool RayCast()
        {

            RaycastHit hitInfo = new RaycastHit();
            Physics.Raycast(transform.position, transform.forward, out hitInfo, 5);
            {
                outPoint = hitInfo.point;
                if (hitInfo.collider == null)
                {
                    return false;
                }
                if (hitInfo.collider.gameObject.tag == "Detection")//获取到对象了 
                {

                    target = hitInfo.collider.gameObject;
                    if (GameController.JudgeMove(hitInfo.collider.gameObject, gameObject))
                    { //如果没有填充
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }


        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, outPoint);
        }
        /// <summary>
        /// 鼠标抬起的时候调用
        /// </summary>
        public bool MouseUp()
        {
            return RayCast();
        }
        /// <summary>
        /// 当前可以移动
        /// </summary>
        public void Move()
        {

            GameController.Instance.currentDrage = transform.parent.GetComponent<Shap>().shapIndex;

            transform.SetParent(transform.parent.parent);
            transform.DOMove(target.transform.position, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            });
        }
    }
}



