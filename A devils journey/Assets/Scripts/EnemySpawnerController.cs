using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
	public GameObject[] Enemies;
	public float EnemySpawnTimerStart;

	public float EnemySpawnTimer;
	private Bounds Bounds;
	private bool IncreaseDifficulty = false;

	private void Start()
	{
		Debug.Log("EnemySpawnTimerStart " + EnemySpawnTimerStart);
		EnemySpawnTimer = EnemySpawnTimerStart;
		Bounds = GetComponent<BoxCollider2D>().bounds;
	}

	private void Update()
	{
		EnemySpawnTimer -= Time.deltaTime;
		if (EnemySpawnTimer <= 0)
		{
			SpawnEnemy(Enemies[Random.Range(0, Enemies.Count())]);
			EnemySpawnTimer = EnemySpawnTimerStart;
			if (IncreaseDifficulty)
			{
				Debug.Log("EnemySpawnTimerStart " + EnemySpawnTimerStart);
				EnemySpawnTimerStart = EnemySpawnTimerStart - 0.25f;
				IncreaseDifficulty = false;
			}
		}
	}

	private void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, GetStartPosition(), new Quaternion());
		if (enemy.name == "TinyEnemy")
		{
			Instantiate(enemy, GetStartPosition(), new Quaternion());
		}
	}

	private Vector2 GetStartPosition()
	{
		return new Vector2(
			Random.Range(Bounds.min.x, Bounds.max.x),
			Random.Range(Bounds.min.y, Bounds.max.y));
	}

	public void SetIncreaseDifficulty()
	{
		IncreaseDifficulty = true;
	}
}
