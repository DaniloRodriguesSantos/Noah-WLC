using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Assorted_Dialogues : MonoBehaviour
{

    public enum FSMState { SoDeBoa, Interagindo };
    public FSMState state = FSMState.SoDeBoa;

    Audio_MainScript script_Audio_MainScript;
    public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues;
    PensamentoController script_PensamentoController;
    Text textMain;
    Canvas player_canvas;
    Text player_textMain;
    public Image imgNoah;
    public GameObject caixaMental;
    Text player_textCaixaMental;
    public bool passar_por_tempo;

    [TextArea(5, 2)]
    public string[] dialogo;


    public Sprite[] sprites;
    public int current_array_index;
    public float tempo_do_balao;
    float tempo;

    // Use this for initialization
    private void Awake()
    {
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        textMain = GetComponentInChildren<Text>();
        player_canvas = GameObject.Find("CanvasPlayerDialogo").GetComponent<Canvas>();
        script_PensamentoController = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();
        player_textMain = player_canvas.GetComponentInChildren<Text>();
        player_textCaixaMental = caixaMental.GetComponentInChildren<Text>();
    }
    private void Start()
    {
        //caixaMental.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        if (gameObject.name == "_PLAYER_BALAO")
        {
            player_canvas.enabled = true;
        }
        else
        {
            player_canvas.enabled = false;
        }

        if (gameObject.name == "_PLAYER_POPUP")
        {
            caixaMental.SetActive(true);
        }
        else
        {
            caixaMental.SetActive(false);
        }
        if (gameObject.name == "SINAL_ESCOLA")
        {
            script_Audio_MainScript.tocar_SinalEscola();
        }
        

        if (script_Controller_Assorted_Dialogues.isOn == true)
        {

            textMain.text = dialogo[current_array_index];
            player_textMain.text = dialogo[current_array_index];
            player_textCaixaMental.text = dialogo[current_array_index];

            if (gameObject.name == "_PLAYER_POPUP")
            {
                imgNoah.sprite = sprites[current_array_index];
            }

            if (passar_por_tempo)
            {
                tempo += 1 * Time.deltaTime;
                if (tempo > tempo_do_balao)
                {
                    current_array_index += 1;
                    tempo = 0;
                }
                if (current_array_index == dialogo.Length - 1)
                {
                    script_Controller_Assorted_Dialogues.passarObj();
                }
            }
            else
            {
                if (Input.GetKeyDown("e"))
                {
                    current_array_index += 1;
                    if (current_array_index == dialogo.Length - 1)
                    {
                        script_Controller_Assorted_Dialogues.passarObj();
                        current_array_index = 0;
                    }
                }
                
            }
        }

    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.SoDeBoa: soDeBoa_State(); break;
            case FSMState.Interagindo: interagindo_State(); break;
        }
    }

    void soDeBoa_State()
    {

    }

    void interagindo_State()
    {

    }
}


