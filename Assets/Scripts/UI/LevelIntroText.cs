using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;


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
            if(distance == 0)
                distanceText = "\n"+Random.Range(0,1000000000) + "KM";
            else
                distanceText = "\n" + (int)Mathf.Lerp(0, distance, incTimer / 2.0f) + "KM";
            incTimer += Time.deltaTime;
            textDisplay.text = text+distanceText;
            yield return null;
        }
        if(distance == 0)
            distanceText = "\n" + "100000000000000000000000000000000000000" + "KM";
        else
            distanceText = "\n" + distance + "KM";
        textDisplay.text = text + distanceText;

        text += distanceText;

        yield return new WaitForSecondsRealtime(1f);

        for (int i = text.Length-1; i >= 0; i--)
        {
            textDisplay.text = text.Substring(0,i);
            yield return new WaitForSecondsRealtime(0.02f);
        }

        yield return new WaitForSecondsRealtime(2f);
        textDisplay.enabled = false;
    }
}
