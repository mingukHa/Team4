using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private GameObject[,] itemList = new GameObject[5, 5]; // �κ��丮 �������� ���� 5 x 5 �迭
    private GameObject go; // Ray�� ��ȣ�ۿ��� ���� ������

    [SerializeField]
    private GameObject invenItemHolder; // �κ��丮�� ������ ������ Ȧ��

    private Vector3 invenItemDist = new Vector3(2f, 0f, -2f); // �κ��丮 �����۰� ����
    private Vector3 startPos; // �κ��丮 ������ ��ġ ���� ��ġ
    private int itemCount = 0;

    private Vector3 cursorPos;

    private void Start()
    {
        startPos = new Vector3(12f, 0.1f, 1f);
    }

    private void Update()
    {
        BuildObject();
    }

    // ������ ����, �ı� �޼ҵ�
    private void BuildObject()
    {
        cursorPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit);

            // Null ����ó��
            if (hit.collider == null)
            {
                return;
            }

            // Ray�� ������Ʈ�� �¾��� ��
            go = hit.transform.gameObject;

            // ����� �������̸� �κ��丮 ��ġ�� ����
            if (go.CompareTag("StoreItems") && itemCount < 25)
            {
                // �κ��丮�� 5 x 5 ��ġ
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (itemList[row, col] == null)
                        {
                            // �κ��丮�� ����� �������� ��ġ�� ��ġ
                            Vector3 goPos = startPos + new Vector3(col * invenItemDist.x, 0, row * invenItemDist.z);
                            GameObject newGo = Instantiate(go, goPos, go.transform.rotation);

                            // ������ ������ InvenItemHolder�� ��ġ �� �±� ����
                            newGo.transform.SetParent(invenItemHolder.transform);
                            itemList[row, col] = newGo;
                            newGo.tag = "InventoryItems";

                            itemCount++;

                            return;
                        }
                    }
                }
            }
            // �κ��丮 �������̸� �κ��丮�� ����Ʈ���� ����
            else if (go.CompareTag("InventoryItems"))
            {
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (itemList[row, col] == go)
                        {
                            itemList[row, col] = null;
                            Destroy(go);

                            itemCount--;

                            return;
                        }
                    }
                }
            }
        }
    }
}