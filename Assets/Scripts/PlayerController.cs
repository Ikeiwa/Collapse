using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float accel = 10;
    public float maxDistance = 5;
    public float friction = 0.95f;
    public float dashChargeTime = 1f;
    public float dashDistance = 2;

    public float velocity { get; private set; }

    private bool canDash = true;
    private float dashTimer = 0;

    private float move = 0;
    private float dash = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        dash = Input.GetAxisRaw("Dash");

        if (Input.GetButtonDown("Fire"))
        {

        }

        transform.localEulerAngles = new Vector3(0, 0, -velocity*0.5f);
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
        }
        else if (dashTimer <= 0 && !canDash)
        {
            canDash = true;
        }

        float newX = Mathf.Clamp(transform.localPosition.x + velocity * Time.fixedDeltaTime + dashOffset, -maxDistance, maxDistance);
        transform.localPosition = new Vector3(newX, 0, 0);

        if (dashTimer > 0)
            dashTimer -= Time.fixedDeltaTime;
    }
}
