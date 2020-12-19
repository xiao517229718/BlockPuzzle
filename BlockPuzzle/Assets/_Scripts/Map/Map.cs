using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    [System.Serializable]
    public class Map
    {
        public List<List<MapSingleInfo>> _mapInfo = new List<List<MapSingleInfo>>();
    }
}
