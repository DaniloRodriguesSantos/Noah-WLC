using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniG_UIController : MonoBehaviour {

    public Text life_Indicator;
    public Text time_Indicator;
    private Controls controls_Script;
    private MiniG_Controller minigController_Script;

    // Menu Pause
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject dieMenu;

    private void Awake()
    {
        controls_Script = GameObject.Find("Player_MiniG").GetComponent<Controls>();
        minigController_Script = GameObject.Find("MiniG_Controller").GetComponent<MiniG_Controller>();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        int seconds = (int)(minigController_Script.countdown % 60);
        life_Indicator.text = "Vida: " + controls_Script.player_life.ToString();
        time_Indicator.text = string.Format("Tempo: {0:00}", seconds);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(dieMenu.activeInHierarchy == false)
            {
                PauseGame();
            }
        }
	}

    public void unPauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
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
        dieMenu.SetActive(true);
    }

    public void retryGame()
    {
        minigController_Script.countdown = 2f;
        controls_Script.gameObject.SetActive(true);
        controls_Script.player_life = 3;
        controls_Script.gameObject.transform.position = controls_Script.originalPos;
    }
}
