using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletMathUtils
{

    /// <summary>
    /// Returns a point on a circle with the given radius and offset angle.
    /// </summary>
    /// <param name="center">The center of the circle</param>
    /// <param name="radius">The radius of the circle, giving how far the returned point will be from the center</param>
    /// <param name="angle">the angle at which the returned point generates. 0 is on top, 90 on the right</param>
    /// <returns></returns>
    public static Vector3 getCirclePos(Vector3 center,float radius, float angle) {
        return new Vector3(Mathf.Cos(angle) * radius + center.x , center.y, Mathf.Sin(angle) * radius + center.z);
    }

}
