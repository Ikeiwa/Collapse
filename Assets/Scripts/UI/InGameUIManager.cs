using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{

    public static InGameUIManager instance;

    public RectTransform PowerSlider;
    public TextMeshProUGUI PowerValue;
    public RectTransform PowerupUI;
    public Image PowerupIcon;

    public Sprite PowerupIcon_Shield, PowerupIcon_Bomb, PowerupIcon_Jump;

    private void Awake()
    {
        instance = this;
    }

    public void SetPowerup(PowerUp type)
    {
        Debug.Log("Setting UI powerup to : " + type);
        switch (type)
        {
            default:
            case PowerUp.None:
                PowerupIcon.enabled = false;
                PowerupUI.GetComponent<Animator>().SetBool("ON",false);
                break;
            case PowerUp.Jump:
                PowerupIcon.sprite = PowerupIcon_Jump;
                PowerupIcon.enabled = true;
                PowerupUI.GetComponent<Animator>().SetBool("ON", true);
                break;
            case PowerUp.Bomb:
                PowerupIcon.sprite = PowerupIcon_Bomb;
                PowerupIcon.enabled = true;
                PowerupUI.GetComponent<Animator>().SetBool("ON", true);
                break;
            case PowerUp.Shield:
                PowerupIcon.sprite = PowerupIcon_Shield;
                PowerupIcon.enabled = true;
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
            PowerValue.text = s.value + "%";
        }
    }

}
