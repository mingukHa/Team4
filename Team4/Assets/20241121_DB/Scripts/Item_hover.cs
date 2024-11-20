using UnityEngine;
using static DBCheck;

public class Item_hover : MonoBehaviour
{
    // Ȱ��ȭ�� Ư�� ���ӿ�����Ʈ
    [SerializeField]
    private GameObject statusBox;
    [SerializeField]
    private status_text statusText;  // �����ۿ� �´� �ؽ�Ʈ�� ������Ʈ�� statusText

    private ItemData itemData;  // �ش� �������� ������ ����

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

    public void SetItemData(ItemData data)
    {
        itemData = data;  // ������ �����͸� ����
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
            Vector3 newPosition = itemCenter + new Vector3(statusBoxSize.x / 2, -statusBoxSize.y / 2, -0.75f);

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
