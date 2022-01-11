using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelContent
{
    public Level2()
    {
        content.Add(new HObstacles(3f));
        content.Add(new HObstacles(6.5f));
        content.Add(new NextLevelTrigger(10f));
    }
}
