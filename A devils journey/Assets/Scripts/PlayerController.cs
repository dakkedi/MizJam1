﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public float Speed = 5f; // Speed defaults to 5 if nothing set in unity
    public GameObject Projectile;
    public float ShootingCooldownStart;

    private float H; // Horizontal input
    private float MoveLimiter = 0.7f;
    private Rigidbody2D body;
    private float ShootingCooldown;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Fetch scripts gameobjects Rigidbody
    }

    // Update is called once per frame
    private void Update()
    {
        H = Input.GetAxisRaw("Horizontal");
        if (H != 0)
        {
            Animator.SetFloat("Speed", Mathf.Abs(1));
        }
        else
        {
            Animator.SetFloat("Speed", Mathf.Abs(0));
        }

        Shoot();
    }

    private void Shoot()
    {
        if (ShootingCooldown <= 0)
        {
            if (Input.GetKey("space"))
            {
                Instantiate(Projectile, transform.position, new Quaternion());
                ShootingCooldown = ShootingCooldownStart;
            }
        }
        else
        {
            ShootingCooldown -= Time.deltaTime;
        }
    }

	private void FixedUpdate()
	{
        // Diagonal fix
        if (H != 0)
        {
            H *= MoveLimiter;
        }
            
        body.velocity = new Vector2(H * Speed, 0);
	}
}
