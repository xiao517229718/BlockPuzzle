using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class LocalizationComponent : BaseComponet
    {
        /// <summary>
        /// 游戏开始获取存储数据
        /// </summary>
        private void Awake()
        {
            GameInfoSaveManager.StartGetSaveInfo();
         
        }
        /// <summary>
        /// 结束游戏 保存游戏数据
        /// </summary>
        private void OnDestroy()
        {
            GameInfoSaveManager.EndSetSaveInfo();
        }
      
      
    }
}