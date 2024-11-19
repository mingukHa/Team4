using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<GameObject> itemList = new List<GameObject>();

    private GameObject go;
    private Vector3 goPos;
    private float dist;

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

        // ���� ������Ʈ�κ����� ���� �Ÿ� ����
        dist = 3f;

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit);
            
            // ����� Ŭ������ �� Null Exception ���� ��
            go = hit.transform.gameObject;
            goPos = go.transform.position * dist;
            
            if (go.CompareTag("StoreItems"))
            {
                GameObject newGo =
                    Instantiate(go, goPos, Quaternion.identity);
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