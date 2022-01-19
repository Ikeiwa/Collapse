using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp
{
    None,
    Jump,
    Shield,
    Bomb
}

public class PlayerController : MonoBehaviour
{
    public GameObject dashTrail;
    public ParticleSystem jumpSmoke;

    public float maxSpeed = 5;
    public float accel = 10;
    public float maxDistance = 5;
    public float friction = 0.95f;
    public float dashChargeTime = 1f;
    public float dashDistance = 2;
    public float focusSpeedFactor = 0.25f;
    public float fireRate = 0.1f;
    public int power = 0;
    public float velocity { get; private set; }
    public float position { get; private set; }

    private bool canDash = true;
    private float dashTimer = 0;

    private float move = 0;
    private bool dash = false;
    private bool focused = false;

    private float nextFire = 0;
    private float nextMissile = 1f;

    private PowerUp powerUp = PowerUp.None;
    private float jump = 0;
    private bool shielded = false;

    private InterpolatedTransform interpolatedTransform;
    private Animator anim;
    private AudioSource audioSource;

    private static readonly int VelocityParam = Animator.StringToHash("Velocity");

    // Start is called before the first frame update
    void Awake()
    {
        interpolatedTransform = GetComponent<InterpolatedTransform>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void setPower(int power)
    {
        power = Mathf.Clamp(power, 0, 100);
        this.power = power;
        InGameUIManager pointer = InGameUIManager.instance;
        if (pointer != null)
        {
            pointer.SetPower(power);
        }
    }

    public void ObtainPowerup(PowerUp newPowerUp)
    {
        InGameUIManager uipointer = InGameUIManager.instance;
        if (uipointer != null)
        {
            // TODO: don't call this if the powerup is consumed immediately instead of being stored
            uipointer.SetPowerup(newPowerUp);
        }

        if (powerUp == PowerUp.None)
        {
            if (newPowerUp == PowerUp.Shield)
            {
                if (!shielded)
                    SetShieldState(true);
                return;
            }
            powerUp = newPowerUp;
        }
    }

    public void UsePowerup()
    {
        Debug.Log("Use Powerup");
        switch (powerUp)
        {
            case PowerUp.None: return;
            case PowerUp.Jump:
                if (jump == 0)
                {
                    jump = 1;
                    anim.SetBool("Jumping", true);
                }
                else return;
                break;
            case PowerUp.Shield:
                if (!shielded)
                {
                    SetShieldState(true);
                }
                else return;
                break;
            case PowerUp.Bomb:

                break;
        }

        powerUp = PowerUp.None;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("PowerUp"))
        {
            UsePowerup();
        }

        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }

        focused = Input.GetButton("Focus");

        if (Input.GetKeyDown(KeyCode.Keypad1))
            ObtainPowerup(PowerUp.Jump);

        if (Input.GetKeyDown(KeyCode.Keypad2))
            ObtainPowerup(PowerUp.Shield);

        anim.SetFloat(VelocityParam, velocity);
    }

    private void FixedUpdate()
    {
        float playerSpeed = maxSpeed * (focused ? focusSpeedFactor : 1);

        velocity += -velocity * friction * Time.fixedDeltaTime;
        velocity += move * Time.fixedDeltaTime * accel;
        velocity = Mathf.Clamp(velocity, -playerSpeed, playerSpeed);

        float dashOffset = 0;

        if (dash && Mathf.Abs(move) > 0.5f && canDash)
        {
            dash = false;
            canDash = false;
            dashTimer = dashChargeTime;
            dashOffset = Mathf.Sign(move) * dashDistance;
            dashTrail.SetActive(true);
        }
        else if (dashTimer <= 0 && !canDash)
        {
            canDash = true;
        }
        else
        {
            dash = false;
        }

        float newX = Mathf.Clamp(position + velocity * Time.fixedDeltaTime + dashOffset, -maxDistance, maxDistance);
        position = newX;
        transform.position = new Vector3(position, Mathf.Sin(jump * Mathf.PI) * 8, 0);
        transform.localEulerAngles = new Vector3(0, 0, -velocity * 0.5f);

        if (dashTimer > 0)
            dashTimer -= Time.fixedDeltaTime;

        if (jump > 0)
        {
            jump -= Time.fixedDeltaTime;
            if (jump < 0)
            {
                jump = 0;
                anim.SetBool("Jumping", false);
                LevelManager.instance.mainCamera.Shake(0.1f, 0.5f, 10);
            }
        }

        if (Input.GetButton("Fire") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Gamefield.instance.AddProjectileAlly(Gamefield.instance.PREFAB_Shot_Ally, transform.position + new Vector3(0.35f, 0, 3), Quaternion.identity);
            Gamefield.instance.AddProjectileAlly(Gamefield.instance.PREFAB_Shot_Ally, transform.position + new Vector3(-0.35f, 0, 3), Quaternion.identity);
        }

        if (Input.GetButton("Fire") && Time.time > nextMissile && power >= 1)
        {
            nextMissile = Time.time + ((float)(120 - power) / 100f);
            Gamefield.instance.AddProjectileAlly(Gamefield.instance.PREFAB_Shot_MissileAlly, transform.position + new Vector3(1.2f, 0, 1.5f), Quaternion.identity);
            Gamefield.instance.AddProjectileAlly(Gamefield.instance.PREFAB_Shot_MissileAlly, transform.position + new Vector3(-1.2f, 0, 1.5f), Quaternion.identity);
        }

        interpolatedTransform.LateFixedUpdate();
    }

    public bool TestObstacleHit(ObstacleBase obstacle)
    {
        if (position > obstacle.position - obstacle.width / 2 && position < obstacle.position + obstacle.width / 2)
        {
            if (!obstacle.jumpable || (obstacle.jumpable && (jump < 0.1f || jump > 0.9f)))
            {
                if (shielded)
                {
                    SetShieldState(false);
                }
                else
                {
                    LevelManager.instance.Death();
                }
                return true;
            }
        }

        return false;
    }

    private void SetShieldState(bool hasShield)
    {
        shielded = hasShield;
        anim.SetBool("HasShield", shielded);
        AudioEffectsController.instance.SetShieldEffect(hasShield);
        if (!hasShield)
        {
            LevelManager.instance.SetGameSpeed(0.05f);
            LevelManager.instance.SetGameSpeed(1, 2, CurveLibrary.easeOut);
            PostProcessController.instance.SetChromaticAberation(1);
            PostProcessController.instance.SetChromaticAberation(0, 2, CurveLibrary.easeOut);
            PostProcessController.instance.SetLensDistortion(-50);
            PostProcessController.instance.SetLensDistortion(0, 2, CurveLibrary.easeOut);
        }
    }

    public void PlayJumpSmoke()
    {
        jumpSmoke.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
