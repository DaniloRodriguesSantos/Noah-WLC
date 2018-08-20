using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    private Rigidbody2D rb;
    public float movespeed;
    public bool moveLeft;
    public bool moveRight;
    public int player_life;
    public int player_agility;
    private MiniG_UIController miniG_Canvas;

    // Retry
    public Vector3 originalPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        miniG_Canvas = GameObject.Find("MiniG_Canvas").GetComponent<MiniG_UIController>();
    }

    private void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
    }

    private void FixedUpdate()
    {
        if (moveLeft)
        {
            rb.velocity = new Vector2(-movespeed, 0 * Time.deltaTime);
        }

        if (moveRight)
        {
            rb.velocity = new Vector2(movespeed, 0 * Time.deltaTime);
        }

        if (moveLeft == false && moveRight == false)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.BORDER_TAG))
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG))
        {
            player_life--;
            if (player_life == 0)
            {
                gameObject.SetActive(false);
                miniG_Canvas.playerDied();
            }
        }
    }
}

