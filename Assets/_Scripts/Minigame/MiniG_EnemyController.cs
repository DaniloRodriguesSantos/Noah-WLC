using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniG_EnemyController : MonoBehaviour {

    private Transform[] EnemySpawnPoints;
    private Vector3 originalPos;
    private MiniG_Controller minigController_Script;
    private Transform trans;
    [HideInInspector] public bool moveDown = true;
    [HideInInspector] public bool moveUp = false;

    private void Awake()
    {
        EnemySpawnPoints = GameObject.Find("EnemySpawnPoints_Parent").GetComponentsInChildren<Transform>();
        minigController_Script = GameObject.Find("MiniG_Controller").GetComponent<MiniG_Controller>();
        trans = GetComponent<Transform>();
    }

    private void Start()
    {
        originalPos = trans.position;
    }

    private void FixedUpdate()
    {
        if (moveDown)
        {
            trans.Translate(-Vector3.up * minigController_Script.enemySpeed * Time.deltaTime);
        }

        if (moveUp)
        {
            trans.Translate(Vector3.up * minigController_Script.enemySpeed * Time.deltaTime);
        }

    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public void ToggleActive(bool b)
    {
        gameObject.SetActive(b);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.BORDER_TAG))
        {
            trans.position = EnemySpawnPoints[Random.Range(1, EnemySpawnPoints.Length)].position;

            if (moveUp)
            {
                moveDown = true;
                moveUp = false;
            }
        }

        if (collision.gameObject.CompareTag(GameConstants.PLAYER_ATTACK))
        {
            moveDown = false;
            moveUp = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG))
        {
            MiniG_EnemyController enemy = collision.gameObject.GetComponent<MiniG_EnemyController>();
            if (!enemy.moveUp)
            {
                if (moveDown)
                {
                    trans.position = EnemySpawnPoints[Random.Range(1, EnemySpawnPoints.Length)].position;
                }
            }
        }
    }
}
