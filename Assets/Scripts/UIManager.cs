using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("팝업 오브젝트")]
    [SerializeField] GameObject loginPopup;      
    [SerializeField] GameObject signupPopup;     

    [Header("메인 메뉴 버튼")]
    [SerializeField] Button loginButton;        
    [SerializeField] Button signupButton;        
    [SerializeField] Button startGameButton;     

    [Header("로그인 팝업 필드")]
    [SerializeField] TMP_InputField loginIDInput;         
    [SerializeField] TMP_InputField loginPWInput;        
    [SerializeField] Button           loginSubmitButton;  
    [SerializeField] Button           closeLoginPopButton;

    [Header("회원가입 팝업 필드")]
    [SerializeField] TMP_InputField signupIDInput;           
    [SerializeField] TMP_InputField signupPWInput;            
    [SerializeField] TMP_InputField signupConfirmPWInput;     
    [SerializeField] Button           signupSubmitButton;    
    [SerializeField] Button           closeSignupPopupButton;

    [Header("유저 정보 텍스트")]
    [SerializeField] TMP_Text userIDText;     
    [SerializeField] TMP_Text stageStatusText; 

    APIManager api;

    void Awake()
    {
        api = FindObjectOfType<APIManager>();

        loginPopup.SetActive(false);
        signupPopup.SetActive(false);
        startGameButton.interactable = false;

        loginButton.onClick .AddListener(() => loginPopup.SetActive(true));
        signupButton.onClick.AddListener(() => signupPopup.SetActive(true));

        loginSubmitButton. onClick.AddListener(OnLoginSubmit);
        closeLoginPopButton.onClick.AddListener(() => loginPopup.SetActive(false));

        signupSubmitButton.     onClick.AddListener(OnSignupSubmit);
        closeSignupPopupButton.onClick.AddListener(() => signupPopup.SetActive(false));

        startGameButton.onClick.AddListener(() => Debug.Log("게임 시작"));
    }

    void OnLoginSubmit()
    {
        string email = loginIDInput.text;
        string pw    = loginPWInput.text;
        api.Login(email, pw, success =>
        {
            loginPopup.SetActive(false);
            if (success) RefreshUserInfo();
            else Debug.LogWarning("로그인 실패");
        });
    }

    void OnSignupSubmit()
    {
        if (signupPWInput.text != signupConfirmPWInput.text)
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            return;
        }

        string email = signupIDInput.text;
        string pw    = signupPWInput.text;
        api.Signup(email, pw, success =>
        {
            signupPopup.SetActive(false);
            if (success) RefreshUserInfo();
            else Debug.LogWarning("회원가입 실패");
        });
    }

    void RefreshUserInfo()
    {
        api.GetMyInfo(user =>
        {
            if (user == null) return;
            userIDText.text      = $"User ID: {user.username}";
            stageStatusText.text = $"Cleared Stage: {user.stageStatus}";
            startGameButton.interactable = true;
            loginButton .gameObject.SetActive(false);
            signupButton.gameObject.SetActive(false);
        });
    }
}
