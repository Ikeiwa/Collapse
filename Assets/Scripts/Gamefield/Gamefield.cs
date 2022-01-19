using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for the game area having entities with complex behavior in front of the player. <br/>
/// This object is static in world space.
/// </summary>
public class Gamefield : MonoBehaviour
{
    /// <summary>
    /// Contains a pointer to the last awaken gamefield instance.
    /// </summary>
    public static Gamefield instance;

    public GameObject PREFAB_Enemy_Basic, PREFAB_Enemy_CreepSingle, PREFAB_Enemy_WallingEnemy;
    public GameObject PREFAB_Shot_LinearSmall, PREFAB_Shot_Walling;
    public GameObject PREFAB_Shot_Ally, PREFAB_Shot_MissileAlly;
    public GameObject PREFAB_Powerup_Shield, PREFAB_Powerup_Bomb, PREFAB_Powerup_Jump, PREFAB_Powerup_Power;
    
    public GameObject anchorBackLeft, anchorBackRight;
    [Space]
    public GameObject player;

    /// <summary>
    /// Short access copies to know the gamefield location in world space.<br/>
    /// Equivalent to <code>anchorBackLeft.transform.position.x</code>but this value is fixed on awake and does not move.
    /// </summary>
    [System.NonSerialized]
    public float xmin = 0, xmax = 0, zmin = 0, zmax = 0;

    private readonly List<GameObject> content_enemies = new List<GameObject>(30);
    private readonly List<GameObject> content_projectiles = new List<GameObject>(200);
    private readonly List<GameObject> content_powerups = new List<GameObject>(10);
    private readonly List<GameObject> content_projectilesAlly = new List<GameObject>(100);
    private readonly List<GameObject> content_obstacles = new List<GameObject>(30);

    void Awake()
    {
        instance = this;
        xmin = anchorBackLeft.transform.position.x;
        xmax = anchorBackRight.transform.position.x;
        zmin = player.transform.position.z;
        zmax = anchorBackRight.transform.position.z;
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
    }

    // Add list elements
    public void AddEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        content_enemies.Add(Instantiate(prefab, position, rotation, transform));
    }
    public void AddProjectile(GameObject prefab, Vector3 position, Quaternion rotation, BulletArguments args)
    {
        GameObject inst = Instantiate(prefab, position, rotation, transform);
        AbstractBullet bullet = inst.GetComponent<AbstractBullet>();
        if (bullet != null) bullet.OnSpawn(args);
        content_projectiles.Add(inst);
    }
    public void AddProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        AddProjectile(prefab, position, rotation, BulletArguments.NONE);
    }
    public void AddPowerup(GameObject prefab, Vector3 position)
    {
        content_powerups.Add(Instantiate(prefab, position, AbstractPowerup.QUATERNION_DOWN, transform));
    }
    public void AddProjectileAlly(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        content_projectilesAlly.Add(Instantiate(prefab, position, rotation, transform));
    }
    public void AddObstacle(float position, float time = 5)
    {
        content_obstacles.Add(LevelManager.instance.SpawnObstacleRandom(position, time).gameObject);
    }
    // Remove list elements
    public void RemoveEnemy(GameObject g)
    {
        content_enemies.Remove(g);
    }
    public void RemoveProjectile(GameObject g)
    {
        content_projectiles.Remove(g);
    }
    public void RemovePowerup(GameObject g)
    {
        content_powerups.Remove(g);
    }
    public void RemoveProjectileAlly(GameObject g)
    {
        content_projectilesAlly.Remove(g);
    }
    public void RemoveObstacle(GameObject g)
    {
        content_obstacles.Remove(g);
    }


    private static readonly float OOBthreshold = 3.5f;

    /// <summary>
    /// Predicate that tests if the bounds of an object are within the game field.
    /// Only tests horizontal space, this has no vertical killplane.
    /// </summary>
    /// <param name="t"></param>
    /// <returns>True if the object is out of bounds by more than a threshold.</returns>
    public bool IsOOB(Transform t)
    {
        return t.position.x < xmin - OOBthreshold || t.position.x > xmax + OOBthreshold
            || t.position.z < zmin - OOBthreshold * 2 || t.position.z > zmax + OOBthreshold * 2;
    }
}
