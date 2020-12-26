using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class ResourceLoad : GameModle
    {
        /// <summary>
        /// 从resource加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceType"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T LoadAsset<T>(string resourcePath) where T : Object
        {
            T value = Resources.Load<T>(resourcePath);
            return value;
        }
    }
}