using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniG_UIController : MonoBehaviour {

    public Text life_Indicator;
    public Text time_Indicator;
    public Text score_Indicator;
    public Text score_DieMenu_Indicator;
    public Text score_VictoryMenu_Indicator;
    public Text wave_Indicator;
    public Slider BossLife_Indicator;
    public Slider AttackCoolDown_Indicator;
    private int waveNumber;
    private Controls controls_Script;
    private MiniG_Controller minigController_Script;
    public MiniG_BossController miniGBossController_Script;
    public GameObject poolingEnemyController;
    public MiniG_EnemyController[] allEnemies;


    // Menus
    [Space(10)]
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject dieMenu;
    public GameObject MiniGame;

    [Space(10)]
    [Header("Exclusive Boss Menu")]
    public GameObject victoryMenu;
    public GameObject dieMenu_FirstTry;
    public GameObject dieMenu_SecondTry;

    private void Awake()
    {
        controls_Script = GameObject.Find("Player_MiniG").GetComponent<Controls>();
        minigController_Script = GameObject.Find("MiniG_Controller").GetComponent<MiniG_Controller>();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        dieMenu.SetActive(false);
        if (minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Waves_MiniGame)
        {
            wave_Indicator.gameObject.SetActive(false);
        }
        if(minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Boss_MiniGame)
        {
            victoryMenu.SetActive(false);
            dieMenu_FirstTry.SetActive(false);
            dieMenu_SecondTry.SetActive(false);
            BossLife_Indicator.maxValue = miniGBossController_Script.boss_Life;
            AttackCoolDown_Indicator.maxValue = GameConstants.PLAYER_ATTACK_COOLDOWN;
        }




    }

    // Update is called once per frame
    void Update () {
        if(minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Waves_MiniGame)
        {
            int seconds = (int)(minigController_Script.countdown % 60);
            time_Indicator.text = string.Format("Tempo: {0:00}", seconds);
            score_Indicator.text = "Pontuação: " + minigController_Script.score.ToString();

            if (minigController_Script.countdown <= 0f)
            {
                wave_Indicator.gameObject.SetActive(true);
                waveNumber++;
            }

            if (wave_Indicator.gameObject.activeSelf == true)
            {
                wave_Indicator.text = "Onda: " + waveNumber.ToString();
            }
        }

        if (minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Boss_MiniGame)
        {
            BossLife_Indicator.value = miniGBossController_Script.boss_Life;
            AttackCoolDown_Indicator.value = controls_Script.attackCoolDownNumber;
        }

        life_Indicator.text = "Vida: " + controls_Script.player_life.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(dieMenu.activeInHierarchy == false)
            {
                PauseGame();
            }
        }

        if (Input.GetKey(KeyCode.B))
        {
            PlayerPrefs.DeleteKey("canRetry");
            print("The Key has been deleted");
        }
	}

    private void pauseTime()
    {
        Time.timeScale = 0f;
    }

    private void unpauseTime()
    {
        Time.timeScale = 1f;
    }

    public void unPauseGame()
    {
        pauseMenu.SetActive(false);
        unpauseTime();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseTime();
    }

    public void GoToControlsMenu()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void GoToPauseMenu()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void playerDied()
    {
        if(minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Waves_MiniGame)
        {
            dieMenu.SetActive(true);
            score_DieMenu_Indicator.text = "Pontuação: " + minigController_Script.score.ToString();
            pauseTime();
            return;
        }

        if (minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Boss_MiniGame)
        {
            if(PlayerPrefs.GetString("canRetry") == "false")
            {
                dieMenu_FirstTry.SetActive(true);
                pauseTime();
                return;
            }

            if (PlayerPrefs.GetString("canRetry") == "true")
            {
                dieMenu_SecondTry.SetActive(true);
                pauseTime();
                return;
            }
        }
    }

    public void playerVictory()
    {
        victoryMenu.SetActive(true);
        score_VictoryMenu_Indicator.text = "Pontuação: " + minigController_Script.score.ToString();
        pauseTime();
    }

    public void closeGame()
    {
        MiniGame.SetActive(false);
        retryGame_Waves();
        unPauseGame();
    }

    public void closeGameAfterWinOrLose()
    {
        MiniGame.SetActive(false);
        unPauseGame();
        retryGame_Boss();
        if (minigController_Script.MiniG_Type == MiniG_Controller.MiniG_State.Boss_MiniGame)
        {
            if (PlayerPrefs.GetString("canRetry") == "false")
            {
                PlayerPrefs.SetString("canRetry", "true");
                print("Hoje não");
            } else if (PlayerPrefs.GetString("canRetry") == "true")
            {
                PlayerPrefs.SetString("canRetry", "false");
                print("Hoje sim");
            }
        }
    }

    public void retryGame_Waves()
    {
        // This Script
        unpauseTime();
        dieMenu.SetActive(false);
        waveNumber = 0;
        allEnemies = poolingEnemyController.GetComponentsInChildren<MiniG_EnemyController>();
        for (int i = 0; i < allEnemies.Length; i++)
        {
            allEnemies[i].ToggleActive(false);
        }

        // MiniG_Controller
        minigController_Script.countdown = 2f;
        minigController_Script.waveIndex = 0;
        minigController_Script.enemySpeed = 3.75f;
        minigController_Script.score = 0;

        //Controls
        controls_Script.gameObject.SetActive(true);
        controls_Script.player_life = controls_Script.player_life_Max;
        controls_Script.gameObject.transform.position = controls_Script.originalPos;

    }

    public void retryGame_Boss()
    {
        // This Script
        unpauseTime();
        if(dieMenu_SecondTry.activeSelf == true)
        {
            dieMenu_SecondTry.SetActive(false);
        } else if(victoryMenu.activeSelf == true)
        {
            victoryMenu.SetActive(false);
        }

        allEnemies = poolingEnemyController.GetComponentsInChildren<MiniG_EnemyController>();
        for (int i = 0; i < allEnemies.Length; i++)
        {
            allEnemies[i].ToggleActive(false);
        }

        // MiniG_Controller
        minigController_Script.countdown = 2f;
        minigController_Script.waveIndex = 0;
        minigController_Script.enemySpeed = minigController_Script.enemySpeed;
        minigController_Script.score = miniGBossController_Script.boss_MaxScore;
        minigController_Script.spawnFirstWave_Boss = true;

        //Controls
        controls_Script.gameObject.SetActive(true);
        controls_Script.player_life = controls_Script.player_life_Max;
        controls_Script.gameObject.transform.position = controls_Script.originalPos;
        controls_Script.PlayerCanAttack = true;
        controls_Script.attackCoolDownNumber = GameConstants.PLAYER_ATTACK_COOLDOWN;

        // Boss Controller
        miniGBossController_Script.gameObject.transform.position = miniGBossController_Script.originalPos;
        miniGBossController_Script.boss_Life = miniGBossController_Script.boss_MaxLife;
        miniGBossController_Script.gameObject.SetActive(true);
    }
}
