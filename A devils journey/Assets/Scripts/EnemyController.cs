using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float EnemySpeed;
    public int Health;
	public int Damage;
	public int WorthScore;
    
	private GameController GameController;

	private void Start()
	{
		GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	// Update is called once per frame
	void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (EnemySpeed * Time.deltaTime));
    }

	public void TakeDamage(int value)
	{
        Health -= value;
		if (Health <= 0)
		{
			Destroy(gameObject);
			GameController.CheckScore(WorthScore);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Barrier")
        {
			GameController.TakeDamage(Damage);
			Destroy(gameObject);
        }
	}
}
