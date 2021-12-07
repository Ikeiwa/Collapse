using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelIntroText : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;

    private void Awake()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayText(string levelName, int distance)
    {
        StartCoroutine(TextDisplayAnim(levelName, distance));
    }

    private IEnumerator TextDisplayAnim(string levelName, int distance)
    {
        textDisplay.text = "";
        textDisplay.enabled = true;

        string text = "";
        string distanceText = "";
        for (int i = 0; i < levelName.Length; i++)
        {
            text += levelName[i];
            textDisplay.text = text;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return new WaitForSecondsRealtime(0.5f);

        distanceText = "\n0KM";
        float incTimer = 0;
        while (incTimer<2)
        {
            distanceText = "\n"+(int) Mathf.Lerp(0, distance, incTimer / 2.0f) + "KM";
            incTimer += Time.deltaTime;
            textDisplay.text = text+distanceText;
            yield return null;
        }
        distanceText = "\n" + distance + "KM";
        textDisplay.text = text + distanceText;

        yield return new WaitForSecondsRealtime(2f);
        textDisplay.enabled = false;
    }
}
