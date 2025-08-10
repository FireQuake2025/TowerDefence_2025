using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public float TimeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

[System.Serializable]
public struct WaveData
{
    public float TimeBeforeWave;
    public List<SpawnData> enemyData;
}

public class WaveManager : MonoBehaviour
{
    public List<WaveData> LevelWaveData;
    public GameObject LevelWin;
    public TextMeshProUGUI levelWinText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        Enemy enemy = FindAnyObjectByType<Enemy>();
        Health health = GetComponent<Health>();
        foreach (WaveData waveData in LevelWaveData)
        {
            yield return new WaitForSeconds(waveData.TimeBeforeWave);
            Debug.Log("Round Start");
            foreach (SpawnData currentEnemy in waveData.enemyData)
            {
                yield return new WaitForSeconds(currentEnemy.TimeBeforeSpawn);
                SpawnEnemy(currentEnemy.EnemyToSpawn, currentEnemy.SpawnPoint, currentEnemy.EndPoint);
            }
        }
        if (!enemy)
        {
            LevelWin.SetActive(true);
            Time.timeScale = 0f;

            if (health.currentHealth == 100)
            {
                levelWinText.text = "Perfect";
            }
            else if (health.currentHealth <= 100)
            {
                levelWinText.text = "You Win";
            }
            else if (health.currentHealth <= 20)
            {
                levelWinText.text = "You Barely Suvived";
            }
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnpoint, Transform endpoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnpoint.position, endpoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialized(endpoint);
    }


}
