using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable content element that happens in a level. 
/// </summary>
public abstract class AbstractContent
{
    public readonly float spawnTime;

    public AbstractContent(float spawntime) {
        this.spawnTime = spawntime;
    }

    public virtual void OnTick() { 
        
    }
}
