using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image DashIcon;
    public Image JumpIcon;

    public void SetAbilitiesActive()
    {
        DashIcon.GetComponent<SpriteRenderer>().color = new Color(FFFFFF);
        JumpIcon.GetComponent<SpriteRenderer>().color = new Color(FFFFFF);
    }

    public void SetAbilitiesInactive()
    {
        DashIcon.GetComponent<SpriteRenderer>().color = new Color(313131);
        JumpIcon.GetComponent<SpriteRenderer>().color = new Color(313131);
    }
}
