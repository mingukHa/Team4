using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class exit : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField Username;
    [SerializeField]
    private TMP_InputField Password;
    [SerializeField]
    private Button exitbt;
    [SerializeField]
    private GameObject exitUI;
    [SerializeField]
    private TMP_InputField noexit;

    private const string exitURL = "http://127.0.0.1/exit.php"; //���� �ּ�
    private void Start()
    {
        exitbt.onClick.AddListener(() => StartCoroutine(exitCoroutine(Username.text, Password.text)));
        //��ư�� Ŭ���Ǹ� �ڷ�ƾ ���� ��ǲ�ʵ� ���� �ؽ�Ʈ�� ��ȯ�ؼ� �Լ��� �ѱ��
    }
    private IEnumerator exitCoroutine(string username, string password)
    {
        Debug.Log($"{username},{password} �� ����");

        WWWForm form = new WWWForm();
        form.AddField("username", username); //php�� �����Ǵ� �ʵ带 �߰��Ѵ�
        form.AddField("password", password);
        using (UnityWebRequest www = UnityWebRequest.Post(exitURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError($"��û ����: {www.error}");                
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log($"���� ���� ���� - Response: {response}");
                exitUI.SetActive(false);
                    
            }
        }
    }   

}