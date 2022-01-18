using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract interface for all pathfinding enemies
/// </summary>
public abstract class PathfindingEnemy : AbstractEnemy
{

    protected int followIndex = 0;
    protected EntityPath path = new EntityPath();

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (followIndex < path.Size())
        {
            PathingLocation target = path.Get(followIndex);
            Vector3 targetPoint = target.GetPositionGlobal();
            // Computes by how much this entity should move
            float framestogo = (1 / Time.fixedDeltaTime) * (target.time - timeLocale);
            Vector3 offset = (targetPoint - transform.position) / framestogo;
            transform.position = transform.position + offset;

            if (timeLocale >= target.time)
                followIndex++;

            Debug.Log("Target : " + targetPoint + "/nPosition : " + transform.position);
        }
    }

}
