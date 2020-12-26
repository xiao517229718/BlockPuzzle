using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace BlockPuzzle
{
    public enum ResourceType
    {
        /// <summary>
        /// 背景音乐
        /// </summary>
        Music,
        /// <summary>
        /// 人声
        /// </summary>
        Sound,
        /// <summary>
        /// 音效
        /// </summary>
        UISound,
        /// <summary>
        /// 预制件
        /// </summary>
        Prefab,
        /// <summary>
        /// text表
        /// </summary>
        DataTables,
        /// <summary>
        /// 图片资源
        /// </summary>
        Texture,
        /// <summary>
        /// 音频文件
        /// </summary>
        VideoRT,
        /// <summary>
        /// 动画
        /// </summary>
        AnimatorController
    }
    public class ResourcePath
    {
        /// <summary>
        /// 获取资源对应的Resource路径
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static string GetPath(ResourceType resourceType, string resourceName)
        {
            StringBuilder sb = new StringBuilder();

            switch (resourceType)
            {
                case ResourceType.AnimatorController:
                    sb.Append("Animation/" + resourceName);
                    break;
                case ResourceType.DataTables:
                    sb.Append("DataTables/" + resourceName);
                    break;
                case ResourceType.Music:
                    sb.Append("Audio/Music" + resourceName);
                    break;
                case ResourceType.Prefab:
                    sb.Append("Prefab/" + resourceName);
                    break;
                case ResourceType.Sound:
                    sb.Append("Audio/Sound/" + resourceName);
                    break;
                case ResourceType.Texture:
                    sb.Append("Texture/" + resourceName);
                    break;
                case ResourceType.UISound:
                    sb.Append("Audio/UISound/" + resourceName);
                    break;
                case ResourceType.VideoRT:
                    sb.Append("Video/" + resourceName);
                    break;
            }
            return sb.ToString();
        }
    }
}
