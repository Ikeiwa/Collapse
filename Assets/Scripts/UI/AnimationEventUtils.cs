using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventUtils : MonoBehaviour
{
    public void SetActive(int active)
    {
        gameObject.SetActive(active != 0);
    }
}
