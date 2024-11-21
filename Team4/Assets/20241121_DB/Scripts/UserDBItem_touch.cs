using System.Collections.Generic;
using UnityEngine;

public class UserDBItem_touch : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> userItems;  // Ŭ�� ������ ���� ������Ʈ ����Ʈ

    public List<GameObject> GetGameObjects()
    {
        return userItems;
    }

    private void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ��, Raycast�� ����Ͽ� Ŭ���� ������Ʈ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // ���콺 ��ġ���� Ray ����
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Raycast�� ���� ������Ʈ�� gameObjects ����Ʈ�� ���Եǰ�, "InventoryItem" �±׸� ���� ���
                if (userItems.Contains(hit.transform.gameObject) && hit.transform.CompareTag("InventoryItem"))
                {
                    // Ŭ���� ���� ������Ʈ�� �̸��� �ֿܼ� ���
                    Debug.Log("Clicked on: " + hit.transform.gameObject.name);

                    // �ش� ������Ʈ�� �ε����� ���ؼ� ���
                    int index = userItems.IndexOf(hit.transform.gameObject);
                    Debug.Log("This is element number: " + index);
                }
            }
        }
    }
}