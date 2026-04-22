using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필수!

public class MainMenu : MonoBehaviour
{
    // 게임 시작 버튼을 눌렀을 때 실행될 함수
    public void PlayGame()
    {
        // "Stage1" 대신 본인이 만든 실제 1스테이지 씬 이름을 넣으세요.
        SceneManager.LoadScene("Level_1"); 
        
        // 또는 인덱스 번호를 써도 됩니다.
        // SceneManager.LoadScene(1);
    }

    // 게임 종료 버튼을 눌렀을 때 실행될 함수
    public void QuitGame()
    {
        Debug.Log("게임 종료!"); // 에디터에서는 꺼지지 않으므로 로그로 확인
        Application.Quit();     // 실제 빌드된 게임에서는 종료됨
    }
}