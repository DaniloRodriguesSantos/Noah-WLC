using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_DialoguePerTime : MonoBehaviour
{
    public enum FSMState { SoDeBoa, Interagindo, Mental, Entry };
    public FSMState state = FSMState.SoDeBoa;

    [Header("--Habilidades--")]
    public bool interagir_sem_apertar_E;
    public bool interagir_somente_1_vez;
    public bool noah_pensa_depois;
    public bool ativar_minigame_p1;
    public bool ativar_minigame_p2;
    public bool conversa_quando_volta_do_minigame;
    public bool pensar_quando_volta_do_minigame;

    [Space(5)]
    [Header("Dialogo Mental quando acaba a conversa")]
    [TextArea(5, 2)]
    public string[] array_Dialogo_mental;
    public Sprite[] sprites_Dialogo_mental;

    [Space(5)]
    [Header("Dialogo Balao quando volta do minigame")]
    [TextArea(5, 2)]
    public string[] array_Dialogo_balao_Minigame;

    [Space(5)]
    [Header("Dialogo Mental quando volta do minigame")]
    [TextArea(5, 2)]
    public string[] array_Dialogo_mental_Minigame;
    public Sprite[] sprites_Dialogo_mental_Minigame;

    [Space(10)]
    public GameObject[] NPCs;
    int current_array_index;
    [HideInInspector] public bool isOn;
    [HideInInspector] public BoxCollider2D boxC;
    int Dialogo_mental__CURRENT_INDEX;
    GameObject mental_player_Canvas;
    Text player_text_mental;
    Image main_Sprite;
    Image img_efeito_minigame;

    bool voltandoDoMiniGame_Balao;
    bool voltandoDoMiniGame_Mental;

    PensamentoController script_pensamentoController;
    // Use this for initialization
    void Awake()
    {
        boxC = GetComponent<BoxCollider2D>();
        mental_player_Canvas = GameObject.Find("_CaixaMental");
        player_text_mental = GameObject.Find("TextMental").GetComponent<Text>();
        script_pensamentoController = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();
        main_Sprite = GameObject.Find("imgNoah").GetComponent<Image>();
        img_efeito_minigame = GameObject.Find("img_efeitoMiniGame").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {


        if (state == FSMState.Mental)
        {
            if (Input.GetKeyDown("t"))
            {
                Dialogo_mental__CURRENT_INDEX += 1;
            }
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.SoDeBoa: soDeBoa_State(); break;
            case FSMState.Interagindo: interagindo_State(); break;
            case FSMState.Mental: mental_State(); break;
            case FSMState.Entry: entry_State(); break;
        }
    }
    void soDeBoa_State()
    {
        /*if (!interagir_somente_1_vez)
        {
            boxC.enabled = true;
        }*/
    }
    void interagindo_State()
    {
        if (!voltandoDoMiniGame_Balao)
        {
            if (current_array_index == NPCs.Length - 1)
            {

                if (noah_pensa_depois)
                {
                    ativarBangs_Mental();
                    state = FSMState.Mental;
                }
                else
                {
                    if (ativar_minigame_p1)
                    {
                        StartCoroutine(irParaMiniGames_P1());
                    }
                    else if (ativar_minigame_p2)
                    {
                        StartCoroutine(irParaMiniGames_P2());
                    }
                    else
                    {
                        state = FSMState.SoDeBoa;
                    }
                }
            }
        }
        else
        {

        }
    }
    void mental_State()
    {
        if (!voltandoDoMiniGame_Mental)
        {
            player_text_mental.text = array_Dialogo_mental[Dialogo_mental__CURRENT_INDEX];
            main_Sprite.sprite = sprites_Dialogo_mental[Dialogo_mental__CURRENT_INDEX];

            if (Dialogo_mental__CURRENT_INDEX == array_Dialogo_mental.Length - 1)
            {
                if (ativar_minigame_p1)
                {
                    StartCoroutine(irParaMiniGames_P1());
                }
                else if (ativar_minigame_p2)
                {
                    StartCoroutine(irParaMiniGames_P2());
                }
                else
                {
                    desativarBangs_Mental();
                    state = FSMState.SoDeBoa;
                }
            }
        }
        else
        {
            player_text_mental.text = array_Dialogo_mental_Minigame[Dialogo_mental__CURRENT_INDEX];
            main_Sprite.sprite = sprites_Dialogo_mental_Minigame[Dialogo_mental__CURRENT_INDEX];

            if (Dialogo_mental__CURRENT_INDEX == array_Dialogo_mental_Minigame.Length - 1)
            {
                desativarBangs_Mental();
                state = FSMState.SoDeBoa;
            }
        }
    }
    void entry_State()
    {

    }

    

    IEnumerator irParaMiniGames_P1()
    {
        script_pensamentoController.trocaPosicao_PTipo1 = false;
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativarBangs_Mental();
        //desativarTUDO();
        script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);

    }
    IEnumerator irParaMiniGames_P2()
    {
        script_pensamentoController.trocaPosicao_PTipo1 = false;
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativarBangs_Mental();
        //desativarTUDO();
        script_pensamentoController.trocaPosicao_PTipo2 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);
    }





    public void start_conversa_voltando_Balao()
    {
        voltandoDoMiniGame_Balao = true;
        state = FSMState.Interagindo;
    }
    public void start_conversa_voltando_Mental()
    {
        ativarBangs_Mental();
        Dialogo_mental__CURRENT_INDEX = 0;
        voltandoDoMiniGame_Mental = true;
        state = FSMState.Mental;
    }




    void ativarBangs_Mental()
    {
        player_text_mental.text = "";
        mental_player_Canvas.SetActive(true);
    }
    void desativarBangs_Mental()
    {
        player_text_mental.text = "";
        mental_player_Canvas.SetActive(false);
    }




    public void passarNPC()
    {
        current_array_index += 1;
        for (int i = 0; i < NPCs.Length; i++)
        {
            if (current_array_index == NPCs.Length - 1)
            {
                NPCs[i].SetActive(false);
            }
            else if (NPCs[i] != NPCs[current_array_index])
            {
                NPCs[i].SetActive(false);
            }
            else
            {
                NPCs[current_array_index].SetActive(true);
            }
        }
    }




    public void comecar_sistema()
    {
        current_array_index = 0;
        Dialogo_mental__CURRENT_INDEX = 0;
        for (int i = 0; i < NPCs.Length; i++)
        {
            if (NPCs[i] != NPCs[current_array_index])
            {
                NPCs[i].SetActive(false);
            }
            else
            {
                NPCs[current_array_index].SetActive(true);
            }
        }
        state = FSMState.Interagindo;
        isOn = true;
    }

}
