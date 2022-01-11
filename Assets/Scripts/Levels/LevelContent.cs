using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelContent
{
    // TODO: use a linkedlist here, perf gains would be massive
    public List<AbstractContent> content = new List<AbstractContent>(128);

    /// <summary>
    /// Trigger all events that haven't been triggered yet, given the current time.
    /// </summary>
    /// <param name="time">The current time in the level, in seconds.</param>
    public void Trigger(float time) {

        if (content.Count == 0) return;

        if (content[0].spawnTime <= time) {
            content[0].OnTick();
            content.RemoveAt(0);
        }
    
    }

}
