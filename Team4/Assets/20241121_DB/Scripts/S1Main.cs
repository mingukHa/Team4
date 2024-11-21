using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Networking;

public class S1Main : MonoBehaviour
{
    [SerializeField]
    private Button Jclosebt = null;
    [SerializeField]
    private Button Loginbt = null;
    [SerializeField]
    private Button Eclosebt = null;
    [SerializeField]
    private Button joinbt = null;
    [SerializeField]
    private Button exitbt = null;
    [SerializeField]
    private GameObject joinUI = null;
    [SerializeField]
    private GameObject exitUI = null;
    [SerializeField]
    private TMP_InputField username;
    [SerializeField]
    private TMP_InputField password;

    private const string loginURL = "http://127.0.0.1/login.php";


    private void Start()
    {
        Loginbt.onClick.AddListener(() => StartCoroutine(LoginCoroutine(username.text, password.text)));
        joinbt.onClick.AddListener(() => OnjoinUI(true));
        exitbt.onClick.AddListener(() => OnExitUI(true));
        Jclosebt.onClick.AddListener(() => Jclose(false));
        Eclosebt.onClick.AddListener(() => Eclose(false));
    }
    private IEnumerator LoginCoroutine(string username, string password)
    {
        Debug.Log($"{username},{password} 값 들어옴");
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            string response = www.downloadHandler.text;
            Debug.Log("서버 응답: " + response);

            if (response.Contains("Login success"))
            {
                Debug.Log("로그인 성공!");
                SceneManager.LoadScene("Scene2"); // 다음 씬으로 전환
            }
            else
            {
                Debug.Log("로그인 실패: " + response);
            }
        }
    }


    private void OnLogin()
    {
        SceneManager.LoadScene("Scene2");
    }
    private void Jclose(bool close)
    {
        joinUI.SetActive(close);
    }
    private void Eclose(bool close)
    {
        exitUI.SetActive(close);
    }
    private void EnjoinUI(bool close)
    {
        exitUI.SetActive(close);
    }

    private void OnjoinUI(bool join)
    {
        joinUI.SetActive(join);
    }

    private void OnExitUI(bool exit)
    {
        exitUI.SetActive(exit);
    }
}

