using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurveLibrary
{
    public static AnimationCurve linear = AnimationCurve.Linear(0, 0, 1, 1);
    public static AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public static AnimationCurve easeIn =
        new AnimationCurve(new[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 2, 2) });

    public static AnimationCurve easeOut =
        new AnimationCurve(new[] { new Keyframe(0, 0, 2, 2), new Keyframe(1, 1, 0, 0) });
}
