using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnRange;
    public int enemiesNumToSpawn;
    public int enemyCount;
    public int waveNumber;

    public GameObject powerUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        SpawnEnemyWave(enemiesNumToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)

        {
            if (gameObject.CompareTag("PowerUp"))
            {
                Destroy(gameObject);
            }
            enemiesNumToSpawn++;
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
            SpawnEnemyWave(enemiesNumToSpawn);
        }
        else if (enemyCount == enemyCount * .5)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }

    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);

        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 5, spawnPosZ);

        return randomPos;
    }


}
