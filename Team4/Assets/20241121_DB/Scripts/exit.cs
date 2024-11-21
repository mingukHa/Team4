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
    private TMP_InputField passwordcheck;
    [SerializeField]
    private Button exitbt;
    private const string exitURL = "http://127.0.0.1/exit.php";
    private void Start()
    {
        exitbt.onClick.AddListener(() => StartCoroutine(exitCoroutine(Username.text, Password.text)));
    }
    private IEnumerator exitCoroutine(string username, string password)
    {
        Debug.Log($"{username},{password} 값 들어옴");

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (UnityWebRequest www = UnityWebRequest.Post(exitURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError($"요청 실패: {www.error}");
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log($"서버 응답 수신 - Response: {response}");

            }
        }
    }

}
