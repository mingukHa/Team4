using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class DBCheck : MonoBehaviour
{
    public event Action OnDataLoaded;  // 데이터 로드 완료 시 호출될 이벤트

    public class StoreItemData
    {
        public int item_num { get; set; }
        public string item_name { get; set; }
        public string item_state { get; set; }
    }
    [SerializeField]
    private GameObject itemList;  // item_list 오브젝트

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
                
                // 아이템 리스트의 하위 오브젝트와 데이터를 매칭
                Debug.Log("아이템 갯수:" + store_itemDatas.Count);
                for (int i = 0; i < store_itemDatas.Count; i++)
                {
                    if (i < itemList.transform.childCount)
                    {
                        // item_list 하위 오브젝트의 Item_hover 컴포넌트를 가져옴
                        Item_hover itemHover = itemList.transform.GetChild(i).GetComponent<Item_hover>();

                        if (itemHover != null)
                        {
                            // 아이템의 데이터(ItemData)를 해당 아이템 오브젝트에 저장
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
