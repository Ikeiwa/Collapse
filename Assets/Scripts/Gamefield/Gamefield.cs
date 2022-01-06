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

    public GameObject PREFAB_Enemy_Basic;

    public GameObject PREFAB_Shot_LinearSmall;
    public GameObject PREFAB_Shot_Ally;

    public GameObject anchorBackLeft, anchorBackRight;

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
    private readonly List<GameObject> content_obstacles = new List<GameObject>(100);

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
        AddEnemy(PREFAB_Enemy_Basic, anchorBackLeft.transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
    }

   // Add list elements
    public void AddEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        content_enemies.Add(Instantiate(prefab, position, rotation, transform));
    }
    public void AddProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        content_projectiles.Add(Instantiate(prefab, position, rotation, transform));
    }
    public void AddPowerup(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        content_powerups.Add(Instantiate(prefab, position, rotation, transform));
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


    private static readonly float OOBthreshold = 1.1f;

    /// <summary>
    /// Predicate that tests if the bounds of an object are within the game field.
    /// Only tests horizontal space, this has no vertical killplane.
    /// </summary>
    /// <param name="t"></param>
    /// <returns>True if the object is out of bounds by more than a threshold.</returns>
    public bool IsOOB(Transform t)
    {
        return t.position.x < xmin - OOBthreshold || t.position.x > xmax + OOBthreshold
            || t.position.z < zmin - OOBthreshold || t.position.z > zmax + OOBthreshold;
    }
}
