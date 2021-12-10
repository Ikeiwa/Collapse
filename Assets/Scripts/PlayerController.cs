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
    

    public float moveSpeed = 5;
    public float accel = 10;
    public float maxDistance = 5;
    public float friction = 0.95f;
    public float dashChargeTime = 1f;
    public float dashDistance = 2;

    public float velocity { get; private set; }
    public float position { get; private set; }

    private bool canDash = true;
    private float dashTimer = 0;

    private float move = 0;
    private float dash = 0;

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

    public void ObtainPowerup(PowerUp newPowerUp)
    {
        if (powerUp == PowerUp.None)
            powerUp = newPowerUp;
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
        dash = Input.GetAxis("Dash");

        if (Input.GetButtonDown("Fire"))
        {
            Debug.Log("Fire");
        }

        if (Input.GetButtonDown("PowerUp"))
        {
            UsePowerup();
        }

        if(Input.GetKeyDown(KeyCode.Keypad1))
            ObtainPowerup(PowerUp.Jump);

        if (Input.GetKeyDown(KeyCode.Keypad2))
            ObtainPowerup(PowerUp.Shield);

        anim.SetFloat(VelocityParam, velocity);
    }

    private void FixedUpdate()
    {
        float dashOffset = 0;

        velocity += -velocity * friction * Time.fixedDeltaTime;
        velocity += Mathf.Clamp(move * Time.fixedDeltaTime * accel, -moveSpeed, moveSpeed);

        if (Mathf.Abs(dash) > 0.5f && canDash)
        {
            canDash = false;
            dashTimer = dashChargeTime;
            dashOffset = Mathf.Sign(dash) * dashDistance;
            dashTrail.SetActive(true);
        }
        else if (dashTimer <= 0 && !canDash)
        {
            canDash = true;
        }

        float newX = Mathf.Clamp(position + velocity * Time.fixedDeltaTime + dashOffset, -maxDistance, maxDistance);
        position = newX;
        transform.position = new Vector3(position, Mathf.Sin(jump*Mathf.PI)*8, 0);
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

        interpolatedTransform.LateFixedUpdate();
    }

    public bool TestObstacleHit(ObstacleBase obstacle)
    {
        if (position > obstacle.position - obstacle.width / 2 && position < obstacle.position + obstacle.width / 2)
        {
            if (!obstacle.jumpable || (obstacle.jumpable && (jump < 0.25f || jump > 0.75f)))
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
            LevelManager.instance.SetGameSpeed(1,1, CurveLibrary.easeOut);
            PostProcessController.instance.SetChromaticAberation(1);
            PostProcessController.instance.SetChromaticAberation(0,1, CurveLibrary.easeOut);
            PostProcessController.instance.SetLensDistortion(-50);
            PostProcessController.instance.SetLensDistortion(0, 1, CurveLibrary.easeOut);
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
