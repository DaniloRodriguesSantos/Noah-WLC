using System.Collections;
using UnityEngine;

public class MiniG_Controller : MonoBehaviour {

    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;
    [HideInInspector] public float countdown = 2f;
    [HideInInspector] public int waveIndex = 0;
    public GameObject EnemySpawnPoints_Parent;
    private Transform[] EnemySpawnPoints;
    public float timeBetweenEnemySpawn = 0.5f;
    // Use this for initialization
    private void Awake()
    {
        EnemySpawnPoints = EnemySpawnPoints_Parent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime; 
	}

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(EnemySpawnPoints[Random.Range(1, EnemySpawnPoints.Length)]);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
    }

    void SpawnEnemy(Transform prefabPosition)
    {
        Instantiate(enemyPrefab, prefabPosition.position, prefabPosition.rotation);
    }
}
