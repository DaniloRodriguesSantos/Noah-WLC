using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingEnemyController : MonoBehaviour {
    public GameObject enemyPrefab;
    public Transform enemiesContainer;
    private int currentEnemyIndex;
    private List<MiniG_EnemyController> enemies;


    public void Start()
    {
        enemies = new List<MiniG_EnemyController>();
        enemies.Capacity = GameConstants.ENEMY_NUMBER;
        AddEnemiesToThePool();
    }

    #region Enemies
    private void AddEnemiesToThePool()
    {
        for (int i = 0; i < GameConstants.ENEMY_NUMBER; i++)
        {
            var go = Instantiate(enemyPrefab);
            go.SetActive(false);
            go.transform.parent = enemiesContainer;
            enemies.Add(go.GetComponent<MiniG_EnemyController>());
        }
    }

    public MiniG_EnemyController GetEnemy(Transform Enemyposition)
    {
        MiniG_EnemyController b = enemies[currentEnemyIndex];
        if (b.IsActive())
        {
            print("Number of Enemies not enough");
            return null;
        }

        currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
        b.gameObject.transform.position = Enemyposition.position;
        b.ToggleActive(true);
        return b;
    }
    #endregion
}
