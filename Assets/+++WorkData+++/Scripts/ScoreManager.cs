using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int highscore = 0;
    
    private const string HIGHSCORE_KEY = "Highscore";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadHighscore();
        }
        
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
    
    public void SaveHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highscore);
            PlayerPrefs.Save();
            Debug.Log("New Highscore Saved: " + highscore);
        }
    }

    private void LoadHighscore()
    {
        highscore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        Debug.Log("Loaded Highscore: " + highscore);
    }
}