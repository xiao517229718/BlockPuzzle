using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public class ShapManager
    {
        /// <summary>
        /// 所有形状的索引 使用一次少一次 新游戏重新赋值
        /// </summary>
        public static List<int> questionBank = new List<int>();
        /// <summary>
        /// 当前开始的索引
        /// </summary>
        public static int currentIndex = 3;

        /// <summary>
        /// 初始化形状生成列表
        /// </summary>
        public static void QuestionInit()
        {
            questionBank.Clear();
            currentIndex = 0;
            for (int i = 0; i < 31; i++)
            {
                questionBank.Add(i);
            }
        }
        /// <summary>
        /// 获取当前应该生成的形状索引 (默认生成三个)
        /// </summary>
        /// <returns></returns>
        public static List<int> GetQuestionIndex()
        {
            List<int> quesIndex = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                quesIndex.Add(GetSingleIndex());
            }
            return quesIndex;
        }
        /// <summary>
        /// 内部获取一个形状id
        /// </summary>
        /// <returns></returns>
        private static int GetSingleIndex()
        {
            int returnValue = currentIndex;
            if (currentIndex < 31)
            {
                returnValue = currentIndex;
                currentIndex += 1;
            }
            else
            {
                returnValue = Random.Range(0, 31);
            }
            return returnValue;
        }
    }
}