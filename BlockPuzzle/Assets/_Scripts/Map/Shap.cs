using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class Shap : MonoBehaviour
    {
        /// <summary>
        /// 标记形状
        /// </summary>
        public int shapIndex = 0;

        public Transform target;

        public bool isMove = false;

        private void Update()
        {
            //if (isMove)
            //    MoveTarget();
        }


        void MoveTarget() 
        {
            if (target!=null) 
            {
                transform.position += Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 10f);
                //if (Vector3.Distance(transform.position, target.position) < 0.2f) 
                //{
                //    isMove = false;
                //}
            }
        }
             
    }
}

