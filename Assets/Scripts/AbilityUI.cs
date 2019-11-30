using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{

    public Text BalloonTextUI;
    public Text TimeTextUI;

    public Image DashIcon;
    public Image JumpIcon;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }

    public void SetAbilitiesActive()
    {
        DashIcon.GetComponent<Image>().color = Color.white;
        JumpIcon.GetComponent<Image>().color = Color.white;
    }

    public void SetAbilitiesInactive()
    {
        //(313131)
        DashIcon.GetComponent<Image>().color = Color.gray;
        JumpIcon.GetComponent<Image>().color = Color.gray;
    }

    public void UpdateBalloonCount(int balloonCount)
    {
        BalloonTextUI.text = "Balloons Hit: " + balloonCount;
    }
}
