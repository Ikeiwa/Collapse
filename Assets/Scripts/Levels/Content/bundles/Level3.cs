using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : LevelContent
{
    public Level3()
    {
        content.Add(new NextLevelTrigger(10f));
    }
}
