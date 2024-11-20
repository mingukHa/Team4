using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private GameObject[,] itemList = new GameObject[5, 5]; // 인벤토리 아이템을 담을 5 x 5 배열
    private GameObject go; // Ray와 상호작용할 상점 아이템

    [SerializeField]
    private GameObject invenItemHolder; // 인벤토리에 생성될 아이템 홀더

    private Vector3 invenItemDist = new Vector3(2f, 0f, -2f); // 인벤토리 아이템간 간격
    private Vector3 startPos; // 인벤토리 아이템 배치 시작 위치
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

    // 아이템 생성, 파괴 메소드
    private void BuildObject()
    {
        cursorPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit);

            // Null 예외처리
            if (hit.collider == null)
            {
                return;
            }

            // Ray가 오브젝트에 맞았을 때
            go = hit.transform.gameObject;

            // 스토어 아이템이면 인벤토리 위치로 복사
            if (go.CompareTag("StoreItems") && itemCount < 25)
            {
                // 인벤토리는 5 x 5 배치
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (itemList[row, col] == null)
                        {
                            // 인벤토리에 복사된 아이템이 배치될 위치
                            Vector3 goPos = startPos + new Vector3(col * invenItemDist.x, 0, row * invenItemDist.z);
                            GameObject newGo = Instantiate(go, goPos, go.transform.rotation);

                            // 생성된 아이템 InvenItemHolder로 배치 후 태그 변경
                            newGo.transform.SetParent(invenItemHolder.transform);
                            itemList[row, col] = newGo;
                            newGo.tag = "InventoryItems";

                            itemCount++;

                            return;
                        }
                    }
                }
            }
            // 인벤토리 아이템이면 인벤토리와 리스트에서 제거
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