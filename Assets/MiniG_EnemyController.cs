using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniG_EnemyController : MonoBehaviour {

    public float speed;
    private Transform[] EnemySpawnPoints;
    private Vector3 originalPos;
    private MiniG_Controller minigController_Script;

    private void Awake()
    {
        EnemySpawnPoints = GameObject.Find("EnemySpawnPoints_Parent").GetComponentsInChildren<Transform>();
        minigController_Script = GameObject.Find("MiniG_Controller").GetComponent<MiniG_Controller>();
    }

    private void Start()
    {
        originalPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(-Vector3.up * (speed * minigController_Script.waveIndex) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.BORDER_TAG))
        {
            //transform.position = originalPos;
            transform.position = EnemySpawnPoints[Random.Range(1, EnemySpawnPoints.Length)].position;
        }
    }
}
