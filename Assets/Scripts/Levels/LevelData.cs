using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Collapse/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public string name = "Level";
    public int distance = 1000;
    public float duration = 120;
    public AudioClip music;

    public GameObject startTile;
    public GameObject[] tiles;

    public ObstacleBase[] obstacles;
    public float[] obstaclesChance;

    //public EnemyBase[] enemies;
    public float[] enemyChance;
}
