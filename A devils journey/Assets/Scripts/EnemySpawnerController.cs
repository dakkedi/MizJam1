using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
	public GameObject[] Enemies;
	public float EnemySpawnTimerStart;

	private float EnemySpawnTimer;
	private Bounds Bounds;

	private void Start()
	{

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
		}
	}

	private void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, GetStartPosition(), new Quaternion());
	}

	private Vector2 GetStartPosition()
	{
		return new Vector2(
			Random.Range(Bounds.min.x, Bounds.max.x),
			Random.Range(Bounds.min.y, Bounds.max.y));
	}
}
