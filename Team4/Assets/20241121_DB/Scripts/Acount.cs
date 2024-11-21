using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

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
    [SerializeField]
    private GameObject acountUI;

    private const string idckURL = "http://127.0.0.1/idck.php";
    private const string acountURL = "http://127.0.0.1/acount.php";
    private void Start()
    {
        acountet.interactable = false; //회원 가입 버튼 off
        Password.onValueChanged.AddListener(OnPasswordChanged);
        passwordcheck.onValueChanged.AddListener(OnPasswordCheckChanged);
        idck.onClick.AddListener(() => StartCoroutine(OnIDCoroutine(Username.text)));
        acountet.onClick.AddListener(() => StartCoroutine(acountCoroutine(Username.text, Password.text)));
        
    }
    private void FixedUpdate()
    {
        acounbtON();
    }
    private void acounbtON()
    {
        if (idckOX.color == Color.green && passwordCheckicon.color == Color.green && passwordMatcgicon.color == Color.green)
        {
            acountet.interactable = true;
            //아이콘들이 초록색이 다 되면 가입 버튼이 켜진다
        }
        else
        {
            acountet.interactable = false;
            //하나라도 빨간색이면 꺼진다
        }

    }

    private IEnumerator acountCoroutine(string username, string password)
    {
        Debug.Log($"{username},{password} 값 들어옴");

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (UnityWebRequest www = UnityWebRequest.Post(acountURL, form))
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
                
                    acountUI.SetActive(false);
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

            // 네트워크 오류 처리
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError($"요청 실패: {www.error}");
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log($"서버 응답 수신 - Response: {response}");

                if (response.Contains("사용 가능한 아이디입니다.")) //서버에서 이 문구가 나오면 실행 한다
                {
                    idckOX.color = Color.green;
                    idckOX.text = "O";
                    idckMessage.text = "사용이 가능한 아이디 입니다!";
                    idckMessage.color = Color.green;
                }
            }
        }
    }


    private void OnSignupClicked()
    {

        if (!IsPasswordMatch())//만약에 비밀번호가 일치하지 않다면
        {
            passwordMatchMessage.text = "비밀번호가 일치하지 않습니다.";
            passwordMatchMessage.color = Color.red;

            return;
        }

        if (!IsPassword(Password.text))
        {
            passwordMessage.text = "비밀번호는 영어, 숫자로 4~10자 이내로 입력하세요.";
            passwordMessage.color = Color.red;
            return;
        }

    }

    private void OnIdCheckID() //아이디 체크 함수
    {
        string username = Username.text.Trim(); //트림을 이용하여 보정

        if (string.IsNullOrEmpty(username))
        {
            idCheckMessage.text = "아이디를 입력하세요.";
            idCheckMessage.color = Color.red;
            idCheckIcon.text = "X";
            idCheckIcon.color = Color.red;
            return;
        }

    }

    private void OnPasswordChanged(string password) 
    {
        if (IsPassword(password))
        {
            passwordMessage.text = "사용 가능한 비밀번호입니다.";
            passwordMessage.color = Color.green;
            passwordCheckicon.text = "O";
            passwordCheckicon.color = Color.green;
        }
        else
        {
            passwordMessage.text = "비밀번호는 영어와 숫자로 4~10자 이내로 입력하세요.";
            passwordMessage.color = Color.red;
            passwordCheckicon.text = "X";
            passwordCheckicon.color = Color.red;
        }
    }

    private void OnPasswordCheckChanged(string confirmPassword)
    {
        if (IsPasswordMatch())
        {
            passwordMatchMessage.text = "비밀번호가 일치합니다.";
            passwordMatchMessage.color = Color.green;
            passwordMatcgicon.text = "O";
            passwordMatcgicon.color = Color.green;
        }
        else
        {
            passwordMatchMessage.text = "비밀번호가 일치하지 않습니다.";
            passwordMatchMessage.color = Color.red;
            passwordMatcgicon.text = "X";
            passwordMatcgicon.color = Color.red;
        }
    }

    private bool IsPassword(string password) //정규표현식으로 배열의 길이와 영어,숫자의 입력만 받는다
    {
        if (password.Length < 4 || password.Length > 10) //배열의 길이 설정
        {
            return false;
        }
        Regex regex = new Regex("^[a-zA-Z0-9]+$"); //숫자와 영어만 입력 받음
        return regex.IsMatch(password);
        
    }

    private bool IsPasswordMatch() //비밀번호 텍스트가 같은지 확인
    {
        return Password.text == passwordcheck.text;
    }
}
