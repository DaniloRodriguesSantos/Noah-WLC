using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //Public Variables
    [HideInInspector] public int player_life;
    public int player_life_Max = 3;
    public float player_agility;
    public GameObject playerAttackHitbox;
    [HideInInspector] public float attackCoolDownNumber;

    //Private Variables
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private MiniG_UIController miniG_Canvas;
    private MiniG_Controller minigController_Script;
    [HideInInspector] public bool PlayerCanAttack = true;

    // Retry
    [HideInInspector] public Vector3 originalPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        miniG_Canvas = GameObject.Find("MiniG_Canvas").GetComponent<MiniG_UIController>();
        minigController_Script = GameObject.Find("MiniG_Controller").GetComponent<MiniG_Controller>();
    }

    private void Start()
    {
        
        originalPos = transform.position;
        player_life = player_life_Max;
        attackCoolDownNumber = GameConstants.PLAYER_ATTACK_COOLDOWN;
        if(playerAttackHitbox != null)
        {
            playerAttackHitbox.SetActive(false);
        }

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

        if (playerAttackHitbox != null && PlayerCanAttack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAttackHitbox.SetActive(true);
                StartCoroutine(attackDuration());
            }
        }

        if (!PlayerCanAttack)
        {
            attackCoolDownNumber += 1 * Time.deltaTime;
        }


    }

    private void FixedUpdate()
    {
        if (moveLeft)
        {
            rb.velocity = new Vector2(-player_agility, 0 * Time.deltaTime);
        }

        if (moveRight)
        {
            rb.velocity = new Vector2(player_agility, 0 * Time.deltaTime);
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
            if(minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Boss_MiniGame)
            {
                minigController_Script.score -= 100;
            }
            if (player_life == 0)
            {
                gameObject.SetActive(false);
                miniG_Canvas.playerDied();
            }
        }
    }

    IEnumerator attackDuration()
    {
        attackCoolDownNumber = 0;
        yield return new WaitForSeconds(GameConstants.PLAYER_ATTACK_DURATION);
        playerAttackHitbox.SetActive(false);
        PlayerCanAttack = false;
        yield return new WaitForSeconds(GameConstants.PLAYER_ATTACK_COOLDOWN);
        PlayerCanAttack = true;
        attackCoolDownNumber = GameConstants.PLAYER_ATTACK_COOLDOWN;
    }
}

