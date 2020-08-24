using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int BarrierHealth;
    public int Score;
    public int MaxScoreToNextLevel;
    public GameObject NumberPrefab;
    public GameObject Tens;
    public GameObject Singles;

    private EnemySpawnerController EnemySpawnerController;
    private NumberController TensController;
    private NumberController SinglesController;

    private void Start()
	{
        EnemySpawnerController = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerController>();
        Tens = Instantiate(NumberPrefab, Tens.transform);
        Singles = Instantiate(NumberPrefab, Singles.transform);
        TensController = Tens.GetComponent<NumberController>();
        SinglesController = Singles.GetComponent<NumberController>();
        SetHealth(BarrierHealth);
    }

    private void SetHealth(int newHealth)
    {
        if (newHealth < 0) newHealth = 0;
        if (newHealth.ToString().Length > 1)
        {
            var ten = int.Parse(newHealth.ToString()[0].ToString());
            var single = int.Parse(newHealth.ToString()[1].ToString());
            var tensSprite = TensController.Sprites[ten];
            var singlesSprite = SinglesController.Sprites[single];
            TensController.GetComponent<SpriteRenderer>().sprite = tensSprite;
            SinglesController.GetComponent<SpriteRenderer>().sprite = singlesSprite;
        }
        else
        {
            var tensSprite = TensController.Sprites[0];
            var singlesSprite = SinglesController.Sprites[newHealth];
            TensController.GetComponent<SpriteRenderer>().sprite = tensSprite;
            SinglesController.GetComponent<SpriteRenderer>().sprite = singlesSprite;
        }
    }

	public void TakeDamage(int damage)
    {
        BarrierHealth -= damage;
        SetHealth(BarrierHealth);
        if (BarrierHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        StartCoroutine(CoroutineEndGame());
    }

    private IEnumerator CoroutineEndGame()
    {
        EnemySpawnerController.EnemySpawnTimerStart = -20;
        EnemySpawnerController.EnemySpawnTimer = 0;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    public void CheckScore(int incomingScore)
    {
		for (int i = 0; i < incomingScore; i++)
		{
            Score++;
            if (Score % MaxScoreToNextLevel == 0)
            {
                EnemySpawnerController.SetIncreaseDifficulty();
            }
		}
    }
}
