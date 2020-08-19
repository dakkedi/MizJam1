using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float Speed;
	public int DamageValue;
	public GameObject Explosion;
	private float xPos;

	private void Start()
	{
		xPos = GameObject.FindGameObjectWithTag("Player").transform.position.x;
	}
	// Update is called once per frame
	void Update()
    {
        transform.position = new Vector2(xPos, transform.position.y + (Speed * Time.deltaTime));
    }

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			collision.gameObject.GetComponent<EnemyController>().TakeDamage(DamageValue);
			var explosion = Instantiate(Explosion, collision.transform.position, new Quaternion());
			DestroyProjectile();
		}
	}

	private void DestroyProjectile()
	{
		Destroy(gameObject);
	}
}
