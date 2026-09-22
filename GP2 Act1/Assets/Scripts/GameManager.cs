using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score = 0;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();

        Debug.Log("Score: " + score);
    }

    public void DeductScore(int value)
    {
        score -= value;
        UpdateScoreUI();

        Debug.Log("Score: " + score);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }
    }
}