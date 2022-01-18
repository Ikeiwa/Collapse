using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : LevelContent
{
    public Level5()
    {
        //content.Add(new RObstacle1(0.2f));
        //content.Add(new RObstacle4(0.2f));

        content.Add(new SpawnCreepLeft(0.5f));
    }
}
