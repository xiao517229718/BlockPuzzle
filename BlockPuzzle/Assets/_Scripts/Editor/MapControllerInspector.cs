using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BlockPuzzle
{
    [CustomEditor(typeof(MapController))]
    public class MapControllerInspector : Editor
    {
        private MapController _mapController;
        private void OnEnable()
        {
            _mapController = target as MapController;
        }
        private void OnDisable()
        {

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            _mapController.shapIndex = EditorGUILayout.IntField("形状索引", _mapController.shapIndex);
            //_mapController.shapIndex=GUILayout.Label(_mapController.shapIndex)
            if (_mapController.intMapInfo == null) _mapController.intMapInfo = new IntMapInfo();
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            if (GUILayout.Button("保存地图数据"))
            { //将数据保存到地图中
                _mapController.InspectorSaveMapInfo();
            }
            if (GUILayout.Button("地图全部填充"))
            { //将数据保存到地图中
                _mapController.AllToOneFill(1);
            }
            if (GUILayout.Button("地图全部置空"))
            { //将数据保存到地图中
                _mapController.AllToOneFill(0);
            }
            GUILayout.EndVertical();
           
        }

    }
}