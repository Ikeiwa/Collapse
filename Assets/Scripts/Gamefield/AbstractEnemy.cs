using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract interface for gamefield enemies.
/// </summary>
public abstract class AbstractEnemy : MonoBehaviour
{

    protected Gamefield gf = Gamefield.instance;

    public int health = 1;
    /// <summary>
    /// Length of the landing animation, in seconds
    /// </summary>
    public float landingtime = 0.3f;
    /// <summary>
    /// Locale timer. Is always equal to how long this entity has been existing for.
    /// </summary>
    protected float timeLocale = 0f;

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
    public virtual void OnKill()
    {
    }


    private float StartingHeight = -1f;
    /// <summary>
    /// Computes the landing animation
    /// </summary>
    protected virtual void FixedUpdate()
    {
        timeLocale += Time.fixedDeltaTime;
        float height = transform.position.y;

        if (height != 0)
            if (Mathf.Approximately(height, 0))
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            else
            {
                if (StartingHeight == -1f)
                    StartingHeight = transform.position.y;
                if (timeLocale >= landingtime) { 
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                } else {
                    float newHeight =  (1 - Easing.Quadratic.Out(timeLocale / landingtime)) * StartingHeight + 0.05f;
                    transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
                }
            }

    }

    /// <summary>
    /// Predicate that computes if the player has finished his landing animation
    /// </summary>
    /// <returns>True if the enemy has landed.</returns>
    public bool Grounded() {
        return timeLocale >= landingtime;
    }

}
