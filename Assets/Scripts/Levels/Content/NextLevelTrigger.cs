using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : AbstractContent
{
    public NextLevelTrigger(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        LevelManager.instance.NextLevel();
    }

}
