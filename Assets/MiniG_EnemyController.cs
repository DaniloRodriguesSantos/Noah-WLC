using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniG_EnemyController : MonoBehaviour {

    public float speed;
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.BORDER_TAG))
        {
            transform.position = originalPos;
        }
    }
}
