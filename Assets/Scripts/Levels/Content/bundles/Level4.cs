using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : LevelContent
{
    public Level4()
    {
        content.Add(new NextLevelTrigger(10f));
    }
}
