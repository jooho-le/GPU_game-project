
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class User {
    public string id;
    public string username;
    public string email;
    public int stageStatus;
}

[System.Serializable]
public class AuthResponse {
    public string token;
    public User user;
}

[System.Serializable]
public class SignupData {
    public string username;
    public string email;
    public string password;
    public SignupData(string username, string email, string password) {
        this.username = username;
        this.email    = email;
        this.password = password;
    }
}

[System.Serializable]
public class LoginData {
    public string email;
    public string password;
    public LoginData(string email, string password) {
        this.email    = email;
        this.password = password;
    }
}

[System.Serializable]
public class UserWrapper {
    public User user;
}

public class APIManager : MonoBehaviour
{
    [Header("API Base URL")]
    public string baseUrl = "http://localhost:4000/api";

    private string jwtToken;

    // 회원가입 
    public void Signup(string id, string pw, System.Action<bool> callback)
    {
        StartCoroutine(SignupRoutine(id, pw, callback));
    }

    private IEnumerator SignupRoutine(string id, string pw, System.Action<bool> callback)
    {
        var data = new SignupData(id, id, pw);
        string json = JsonUtility.ToJson(data);

        using var req = new UnityWebRequest($"{baseUrl}/auth/register", "POST");
        req.uploadHandler   = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success) {
            var resp = JsonUtility.FromJson<AuthResponse>(req.downloadHandler.text);
            jwtToken = resp.token;
            callback?.Invoke(true);
        }
        else {
            Debug.LogError($"[Signup] Error: {req.error}\n{req.downloadHandler.text}");
            callback?.Invoke(false);
        }
    }

    // 로그인 
    public void Login(string email, string password, System.Action<bool> callback)
    {
        StartCoroutine(LoginRoutine(email, password, callback));
    }

    private IEnumerator LoginRoutine(string email, string password, System.Action<bool> callback)
    {
        var data = new LoginData(email, password);
        string json = JsonUtility.ToJson(data);

        using var req = new UnityWebRequest($"{baseUrl}/auth/login", "POST");
        req.uploadHandler   = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success) {
            var resp = JsonUtility.FromJson<AuthResponse>(req.downloadHandler.text);
            jwtToken = resp.token;
            callback?.Invoke(true);
        }
        else {
            Debug.LogError($"[Login] Error: {req.error}\n{req.downloadHandler.text}");
            callback?.Invoke(false);
        }
    }

    // 내 정보 조회 
    public void GetMyInfo(System.Action<User> callback)
    {
        StartCoroutine(GetMeRoutine(callback));
    }

    private IEnumerator GetMeRoutine(System.Action<User> callback)
    {
        using var req = UnityWebRequest.Get($"{baseUrl}/users/me");
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Authorization", $"Bearer {jwtToken}");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success) {
            var wrapper = JsonUtility.FromJson<UserWrapper>(req.downloadHandler.text);
            callback?.Invoke(wrapper.user);
        }
        else {
            Debug.LogError($"[GetMyInfo] Error: {req.error}\n{req.downloadHandler.text}");
            callback?.Invoke(null);
        }
    }
}
