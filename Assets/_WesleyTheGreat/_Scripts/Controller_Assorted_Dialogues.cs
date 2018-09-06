using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_Assorted_Dialogues : MonoBehaviour
{
    public GameObject img_SamGrandona;
    public bool add_diario;
    public Image item_diario_Noah;
    public GameObject muzzleDiario;
    public RectTransform rTransform;
    public enum FSMState { SoDeBoa, Andando, Interagindo, Entry, Notificacao }
    public FSMState state = FSMState.SoDeBoa;
    PensamentoController script_pensamentoController;
    Audio_MainScript script_Audio_MainScript;
    public Main_Memoria scrip_Main_Memoria;
    public Teleporte teleporte_escola_sala_Dia_1;
    public Celular_MainScript script_Celular_MainScript;
    public movimento_Sam script_movimento_Sam;
    public conversa_MAE script_conversa_MAE;
    public conversa_GRUPODASALA script_conversa_LEONARD;
    public GameObject exclamationSprite;
    public GameObject conversaVoltando;
    public GameObject conversaVoltando_2;

    // public bool boolVerificar_volta;

    [Header("--Habilidades--")]
    public bool travarMovimentacao;
    public bool interagir_sem_apertar_E;
    public bool interagir_somente_1_vez;
    public bool ativar_minigame_p1;
    public bool ativar_minigame_p2;
    public int puzzleIndex;
    public bool resposta_Quando_Voltar;
    public bool isso_é_resposta_de_algo;
    public bool temNotificação;
    public bool continuar_depois_de_temNotificação;
    [Space(10)]
    public bool andar_ate_ponto_especifico;
    public Transform ponto_especifico;
    public Image img_efeito_minigame;

    [Space(15)]
    [Header("--Objetos de dialogo--")]
    public GameObject[] objs_de_dialogo;
    public GameObject[] objs_de_dialogo_R1;
    public GameObject[] objs_de_dialogo_R2;
    public GameObject[] objs_de_dialogo_R3;

    Transform player;
    Platformer2DUserControl script_Platformer2DUserControl;
    public Canvas_MainScript script_Canvas_MainScript;
    public int current_objs_array_index;
    [HideInInspector] public bool isOn;
    [HideInInspector] public BoxCollider2D boxC;
    public float tempo;
    Canvas player_canvas;
    public GameObject _caixaMental;

    bool contar;
    float cont;
    Teleporte script_Teleporte;
    public Teleporte script_TeleporteSAM;
    public Teleporte script_mercado_Dia2;

    public GameObject CacaPalavras;
    public GameObject JogoMemoria;
    public GameObject CompleteAFrase;
    public Button btt_completefrase;
    GameObject CacaPalavras2;
    public GameObject colisorParaRefeitorio;
    desativarDias script_salaDia1;
    public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues_Notifi_leonard_foto_noah_HELP_ACIMA;
    public Image imgCanetas_Prologo;
    public GameObject vaca;
    public GameObject vaca_2;
    public GameObject PortaSalaDepois_CompleteAFrase;



    // Use this for initialization
    public void finalizarCompleteFrase()
    {
        btt_completefrase.enabled = false;
        StartCoroutine(sairDaSala_2());
    }
    private void Awake()
    {

        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        boxC = GetComponent<BoxCollider2D>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        player_canvas = GameObject.Find("CanvasPlayerDialogo").GetComponent<Canvas>();
        if (gameObject.name != "_NPCs_(SALADEAULA)_1")
        {
            if (gameObject.name != "_NPCs_(SALADEAULA)_2")
            {
                if (gameObject.name != "_NPCs_(SALADEAULA)_3")
                {
                    //img_efeito_minigame = GameObject.Find("img_efeitoMiniGame").GetComponent<Image>();
                }
            }
        }

        script_pensamentoController = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();

        //Alterações Danilo
        if (SceneManager.GetActiveScene().name == "Dia_1")
        {
            if (GameObject.Find("Teleporte(sala<->escola)") != null)
            {
                script_Teleporte = GameObject.Find("Teleporte(sala<->escola)").GetComponent<Teleporte>();
            }
        }

        //CacaPalavras = GameObject.Find("_Caça-Palavras");
        //CacaPalavras2 = GameObject.Find("_Caça-Palavras2");
    }
    // Update is called once per frame
    private void Start()
    {
        CacaPalavras.SetActive(false);
        _caixaMental.SetActive(false);

        if (gameObject.name != "_NPCs_(SALADEAULA)_1")
        {
            if (gameObject.name != "_NPCs_(SALADEAULA)_2")
            {
                if (gameObject.name != "_NPCs_(SALADEAULA)_3")
                {
                    img_efeito_minigame.gameObject.SetActive(false);
                }
            }
        }


    }

    void Update()
    {
        if (contar)
        {
            cont += 1 * Time.deltaTime;
            if (cont > 5)
            {
                cont = 0;
                contar = false;
                desativarTUDO();
                script_Canvas_MainScript.apresentacaoNOAH();

            }
        }
        /*if (gameObject.name == "Aula_MemoriaGame")
        {
            if (scrip_Main_Memoria.countFINAL == 5)
            {
                print("ENTROU AKI POURA");
                StartCoroutine(sairDaSala());
            }
        }*/
        if (gameObject.name == "SamChega_Dia2"/* || gameObject.name == "SamChega_Dia2Resposta" && script_pensamentoController.trocaPosicao_PTipo2 == false*/)
        {
            script_Platformer2DUserControl.iNeverFreeze = false;
        }

    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.SoDeBoa: soDeBoa_State(); break;
            case FSMState.Andando: andando_State(); break;
            case FSMState.Interagindo: interagindo_State(); break;
            case FSMState.Entry: entry_State(); break;
            case FSMState.Notificacao: notifi_State(); break;
        }
    }

    public void notifi_State()
    {
        if (travarMovimentacao)
        {
            script_Platformer2DUserControl.iNeverFreeze = false;
        }
        if (temNotificação)
        {
            if (gameObject.name == "smsSAM_Dia4")
            {
                script_Celular_MainScript.addZAPZAP_SAM();
                script_Celular_MainScript.addNotificacao_SAM();
                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Sam";
                script_Celular_MainScript.textMensagem.text = "Você tem uma nova notificação";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
            if (gameObject.name == "VACA_HELP_ACIMA")
            {

                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Mãe";
                script_Celular_MainScript.textMensagem.text = "Eu e seu pai vamos voltar para casa depois das 23:00, haverá um happy hour dos funcionarios. Não esqueça que suas aulas começas amanhã, portanto durma cedo";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
            if (gameObject.name == "notidiMae")
            {
                script_Celular_MainScript.addZAPZAP_MAE();
                script_Celular_MainScript.addNotificacao_MAE();
                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Mãe";
                script_Celular_MainScript.textMensagem.text = "Você tem 1 nova notificação";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
            if (gameObject.name == "notidiLeonard")
            {
                script_conversa_LEONARD.conversa_LEONARD_1 = true;
                script_conversa_LEONARD.conversa_LEONARD_2 = false;
                script_conversa_LEONARD.conversa_LEONARD_3 = false;
                script_Celular_MainScript.addZAPZAP_LEONARD();
                script_Celular_MainScript.addNotificacao_LEONARD();
                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Leonard";
                script_Celular_MainScript.textMensagem.text = "Você tem 1 nova notificação";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
            if (gameObject.name == "nofiMaeForaEscola")
            {
                script_conversa_MAE.conversa_MAE_1 = false;
                script_conversa_MAE.conversa_MAE_2 = true;
                script_conversa_MAE.conversa_MAE_3 = false;
                script_Celular_MainScript.addZAPZAP_MAE();
                script_Celular_MainScript.addNotificacao_MAE();
                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Mãe";
                script_Celular_MainScript.textMensagem.text = "Você tem 1 nova notificação";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
            if (gameObject.name == "nofiSamSALADEESTAR")
            {
                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Sam - Pedido de amizade";
                script_Celular_MainScript.textMensagem.text = "Oi laranjinha, me add :)";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
            if (gameObject.name == "Notifi_leonard_foto_noah")
            {
                script_conversa_LEONARD.conversa_LEONARD_1 = false;
                script_conversa_LEONARD.conversa_LEONARD_2 = true;
                script_conversa_LEONARD.conversa_LEONARD_3 = false;
                script_Celular_MainScript.addZAPZAP_LEONARD();
                script_Celular_MainScript.addNotificacao_LEONARD();
                script_Celular_MainScript.chamarPopUp();
                script_Celular_MainScript.textNome.text = "Leonard";
                script_Celular_MainScript.textMensagem.text = "Você tem 1 nova notificação";

                temNotificação = false;
                StartCoroutine(coroutineNotificacao());
            }
        }
    }
    IEnumerator coroutineNotificacao()
    {
        print("asfasdfasdf");
        boxC.enabled = false;
        yield return new WaitForSeconds(5f);
        if (continuar_depois_de_temNotificação)
        {
            boxC.enabled = true;

            comecarInteracao();
        }
        else
        {
            print("entrou aki merda");
            gameObject.SetActive(false);
        }
    }
    public void entry_State()
    {

    }

    void soDeBoa_State()
    {
        /*if(gameObject.name == "SamChega_Dia2")
        {
            script_Platformer2DUserControl.iNeverFreeze = false;
        }*/

    }

    void andando_State()
    {
        // script_Platformer2DUserControl.iNeverFreeze = false;
        script_Platformer2DUserControl.rb.MovePosition(Vector2.Lerp(player.position, ponto_especifico.position, 1f * Time.deltaTime));
        script_Platformer2DUserControl.iNeverFreeze = false;
        tempo += 1 * Time.deltaTime;
        if (tempo > 5f)
        {

            script_Platformer2DUserControl.iNeverFreeze = true;

            isOn = true;

            for (int i = 0; i < objs_de_dialogo.Length; i++)
            {
                if (objs_de_dialogo[i] != objs_de_dialogo[current_objs_array_index])
                {
                    objs_de_dialogo[i].SetActive(false);
                }
                else
                {
                    objs_de_dialogo[current_objs_array_index].SetActive(true);
                }
            }

            state = FSMState.Interagindo;

        }

    }

    IEnumerator irParaMiniGames_P1()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        desativarTUDO();
        script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
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
        if (gameObject.name == "eSTANTE")
        {
            imgCanetas_Prologo.enabled = false;
            exclamationSprite.SetActive(false);
            vaca.SetActive(true);
        }
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1f);
        //script_Platformer2DUserControl.iNeverFreeze = true;
        
        yield return new WaitForSeconds(0.5f);
       // script_Platformer2DUserControl.iNeverFreeze = true;
        img_efeito_minigame.gameObject.SetActive(false);
        if (resposta_Quando_Voltar)
        {
           // script_Platformer2DUserControl.iNeverFreeze = true;
            conversaVoltando.SetActive(true);

            gameObject.SetActive(false);
            //script_Platformer2DUserControl.iNeverFreeze = true;
        }
        if (gameObject.name == "SamChega_Dia2")
        {
            script_Platformer2DUserControl.iNeverFreeze = true;
        }
        // script_Platformer2DUserControl.iNeverFreeze = true;
        /* if (!isso_é_resposta_de_algo)
         {
             boolVerificar_volta = true;
         }*/
    }

    public IEnumerator sairDaSala()
    {
        script_Audio_MainScript.tocar_SinalEscola();
        state = FSMState.Entry;
        if (gameObject.name != "_NPCs_(SALADEAULA)_1")
        {
            if (gameObject.name != "_NPCs_(SALADEAULA)_2")
            {
                if (gameObject.name != "_NPCs_(SALADEAULA)_3")
                {
                    img_efeito_minigame.gameObject.SetActive(true);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            scrip_Main_Memoria.countFINAL = 0;

        }

        yield return new WaitForSeconds(1.5f);
        desativarTUDO();
        script_Platformer2DUserControl.iNeverFreeze = true;
        script_Teleporte.teleportar();
        script_Teleporte.enabled = false;
        //script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);
        if (gameObject.name != "_NPCs_(SALADEAULA)_1")
        {
            if (gameObject.name != "_NPCs_(SALADEAULA)_2")
            {
                if (gameObject.name != "_NPCs_(SALADEAULA)_3")
                {
                    img_efeito_minigame.gameObject.SetActive(false);
                }
            }
        }
    }
    public IEnumerator sairDaSala_2()
    {
        script_Audio_MainScript.tocar_SinalEscola();
        script_TeleporteSAM.gameObject.SetActive(true);
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Cursor.visible = false;
        script_Platformer2DUserControl.iNeverFreeze = true;
        desativarTUDO();
        script_TeleporteSAM.teleportar();
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            CompleteAFrase.SetActive(false);
        }
        script_TeleporteSAM.gameObject.SetActive(false);
        //script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);

    }
    public IEnumerator sairSAM()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        if (gameObject.name == "Aula_CompleteAFrase" || gameObject.name == "Aula_CompleteAFrase (1)")
        {
            CompleteAFrase.SetActive(false);
            PortaSalaDepois_CompleteAFrase.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
        gameObject.SetActive(false);
    }
    IEnumerator finalizarDia()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(3);
        /*desativarTUDO();
        script_Teleporte.teleportar();
        script_Teleporte.enabled = false;
        //script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);*/

    }
    IEnumerator Dia_3_finalizarDia()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(5);
        /*desativarTUDO();
        script_Teleporte.teleportar();
        script_Teleporte.enabled = false;
        //script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);*/

    }
    IEnumerator Dia_4_finalizarDia()
    {
        state = FSMState.Entry;
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(6);
        /*desativarTUDO();
        script_Teleporte.teleportar();
        script_Teleporte.enabled = false;
        //script_pensamentoController.trocaPosicao_PTipo1 = true;
        state = FSMState.SoDeBoa;
        yield return new WaitForSeconds(1.5f);*/

    }

    void desativarTUDO()
    {
        _caixaMental.SetActive(false);
    }

    void interagindo_State()
    {
        if (Input.GetKeyDown("e"))
        {
            script_Audio_MainScript.tocar_balao();
        }
        if (add_diario)
        {
            if (Input.GetKeyDown("e"))
            {
                add_diario = false;
                script_Celular_MainScript.countNotifi_DIARIO += 1;
                Debug.Log("vezes_debug");
                print("vezes_print");
                rTransform.sizeDelta += new Vector2(0, +167);
                item_diario_Noah.transform.parent = muzzleDiario.transform;
            }
        }
        if (gameObject.name == "NoahForoSAM")
        {
            img_SamGrandona.SetActive(true);
        }

        if (gameObject.name == "eSTANTE")
        {
            imgCanetas_Prologo.enabled = true;
        }
        if (gameObject.name == "SamChega_Dia2")
        {
            script_movimento_Sam.movimento = false;
        }
        if (gameObject.name == "SamEncontraNoah")
        {
            script_movimento_Sam.movimento = false;
        }
        tempo = 0;
        exclamationSprite.SetActive(false);
        boxC.enabled = false;
        if (travarMovimentacao)
        {
            script_Platformer2DUserControl.iNeverFreeze = false;
        }
        for (int i = 0; i < objs_de_dialogo.Length; i++)
        {
            if (current_objs_array_index == objs_de_dialogo.Length - 1)
            {
                objs_de_dialogo[i].SetActive(false);
                player_canvas.enabled = false;
                script_Platformer2DUserControl.iNeverFreeze = true;
                //current_objs_array_index = 0;
                state = FSMState.SoDeBoa;
                exclamationSprite.SetActive(true);
                _caixaMental.SetActive(false);


                if (interagir_somente_1_vez)
                {
                    boxC.enabled = false;
                }
                else
                {
                    boxC.enabled = true;
                }


                if (ativar_minigame_p1)
                {
                    StartCoroutine(irParaMiniGames_P1());
                    script_Platformer2DUserControl.iNeverFreeze = true;
                }
                else if (ativar_minigame_p2)
                {
                    StartCoroutine(irParaMiniGames_P2());
                    script_Platformer2DUserControl.iNeverFreeze = true;
                }
                else
                {
                    script_Platformer2DUserControl.iNeverFreeze = true;
                    gameObject.SetActive(false);
                }


                //////

                /*if (gameObject.name == "_NPCs_(SALADEAULA3)")
                {
                    print("sdfasfsaf");
                    _caixaMental.SetActive(false);
                    script_Platformer2DUserControl.iNeverFreeze = false;
                    CacaPalavras.SetActive(true);
                    state = FSMState.Entry;
                }*/
                /*if (gameObject.name == "_NPCs_(SALADEAULA)")
                {
                    colisorParaRefeitorio.SetActive(false);
                    if (ativar_minigame_p1)
                    {
                        StartCoroutine(irParaMiniGames_P1());
                        script_Platformer2DUserControl.iNeverFreeze = true;
                    }
                    else if (ativar_minigame_p2)
                    {
                        StartCoroutine(irParaMiniGames_P2());
                        script_Platformer2DUserControl.iNeverFreeze = true;
                    }
                    else
                    {
                        script_Platformer2DUserControl.iNeverFreeze = true;
                        gameObject.SetActive(false);
                    }
                }*/
                if (gameObject.name == "_NPCs_(SALADEAULA)_1" || gameObject.name == "_NPCs_(SALADEAULA)_2" || gameObject.name == "_NPCs_(SALADEAULA)_3")
                {
                    StartCoroutine(sairDaSala());
                }
            }
        }
    }

    public void passarObj()
    {
        current_objs_array_index += 1;

        if (!isso_é_resposta_de_algo)
        {
            for (int i = 0; i < objs_de_dialogo.Length; i++)
            {
                if (current_objs_array_index == objs_de_dialogo.Length - 1)
                {

                    objs_de_dialogo[i].SetActive(false);
                    script_Platformer2DUserControl.iNeverFreeze = true;
                    player_canvas.enabled = false;
                    // current_objs_array_index = 0;
                    state = FSMState.SoDeBoa;
                    exclamationSprite.SetActive(true);
                    _caixaMental.SetActive(false);
                    if (interagir_somente_1_vez)
                    {
                        boxC.enabled = false;
                    }
                    else
                    {
                        boxC.enabled = true;
                    }

                    if (ativar_minigame_p1)
                    {
                        StartCoroutine(irParaMiniGames_P1());
                        script_Platformer2DUserControl.iNeverFreeze = true;
                    }
                    else if (ativar_minigame_p2)
                    {
                        StartCoroutine(irParaMiniGames_P2());
                        script_Platformer2DUserControl.iNeverFreeze = true;
                    }
                    else
                    {
                        script_Platformer2DUserControl.iNeverFreeze = true;
                        if (gameObject.name == "depois_notidiLeonard")
                        {
                            StartCoroutine(finalizarDia());
                            boxC.enabled = false;
                        }
                        else
                        {
                            gameObject.SetActive(false);

                        }
                    }
                    if (gameObject.name == "_ANTESDETELEPORTAR_")
                    {
                        teleporte_escola_sala_Dia_1.teleportar();
                    }
                    if (gameObject.name == "NoahForoSAM")
                    {
                        //script_Controller_Assorted_Dialogues_Notifi_leonard_foto_noah_HELP_ACIMA.StartCoroutine(Dia_4_finalizarDia());
                    }
                    if (gameObject.name == "Comida")
                    {
                        //teleporte_escola_sala_Dia_1.teleportar();
                        colisorParaRefeitorio.SetActive(true);
                    }
                    if (gameObject.name == "MesaSozinho")
                    {
                        //teleporte_escola_sala_Dia_1.teleportar();
                        colisorParaRefeitorio.SetActive(false);
                    }
                    if (gameObject.name == "_NPCs_(SALADEAULA2)")
                    {
                        /*// print("sdfasfsaf");
                        _caixaMental.SetActive(false);
                        script_Platformer2DUserControl.iNeverFreeze = false;*/
                        CacaPalavras.SetActive(true);
                        /* state = FSMState.Entry;
                         colisorParaRefeitorio.SetActive(false);*/
                    }
                    if (gameObject.name == "Aula_MemoriaGame")
                    {
                        /*// print("sdfasfsaf");
                        _caixaMental.SetActive(false);
                        script_Platformer2DUserControl.iNeverFreeze = false;*/
                        JogoMemoria.SetActive(true);
                        /* state = FSMState.Entry;
                         colisorParaRefeitorio.SetActive(false);*/
                    }
                    if (gameObject.name == "SamChega_Dia2Resposta_2")
                    {
                        script_movimento_Sam.movimento = true;
                        script_movimento_Sam.velocidade = -5;
                        //conversaVoltando_2.SetActive(true);
                        gameObject.SetActive(true);
                        StartCoroutine(sairSAM());
                    }
                    if (gameObject.name == "SamEncontraNoah_Resposta2_2")
                    {
                        script_movimento_Sam.movimento = true;
                        script_movimento_Sam.velocidade = -10;
                        // conversaVoltando_2.SetActive(true);
                        gameObject.SetActive(true);
                        StartCoroutine(sairSAM());
                    }
                    if (gameObject.name == "Aula_perguntandoSam")
                    {
                        gameObject.SetActive(true);
                        StartCoroutine(sairDaSala_2());
                    }
                    if (gameObject.name == "Aula_CompleteAFrase")
                    {
                        CompleteAFrase.SetActive(true);

                    }
                    if (gameObject.name == "Notifi_leonard_foto_noah_HELP_ACIMA")
                    {
                        script_Controller_Assorted_Dialogues_Notifi_leonard_foto_noah_HELP_ACIMA.StartCoroutine(script_Controller_Assorted_Dialogues_Notifi_leonard_foto_noah_HELP_ACIMA.Dia_3_finalizarDia());

                    }
                    if (gameObject.name == "VACA")
                    {
                        vaca_2.SetActive(true);
                        //state = FSMState.Notificacao;
                    }

                    if (gameObject.name == "VACA_HELP_ACIMA")
                    {
                        script_Canvas_MainScript.StartCoroutine(script_Canvas_MainScript.coroutineApresentacaoNOAH());
                        //state = FSMState.Notificacao;
                    }

                    if (gameObject.name == "NoahForoSAM")
                    {
                        img_SamGrandona.SetActive(false);
                    }

                }
                else if (objs_de_dialogo[i] != objs_de_dialogo[current_objs_array_index])
                {
                    objs_de_dialogo[i].SetActive(false);
                }
                else
                {
                    objs_de_dialogo[current_objs_array_index].SetActive(true);
                }
            }
        }
        else
        {
            if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 1")
            {
                for (int i = 0; i < objs_de_dialogo_R1.Length; i++)
                {

                    if (objs_de_dialogo_R1[i] != objs_de_dialogo_R1[current_objs_array_index])
                    {
                        objs_de_dialogo_R1[i].SetActive(false);
                    }
                    else
                    {
                        objs_de_dialogo_R1[current_objs_array_index].SetActive(true);
                    }


                    if (current_objs_array_index == objs_de_dialogo_R1.Length - 1)
                    {
                        objs_de_dialogo_R1[i].SetActive(false);
                        script_Platformer2DUserControl.iNeverFreeze = true;
                        player_canvas.enabled = false;
                        // current_objs_array_index = 0;
                        state = FSMState.SoDeBoa;
                        exclamationSprite.SetActive(true);
                        _caixaMental.SetActive(false);
                        if (gameObject.name == "_NPCs_(SALADEAULA)RESPOSTA")
                        {
                            StartCoroutine(sairDaSala());
                        }
                        /* if (gameObject.name == "_NPCs_(SALADEAULA)RESPOSTA")
                         {
                             StartCoroutine(sairDaSala());
                         }*/
                        if (gameObject.name == "SamChega_Dia2Resposta")
                        {
                            conversaVoltando_2.SetActive(true);
                            gameObject.SetActive(false);
                        }
                        if (gameObject.name == "SamEncontraNoah_Resposta2")
                        {
                            conversaVoltando_2.SetActive(true);
                            gameObject.SetActive(false);
                        }

                    }

                }

            }
            else if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 2")
            {
                for (int i = 0; i < objs_de_dialogo_R2.Length; i++)
                {


                    if (objs_de_dialogo_R2[i] != objs_de_dialogo_R2[current_objs_array_index])
                    {
                        objs_de_dialogo_R2[i].SetActive(false);
                    }
                    else
                    {
                        objs_de_dialogo_R2[current_objs_array_index].SetActive(true);
                    }


                    if (current_objs_array_index == objs_de_dialogo_R2.Length - 1)
                    {
                        objs_de_dialogo_R2[i].SetActive(false);
                        script_Platformer2DUserControl.iNeverFreeze = true;
                        player_canvas.enabled = false;
                        // current_objs_array_index = 0;
                        state = FSMState.SoDeBoa;
                        exclamationSprite.SetActive(true);
                        _caixaMental.SetActive(false);
                        if (gameObject.name == "_NPCs_(SALADEAULA)RESPOSTA")
                        {
                            StartCoroutine(sairDaSala());
                        }
                        if (gameObject.name == "SamChega_Dia2Resposta")
                        {
                            conversaVoltando_2.SetActive(true);
                            gameObject.SetActive(false);
                        }
                        if (gameObject.name == "SamEncontraNoah_Resposta2")
                        {
                            conversaVoltando_2.SetActive(true);
                            gameObject.SetActive(false);
                        }
                    }


                }

            }
            else if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 3")
            {
                for (int i = 0; i < objs_de_dialogo_R3.Length; i++)
                {
                    if (objs_de_dialogo_R3[i] != objs_de_dialogo_R3[current_objs_array_index])
                    {
                        objs_de_dialogo_R3[i].SetActive(false);
                    }
                    else
                    {
                        objs_de_dialogo_R3[current_objs_array_index].SetActive(true);
                    }

                    if (current_objs_array_index == objs_de_dialogo_R3.Length - 1)
                    {
                        objs_de_dialogo_R3[i].SetActive(false);
                        script_Platformer2DUserControl.iNeverFreeze = true;
                        player_canvas.enabled = false;
                        // current_objs_array_index = 0;
                        state = FSMState.SoDeBoa;
                        exclamationSprite.SetActive(true);
                        _caixaMental.SetActive(false);
                        if (gameObject.name == "_NPCs_(SALADEAULA)RESPOSTA")
                        {
                            StartCoroutine(sairDaSala());
                        }
                        if (gameObject.name == "SamChega_Dia2Resposta")
                        {
                            conversaVoltando_2.SetActive(true);
                            gameObject.SetActive(false);
                        }
                        if (gameObject.name == "SamEncontraNoah_Resposta2")
                        {
                            conversaVoltando_2.SetActive(true);
                            gameObject.SetActive(false);
                        }
                    }

                }

            }
        }
    }

    public void comecarInteracao()
    {
        current_objs_array_index = 0;
        if (gameObject.name == "_mentalDepoisQuePassaOPopUp")
        {
            contar = true;
        }
        if (andar_ate_ponto_especifico)
        {
            state = FSMState.Andando;
        }
        else
        {
            isOn = true;
            if (temNotificação)
            {
                state = FSMState.Notificacao;
            }
            else
            {
                state = FSMState.Interagindo;
                if (isso_é_resposta_de_algo)
                {
                    if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 1")
                    {
                        for (int i = 0; i < objs_de_dialogo_R1.Length; i++)
                        {
                            if (objs_de_dialogo_R1[i] != objs_de_dialogo_R1[current_objs_array_index])
                            {
                                objs_de_dialogo_R1[i].SetActive(false);
                            }
                            else
                            {
                                objs_de_dialogo_R1[current_objs_array_index].SetActive(true);
                            }
                        }
                    }
                    else if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 2")
                    {
                        for (int i = 0; i < objs_de_dialogo_R2.Length; i++)
                        {
                            if (objs_de_dialogo_R2[i] != objs_de_dialogo_R2[current_objs_array_index])
                            {
                                objs_de_dialogo_R2[i].SetActive(false);
                            }
                            else
                            {
                                objs_de_dialogo_R2[current_objs_array_index].SetActive(true);
                            }
                        }
                    }
                    else if (script_pensamentoController.tipo_resposta_Pesamento == "Resposta 3")
                    {
                        for (int i = 0; i < objs_de_dialogo_R3.Length; i++)
                        {
                            if (objs_de_dialogo_R3[i] != objs_de_dialogo_R3[current_objs_array_index])
                            {
                                objs_de_dialogo_R3[i].SetActive(false);
                            }
                            else
                            {
                                objs_de_dialogo_R3[current_objs_array_index].SetActive(true);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < objs_de_dialogo.Length; i++)
                    {
                        if (objs_de_dialogo[i] != objs_de_dialogo[current_objs_array_index])
                        {
                            objs_de_dialogo[i].SetActive(false);
                        }
                        else
                        {
                            objs_de_dialogo[current_objs_array_index].SetActive(true);
                        }
                    }
                }
            }

        }
    }
}
