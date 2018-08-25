using System.Collections;
using UnityEngine;

public class MiniG_Controller : MonoBehaviour
{
    public enum MiniG_State { Boss_MiniGame, Waves_MiniGame }
    [Header("What kind of MiniG is this?")]
    public MiniG_State MiniG_Type;

    [Space(10)]
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;
    /*[HideInInspector]*/ public float countdown = 2f;
    public int waveIndex = 0;
    public GameObject EnemySpawnPoints_Parent;
    private Transform[] EnemySpawnPoints;
    public float timeBetweenEnemySpawn = 0.5f;
    public float firstWaveEnemyCount;
    public PoolingEnemyController pool;
    public float enemySpeed;
    [HideInInspector] public int score = 0;
    private MiniG_UIController miniG_Canvas;
    [HideInInspector] public bool spawnFirstWave_Boss = true;
    public MiniG_BossController miniGBossController_Script;
    [HideInInspector] public string canRetryMinigame = "false";
    private void Awake()
    {
        EnemySpawnPoints = EnemySpawnPoints_Parent.GetComponentsInChildren<Transform>();
        miniG_Canvas = GameObject.Find("MiniG_Canvas").GetComponent<MiniG_UIController>();
    }

    private void Start()
    {
        if(MiniG_Type == MiniG_State.Boss_MiniGame)
        {
            score = miniGBossController_Script.boss_MaxScore;
            miniGBossController_Script = GameObject.Find("Boss_MiniG").GetComponent<MiniG_BossController>();
        }
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            if(MiniG_Type == MiniG_State.Boss_MiniGame)
            {
                if (spawnFirstWave_Boss)
                {
                    StartCoroutine(SpawnWave());
                    spawnFirstWave_Boss = false;
                }
            }
            

            if(MiniG_Type == MiniG_State.Waves_MiniGame)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                enemySpeed += 0.25f;
                if (waveIndex > 0)
                {
                    score += 5;
                }
            }
        } else
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        if (waveIndex < 1)
        {
            for (int i = 0; i < firstWaveEnemyCount; i++)
            {
                SpawnEnemy(EnemySpawnPoints[Random.Range(1, EnemySpawnPoints.Length)]);
                yield return new WaitForSeconds(timeBetweenEnemySpawn);
            }
        }
        else if (waveIndex > 0 && waveIndex < 9 && MiniG_Type == MiniG_State.Waves_MiniGame)
        {
            SpawnEnemy(EnemySpawnPoints[Random.Range(1, EnemySpawnPoints.Length)]);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }


        waveIndex++;
    }

    void SpawnEnemy(Transform spawnPostion)
    {
        var bullet = pool.GetEnemy(spawnPostion);
    }
}
