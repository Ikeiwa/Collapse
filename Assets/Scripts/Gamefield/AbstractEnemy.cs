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
    /// This may be overriden by more complex enemies for better damage control.
    /// </summary>
    /// <param name="d">The amount of damage dealt</param>
    public virtual void Damage(int d)
    {
        health -= d;
        if (health <= 0)
            Kill();
    }

    /// <summary>
    /// Destroys this enemy from the gamefield.
    /// Calling this method calls <code>OnKill()</code> on this entity first.
    /// </summary>
    public void Kill()
    {
        OnKill();
        gf.RemoveEnemy(this.gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// Event triggered on entity kill, may be overriten by specific implementations. 
    /// </summary>
    public virtual void OnKill() { 
    }

}
