using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{

    public static InGameUIManager instance;

    public RectTransform PowerSlider;
    public RectTransform PowerupUI;

    public Sprite PowerupIcon_Shield, PowerupIcon_Bomb, PowerupIcon_Jump;

    private void Awake()
    {
        instance = this;
    }

    public void SetPowerup(PowerUp type)
    {
        Debug.Log("Setting UI powerup to : " + type);
        RectTransform icontr = (RectTransform) PowerupUI.GetChild(0);
        switch (type)
        {
            default:
            case PowerUp.None:
                icontr.GetComponent<Image>().enabled = false;
                PowerupUI.GetComponent<Animator>().SetBool("ON",false);
                break;
            case PowerUp.Jump:
                icontr.GetComponent<Image>().sprite = PowerupIcon_Jump;
                icontr.GetComponent<Image>().enabled = true;
                PowerupUI.GetComponent<Animator>().SetBool("ON", true);
                break;
            case PowerUp.Bomb:
                icontr.GetComponent<Image>().sprite = PowerupIcon_Bomb;
                icontr.GetComponent<Image>().enabled = true;
                PowerupUI.GetComponent<Animator>().SetBool("ON", true);
                break;
            case PowerUp.Shield:
                icontr.GetComponent<Image>().sprite = PowerupIcon_Shield;
                icontr.GetComponent<Image>().enabled = true;
                PowerupUI.GetComponent<Animator>().SetBool("ON", true);
                break;
        }
    }

    public void SetPower(int power)
    {
        Debug.Log("Setting UI power bar to : " + power);
        Slider s = PowerSlider.GetComponent<Slider>();
        if (s != null)
        {
            s.value = Mathf.Clamp(power, 0, 100);
        }
    }

}
