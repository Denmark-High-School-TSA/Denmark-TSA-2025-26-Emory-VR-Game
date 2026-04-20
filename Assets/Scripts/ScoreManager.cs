using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int humanScore = 0;
    public int alienScore = 0;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScoreHuman(int points)
    {
        humanScore += points;
        UpdateScoreUI();
    }

    public void AddScoreAlien(int points)
    {
        alienScore += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = humanScore.ToString() + " - " + alienScore.ToString();
        }       
    }
}