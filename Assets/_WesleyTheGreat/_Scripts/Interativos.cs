using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interativos : MonoBehaviour
{
    /* [Header("Alterações Danilo")]
     #region Alterações Danilo
     public bool hasFPSImg;
     public GameObject FPSImage;
     public bool goTo_Pensamento_Tipo1;
     public bool goTo_Pensamento_Tipo2;
     private PensamentoController pensamentoController_Script;
     public bool runsOnce;
     private bool runsOnce_Activate = false;
     public string type_CaixaMental;
     #endregion*/

    public enum FSMState { soDeBoa, Interagindo, Mental, Notifi, Entry };
    public FSMState state = FSMState.soDeBoa;

    [Space(5)]
    [Header("Habilidades")]
    public bool sera_Balao;
    public bool sera_pensamento;
    public bool sera_notificacao;
    public int contMsg;
    [Space(5)]

    public bool interagir_sem_apertar_E;
    public bool interagir_somente_1_vez;
    public bool Noah_pensa_depois;
    public bool ativar_minigame_P1;
    public bool ativar_minigame_P2;
    public bool conversa_quando_volta_do_minigame;
    public bool pensar_quando_volta_do_minigame;

    [Space(10)]
    [Header("--DIALOGOS--")]

    [Space(5)]
    [Header("Conversa ou mental inicial")]
    [TextArea(4, 2)]
    public string[] array_dialogo_inicial;
    public Sprite[] array_sprite_inicial;

    [Space(5)]
    [Header("Mental quando acaba os baloes")]
    public string[] array_mental_depois_doInicial;
    public Sprite[] array_sprite_depois_doInicial;

    [Space(5)]
    [Header("Conversa quando volta do miniGame")]
    public string[] array_conversa_depois_minigame;

    [Space(5)]
    [Header("Mental quando volta do miniGame")]
    public string[] array_mental_depois_minigame;
    public Sprite[] array_sprite_depois_minigame;

    [HideInInspector] public SpriteRenderer exclamation_sprite;
    Canvas player_Canvas;
    Text player_text;
    public GameObject mental_player_Canvas;
    public Text player_text_mental;
    //Canvas main_Canvas;
    public Image main_Sprite;
    public Image img_efeito_minigame;
    // Text main_Text;
    [HideInInspector] public BoxCollider2D boxC;
    Platformer2DUserControl script_Platformer2DUserControl;
    PensamentoController script_pensamentoController;
    public Celular_MainScript script_Celular_MainScript;

    bool esta_voltando_minigame_Balao;
    bool esta_voltando_minigame_Mental;
    int current_array_index_Balao;
    int current_array_index_Mental;

    Image imgCanetas;
    public GameObject NPC_Vaca;

    Teleporte script_teleporte;

    public GameObject aula1;
    public GameObject aula2;
    public GameObject sala1;
    public GameObject sala2;
    private void Awake()
    {
        exclamation_sprite = GameObject.Find("Sprite_" + gameObject.name).GetComponent<SpriteRenderer>();
        //main_Canvas = GameObject.Find("Canvas_" + gameObject.name).GetComponent<Canvas>();
        //main_Text = main_Canvas.GetComponentInChildren<Text>();
        //main_Sprite = GameObject.Find("imgNoah").GetComponent<Image>();
        player_Canvas = GameObject.Find("CanvasPlayerDialogo").GetComponent<Canvas>();
        //mental_player_Canvas = GameObject.Find("_CaixaMental");
        player_text = player_Canvas.GetComponentInChildren<Text>();
        //player_text_mental = GameObject.Find("TextMental").GetComponent<Text>();
        boxC = GetComponent<BoxCollider2D>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        script_pensamentoController = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();

        //img_efeito_minigame = GameObject.Find("img_efeitoMiniGame").GetComponent<Image>();

        imgCanetas = GameObject.Find("FPSImg_Estante_Papelaria").GetComponent<Image>();
        //script_teleporte = GameObject.Find("Teleporte(Escola<->Sala)").GetComponent<Teleporte>();
        script_Celular_MainScript = GameObject.Find("_Celular_2").GetComponent<Celular_MainScript>();
        imgCanetas.enabled = false;

    }
    private void Start()
    {
        if (gameObject.name == "Estante_IT")
        {
            NPC_Vaca.SetActive(false);
        }
    }
    private void Update()
    {
        //script_Platformer2DUserControl.iNeverFreeze = true;
        if (state == FSMState.Interagindo)
        {
            if (Input.GetKeyDown("t"))
            {
                current_array_index_Balao += 1;
            }
        }
        if (state == FSMState.Mental)
        {
            if (Input.GetKeyDown("t"))
            {
                current_array_index_Mental += 1;
            }
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.soDeBoa: soDeBoaState(); break;
            case FSMState.Interagindo: interagindo_State(); break;
            case FSMState.Mental: mental_State(); break;
            //  case FSMState.Notifi: notifi_State(); break;
            case FSMState.Entry: entryState(); break;
        }
    }
    public void start_notidicacao()
    {
        if (contMsg == 1)
        {
            script_Celular_MainScript.chamarPopUp();
            script_Celular_MainScript.textNome.text = "Sam - Pedido de amizade";
            script_Celular_MainScript.textMensagem.text = "Oi laranjinha, me add ;)";
            StartCoroutine(chamarDialogueDepoisDaNotificacao());
        }
        if (contMsg == 2)
        {
            script_Celular_MainScript.chamarPopUp();
            script_Celular_MainScript.textNome.text = "Mãe";
            script_Celular_MainScript.textMensagem.text = "Oi filho, espero que o primeiro dia de aula tenha sido ótimo, desculpe não podermos estar ai para conversarmos mas amanhã na hora do café da manhã você me conta tudo! Uma boa noite s2";
            StartCoroutine(chamarDialogueDepoisDaNotificacao());
        }

    }

    void soDeBoaState()
    {
        exclamation_sprite.gameObject.SetActive(true);
    }
    void interagindo_State()
    {
        if (!esta_voltando_minigame_Balao)
        {
            player_text.text = array_dialogo_inicial[current_array_index_Balao];

            if (current_array_index_Balao == array_dialogo_inicial.Length - 1)
            {
                if (Noah_pensa_depois)
                {
                    desativaBang_Balao();
                    ativaBang_Mental();
                    state = FSMState.Mental;
                }
                else
                {

                    if (ativar_minigame_P1)
                    {
                        StartCoroutine(irParaMiniGames_P1());
                    }
                    else if (ativar_minigame_P2)
                    {
                        StartCoroutine(irParaMiniGames_P2());

                    }
                    else
                    {

                        desativaBang_Balao();
                        desativaBang_Mental();
                        state = FSMState.soDeBoa;
                    }
                }

            }
        }
        else
        {
            player_text.text = array_conversa_depois_minigame[current_array_index_Balao];

            if (current_array_index_Balao == array_conversa_depois_minigame.Length - 1)
            {
                desativaBang_Balao();
                desativaBang_Mental();
                state = FSMState.soDeBoa;
            }
        }
    }
    IEnumerator acabandoDia1()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(5);
    }
    void mental_State()
    {

        if (!esta_voltando_minigame_Mental)
        {
            if (gameObject.name == "Estante_IT")
            {
                imgCanetas.enabled = true;
            }
            if (Noah_pensa_depois)
            {
                main_Sprite.sprite = array_sprite_depois_doInicial[current_array_index_Mental];
                player_text_mental.text = array_mental_depois_doInicial[current_array_index_Mental];

                if (current_array_index_Mental == array_mental_depois_doInicial.Length - 1)
                {

                    if (ativar_minigame_P1)
                    {
                        StartCoroutine(irParaMiniGames_P1());
                    }
                    else if (ativar_minigame_P2)
                    {
                        StartCoroutine(irParaMiniGames_P2());

                    }
                    else
                    {

                        desativaBang_Balao();
                        desativaBang_Mental();
                        state = FSMState.soDeBoa;
                    }
                }
            }
            else
            {
                main_Sprite.sprite = array_sprite_inicial[current_array_index_Mental];
                player_text_mental.text = array_dialogo_inicial[current_array_index_Mental];


                if (current_array_index_Mental == array_dialogo_inicial.Length - 1)
                {
                    if (gameObject.name == "indoDormir")
                    {
                        img_efeito_minigame.gameObject.SetActive(true);
                        StartCoroutine(acabandoDia1());
                    }
                    if (gameObject.name == "Comida_IT")
                    {
                        print("asfasfasfasf");
                        aula1.SetActive(false);
                        aula2.SetActive(true);
                        sala1.SetActive(false);
                        sala2.SetActive(true);
                    }
                    if (ativar_minigame_P1)
                    {
                        StartCoroutine(irParaMiniGames_P1());
                    }
                    else if (ativar_minigame_P2)
                    {
                        StartCoroutine(irParaMiniGames_P2());

                    }
                    else
                    {
                        if (gameObject.name == "SalaDeAula_IT")
                        {
                            script_teleporte.teleportar();
                        }
                        desativaBang_Balao();
                        desativaBang_Mental();
                        state = FSMState.soDeBoa;
                    }
                }
            }

        }
        else
        {
            player_text_mental.text = array_mental_depois_minigame[current_array_index_Mental];
            main_Sprite.sprite = array_sprite_depois_minigame[current_array_index_Mental];

            if (current_array_index_Mental == array_mental_depois_minigame.Length - 1)
            {
                desativaBang_Balao();
                desativaBang_Mental();
                state = FSMState.soDeBoa;
            }
        }
    }
    void entryState()
    {

    }

    IEnumerator irParaMiniGames_P1()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativaBang_Balao();
        imgCanetas.gameObject.SetActive(false);
        desativaBang_Mental();
        script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.soDeBoa;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);

    }
    IEnumerator irParaMiniGames_P2()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativaBang_Balao();
        imgCanetas.gameObject.SetActive(false);
        desativaBang_Mental();
        script_pensamentoController.trocaPosicao_PTipo2 = true;
        script_pensamentoController.puzzleInfo = 0;
        state = FSMState.soDeBoa;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);
        if (gameObject.name == "Estante_IT")
        {
            NPC_Vaca.SetActive(true);
        }

    }
    IEnumerator chamarDialogueDepoisDaNotificacao()
    {
        yield return new WaitForSeconds(5);
        ativaBang_Mental();
        state = FSMState.Mental;
    }

    public void start_conversa_balao()
    {
        ativaBang_Balao();
        state = FSMState.Interagindo;
    }
    public void start_conversa_mental()
    {
        state = FSMState.Mental;
        ativaBang_Mental();
    }
    public void start_conversa_voltando_Balao()
    {
        esta_voltando_minigame_Balao = true;
        desativaBang_Mental();
        ativaBang_Balao();
        state = FSMState.Interagindo;
    }
    public void start_conversa_voltando_Mental()
    {
        esta_voltando_minigame_Mental = true;
        desativaBang_Balao();
        ativaBang_Mental();
        state = FSMState.Mental;
    }
    public void ativaBang_Balao()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        player_text.text = "";
        player_Canvas.enabled = true;

    }
    public void desativaBang_Balao()
    {
        current_array_index_Balao = 0;
        script_Platformer2DUserControl.iNeverFreeze = true;
        player_text.text = "";
        player_Canvas.enabled = false;

    }
    public void ativaBang_Mental()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        player_text.text = "";
        mental_player_Canvas.SetActive(true);
    }
    public void desativaBang_Mental()
    {
        current_array_index_Mental = 0;
        script_Platformer2DUserControl.iNeverFreeze = true;
        player_text.text = "";
        mental_player_Canvas.SetActive(false);
    }

}
// Update is called once per frame
/*void Update()
{

    if (state == FSMState.InteragindoCOM)
    {
        if (Input.GetKeyDown("t"))
        {
            scriptAudio_MainScript.tocar_balao();
            currentArrayIndex += 1;
        }
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = FSMState.soDeBoa;
            scriptPlatformer2DUserControl.iNeverFreeze = true;
        }
    }
    if (state == FSMState.InteragindoCOMMental)
    {
        spriteMain.sprite = arraySprites[currentArrayIndexMENTAL];

        if (Input.GetKeyDown("t") && hasFPSImg)
        {
            scriptAudio_MainScript.tocar_balao();
            currentArrayIndexMENTAL += 1;
            FPSImage.SetActive(false);
            if (goTo_Pensamento_Tipo2)
            {
                pensamentoController_Script.trocaPosicao_PTipo2 = true;
            }
            if (runsOnce)
            {
                runsOnce_Activate = true;
            }
        }
        else if (Input.GetKeyDown("t"))
        {
            scriptCanvas_MainScript.imgWorm.enabled = false;

            scriptAudio_MainScript.tocar_balao();
            currentArrayIndexMENTAL += 1;
            if (runsOnce)
            {
                runsOnce_Activate = true;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = FSMState.soDeBoa;
            caixaMental.SetActive(false);
            scriptPlatformer2DUserControl.iNeverFreeze = true;
        }
    }

}
private void FixedUpdate()
{
    switch (state)
    {
        case FSMState.soDeBoa: soDeBoaState(); break;
        case FSMState.InteragindoCOM: interagindoCOMState(); break;
        case FSMState.InteragindoCOMMental: interagindoCOMMentalState(); break;
    }
}

// //////////////////////////////////////////////////////////////////////////////////////////////////////// STATEs ///
void soDeBoaState()
{
    if (spriteInteracao != null)
    {
        spriteInteracao.SetActive(true);
    }
    currentArrayIndex = 0;
    currentArrayIndexMENTAL = 0;
    if (runsOnce_Activate)
    {
        gameObject.SetActive(false);
    }
}*/


