using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{

    public static InGameUIManager instance;

    public RectTransform PowerSlider;
    public RectTransform PowerupUI;

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

                break;
            case PowerUp.Jump:

                break;
            case PowerUp.Bomb:

                break;
            case PowerUp.Shield:

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
