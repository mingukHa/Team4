using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Acount : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField Username;
    [SerializeField]
    private TMP_InputField Password;
    [SerializeField]
    private TMP_InputField passwordcheck;
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
    [SerializeField]
    private Button idck;
    [SerializeField]
    private TextMeshProUGUI idckOX;
    [SerializeField]
    private TextMeshProUGUI idckMessage;
    [SerializeField]
    private Button acountet;

    private const string idckURL = "http://127.0.0.1/idck.php";
    private const string acountURL = "http://127.0.0.1/acount.php";
    private void Start()
    {
        Password.onValueChanged.AddListener(OnPasswordChanged);
        passwordcheck.onValueChanged.AddListener(OnPasswordCheckChanged);
        idck.onClick.AddListener(() => StartCoroutine(OnIDCoroutine(Username.text)));
        acountet.onClick.AddListener(() => StartCoroutine(acount(Username.text, Password.text)));
    }
    private IEnumerator acount(string username, string password)
    {
        Debug.Log($"{username},{password} �� ����");

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (UnityWebRequest www = UnityWebRequest.Post(acountURL, form))
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

            }
        }
    }
        private IEnumerator OnIDCoroutine(string username)
    {
        Debug.Log($"Username: {username}");

        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(idckURL, form))
        {
            yield return www.SendWebRequest();

            // ��Ʈ��ũ ���� ó��
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError($"��û ����: {www.error}");
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log($"���� ���� ���� - Response: {response}");

                if (response.Contains("��� ������ ���̵��Դϴ�.")) //�������� �� ������ ������ ���� �Ѵ�
                {
                    idckOX.color = Color.green;
                    idckOX.text = "O";
                    idckMessage.text = "����� ������ ���̵� �Դϴ�!";
                    idckMessage.color = Color.green;
                }
            }
        }
    }


    private void OnSignupClicked()
    {

        if (!IsPasswordMatch())
        {
            passwordMatchMessage.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            passwordMatchMessage.color = Color.red;

            return;
        }

        if (!IsPasswordValid(Password.text))
        {
            passwordMessage.text = "��й�ȣ�� ����, ���ڷ� 4~10�� �̳��� �Է��ϼ���.";
            passwordMessage.color = Color.red;
            return;
        }

    }

    private void OnIdCheckID()
    {
        string username = Username.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            idCheckMessage.text = "���̵� �Է��ϼ���.";
            idCheckMessage.color = Color.red;
            idCheckIcon.text = "X";
            idCheckIcon.color = Color.red;
            return;
        }

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
        return Password.text == passwordcheck.text;
    }
}
