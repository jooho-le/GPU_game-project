using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class APIManager : MonoBehaviour
{
    private string serverUrl = "http://localhost:3000"; // 서버 주소에 맞게 수정해줘

    // ✅ 외부에서 호출하는 로그인 함수
    public void Login(string id, string pw, System.Action<bool> callback)
    {
        StartCoroutine(LoginRequest(id, pw, callback));
    }

    // ✅ 외부에서 호출하는 회원가입 함수
    public void Signup(string id, string pw, System.Action<bool> callback)
    {
        StartCoroutine(SignupRequest(id, pw, callback));
    }

    // 🔐 로그인 요청 처리
    IEnumerator LoginRequest(string id, string pw, System.Action<bool> callback)
    {
        string jsonData = JsonUtility.ToJson(new LoginData(id, pw));
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(serverUrl + "/login", "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ 로그인 성공: " + request.downloadHandler.text);
            callback?.Invoke(true);
        }
        else
        {
            Debug.LogError("❌ 로그인 실패: " + request.error);
            callback?.Invoke(false);
        }
    }

    // ✍️ 회원가입 요청 처리
    IEnumerator SignupRequest(string id, string pw, System.Action<bool> callback)
    {
        string jsonData = JsonUtility.ToJson(new LoginData(id, pw));
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(serverUrl + "/signup", "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ 회원가입 성공: " + request.downloadHandler.text);
            callback?.Invoke(true);
        }
        else
        {
            Debug.LogError("❌ 회원가입 실패: " + request.error);
            callback?.Invoke(false);
        }
    }

    // 로그인/회원가입 데이터 구조
    [System.Serializable]
    public class LoginData
    {
        public string id;
        public string password;

        public LoginData(string id, string password)
        {
            this.id = id;
            this.password = password;
        }
    }
}
