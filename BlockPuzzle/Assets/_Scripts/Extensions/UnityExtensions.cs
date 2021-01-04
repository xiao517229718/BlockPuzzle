using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public static class UnityExtensions
    {
        public static Transform[] GetAllChildTrans(this Transform trans)
        {
            int childCound = trans.childCount;
            Transform[] allChild = new Transform[childCound];
            for (int i = 0; i < childCound; i++)
            {

                allChild[i] = trans.GetChild(i);
            }
            return allChild;
        }
    }
}
