using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Collapse/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public string displayName = "Level";
    public int distance = 1000;
    public float duration = 120;
    public AudioClip music;
    public Color skyColor;
    public Vector3 maxCurve;

    public LevelTile startTile;
    public LevelTile[] tiles;

    public ObstacleBase[] obstacles;
    public float[] obstaclesChance;

    //public EnemyBase[] enemies;
    public float[] enemyChance;

    public Vector3 GetRandomCurve()
    {
        return new Vector3(Random.Range(-maxCurve.x, maxCurve.x), Random.Range(-maxCurve.y, maxCurve.y), 0);
    }
}
