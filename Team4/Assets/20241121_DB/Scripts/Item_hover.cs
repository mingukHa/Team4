using UnityEngine;
using static DBCheck;

public class Item_hover : MonoBehaviour
{
    // Ȱ��ȭ�� Ư�� ���ӿ�����Ʈ
    [SerializeField]
    private GameObject statusBox;
    [SerializeField]
    private status_text statusText;  // �����ۿ� �´� �ؽ�Ʈ�� ������Ʈ�� statusText

    private StoreItemData itemData;  // �ش� �������� ������ ����

    void Start()
    {
        // statusBox�� ��Ȱ��ȭ ���·� ����
        if (statusBox != null)
        {
            statusBox.SetActive(false);
        }

        // statusBox���� status_text ������Ʈ�� ã��
        //statusText = statusBox.GetComponent<status_text>();
    }

    public void SetItemData(StoreItemData data)
    {
        itemData = data;  // ������ �����͸� ����
    }

    public StoreItemData GetItemData()
    {
        return itemData;  // ������ �����͸� ��ȯ
    }

    void OnMouseEnter()
    {
        // ���콺 Ŀ���� ������ ������Ʈ�� �ö��� ��
        if (statusBox != null)
        {
            // statusBox�� Ȱ��ȭ
            statusBox.SetActive(true);

            // ������ ������Ʈ�� �߽��� ���
            Renderer itemRenderer = GetComponent<Renderer>();
            Vector3 itemCenter = itemRenderer.bounds.center; // ������ ������Ʈ�� �߽���

            // statusBox�� ũ�� ��� (���� ������Ʈ�� Renderer ���)
            Renderer statusChildRenderer = statusBox.GetComponentInChildren<Renderer>();
            Vector3 statusBoxSize = statusChildRenderer.bounds.size;

            // ������ ������Ʈ�� �߽������� statusBox�� �»���� ������ ��ġ ����
            Vector3 newPosition = itemCenter + new Vector3(statusBoxSize.x / 2, 0.5f, -statusBoxSize.z / 2);

            // statusBox�� ��ġ�� ���� ���� ��ġ�� ����
            statusBox.transform.position = newPosition;
        }

        // �����ۿ� �ش��ϴ� �̸��� statusText�� ����
        if (itemData != null)
        {
            statusText.UpdateText(itemData.item_name, itemData.item_state);  // ������ �������� �̸��� �ؽ�Ʈ�� ������Ʈ
        }
    }

    void OnMouseExit()
    {
        // ���콺 Ŀ���� ������ ������Ʈ���� ����� ��
        if (statusBox != null)
        {
            // statusBox�� ��Ȱ��ȭ
            statusBox.SetActive(false);
        }
    }
}
