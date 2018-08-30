using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC1_MainScript : MonoBehaviour
{
    public GameObject exclamation_sprite;
    Canvas player_Canvas;
    GameObject mental_player_Canvas;
    Canvas main_Canvas;
    Image main_Sprite;
    Image img_efeito_minigame;
    Text main_Text;
    Text player_text;
    Text player_text_mental;
    [HideInInspector] public BoxCollider2D boxC;
    Platformer2DUserControl script_Platformer2DUserControl;
    PensamentoController script_pensamentoController;
	Celular_MainScript script_Celular_MainScript;
	apresentacao_NOAH script_apresentacao_NOAH;

    public enum FSMState { Boiando, Interagindo, Mental, Entry };
    public FSMState state = FSMState.Boiando;

    bool voltandoDoMiniGame_Balao;
    bool voltandoDoMiniGame_Mental;

    #region ---------------------------------------------------------------- HABILIDADES //
    [Header("--Habilidades--")]

    public bool interagir_sem_apertar_E;
    public bool interagir_somente_1_vez;
    public bool noah_pensa_depois;
    public bool tem_pergunta;
    public bool ativar_minigame_p1;
    public bool ativar_minigame_p2;
    public int puzzleIndex;
    public bool conversa_quando_volta_do_minigame;
    public bool pensar_quando_volta_do_minigame;
    #endregion

    #region ---------------------------------------------------------------- ARRAYS DE DIALOGO //
    [Space(10)]
    [Header("Dialogo Inicial")]

    [TextArea(5, 2)]
    public string[] array_Dialogo_inicial;

    [Space(5)]
    [Header("Dialogo Mental quando acaba a conversa")]
    [TextArea(5, 2)]
    public string[] array_Dialogo_mental;
    public Sprite[] sprites_Dialogo_mental;

    [Space(5)]
    [Header("Dialogo Balao quando volta do minigame")]
    [TextArea(5, 2)]
    public string[] array_Dialogo_balao_Minigame_1;
    public string[] array_Dialogo_balao_Minigame_2;
    public string[] array_Dialogo_balao_Minigame_3;

    [Space(5)]
    [Header("Dialogo Mental quando volta do minigame")]
    [TextArea(5, 2)]
    public string[] array_Dialogo_mental_Minigame_1;
    public Sprite[] sprites_Dialogo_mental_Minigame_1;
    public string[] array_Dialogo_mental_Minigame_2;
    public Sprite[] sprites_Dialogo_mental_Minigame_2;
    public string[] array_Dialogo_mental_Minigame_3;
    public Sprite[] sprites_Dialogo_mental_Minigame_3;

    int Dialogo_inicial__CURRENT_INDEX;
    int Dialogo_mental__CURRENT_INDEX;
    #endregion
    
    bool ativarPopUp;

    #region ---------------------------------------------------------------- AWAKE/START //

    private void Awake()
    {
        main_Canvas = GameObject.Find("Canvas_" + gameObject.name).GetComponent<Canvas>();
        main_Text = main_Canvas.GetComponentInChildren<Text>();
        main_Sprite = GameObject.Find("imgNoah").GetComponent<Image>();
        player_Canvas = GameObject.Find("CanvasPlayerDialogo").GetComponent<Canvas>();
        mental_player_Canvas = GameObject.Find("_CaixaMental");
        player_text = player_Canvas.GetComponentInChildren<Text>();
        player_text_mental = GameObject.Find("TextMental").GetComponent<Text>();
        boxC = GetComponent<BoxCollider2D>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        script_pensamentoController = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();
        img_efeito_minigame = GameObject.Find("img_efeitoMiniGame").GetComponent<Image>();
		script_Celular_MainScript = GameObject.Find ("_Celular_2").GetComponent<Celular_MainScript>();
		script_apresentacao_NOAH = GameObject.Find ("CONTROLLER_MINE").GetComponent<apresentacao_NOAH>();
    }

    void Start()
    {
        mental_player_Canvas.SetActive(false);
        main_Canvas.enabled = false;
        player_Canvas.enabled = false;
        Dialogo_inicial__CURRENT_INDEX = 0;
        Dialogo_mental__CURRENT_INDEX = 0;
        img_efeito_minigame.gameObject.SetActive(false);
    }

    #endregion

    #region ---------------------------------------------------------------- UPDATE/FIXED //

    void Update()
    {
        
        if (state == FSMState.Interagindo)
        {
            if (Input.GetKeyDown("e"))
            {
                Dialogo_inicial__CURRENT_INDEX += 1;
            }
        }
        if (state == FSMState.Mental)
        {
            if (Input.GetKeyDown("e"))
            {
                Dialogo_mental__CURRENT_INDEX += 1;
            }
        }
		if (ativarPopUp) {

			
			script_Celular_MainScript.chamarPopUp ();
			ativarPopUp = false;
            gameObject.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.Boiando: boiandoState(); break;
            case FSMState.Interagindo: interagindoState(); break;
            case FSMState.Mental: mentalState(); break;
            case FSMState.Entry: entryState(); break;
        }

        if (state == FSMState.Interagindo)
        {
            if (main_Text.text == "&")
            {
                Dialogo_inicial__CURRENT_INDEX += 1;
                if (main_Canvas.enabled == true)
                {
                    main_Canvas.enabled = false;
                    player_Canvas.enabled = true;
                }
                else
                {
                    main_Canvas.enabled = true;
                    player_Canvas.enabled = false;
                }
            }
        }


    }

    #endregion

    #region ---------------------------------------------------------------- STATES //

    void boiandoState()
    {
        if(exclamation_sprite != null) { exclamation_sprite.SetActive(true); }
        
    }

    void interagindoState()
    {
        if(exclamation_sprite != null) { exclamation_sprite.SetActive(false); }
        


        if (voltandoDoMiniGame_Balao == false)
        {
            main_Text.text = array_Dialogo_inicial[Dialogo_inicial__CURRENT_INDEX];
            player_text.text = array_Dialogo_inicial[Dialogo_inicial__CURRENT_INDEX];

            if (Dialogo_inicial__CURRENT_INDEX == array_Dialogo_inicial.Length - 1)
            {
                if (noah_pensa_depois == true)
                {
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
                        state = FSMState.Boiando;
						if (gameObject.name == "Vaca_NPC") {
							ativarPopUp = true;
						}
                        desativarTUDO();
                    }
                }
            }
        }
        else
        {
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 1")
            {
                main_Text.text = array_Dialogo_balao_Minigame_1[Dialogo_inicial__CURRENT_INDEX];
                player_text.text = array_Dialogo_balao_Minigame_1[Dialogo_inicial__CURRENT_INDEX];

                if (Dialogo_inicial__CURRENT_INDEX == array_Dialogo_balao_Minigame_1.Length - 1)
                {
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 2")
            {
                main_Text.text = array_Dialogo_balao_Minigame_2[Dialogo_inicial__CURRENT_INDEX];
                player_text.text = array_Dialogo_balao_Minigame_2[Dialogo_inicial__CURRENT_INDEX];

                if (Dialogo_inicial__CURRENT_INDEX == array_Dialogo_balao_Minigame_2.Length - 1)
                {
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 3")
            {
                main_Text.text = array_Dialogo_balao_Minigame_3[Dialogo_inicial__CURRENT_INDEX];
                player_text.text = array_Dialogo_balao_Minigame_3[Dialogo_inicial__CURRENT_INDEX];

                if (Dialogo_inicial__CURRENT_INDEX == array_Dialogo_balao_Minigame_3.Length - 1)
                {
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
        }
    }

    void mentalState()
    {

        mental_player_Canvas.SetActive(true);
        main_Canvas.enabled = false;
        player_Canvas.enabled = false;


        if (voltandoDoMiniGame_Mental == false)
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
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
        }
        else
        {
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 1")
            {
                player_text_mental.text = array_Dialogo_mental_Minigame_1[Dialogo_mental__CURRENT_INDEX];
                main_Sprite.sprite = sprites_Dialogo_mental_Minigame_1[Dialogo_mental__CURRENT_INDEX];
                if (Dialogo_mental__CURRENT_INDEX == array_Dialogo_balao_Minigame_1.Length - 1)
                {
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 2")
            {
                player_text_mental.text = array_Dialogo_mental_Minigame_2[Dialogo_mental__CURRENT_INDEX];
                main_Sprite.sprite = sprites_Dialogo_mental_Minigame_2[Dialogo_mental__CURRENT_INDEX];
                if (Dialogo_mental__CURRENT_INDEX == array_Dialogo_balao_Minigame_2.Length - 1)
                {
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 3")
            {
                player_text_mental.text = array_Dialogo_mental_Minigame_3[Dialogo_mental__CURRENT_INDEX];
                main_Sprite.sprite = sprites_Dialogo_mental_Minigame_3[Dialogo_mental__CURRENT_INDEX];
                if (Dialogo_mental__CURRENT_INDEX == array_Dialogo_balao_Minigame_3.Length - 1)
                {
                    state = FSMState.Boiando;
                    desativarTUDO();
                }
            }
            

            
        }
    }

    void entryState()
    {

    }

    #endregion
    IEnumerator irParaMiniGames_P1()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativarTUDO();
        script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.Boiando;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);

    }
    IEnumerator irParaMiniGames_P2()
    {
        script_pensamentoController.puzzleInfo = puzzleIndex;
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativarTUDO();
        script_pensamentoController.trocaPosicao_PTipo2 = true;
        state = FSMState.Boiando;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);
    }
    #region ---------------------------------------------------------------- VOIDS //

    void ativarBangs()
    {
        main_Text.text = "";
        player_text.text = "";
        main_Canvas.enabled = true;
    }

    void desativarTUDO()
    {
        mental_player_Canvas.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
        main_Canvas.enabled = false;
        player_Canvas.enabled = false;
        Dialogo_inicial__CURRENT_INDEX = 0;
        Dialogo_mental__CURRENT_INDEX = 0;
        
    }

    public void start_conversa_Balao()
    {
        ativarBangs();
        state = FSMState.Interagindo;
    }
    public void start_conversa_voltando_Balao()
    {
        Dialogo_inicial__CURRENT_INDEX = 0;
        voltandoDoMiniGame_Balao = true;
        ativarBangs();
        state = FSMState.Interagindo;
    }
    public void start_conversa_voltando_Mental()
    {
        Dialogo_mental__CURRENT_INDEX = 0;
        voltandoDoMiniGame_Mental = true;
        ativarBangs();
        state = FSMState.Mental;
    }
    #endregion
}
