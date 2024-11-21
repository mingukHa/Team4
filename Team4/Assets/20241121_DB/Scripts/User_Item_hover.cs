using NUnit.Framework.Interfaces;
using UnityEngine;
using static UserDBCheck;

public class User_Item_hover : MonoBehaviour
{
    // Ȱ��ȭ�� Ư�� ���ӿ�����Ʈ

    public GameObject statusBox;

    public status_text statusText;  // �����ۿ� �´� �ؽ�Ʈ�� ������Ʈ�� statusText

    private User_ItemData itemData;  // �ش� �������� ������ ����

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

    public void SetItemData(User_ItemData data)
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