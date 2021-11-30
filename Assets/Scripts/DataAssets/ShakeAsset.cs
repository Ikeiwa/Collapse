using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Preset", menuName = "Collapse/Shake Preset", order = 1)]
public class ShakeAsset : ScriptableObject
{
    public float duration;
    public float magnitude;
    public float damping;
    public AnimationCurve fadeCurve;

    public ShakeAsset()
    {
        fadeCurve = AnimationCurve.Linear(0,1,1,0);
    }
}
