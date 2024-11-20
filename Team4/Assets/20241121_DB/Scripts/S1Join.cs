using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Signup : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField; 
    [SerializeField]
    private TMP_InputField passwordField; 
    [SerializeField]
    private TMP_InputField passwordcheck; 
    [SerializeField]
    private Button signupButton; 
    [SerializeField]
    private Button idcheck; 
    [SerializeField]
    private TextMeshProUGUI passwordMessage;
    [SerializeField]
    private TextMeshProUGUI passwordMatchMessage; 
    [SerializeField]
    private TextMeshProUGUI passwordCheckicon;
    [SerializeField]
    private TextMeshProUGUI passwordMatcgicon;
    [SerializeField]
    private TextMeshProUGUI idCheckMessage;
    [SerializeField]
    private TextMeshProUGUI idCheckIcon; 

    private const string loginUri = "http://192.168.2.24/login.php";
    private const string checkUsernameUri = "http://192.168.2.24/check_username.php";  

    private void Start()
    {

        signupButton.onClick.AddListener(OnSignupClicked);
        idcheck.onClick.AddListener(OnIdCheckID);

        

        passwordField.onValueChanged.AddListener(OnPasswordChanged);
        passwordcheck.onValueChanged.AddListener(OnPasswordCheckChanged);
    }

    private void OnSignupClicked()
    {

        if (!IsPasswordMatch())
        {
            passwordMatchMessage.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            passwordMatchMessage.color = Color.red;

            return;
        }

        if (!IsPasswordValid(passwordField.text))
        {
            passwordMessage.text = "��й�ȣ�� ����, ���ڷ� 4~10�� �̳��� �Է��ϼ���.";
            passwordMessage.color = Color.red;
            return;
        }

        StartCoroutine(SignupCoroutine(usernameField.text, passwordField.text));
    }

    private void OnIdCheckID()
    {
        string username = usernameField.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            idCheckMessage.text = "���̵� �Է��ϼ���.";
            idCheckMessage.color = Color.red;
            idCheckIcon.text = "X";
            idCheckIcon.color = Color.red;
            return;
        }

        StartCoroutine(CheckUsernameCoroutine(username));
    }

    private void OnPasswordChanged(string password)
    {
        if (IsPasswordValid(password))
        {
            passwordMessage.text = "��� ������ ��й�ȣ�Դϴ�.";
            passwordMessage.color = Color.green;
            passwordCheckicon.text = "O";
            passwordCheckicon.color = Color.green;
        }
        else
        {
            passwordMessage.text = "��й�ȣ�� ����� ���ڷ� 4~10�� �̳��� �Է��ϼ���.";
            passwordMessage.color = Color.red;
            passwordCheckicon.text = "X";
            passwordCheckicon.color = Color.red;
        }
    }

    private void OnPasswordCheckChanged(string confirmPassword)
    {
        if (IsPasswordMatch())
        {
            passwordMatchMessage.text = "��й�ȣ�� ��ġ�մϴ�.";
            passwordMatchMessage.color = Color.green;
            passwordMatcgicon.text = "O";
            passwordMatcgicon.color = Color.green;
        }
        else
        {
            passwordMatchMessage.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            passwordMatchMessage.color = Color.red;
            passwordMatcgicon.text = "X";
            passwordMatcgicon.color = Color.red;
        }
    }

    private bool IsPasswordValid(string password)
    {
        if (password.Length < 4 || password.Length > 10)
        {
            return false;
        }

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]+$");
        return regex.IsMatch(password);
    }

    private bool IsPasswordMatch()
    {
        return passwordField.text == passwordcheck.text;
    }

    private IEnumerator SignupCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError("���� �߻�: " + www.error);
            }
            else
            {
                Debug.Log("���� ����: " + www.downloadHandler.text);
            }
        }
    }

    private IEnumerator CheckUsernameCoroutine(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(checkUsernameUri, form))
        {
            yield return www.SendWebRequest();

            string jsonResponse = www.downloadHandler.text;
            UsernameResponse response = JsonUtility.FromJson<UsernameResponse>(jsonResponse);

            if (response.success)
            {
                idCheckMessage.text = response.message; 
                idCheckMessage.color = Color.green;
                idCheckIcon.text = "O";
                idCheckIcon.color = Color.green;
            }
            else
            {
                idCheckMessage.text = response.message; 
                idCheckMessage.color = Color.red;
                idCheckIcon.text = "X";
                idCheckIcon.color = Color.red;
            }
        }
    }

    [System.Serializable]
    private class UsernameResponse
    {
        public bool success; 
        public string message; 
    }
}



