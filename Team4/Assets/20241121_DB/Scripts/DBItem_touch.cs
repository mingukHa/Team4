using System.Collections.Generic;
using UnityEngine;

public class DBItem_touch : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> storeItems;  // Ŭ�� ������ ���� ������Ʈ ����Ʈ

    public List<GameObject> GetGameObjects()
    {
        return storeItems;
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
                // Raycast�� ���� ������Ʈ�� gameObjects ����Ʈ�� ���Եǰ�, "shop_item" �±׸� ���� ���
                if (storeItems.Contains(hit.transform.gameObject) && hit.transform.CompareTag("shop_item"))
                {
                    // Ŭ���� ���� ������Ʈ�� �̸��� �ֿܼ� ���
                    Debug.Log("Clicked on: " + hit.transform.gameObject.name);

                    // �ش� ������Ʈ�� �ε����� ���ؼ� ���
                    int index = storeItems.IndexOf(hit.transform.gameObject);
                    Debug.Log("This is element number: " + index);
                }
            }
        }
    }
}