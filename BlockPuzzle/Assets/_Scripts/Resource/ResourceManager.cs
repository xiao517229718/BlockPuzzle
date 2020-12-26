using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class ResourceManager
    {
        public static T LoadAsset<T>(ResourceType resourceType, string assetName) where T : Object
        {
            ResourceLoad _resourceLoad = (ResourceLoad)GameModleManager.GetGameModel(typeof(ResourceLoad));
            string _resourcePath = ResourcePath.GetPath(resourceType, assetName);
            T value = _resourceLoad.LoadAsset<T>(_resourcePath);
            return value;
        }
    }
}