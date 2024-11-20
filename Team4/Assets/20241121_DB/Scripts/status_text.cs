using TMPro;
using UnityEngine;

public class status_text : MonoBehaviour
{
    [SerializeField]
    private TMP_Text shop_item_name;  // �ؽ�Ʈ�� ����� TMP_Text ������Ʈ
    [SerializeField]
    private TMP_Text shop_item_status;  // �ؽ�Ʈ�� ����� TMP_Text ������Ʈ

    public void UpdateText(string item_name_Text, string item_status_Text)
    {
        if (shop_item_name != null && shop_item_status != null)
        {
            shop_item_name.text = item_name_Text;  // ���޹��� �ؽ�Ʈ�� TMP_Text�� ���
            shop_item_status.text = item_status_Text;  // ���޹��� �ؽ�Ʈ�� TMP_Text�� ���
        }
        else
        {
            Debug.LogError("TMP_Text ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}