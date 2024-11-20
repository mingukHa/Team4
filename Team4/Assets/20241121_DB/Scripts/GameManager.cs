using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<GameObject> itemList = new List<GameObject>(24);

    private GameObject go;
    private Vector3 goPos;
    private Vector3 dist = new Vector3(8f, 0f, 0f);

    private Vector3 mousePos;

    private void Update()
    {
        BuildObject();
    }

    private void BuildObject()
    {
        mousePos = Input.mousePosition;
        Ray ray =
            Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        // 원래 오브젝트로부터의 임의 거리 설정

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit);

            if (hit.collider == null)
            {
                return;
            }

            go = hit.transform.gameObject;
            goPos = go.transform.position + dist;

            if (go.CompareTag("StoreItems"))
            {
                GameObject newGo =
                    Instantiate(go, goPos, Quaternion.identity, go.transform);
                itemList.Add(newGo);
                newGo.tag = "InventoryItems";
            }

            else if (go.CompareTag("InventoryItems"))
            {
                itemList.Remove(go);
                Destroy(go);
            }
        }
    }
}