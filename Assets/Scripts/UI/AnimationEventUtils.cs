using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationEventUtils : MonoBehaviour
{
    public GameObject defaultSelectedElement;

    public void SetActive(int active)
    {
        gameObject.SetActive(active != 0);
    }

    public void SetSelectedElement()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelectedElement);
    }
}
