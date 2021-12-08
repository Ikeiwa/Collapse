using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float jump = 0;
    private bool shielded = false;

    private InterpolatedTransform interpolatedTransform;
    private Animator anim;

    private static readonly int VelocityParam = Animator.StringToHash("Velocity");

    // Start is called before the first frame update
    void Awake()
    {
        interpolatedTransform = GetComponent<InterpolatedTransform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        dash = Input.GetAxisRaw("Dash");

        if (Input.GetButtonDown("Fire"))
        {

        }

        if (Input.GetKeyDown(KeyCode.Space) && jump == 0)
        {
            jump = 1;
            anim.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            shielded = !shielded;
            anim.SetBool("HasShield", shielded);
        }

        if (jump > 0)
        {
            jump -= Time.deltaTime;
            if (jump < 0)
            {
                jump = 0;
                anim.SetBool("Jumping", false);
                LevelManager.instance.mainCamera.Shake(0.1f,0.5f, 10);
            }
        }

        transform.localEulerAngles = new Vector3(0, 0, -velocity*0.5f);
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

        if (dashTimer > 0)
            dashTimer -= Time.fixedDeltaTime;

        interpolatedTransform.LateFixedUpdate();
    }

    public void PlayJumpSmoke()
    {
        jumpSmoke.Play();
    }
}
