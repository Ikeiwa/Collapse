using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    private void OnEnable()
    {
        for (int i = 0; i <= LevelManager.instance.maxObtainLevel && i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}
