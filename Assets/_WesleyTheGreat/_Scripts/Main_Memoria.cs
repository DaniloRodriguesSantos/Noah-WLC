using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Memoria : MonoBehaviour
{

    Canvas thisCanvas;
    Camera mainCamera;
    public GameObject img_efeito_minigame;
    public Teleporte teleportePescola;
    public GameObject teleporteEscola;
    public GameObject panel;
    Platformer2DUserControl script_Platformer2DUserControl;
    Audio_MainScript script_Audio_MainScript;
    public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues;
    public Button[] btts;
    public int countMAX;
    public int countFINAL;

    public Button btt_1;
    public Button btt_2;

    public bool comecarMemoria;
    public float tempoParaComecar;
    float tempo;
    // Use this for initialization
    void Awake()
    {
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        thisCanvas = GetComponentInParent<Canvas>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        btts = GetComponentsInChildren<Button>();
    }
    IEnumerator sairDaSala()
    {
        script_Audio_MainScript.tocar_SinalEscola();
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        teleportePescola.teleportar();
        teleporteEscola.SetActive(false);
        panel.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = false;
        script_Platformer2DUserControl.iNeverFreeze = true;
        img_efeito_minigame.gameObject.SetActive(false);
        panel.SetActive(true);
        gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

        if (thisCanvas.enabled == true)
        {
            Cursor.visible = true;
            script_Platformer2DUserControl.iNeverFreeze = false;
        }

        thisCanvas.transform.position = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
        if (!comecarMemoria)
        {
            for (int i = 0; i < btts.Length; i++)
            {
                btts[i].enabled = false;
                btts[i].image.enabled = false;
            }


            tempo += 1 * Time.deltaTime;
            if (tempo > tempoParaComecar)
            {
                tempo = 0;
                for (int i = 0; i < btts.Length; i++)
                {
                    btts[i].enabled = true;
                    btts[i].image.enabled = true;
                }
                comecarMemoria = true;
            }
        }
        else
        {
            if (countMAX == 2)
            {
                if (btt_1.name != btt_2.name)
                {
                    StartCoroutine(errou());
                    countMAX = 0;
                }
                else
                {
                    acertou();
                    countMAX = 0;
                }
            }
            if (countFINAL == 5)
            {
                StartCoroutine(sairDaSala());
                countFINAL = 0;
                comecarMemoria = false;
                
            }
        }
        

    }
    public void acertou()
    {
        countFINAL += 1;
        btt_1.enabled = false;
        btt_2.enabled = false;
        btt_1 = null;
        btt_2 = null;

    }
    IEnumerator errou()
    {

       /* Image img_1 = btt_1.GetComponentInParent<Image>();
        Image img_2 = btt_2.GetComponentInParent<Image>();

        img_1.color = Color.red;
        img_2.color = Color.red;*/

        for (int i = 0; i < btts.Length; i++)
        {
            btts[i].image.raycastTarget = false;
        }

        yield return new WaitForSeconds(1);
        btt_1.image.enabled = true;
        btt_2.image.enabled = true;

        for (int i = 0; i < btts.Length; i++)
        {
            btts[i].image.raycastTarget = true;
        }
        btt_1 = null;
        btt_2 = null;

       /* img_1.color = Color.white;
        img_2.color = Color.white;*/

    }
}
