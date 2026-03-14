using UnityEngine;
using UnityEngine.SceneManagement;

// 역할: 씬 전환 및 선택한 스테이지 인덱스 PlayerPrefs 저장만 담당
public static class StageLoader
{
    public static void LoadStage(int stageIndex)
    {
        PlayerPrefs.SetInt("SelectedStage", stageIndex);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("InGame");
    }

    public static void LoadLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }

    public static int GetSelectedStage() => PlayerPrefs.GetInt("SelectedStage", 0);
}
