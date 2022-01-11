using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelContent
{
    public List<AbstractContent> content = new List<AbstractContent>(128);

    /// <summary>
    /// Trigger all events that haven't been triggered yet, given the current time.
    /// </summary>
    /// <param name="time">The current time in the level, in seconds.</param>
    public void Trigger(float time) {
        Debug.Log("Levelcontent trigger for time : " + time);
        // TODO : this
    }

}
