using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class ElemShapDrage : MonoBehaviour
    {
        // Start is called before the first frame update
        GameObject target = null;
        private bool isMouseDrag = false;
        private Vector3 screenPosition;
        private Vector3 offset;
        private List<List<int>> shapArry = new List<List<int>>();
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameController.pause) return;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                target = ReturnClickedObject(out hitInfo);
                if (target != null)
                {
                    isMouseDrag = true;
                    screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseDrag = false;
                if (target != null)
                {
                    Transform[] blocks = target.transform.GetAllChildTrans();
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        try
                        {
                            ElemDetection elemDt = blocks[i].GetComponent<ElemDetection>();
                            elemDt.MouseUp();
                        }
                        catch
                        {
                            Debug.LogWarning("dont have scripts");
                        }
                    }
                }
            }
            if (isMouseDrag)
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
                target.transform.position = currentPosition;
                target.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
        }
        GameObject ReturnClickedObject(out RaycastHit hit)
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                if (hit.collider.transform.name == "singleParent")
                    target = hit.collider.gameObject;
            }
            return target;
        }
    }
}