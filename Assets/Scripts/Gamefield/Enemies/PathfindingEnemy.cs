using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract interface for all pathfinding enemies
/// </summary>
public abstract class PathfindingEnemy : AbstractEnemy
{

    protected EntityPath path;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
