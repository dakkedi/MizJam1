using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public float Speed = 5f; // Speed defaults to 5 if nothing set in unity
    private float H; // Horizontal input
    private float V; // Vertical input
    private float MoveLimiter = 0.7f;
    private Rigidbody2D body;
    public void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Fetch scripts gameobjects Rigidbody
    }

    // Update is called once per frame
    private void Update()
    {
        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
        if (H != 0 || V != 0)
        {
            Animator.SetFloat("Speed", Mathf.Abs(1));
        }
        else
        {
            Animator.SetFloat("Speed", Mathf.Abs(0));
        }
    }

	private void FixedUpdate()
	{
        // Diagonal fix
        if (H != 0 && V != 0)
        {
            H *= MoveLimiter;
            V *= MoveLimiter;
        }
            
        body.velocity = new Vector2(H * Speed, V * Speed);
	}
}
