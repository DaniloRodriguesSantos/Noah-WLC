using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniG_BossController : MonoBehaviour {

    public float boss_Speed;
    public int boss_Life;
    public int boss_MaxLife;
    public int boss_MaxScore;
    private bool moveLeft = true;
    private bool moveRight = false;
    private Transform trans;
    private MiniG_UIController miniG_Canvas;
    private MiniG_Controller minigController_Script;
    [HideInInspector] public Vector3 originalPos;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        miniG_Canvas = GameObject.Find("MiniG_Canvas").GetComponent<MiniG_UIController>();
        minigController_Script = GameObject.Find("MiniG_Controller").GetComponent<MiniG_Controller>();
    }

    private void Start()
    {
        originalPos = trans.position;
        boss_Life = boss_MaxLife;
    }

    private void FixedUpdate()
    {
        if (moveLeft)
        {
            trans.Translate(-Vector3.right * boss_Speed * Time.deltaTime);
        }
        if (moveRight)
        {
            trans.Translate(Vector3.right * boss_Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.BOSS_BORDER_TAG))
        {
            if (moveLeft)
            {
                moveRight = true;
                moveLeft = false;
            } else if (moveRight)
            {
                moveLeft = true;
                moveRight = false;
            }
        }

        if (collision.gameObject.CompareTag(GameConstants.ENEMY_TAG))
        {
            MiniG_EnemyController enemy = collision.gameObject.GetComponent<MiniG_EnemyController>();
            if (enemy.moveUp)
            {
                boss_Life -= 15;
                if(boss_Life <= 0)
                {
                    gameObject.SetActive(false);
                    miniG_Canvas.playerVictory();
                }
            }
            return;
        }
    }
}
