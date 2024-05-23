using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    private int highScore;
    public Text resultTime;
    public GameObject resultUI;

    void Start()
    {
        resultUI.SetActive(false);
    }

    void Update()
    {
        if (Goal.goal)
        {
            resultUI.SetActive(true);
            int result = Mathf.FloorToInt(Timer.time);

            int minutes = result / 60;
            int seconds = result % 60;

            resultTime.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
    }

    public void OnRetry()
    {
        Goal.goal = false;
        Timer.time = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
