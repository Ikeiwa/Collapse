using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Element composiong an entity path. Each location has a position and an expected time.
/// </summary>
public class PathingLocation
{

    /// <summary>
    /// Position of the pathing location
    /// </summary>
    public readonly float x, z;
    /// <summary>
    /// Time at which the pathfinding entity should be at this location
    /// </summary>
    public readonly float time;
    private EntityPath parent;

    public PathingLocation(float x, float z, float timelocale, EntityPath parent)
    {
        this.x = x;
        this.z = z;
        this.time = timelocale;
        this.parent = parent;
    }

    private bool bufferset_locale = false;
    private Vector3 posBufferLocale;
    /// <returns>The position of this location in vec3 space. This method returns an immutable buffered response.</returns>
    public Vector3 GetPositionLocale()
    {
        if (bufferset_locale)
            return posBufferLocale;
        posBufferLocale = new Vector3(x, 0, z);
        bufferset_locale = true;
        return posBufferLocale;
    }

    private bool bufferset_global = false;
    private Vector3 posBufferGlobal;
    /// <returns>The position of this location in vec3 global space (relative to the gamefield location, which is world pos if the gamefield is at 0,0,0). 
    /// This method returns an immutable buffered response.</returns>
    public Vector3 GetPositionGlobal()
    {
        if (bufferset_global)
            return posBufferGlobal;
        posBufferGlobal = new Vector3(x + parent.anchor.x, parent.anchor.y, z + parent.anchor.z);
        bufferset_global = true;
        return posBufferGlobal;
    }

}
