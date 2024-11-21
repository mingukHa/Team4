using UnityEngine;
using static UserDBCheck;
using static DBCheck;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public DBCheck dbCheck;
    public UserDBCheck userdbCheck;

    private GameObject[,] itemList = new GameObject[4, 1]; // �κ��丮 �������� ���� 1 x 4 �迭
    private GameObject go;                                 // Ray�� ��ȣ�ۿ��� ���� ������

    //private User_ItemData useritem; //�����κ� ������ DB������
    [SerializeField]
    private GameObject storeitem_list; //���� ������ ����Ʈ
    [SerializeField]
    private GameObject invenItemsHolder; // �κ��丮�� ������ ������ Ȧ��, ���� �κ� ������ ����Ʈ
    [SerializeField]
    private Camera subCamera;            // ���� ������Ʈ�� ���� ����ī�޶�

    private float invenItemScale;        // �κ��丮�� �� ������ �����ϸ� ���
    private Vector3 invenItemDist;       // �κ��丮 �����۰� ����
    private Vector3 startPos;            // �κ��丮 ������ ��ġ ���� ��ġ

    private Vector3 cursorPos;           // ���콺 Ŀ�� ��ġ

    private List<Item_hover> itemHovers = new List<Item_hover>();// Item_hover(���� ������ DB�����) ������Ʈ���� ������ ����Ʈ
    private List<User_Item_hover> useritemHovers = new List<User_Item_hover>();
    private List<User_ItemData> userItemDataList = new List<User_ItemData>(); // ���� ������ ������

    [SerializeField]
    private GameObject user_statusBox;  // ���� ������ ���� �ڽ� (shop_item_status ������Ʈ)
    [SerializeField]
    private status_text user_statusText; // �κ� ������ ���� �ؽ�Ʈ (InvenItemsHolder ������Ʈ)

    //public void SetItemData(User_ItemData data)
    //{
    //    useritem = data;  // ������ �����͸� ����
    //}

    private void Start()
    {
        if (dbCheck == null)
        {
            Debug.LogError("dbCheck is not assigned in GameManager");
            return;
        }

        // DBCheck���� ������ �ε� �Ϸ� �� �۾��� �����ϵ��� �̺�Ʈ ���
        dbCheck.OnDataLoaded += OnDataLoaded;
        userdbCheck.OnUserDataLoaded += OnUserDataLoaded;

        // ���� �ʱ�ȭ
        startPos = new Vector3(7f, 0.1f, 1.8f);
        invenItemDist = new Vector3(0f, 0f, -2.2f);
        invenItemScale = 2.5f;


        //// ������ ����Ʈ�� �ִ� ��� ������ ������Ʈ���� Item_hover ������Ʈ�� ������
        //foreach (Transform child in storeitem_list.transform)
        //{
        //    Item_hover itemHover = child.GetComponent<Item_hover>();
        //    if (itemHover != null)
        //    {
        //        Debug.Log("Item_hover component found for: " + child.name); // �������� ����� �������� �� ���
        //    }
        //    else
        //    {
        //        Debug.Log("No Item_hover component on: " + child.name); // Item_hover ������Ʈ�� ���� �� ���
        //    }
        //}
        //// �����۵��� �����͸� Ȱ���ϴ� ����
        //PrintItemData();
    }
    private void OnDataLoaded()
    {
        // DB���� �����Ͱ� �ε�� �� ������ �۾�
        Debug.Log("������ �ε� �Ϸ�");

        // ������ ����Ʈ���� Item_hover ������Ʈ�� ������ `itemHovers` ����Ʈ�� �߰�
        itemHovers.Clear();  // ������ ����� ���� �ִٸ� �ʱ�ȭ

        foreach (Transform child in storeitem_list.transform)
        {
            Item_hover itemHover = child.GetComponent<Item_hover>();
            if (itemHover != null)
            {
                itemHovers.Add(itemHover);  // ����Ʈ�� �߰�
            }
            else
            {
                Debug.Log("No Item_hover component on: " + child.name);  // ������Ʈ�� ���� ��� �α� ���
            }
        }
        //PrintItemData();
    }
    //private void PrintItemData()
    //{
    //    foreach (Item_hover itemHover in itemHovers)
    //    {
    //        Debug.Log("gameManager Item");
    //        StoreItemData itemData = itemHover.GetItemData(); // Item_hover���� ����� ������ ��������

    //        // itemData�� �� �Ӽ� Ȱ��
    //        Debug.Log("gameManager Item Num: " + itemData.item_num);
    //        Debug.Log("gameManager Item Name: " + itemData.item_name);
    //        Debug.Log("gameManager Item State: " + itemData.item_state);
    //    }
    //}
    private void OnUserDataLoaded()
    {
        // ���� �κ��丮 �����Ͱ� �ε�� �� ������ �۾�
        Debug.Log("���� �κ� ������ �ε� �Ϸ�");

        userItemDataList.Clear();  // ������ ����� �����Ͱ� �ִٸ� �ʱ�ȭ
        userItemDataList.AddRange(userdbCheck.GetUserItemDatas()); // ���� ������ ������ ����Ʈ ��������

        // ���� �κ� �����۰� ���� �������� ���ϰ� �κ��丮 ����
        CompareAndAddItemsToInventory();

        // ���� ��, Item_hover ���� �۾�
        RemoveItemHoverFromInventory();
    }

    private void RemoveItemHoverFromInventory()
    {
        // ������ ����Ʈ�� ��ȸ�ϸ� ������ �����۵��� Item_hover ������Ʈ�� ����
        foreach (var item in itemList)
        {
            if (item != null)
            {
                Item_hover itemHover = item.GetComponent<Item_hover>();
                if (itemHover != null)
                {
                    Destroy(itemHover);  // Item_hover ������Ʈ ����
                }

                // �� �Ŀ� User_Item_hover�� �߰�
                User_Item_hover userItemHover = item.AddComponent<User_Item_hover>();
                if (userItemHover != null)
                {
                    // statusBox�� statusText�� �ش� �����ۿ� �Ҵ�
                    userItemHover.statusBox = user_statusBox;
                    userItemHover.statusText = user_statusText;
                }
            }
        }
    }

    private void CompareAndAddItemsToInventory()
    {
        foreach (var userItem in userItemDataList)
        {
            foreach (var storeItem in itemHovers)
            {
                // ���� ������ ��ȣ�� ���� ������ ��ȣ ��
                if (userItem.item_num == storeItem.GetItemData().item_num)
                {
                    // �κ��丮�� �̹� �ִ��� Ȯ�� ��, ������ ����
                    AddItemToInventory(storeItem.gameObject);
                    break;
                }
            }
        }
    }
    private void AddItemToInventory(GameObject storeItem)
    {
        int itemCount = 0;

        // �κ��丮�� ����� �������� ��ġ�� ��ġ ���
        for (int row = 0; row < 4; ++row)
        {
            for (int col = 0; col < 1; ++col)
            {
                if (itemList[row, col] == null)
                {
                    Vector3 goPos = startPos + new Vector3(col * invenItemDist.x, 0, row * invenItemDist.z);

                    // ������ ���� �� ��ġ ����
                    GameObject newGo = Instantiate(
                        storeItem, goPos, Quaternion.Euler(
                            storeItem.transform.rotation.eulerAngles.x,
                            storeItem.transform.rotation.eulerAngles.y + 30f,
                            storeItem.transform.rotation.eulerAngles.z
                        ));

                    // ������ ũ�� ����
                    newGo.transform.localScale = storeItem.transform.localScale * invenItemScale;

                    // �κ��丮�� �ڽ����� ����
                    newGo.transform.SetParent(invenItemsHolder.transform);
                    itemList[row, col] = newGo;
                    newGo.tag = "InventoryItem";

                    itemCount++;
                    return;
                }
            }
        }
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