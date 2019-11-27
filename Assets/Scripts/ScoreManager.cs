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

    public float timeSurvived;

    private int highScore;
    private int balloonScore;
    private int comboScore;
    private int timeScore;
    private int highestCombo;

    public void CalculateScore()
    {
        balloonScore = balloonCount * 3;
        BalloonText.text = ToString(balloonScore);
        TimeText.text = ToString(comboScore);
        ComboText.text = ToString(timeScore);
        high

    }

    public void SaveHighScore()
    {

    }
}
