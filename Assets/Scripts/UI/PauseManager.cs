using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Animator pauseAnim;
    private bool paused = false;
    private float timeSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!paused)
            {
                paused = true;
                pauseAnim.SetTrigger("Pause");
                timeSpeed = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                UnPause();
            }
        }
    }

    public void UnPause()
    {
        if (paused)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = timeSpeed;
            paused = false;
            pauseAnim.SetTrigger("Quit");
        }
    }
}
