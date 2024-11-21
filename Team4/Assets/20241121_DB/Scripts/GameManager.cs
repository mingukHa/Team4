using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[,] itemList = new GameObject[4, 1]; // �κ��丮 �������� ���� 1 x 4 �迭
    private GameObject go;                                 // Ray�� ��ȣ�ۿ��� ���� ������

    [SerializeField]
    private GameObject invenItemsHolder; // �κ��丮�� ������ ������ Ȧ��
    [SerializeField]
    private Camera subCamera;            // ���� ������Ʈ�� ���� ����ī�޶�

    private float invenItemScale;        // �κ��丮�� �� ������ �����ϸ� ���
    private Vector3 invenItemDist;       // �κ��丮 �����۰� ����
    private Vector3 startPos;            // �κ��丮 ������ ��ġ ���� ��ġ

    private Vector3 cursorPos;           // ���콺 Ŀ�� ��ġ

    private void Start()
    {
        // ���� �ʱ�ȭ
        startPos = new Vector3(7f, 0.1f, 1.8f);
        invenItemDist = new Vector3(0f, 0f, -2.2f);
        invenItemScale = 2.5f;
    }

    private void Update()
    {
        BuildObject();
    }

    // ������ ����, �ı� �޼ҵ�
    private void BuildObject()
    {
        cursorPos = Input.mousePosition;
        Ray ray = subCamera.ScreenPointToRay(cursorPos);
        RaycastHit hit;

        int itemCount = 0;

        // ���콺 ���� ��ư ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŀ�� ��ġ�� Ray �߻��Ͽ� ��� ������Ʈ�� ���� ��ȯ
            Physics.Raycast(ray, out hit);

            // Ray�� ���� �ݶ��̴��� ���� �� null ����ó��
            if (hit.collider == null)
            {
                return;
            }

            // Ray�� �ݶ��̴��� �¾��� ��
            go = hit.collider.gameObject;

            // ����� ������ �±׸� �κ��丮 ��ġ�� ����
            if (go.CompareTag("StoreItem") && itemCount < 4)
            {
                // �κ��丮�� 1 x 4 ��ġ
                for (int row = 0; row < 4; ++row)
                {
                    for (int col = 0; col < 1; ++col)
                    {
                        if (itemList[row, col] == null)
                        {
                            // �κ��丮�� ����� �������� ��ġ�� ��ġ
                            Vector3 goPos = startPos + new Vector3(col * invenItemDist.x, 0, row * invenItemDist.z);

                            // �ش� ��ġ���� ������ ���� �� ��ġ �� ������Ʈ ���� ����
                            GameObject newGo = Instantiate(
                                go, goPos, Quaternion.Euler(
                                    go.transform.rotation.eulerAngles.x,
                                    go.transform.rotation.eulerAngles.y + 30f,
                                    go.transform.rotation.eulerAngles.z
                                ));

                            // ������ Ȯ��
                            newGo.transform.localScale = go.transform.localScale * invenItemScale;

                            // ������ ������ InvenItemsHolder�� ��ġ �� �±� ����
                            newGo.transform.SetParent(invenItemsHolder.transform);
                            itemList[row, col] = newGo;
                            newGo.tag = "InventoryItem";

                            ++itemCount;

                            return;
                        }
                    }
                }
            }
            // �κ��丮 ������ �±׸� �κ��丮�� ����Ʈ���� ����
            else if (go.CompareTag("InventoryItem"))
            {
                for (int row = 0; row < 4; ++row)
                {
                    for (int col = 0; col < 1; ++col)
                    {
                        if (itemList[row, col] == go)
                        {
                            itemList[row, col] = null;
                            Destroy(go);

                            --itemCount;

                            return;
                        }
                    }
                }
            }
        }
    }
}