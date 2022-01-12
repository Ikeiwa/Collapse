using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes a path taken by an ingame entity
/// </summary>
public class EntityPath
{
    /// <summary>
    /// Anchor for this path. Locations contained are relative to this point.
    /// </summary>
    public readonly Vector3 anchor;

    public readonly List<PathingLocation> content = new List<PathingLocation>(20);

    public EntityPath(Vector3 anchor)
    {
        this.anchor = anchor;
    }

    /// <summary>
    /// Chain tails a pathing location to this path
    /// </summary>
    /// <param name="x">The X position of the pathing destination</param>
    /// <param name="z">The Z position of the pathing destination</param>
    /// <param name="time">The expected time at which an entity following this path should be at this point</param>
    /// <returns>A chaining pointer to this object</returns>
    public EntityPath Chain(float x, float z, float time)
    {
        content.Add(new PathingLocation(x, z, time, this));
        return this;
    }

    public PathingLocation Get(int index) {
        return content[index];
    }

    public int Size() {
        return content.Count;
    }
}