/*if (scriptDIAProgress_Controller.scriptNPC1_MainScript.ContinuarMental)
    {
        scriptDIAProgress_Controller.scriptNPC1_MainScript.ContinuarMental = false;
        caixaMental.SetActive(true);
        if (spriteInteracao != null)
        {
            spriteInteracao.SetActive(false);
        }
        textPensamento.text = scriptDIAProgress_Controller.scriptNPC1_MainScript.continuarMental[currentArrayIndexMENTAL];

        if (currentArrayIndexMENTAL == scriptDIAProgress_Controller.scriptNPC1_MainScript.continuarMental.Length - 1)
        {
            state = FSMState.soDeBoa;
            caixaMental.SetActive(false);
            scriptPlatformer2DUserControl.iNeverFreeze = true;
        }
    }
    else
    {
        caixaMental.SetActive(true);
        if (spriteInteracao != null)
        {
            spriteInteracao.SetActive(false);
        }
        textPensamento.text = arrayMainMENTAL[currentArrayIndexMENTAL];

        if (currentArrayIndexMENTAL == arrayMainMENTAL.Length - 1)
        {
            caixaMental.SetActive(false);
            scriptPlatformer2DUserControl.iNeverFreeze = true;
            state = FSMState.soDeBoa;

        }


    }*/
