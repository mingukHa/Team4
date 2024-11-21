using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class DBCheck : MonoBehaviour
{
    public event Action OnDataLoaded;  // ������ �ε� �Ϸ� �� ȣ��� �̺�Ʈ

    public class StoreItemData
    {
        public int item_num { get; set; }
        public string item_name { get; set; }
        public string item_state { get; set; }
    }
    [SerializeField]
    private GameObject itemList;  // item_list ������Ʈ

    private List<StoreItemData> store_itemDatas;

    private void Start()
    {
        StartCoroutine(GetScoreCoroutine());
    }

    private IEnumerator GetScoreCoroutine()
    {
        string uri = "http://127.0.0.1/getitem.php";

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(uri, string.Empty))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string data = www.downloadHandler.text;

                store_itemDatas = JsonConvert.DeserializeObject<List<StoreItemData>>(data);
                
                // ������ ����Ʈ�� ���� ������Ʈ�� �����͸� ��Ī
                Debug.Log("������ ����:" + store_itemDatas.Count);
                for (int i = 0; i < store_itemDatas.Count; i++)
                {
                    if (i < itemList.transform.childCount)
                    {
                        // item_list ���� ������Ʈ�� Item_hover ������Ʈ�� ������
                        Item_hover itemHover = itemList.transform.GetChild(i).GetComponent<Item_hover>();

                        if (itemHover != null)
                        {
                            // �������� ������(ItemData)�� �ش� ������ ������Ʈ�� ����
                            itemHover.SetItemData(store_itemDatas[i]);
                        }
                    }
                }

                foreach (StoreItemData itemdata in store_itemDatas)
                {
                    Debug.Log(itemdata.item_num + ":" + itemdata.item_name + ":" + itemdata.item_state);
                }
                OnDataLoaded?.Invoke();
            }
        }
    }
}
