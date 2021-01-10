using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace BlockPuzzle
{
    public class GameInfoSaveManager
    {
        static Dictionary<string, string> allSaveInfo = new Dictionary<string, string>();

        /// <summary>
        /// 游戏数据保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetGameInfoPath()
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath;
#else
return Application.persistentDataPath;
#endif
        }

        /// <summary>
        /// 存储类信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void SaveClassInfo<T>(string className, T value) where T : class
        {
            string data = LitJson.JsonMapper.ToJson(value);
            allSaveInfo[className] = data;
        }
        /// <summary>
        /// 存储其他类型的数据信息
        /// </summary>
        public static void SaveAttributeInfo(string AttributeName, string jsonData)
        {
            allSaveInfo[AttributeName] = jsonData;
        }

        /// <summary>
        /// 获取类的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="calssName"></param>
        /// <returns></returns>
        public static T GetClassInfo<T>(string calssName) where T : class
        {
            if (allSaveInfo.ContainsKey(calssName))
            {

                string data = allSaveInfo[calssName];
                return LitJson.JsonMapper.ToObject<T>(data);
            }
            Debug.LogWarning($"获取的类型{calssName}没有数据");
            return null;
        }
        /// <summary>
        /// 获取其他类型的值
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <returns></returns>
        public static string GetAttributeInfo(string AttributeName)
        {
            if (allSaveInfo.ContainsKey(AttributeName))
            {
                return allSaveInfo[AttributeName];
            }
            Debug.LogWarning($"获取的类型 {AttributeName} 没有数据");
            return null;
        }

        /// <summary>
        /// 获取存储的数据
        /// </summary>
        public static void StartGetSaveInfo()
        {
            allSaveInfo = new Dictionary<string, string>();
            if (!Directory.Exists(GetGameInfoPath()))
            {
                Directory.CreateDirectory(GetGameInfoPath());
            }
            if (File.Exists(GetGameInfoPath() + "/gameInfo.txt"))
            {
                string allGameInfo = File.ReadAllText(GetGameInfoPath() + "/gameInfo.txt", System.Text.Encoding.UTF8);
                if (allGameInfo != null)
                {
                    JsonData jsonDatas = LitJson.JsonMapper.ToObject(allGameInfo);//  JsonUtility.FromJson<JsonData>(allGameInfo);
                    string allgameinfos = jsonDatas["GameInfos"].ToString();
                    if (!string.IsNullOrEmpty(allgameinfos))
                    {
                        IDictionary dic = jsonDatas;
                        foreach (var values in dic.Values)
                        {
                            IDictionary ite = values as IDictionary;
                            foreach (var elem in ite.Keys)
                            {
                                allSaveInfo.Add(elem.ToString(), ite[elem.ToString()].ToString());
                            }
                        }
#if UNITY_EDITOR   
                        foreach (var value in allSaveInfo)
                        {
                            Debug.LogWarning($"编辑器打印之前保存数据 Key:{value.Key},Value:{value.Value}");
                        }
#endif
                    }
                }
            }
        }
        /// <summary>
        /// 设置需要存储的数据
        /// </summary>
        public static void EndSetSaveInfo()
        {
            if (allSaveInfo.Count == 0) return;
            JsonData jsonData = new JsonData();
            jsonData["GameInfos"] = new JsonData();
            foreach (var value in allSaveInfo)
            {
                jsonData["GameInfos"][value.Key] = value.Value;
            }
            File.WriteAllText(GetGameInfoPath() + "/gameInfo.txt", jsonData.ToJson());
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    }
}