using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER_dialogue : MonoBehaviour
{

    public enum FSMState { Interagindo, Respondendo };
    public FSMState state = FSMState.Interagindo;

    public bool NOAH;
    public bool NPC;
    public bool SEM_DIALOGO;
    [Space(10)]
    [Header("--HABILIDADES NOAH--")]
    public bool isso_e_balao;
    public bool isso_e_popUp;

    [Space(5)]
    public bool passar_por_tempo;
    public int tempo_do_balao;

    [Space(5)]
    public bool terminar_com_pergunta;
    [TextArea(2, 2)]
    public string pergunta_esquerda;
    [TextArea(2, 2)]
    public string pergunta_direita;


    [Space(15)]
    [Header("--EFEITOS SONOROS--")]
    public bool sinal;

    [Space(15)]
    [Header("--TIPO DE BALÃO--")]
    [Header("noah")]
    public bool noah_balao_esquerda;
    public bool noah_balao_centro;
    public bool noah_balao_direita;
    [Space(5)]
    [Header("npcs")]
    public bool npc_balao_G_P;
    public bool npc_balao_M_M;
    public bool npc_balao_P_G;


    [Space(15)]
    [Header("--DIALOGOS--")]
    [TextArea(5, 2)]
    public string[] dialogo_1;
    [TextArea(5, 2)]
    public string[] dialogo_resposta_LEFT;
    [TextArea(5, 2)]
    public string[] dialogo_resposta_RIGHT;

    [Space(15)]
    [Header("--SPRITES--")]
    public Sprite[] sprites;


    // (--HUD--) // 
    public Image imgNoah;

    Canvas canvas_balao_esquerda;
    Text canvas_balao_esquerda_text;
    Canvas canvas_balao_centro;
    Text canvas_balao_centro_text;
    Canvas canvas_balao_direita;
    Text canvas_balao_direita_text;

    Canvas canvas_Mental;
    Text canvas_Mental_text;

    [Space(15)]
    [Header("--CANVAS NPCs--")]

    public Canvas NPC_GP;
    public Canvas NPC_MM;
    public Canvas NPC_PG;

    Text[] textos_NPCs;

    // (--SCRIPTS--) // 
    PLAYER_main_dialogue script_PLAYER_main_dialogue;
    Audio_MainScript script_Audio_MainScript;

    // (--VARIAVEIS--) // 
    float tempo;
    int current_array_index;
    bool respondeu_LEFT;
    bool respondeu_RIGHT;

    void Awake()
    {
        // (--SCRIPTS--) // 
        script_PLAYER_main_dialogue = GetComponentInParent<PLAYER_main_dialogue>();
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();

        // (--HUD--) // 
        canvas_balao_esquerda = GameObject.Find("Canvas_PLAYER_Esquerda").GetComponent<Canvas>();
        canvas_balao_esquerda_text = canvas_balao_esquerda.GetComponentInChildren<Text>();
        canvas_balao_centro = GameObject.Find("Canvas_PLAYER_Centro").GetComponent<Canvas>();
        canvas_balao_centro_text = canvas_balao_centro.GetComponentInChildren<Text>();
        canvas_balao_direita = GameObject.Find("Canvas_PLAYER_Direita").GetComponent<Canvas>();
        canvas_balao_direita_text = canvas_balao_direita.GetComponentInChildren<Text>();

        canvas_Mental = GameObject.Find("Canvas_PLAYER_Mental").GetComponent<Canvas>();
        canvas_Mental_text = canvas_Mental.GetComponentInChildren<Text>();

        textos_NPCs = GetComponentsInChildren<Text>();

        /*canvas_Npc_esquerda_text = canvas_Npc_esquerda.GetComponentInChildren<Text>();
        canvas_Npc_centro_text = canvas_Npc_centro.GetComponentInChildren<Text>();
        canvas_Npc_direita_text = canvas_Npc_direita.GetComponentInChildren<Text>();*/
    }
    private void Start()
    {
        canvas_balao_esquerda.enabled = false;
        canvas_balao_centro.enabled = false;
        canvas_balao_direita.enabled = false;

        canvas_Mental.enabled = false;
    }

    void Update()
    {

        if (state == FSMState.Interagindo)
        {
            if (passar_por_tempo)
            {
                tempo += 1 * Time.deltaTime;
                if (tempo > tempo_do_balao)
                {
                    current_array_index += 1;
                    tempo = 0;
                }
                if (current_array_index == dialogo_1.Length - 1)
                {
                    if (terminar_com_pergunta)
                    {
                        state = FSMState.Respondendo;
                    }
                    else
                    {
                        desativa_Canvas();
                        script_PLAYER_main_dialogue.passaOBJ();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown("e"))
                {
                    current_array_index += 1;
                    if (current_array_index == dialogo_1.Length - 1)
                    {
                        if (terminar_com_pergunta)
                        {
                            state = FSMState.Respondendo;
                            NPC_GP.enabled = false;
                            NPC_MM.enabled = false;
                            NPC_PG.enabled = false;
                        }
                        else
                        {
                            desativa_Canvas();
                            script_PLAYER_main_dialogue.passaOBJ();
                        }
                    }
                }
            }
        }

        if (state == FSMState.Respondendo)
        {
            if (Input.GetKeyDown("a"))
            {
                canvas_balao_esquerda_text.fontSize = 120;
                canvas_balao_direita_text.fontSize = 100;
            }
            else if (Input.GetKeyDown("d"))
            {
                canvas_balao_esquerda_text.fontSize = 100;
                canvas_balao_direita_text.fontSize = 120;
            }


            if (Input.GetKeyDown("e"))
            {
                terminar_com_pergunta = false;
                if (canvas_balao_esquerda_text.fontSize == 120)
                {
                    desativa_Canvas();

                    current_array_index = 0;
                    respondeu_LEFT = true;
                    state = FSMState.Interagindo;
                    script_PLAYER_main_dialogue.ja_respondeu = true;
                }
                else if (canvas_balao_direita_text.fontSize == 120)
                {
                    desativa_Canvas();

                    current_array_index = 0;
                    respondeu_RIGHT = true;
                    state = FSMState.Interagindo;
                    script_PLAYER_main_dialogue.ja_respondeu = true;
                }
                
            }
        }
    }

    public void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.Interagindo: interagindo_State(); break;
            case FSMState.Respondendo: respondendo_State(); break;
        }
    }

    public void interagindo_State()
    {
        if (NOAH)
        {


            canvas_balao_esquerda_text.text = dialogo_1[current_array_index];
            canvas_balao_centro_text.text = dialogo_1[current_array_index];
            canvas_balao_direita_text.text = dialogo_1[current_array_index];




            if (isso_e_balao)
            {
                if (noah_balao_esquerda)
                {
                    //canvas_balao_esquerda_text.text = dialogo[current_array_index];
                    canvas_balao_esquerda.enabled = true;
                    canvas_balao_centro.enabled = false;
                    canvas_balao_direita.enabled = false;
                }
                if (noah_balao_centro)
                {
                    //canvas_balao_centro_text.text = dialogo[current_array_index];
                    canvas_balao_esquerda.enabled = false;
                    canvas_balao_centro.enabled = true;
                    canvas_balao_direita.enabled = false;
                }
                if (noah_balao_direita)
                {
                    //canvas_balao_direita_text.text = dialogo[current_array_index];
                    canvas_balao_esquerda.enabled = false;
                    canvas_balao_centro.enabled = false;
                    canvas_balao_direita.enabled = true;
                }
            }
            else if (isso_e_popUp)
            {

                canvas_Mental_text.text = dialogo_1[current_array_index];


                imgNoah.sprite = sprites[current_array_index];
                canvas_Mental.enabled = true;
            }
            else
            {
                canvas_balao_esquerda.enabled = false;
                canvas_balao_centro.enabled = false;
                canvas_balao_direita.enabled = false;
                canvas_Mental.enabled = false;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        if (NPC)
        {
            if (respondeu_LEFT)
            {
                for (int i = 0; i < textos_NPCs.Length; i++)
                {
                    textos_NPCs[i].text = dialogo_resposta_LEFT[current_array_index];
                }
            }
            else if (respondeu_RIGHT)
            {
                for (int i = 0; i < textos_NPCs.Length; i++)
                {
                    textos_NPCs[i].text = dialogo_resposta_RIGHT[current_array_index];
                }
            }
            else
            {
                for (int i = 0; i < textos_NPCs.Length; i++)
                {
                    textos_NPCs[i].text = dialogo_1[current_array_index];
                }
            }
            /*for (int i = 0; i < textos_NPCs.Length; i++)
            {
                textos_NPCs[i].text = dialogo_1[current_array_index];
            }*/

            if (npc_balao_G_P)
            {
                NPC_GP.enabled = true;
                NPC_MM.enabled = false;
                NPC_PG.enabled = false;
            }
            else if (npc_balao_M_M)
            {
                NPC_GP.enabled = false;
                NPC_MM.enabled = true;
                NPC_PG.enabled = false;
            }
            else if (npc_balao_P_G)
            {
                NPC_GP.enabled = false;
                NPC_MM.enabled = false;
                NPC_PG.enabled = true;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        if (SEM_DIALOGO)
        {
            canvas_balao_esquerda.enabled = false;
            canvas_balao_centro.enabled = false;
            canvas_balao_direita.enabled = false;

            canvas_Mental.enabled = false;

            NPC_GP.enabled = false;
            NPC_MM.enabled = false;
            NPC_PG.enabled = false;
        }

        if (sinal)
        {
            sinal = false;
            script_Audio_MainScript.tocar_SinalEscola();
        }
    }

    public void respondendo_State()
    {
        canvas_balao_esquerda_text.text = pergunta_esquerda;
        canvas_balao_direita_text.text = pergunta_direita;
        canvas_balao_esquerda.enabled = true;
        canvas_balao_direita.enabled = true;
        canvas_balao_esquerda_text.color = Color.yellow;
        canvas_balao_direita_text.color = Color.yellow;
    }

    public void desativa_Canvas()
    {
        current_array_index = 0;
        state = FSMState.Interagindo;

        canvas_balao_esquerda_text.color = Color.white;
        canvas_balao_direita_text.color = Color.white;
        canvas_balao_esquerda_text.fontSize = 100;
        canvas_balao_direita_text.fontSize = 100;

        canvas_balao_esquerda.enabled = false;
        canvas_balao_centro.enabled = false;
        canvas_balao_direita.enabled = false;

        canvas_Mental.enabled = false;

        if (NPC)
        {
            NPC_GP.enabled = false;
            NPC_MM.enabled = false;
            NPC_PG.enabled = false;
        }
    }
}
