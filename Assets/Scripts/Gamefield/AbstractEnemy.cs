using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract interface for gamefield enemies.
/// This class doe NOT specify Update and FixedUpdate behavior.
/// </summary>
public abstract class AbstractEnemy : MonoBehaviour
{

    protected Gamefield gf = Gamefield.instance;

    public int health = 1;

    /// <summary>
    /// Damages this enemy. If it falls below 0hp, kills it.
    /// </summary>
    /// <param name="d">The amount of damage dealt</param>
    public void Damage(int d)
    {
        health -= d;
        if (health <= 0)
            Kill();
    }

    /// <summary>
    /// Destroys this enemy from the gamefield
    /// </summary>
    public void Kill()
    {
        gf.RemoveEnemy(this.gameObject);
        Destroy(gameObject);
    }

}
