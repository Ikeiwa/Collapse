using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPowerup : MonoBehaviour
{
    protected Gamefield gf = Gamefield.instance;
    public static readonly Quaternion QUATERNION_DOWN = AbstractBullet.QUATERNION_DOWN;

    private float speed = -0.3f;
    private bool homingTrigger = false;

    private static readonly float homingspeed = 0.5f, homingrange = 6f;

    private void FixedUpdate()
    {
        if (transform.position.z <= -2f)
        {
            Kill();
        }
        else
        {
            float playerX = gf.player.transform.position.x;
            Debug.Log("Player X : " + playerX + " / powerup Z : " + transform.position.z + " / distance : " + Mathf.Abs(playerX - transform.position.x));
            if (!homingTrigger && transform.position.z <= homingrange && Mathf.Abs(playerX - transform.position.x) < homingrange)
            {
                homingTrigger = true;
                Debug.Log("AYAYA");
            }
            if (homingTrigger)
            {
                transform.position = new Vector3(transform.position.x > playerX ? transform.position.x - homingspeed : transform.position.x + homingspeed, transform.position.y, transform.position.z / 2);
            }
            else
            {
                speed += 0.015f;
                transform.Translate(Vector3.forward * speed);
            }
        }
    }

    /// <summary>
    /// Event triggered when this powerup collides with something. 
    /// Due to the way layers are organized, this is only on the player layer.
    /// </summary>
    void OnTriggerEnter(Collider collision)
    {
        OnCollect();
        Kill();
    }

    /// <summary>
    /// Event triggered when this powerup is collected.<br/>
    /// Should be overriden by each specific powerup to do something.
    /// </summary>
    protected virtual void OnCollect()
    {
    }

    /// <summary>
    /// Destroys this powerup entity from the gamefield
    /// </summary>
    public void Kill()
    {
        gf.RemovePowerup(this.gameObject);
        Destroy(gameObject);
    }


}
