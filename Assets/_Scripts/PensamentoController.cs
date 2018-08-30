using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PensamentoController : MonoBehaviour
{
    public GameObject Estante;
    Platformer2DUserControl script_Platformer2DUserControl;
    private GameObject g_Player;
    private Transform playerTrans;
    private PlatformerCharacter2D platformer2dcharacter_Script;
    private Vector3 originalPos_Player;
    private Vector3 originalScale_Player;
    private bool side_facing;
    [HideInInspector] public int check_Counter = 0;
    [HideInInspector] public string tipo_resposta_Pesamento;
    #region Pensamentos
    [Space(10)]
    [Header("Plataforma de escolha")]
    [SerializeField]
    private Transform[] position_EscolhaArray;
    public int puzzleInfo;
    [HideInInspector] public bool entrouPensamento_Tipo1 = false;
    [HideInInspector] public bool entrouPensamento_Tipo2 = false;
    #endregion
    [HideInInspector] public bool trocaPosicao_PTipo1;
    [HideInInspector] public bool trocaPosicao_PTipo2;
    [HideInInspector] public bool voltaPosicao;
    private GameController GOController;
    private SystemDialogueMental systemDialogueMental_Script;
    private Camera2DFollow camera2Dfollow_Script;


    #region general variables
    [SerializeField] private Camera mainCamera;
    #endregion

    [SerializeField] private bool isPrologo;
    public bool isThinking = false;

    [HideInInspector] public bool isInCity = false;

    #region Trial Logger
    private TrialLogger trialLogger;
    //// participant id (string)
    //public string participantID = "0001";
    #endregion

    #region Alterações Wesley
    DIAProgress_Controller scriptDIAProgress_Controller;
    Audio_MainScript scriptAudio_MainScript;
    public desativarDias script_salaDia1;
    bool KKK;

    [Header("Alterações Wesley")]
    string[] conversas1 = { "oi...", "&", "ah, você deve estar atrasado. boa aula garoto!", "" };
    string[] conversas2 = { "bom dia jaiminho.", "&", " indo pra escola garoto?", "tome cuidado com o caminho e não se esqueça de apertar o botão da passarela!", "" };
    string[] conversas3 = { "bem vindo jaiminho. eu sou noah.", "&", "prazer te conhecer noah!", "tome cuidado quando sair e lembre de apertar o botão da passarela.", "" };
    #endregion


    void Awake()
    {
        scriptAudio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        g_Player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = g_Player.GetComponent<Transform>();
        platformer2dcharacter_Script = g_Player.GetComponent<PlatformerCharacter2D>();
        GOController = GameObject.Find("GameController").GetComponent<GameController>();
        systemDialogueMental_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<SystemDialogueMental>();
        camera2Dfollow_Script = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow>();
        scriptDIAProgress_Controller = GameObject.Find("Player").GetComponent<DIAProgress_Controller>();
        trialLogger = GameObject.Find("Save").GetComponent<TrialLogger>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
    }

    private void Start()
    {
        for (int i = 0; i < position_EscolhaArray.Length; i++)
        {
            position_EscolhaArray[i].parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Teleport();



        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    trocaPosicao_PTipo2 = true;
        //}
        //else
        //{
        //    trocaPosicao_PTipo2 = false;
        //}

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    if (puzzleInfo > 0)
        //    {
        //        puzzleInfo--;
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    if (puzzleInfo < position_EscolhaArray.Length)
        //    {
        //        puzzleInfo++;
        //    }
        //}

        //Debug.Log(puzzleInfo);

    }

    private void Teleport()
    {
        if (trocaPosicao_PTipo2)
        {
            position_EscolhaArray[puzzleInfo].parent.gameObject.SetActive(true);
            scriptAudio_MainScript.aplicar_pensamento_OST();
            scriptAudio_MainScript.prologo_OST.Stop();
            scriptAudio_MainScript.game_OST.Stop();
            isThinking = true;
            originalPos_Player = playerTrans.position;
            side_facing = platformer2dcharacter_Script.m_FacingRight;
            originalScale_Player = playerTrans.localScale;
            playerTrans.position = position_EscolhaArray[puzzleInfo].position;
            entrouPensamento_Tipo2 = true;
            trocaPosicao_PTipo2 = false;
            trialLogger.StartTrial();
            // Camera
            mainCamera.orthographicSize = 6.42f;

        }
        if (voltaPosicao)
        {
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                /*if (script_salaDia1.ativarEscolha == false)
                {
                    script_salaDia1.ATIVAR();
                    script_salaDia1.count = 1;
                }*/
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                /*if (script_salaDia1.ativarEscolha == false)
                {
                    script_salaDia1.ATIVAR();
                    script_salaDia1.count = 2;
                }*/
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                /*if (script_salaDia1.ativarEscolha == false)
                {
                    script_salaDia1.ATIVAR();
                    script_salaDia1.count = 3;
                }*/
            }


            StartCoroutine(DeactivateChoosePlatform(2f));
            if (Estante != null) { Estante.SetActive(false); }
            scriptAudio_MainScript.aplicar_game_OST();
            isThinking = false;
            playerTrans.position = originalPos_Player;
            platformer2dcharacter_Script.m_FacingRight = side_facing;
            playerTrans.localScale = originalScale_Player;
            GOController.interactable_State = null;
            checkTrialLogger();
            if (entrouPensamento_Tipo2)
            {
                //checkResult_Pensamento_Tipo2();
                //GOController.liberarNPC = true;
                entrouPensamento_Tipo2 = false;
            }
            else
            {
                voltaPosicao = false;
            }

            // Camera
            if (!isInCity)
            {
                mainCamera.orthographicSize = 5f;
            }
            // /////////////////////////////////////////
            if (scriptDIAProgress_Controller.scriptNPC1_MainScript != null)
            {
                if (scriptDIAProgress_Controller.scriptNPC1_MainScript.pensar_quando_volta_do_minigame)
                {
                    scriptDIAProgress_Controller.scriptNPC1_MainScript.start_conversa_voltando_Mental();
                }
                if (scriptDIAProgress_Controller.scriptNPC1_MainScript.conversa_quando_volta_do_minigame)
                {
                    scriptDIAProgress_Controller.scriptNPC1_MainScript.start_conversa_voltando_Balao();
                }
            }
            if (scriptDIAProgress_Controller.scriptInterativos != null)
            {
                if (scriptDIAProgress_Controller.scriptInterativos.conversa_quando_volta_do_minigame)
                {
                    scriptDIAProgress_Controller.scriptInterativos.start_conversa_voltando_Balao();
                }
                if (scriptDIAProgress_Controller.scriptInterativos.pensar_quando_volta_do_minigame)
                {
                    scriptDIAProgress_Controller.scriptInterativos.start_conversa_voltando_Mental();
                }
            }
            if (scriptDIAProgress_Controller.script_Controller_DialoguePerTime != null)
            {
                if (scriptDIAProgress_Controller.script_Controller_DialoguePerTime.conversa_quando_volta_do_minigame)
                {
                    scriptDIAProgress_Controller.script_Controller_DialoguePerTime.start_conversa_voltando_Balao();
                }
                if (scriptDIAProgress_Controller.script_Controller_DialoguePerTime.pensar_quando_volta_do_minigame)
                {
                    scriptDIAProgress_Controller.script_Controller_DialoguePerTime.start_conversa_voltando_Mental();
                }
            }

            if(SceneManager.GetActiveScene().name == "Prologo")
            {
                scriptAudio_MainScript.pensamento_OST.Stop();
                scriptAudio_MainScript.aplicar_prologo_OST();
                scriptAudio_MainScript.game_OST.Stop();
            }
            else
            {
                scriptAudio_MainScript.pensamento_OST.Stop();
                scriptAudio_MainScript.prologo_OST.Stop();
                scriptAudio_MainScript.aplicar_game_OST();
            }

           






            // /////////////////////////////////////////
        }
    }

    private IEnumerator DeactivateChoosePlatform(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        position_EscolhaArray[puzzleInfo].parent.gameObject.SetActive(false);
    }

    //private void checkResult_Pensamento_Tipo2()
    //{
    //    if (isPrologo)
    //    {
    //        if (tipo_resposta_Pesamento == "Resposta 1")
    //        {

    //        }
    //        if (tipo_resposta_Pesamento == "Resposta 2")
    //        {

    //        }
    //        if (tipo_resposta_Pesamento == "Resposta 3")
    //        {

    //        }
    //    }
    //}

    private void checkTrialLogger()
    {
        if (puzzleInfo == 0)
        {
            trialLogger.trial["Plataforma de escolha"] = "Prologo";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Escolheu a caneta mais comum";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Escolheu a caneta mais ergonomica";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Escolheu a caneta mais estranha";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 1)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 1 - James";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Sera que ele ta falando comigo mesmo?";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Devo responder por educacao?";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Talvez se eu der uma chance pra ele...";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 2)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 1 - Isaac";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Acho que vou ficar na minha mesmo, outra pessoa deve saber";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Mas e se eu nao tiver 100% de certeza? E se eu acabar falando errado?";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Vou falar, eu ja vi esses nomes varias vezes, tenho certeza de quem sao.";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 3)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 2 - Sam";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Provavelmente ela vai me zuar como as outras pessoas, acho melhor ficar quieto.";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Sera que vao me encher por eu jogar agora?";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Devo falar? Mas eu sou bom, espero que ela nao me ache convencido...";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 4)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 3 - Sam";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "O que sera que eu respondo? E seu eu falar algo que ela nao goste?";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Meu deus ela veio! Mas nao posso demonstrar que to muito feliz.";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Sera que devo fazer alguma brincadeira? Sera que ela nao ligaria?";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 5)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 4 - Kitty Kei";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Acho que ela nem deve ta querendo saber pra valer, ela nem me conhece direito.";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Provavelmente ela so deve estar sendo educada.";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Essa professora ate que parece legal.";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 6)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 4 - Sam";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Provavelmente ela nem quer que eu va…";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Sera que ela quer mesmo que eu va?";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Talvez eu deva aceitar… Nunca se sabe quando alguem vai querer sair comigo de novo…";
            }
            trialLogger.EndTrial();
        }

        if (puzzleInfo == 7)
        {
            trialLogger.trial["Plataforma de escolha"] = "Dia 5";
            if (tipo_resposta_Pesamento == "Resposta 1")
            {
                trialLogger.trial["Escolha"] = "Esse jogo é mais focado no online, sera que eu deveria pegar? Interagir com pessoas…";
            }
            if (tipo_resposta_Pesamento == "Resposta 2")
            {
                trialLogger.trial["Escolha"] = "Esse jogo tem algo entre o online e jogar sozinho… Nao seria obrigado a interagir com pessoas, mas ainda poderia ter a chance…";
            }
            if (tipo_resposta_Pesamento == "Resposta 3")
            {
                trialLogger.trial["Escolha"] = "Hmmmm… Esse é legal, jogar completamente sozinho…";
            }
            trialLogger.EndTrial();
        }
    }
}
