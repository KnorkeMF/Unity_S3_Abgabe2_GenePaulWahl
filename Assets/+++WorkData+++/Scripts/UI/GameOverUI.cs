using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    public GameObject panel;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(int score)
    {
        panel.SetActive(true);
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + ScoreManager.Instance.highscore;
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

