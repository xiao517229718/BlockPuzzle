using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    #region 根据索引 获取形状数组
    public class ShapList
    {

        public static List<List<int>> GetShapList(int shapId)
        {
            List<List<int>> returnValue = new List<List<int>>();
            int[][] shapValue;
            switch (shapId)
            {
                case 0:
                    shapValue = new int[][] { new int[] { 1 } };
                    break;
                case 1:
                    shapValue = new int[][] { new int[] { 1, 1, 1 } };
                    break;
                case 2:
                    shapValue = new int[][] { new int[] { 1 }, new int[] { 1 }, new int[] { 1 }, new int[] { 1 } };
                    break;
                case 3:
                    shapValue = new int[][] { new int[] { 1, 1 }, new int[] { 1, 0 } };
                    break;
                case 4:
                    shapValue = new int[][] { new int[] { 1, 1 }, new int[] { 1, 1 } };
                    break;
                case 5:
                    shapValue = new int[][] { new int[] { 1, 1, 1 }, new int[] { 0, 0, 1 }, new[] { 0, 0, 1 } };
                    break;
                case 6:
                    shapValue = new int[][] { new int[] { 1, 1, 1, 1, 1 } };
                    break;
                case 7:
                    shapValue = new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new[] { 1, 1, 1 } };
                    break;
                case 8:
                    shapValue = new int[][] { new int[] { 1, 0, 0 }, new int[] { 1, 1, 1 } };
                    break;
                case 9:
                    shapValue = new int[][] { new int[] { 1 }, new int[] { 1 } };
                    break;
                case 10:
                    shapValue = new int[][] { new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 } };
                    break;
                case 11:
                    shapValue = new int[][] { new int[] { 1 }, new int[] { 1 }, new[] { 1 }, new[] { 1 }, new[] { 1 } };
                    break;
                case 12:
                    shapValue = new int[][] { new int[] { 1 }, new int[] { 1 }, new[] { 1 } };
                    break;
                case 13:
                    shapValue = new int[][] { new int[] { 0, 1, 1 }, new int[] { 1, 1, 0 } };
                    break;
                case 14:
                    shapValue = new int[][] { new int[] { 1, 1 }, new int[] { 0, 1 } };
                    break;
                case 15:
                    shapValue = new int[][] { new int[] { 1, 0 }, new int[] { 1, 1 }, new[] { 0, 1 } };
                    break;
                case 16:
                    shapValue = new int[][] { new int[] { 1, 1, 1, 1 } };
                    break;
                case 17:
                    shapValue = new int[][] { new int[] { 1 }, new int[] { 1 }, new[] { 1 }, new[] { 1 }, new[] { 1 } };
                    break;
                case 18:
                    shapValue = new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 0, 0 }, new[] { 1, 0, 0 } };
                    break;
                case 19:
                    shapValue = new int[][] { new int[] { 0, 1 }, new int[] { 0, 1 }, new[] { 1, 1 } };
                    break;
                case 20:
                    shapValue = new int[][] { new int[] { 0, 1 }, new int[] { 1, 1 }, new[] { 0, 1 } };
                    break;
                case 21:
                    shapValue = new int[][] { new int[] { 1, 0 }, new int[] { 1, 1 } };
                    break;
                case 22:
                    shapValue = new int[][] { new int[] { 0, 1 }, new int[] { 1, 1 } };
                    break;
                case 23:
                    shapValue = new int[][] { new int[] { 0, 0, 1 }, new int[] { 0, 0, 1 }, new[] { 1, 1, 1 } };
                    break;
                case 24:
                    shapValue = new int[][] { new int[] { 1, 0, 0 }, new int[] { 1, 0, 0 }, new[] { 1, 1, 1 } };
                    break;
                case 25:
                    shapValue = new int[][] { new int[] { 1, 1, 0 }, new int[] { 0, 1, 1 } };
                    break;
                case 26:
                    shapValue = new int[][] { new int[] { 0, 1 }, new int[] { 1, 1 }, new[] { 1, 0 } };
                    break;
                case 27:
                    shapValue = new int[][] { new int[] { 1, 1, 1 }, new int[] { 0, 0, 1 } };
                    break;
                case 28:
                    shapValue = new int[][] { new int[] { 1, 1 }, new int[] { 1, 0 }, new[] { 1, 0 } };
                    break;
                case 29:
                    shapValue = new int[][] { new int[] { 1, 1, 1 }, new int[] { 0, 1, 0 } };
                    break;
                case 30:
                    shapValue = new int[][] { new int[] { 1, 0 }, new int[] { 1, 1 }, new[] { 1, 0 } };
                    break;
                default:
                    shapValue = new int[][] { new int[] { 1, 0 }, new int[] { 1, 1 }, new[] { 1, 0 } };
                    break;
            }

            for (int i = 0; i < shapValue.Length; i++)
            {
                List<int> horielem = new List<int>();
                for (int j = 0; j < shapValue[0].Length; j++)
                {
                    horielem.Add(shapValue[i][j]);
                }
                returnValue.Add(horielem);
            }
            return returnValue;
        }
    }
    #endregion
}