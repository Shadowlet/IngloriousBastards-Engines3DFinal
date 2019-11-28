using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public ScoreManager scoreManager;

    public Text BalloonTextUI;

    public Image DashIcon;
    public Image JumpIcon;

    public void SetAbilitiesActive()
    {
        DashIcon.GetComponent<SpriteRenderer>().color = Color.white;
        JumpIcon.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetAbilitiesInactive()
    {
        //(313131)
        DashIcon.GetComponent<SpriteRenderer>().color = Color.gray;
        JumpIcon.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void UpdateBalloonCount(int balloonCount)
    {
        BalloonTextUI.text = "Balloons Hit: " + balloonCount;
    }
}
