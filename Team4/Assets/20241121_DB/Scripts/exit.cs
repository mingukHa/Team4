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

    private const string exitURL = "http://127.0.0.1/exit.php"; //로컬 주소
    private void Start()
    {
        exitbt.onClick.AddListener(() => StartCoroutine(exitCoroutine(Username.text, Password.text)));
        //버튼이 클릭되면 코루틴 시작 인풋필드 값을 텍스트로 변환해서 함수로 넘긴다
    }
    private IEnumerator exitCoroutine(string username, string password)
    {
        Debug.Log($"{username},{password} 값 들어옴");

        WWWForm form = new WWWForm();
        form.AddField("username", username); //php에 연동되는 필드를 추가한다
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
                exitUI.SetActive(false);
                    
            }
        }
    }   

}
