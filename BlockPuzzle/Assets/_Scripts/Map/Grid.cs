using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public class Grid : MonoBehaviour
    {
        public int m_xSize, m_ySize;

        //出生点的物体
        public GameObject m_InitObj;

        private GameObject[] m_GridObj;

        private Vector3 m_InitPos;
        //间隔
        private float m_Spece = 0.24f;
        // Start is called before the first frame update
        void Start()
        {
            m_InitPos = m_InitObj.transform.position;
            StartCoroutine(Generate());
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 初始话地图位置信息 并设置位置检测
        /// </summary>
        /// <returns></returns>
        private IEnumerator Generate()
        {
            WaitForSeconds wait = new WaitForSeconds(0f);
            List<List<Vector2>> mapPoses = new List<List<Vector2>>();
            for (int i = 0; i < m_xSize; i++)
            {
                List<Vector2> poses = new List<Vector2>();
                for (int j = 0; j < m_ySize; j++)
                {
                    GameObject go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceType.Prefab, "Cube")) as GameObject;
                    go.transform.localPosition = new Vector3(m_InitPos.x + j * m_Spece, m_InitPos.y - i * m_Spece, -0.05f);
                    poses.Add(new Vector2(m_InitPos.x + i * m_Spece, m_InitPos.y + j * m_Spece));
                    yield return wait;
                }
                mapPoses.Add(poses);
            }
            MapHelper.SetElePoses(mapPoses);
        }
    }
}
