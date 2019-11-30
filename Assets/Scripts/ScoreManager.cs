using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text BalloonText;
    public Text TimeText;
    public Text ComboText;
    public Text TotalScoreText;

    public int balloonCount;
    public int comboCount;
    public int highestCombo;

    public float timeSurvived;

    private int highScore;
    private int totalScore;
    private int balloonScore;
    private int comboScore;
    private int timeScore;

    private float balloonScoreMultiplier = 3.5f;
    private float timeScoreMultiplier = 1.2f;
    private float comboScoreMultiplier = 10.0f;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }

    private void Start()
    {
        CalculateScore();
    }

    public void CalculateScore()
    {
        balloonScore = (int)(balloonCount * balloonScoreMultiplier);
        timeScore = (int)(timeSurvived * timeScoreMultiplier);
        comboScore = (int)(comboCount * comboScoreMultiplier);
        totalScore = balloonScore + timeScore + comboScore;

        BalloonText.text = "Balloons Hit: " + balloonCount + " x " + balloonScoreMultiplier + " = " + balloonScore.ToString();
        TimeText.text = "Time Survived: " + timeSurvived + " x " + timeScoreMultiplier + " = " + timeScore.ToString();
        ComboText.text = "Highest Combo: " + comboCount + " x " + comboScoreMultiplier + " = " + comboScore.ToString();
        TotalScoreText.text = "TOTAL SCORE: " + totalScore.ToString();

    }

    public void SaveHighScore()
    {
        totalScore = highScore;
    }
}
