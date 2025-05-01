using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Popup Panels")]
    public GameObject loginPopup;
    public GameObject signupPopup;

    [Header("Login Fields")]
    public TMP_InputField loginIdInput;
    public TMP_InputField loginPwInput;

    [Header("Signup Fields")]
    public TMP_InputField signupIdInput;
    public TMP_InputField signupPwInput;
    public TMP_InputField signupPwConfirmInput;

    [Header("Top UI")]
    public TextMeshProUGUI userIdText;

    [Header("Other")]
    public APIManager apiManager; // 로그인 기능 처리 스크립트 연결용

    private void Start()
    {
        CloseAllPopups();
        userIdText.text = "";
    }

    public void ShowLoginPopup()
    {
        CloseAllPopups();
        loginPopup.SetActive(true);
    }

    public void ShowSignupPopup()
    {
        CloseAllPopups();
        signupPopup.SetActive(true);
    }

    public void CloseAllPopups()
    {
        loginPopup.SetActive(false);
        signupPopup.SetActive(false);
    }

    public void OnClickLogin()
    {
        string id = loginIdInput.text;
        string pw = loginPwInput.text;

        apiManager.Login(id, pw, (success) =>
        {
            if (success)
            {
                userIdText.text = "👤 " + id;
                CloseAllPopups();
            }
        });
    }

    public void OnClickSignup()
    {
        string id = signupIdInput.text;
        string pw = signupPwInput.text;
        string pwConfirm = signupPwConfirmInput.text;

        if (pw != pwConfirm)
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            return;
        }

        apiManager.Signup(id, pw, (success) =>
        {
            if (success)
            {
                Debug.Log("회원가입 성공!");
                CloseAllPopups();
            }
        });
    }
}
