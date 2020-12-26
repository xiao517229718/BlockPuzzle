using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public static class GameModleManager
    {
        /// <summary>
        /// 游戏中可能用到的模块
        /// </summary>
        public static Dictionary<int, GameModle> allComponent = new Dictionary<int, GameModle>();
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public static GameModle GetGameModel(Type modle)
        {
            int hashCode = modle.GetHashCode();
            GameModle bc = null;
            if (allComponent.TryGetValue(hashCode, out bc))
            {
                return bc;
            }
            return CreatModle(modle);
        }
        /// <summary>
        /// 创建一个模块
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        private static GameModle CreatModle(Type modle)
        {
            int hashCode = modle.GetHashCode();
            GameModle bc = (GameModle)Activator.CreateInstance(modle);
            allComponent.Add(hashCode, bc);
            return bc;
        }
    }
}
