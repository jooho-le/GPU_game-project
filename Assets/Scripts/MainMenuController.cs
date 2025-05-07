using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void OnLoginButtonClicked()
    {
        Debug.Log("로그인 버튼이 눌렸습니다!");
    }
}
