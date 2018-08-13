using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DIAProgress_Controller : MonoBehaviour
{

    public GameObject finalizarDEMO;

    #region Scripts
    GameController scriptGameController;
    Platformer2DUserControl scriptPlatformer2DUserControl;
    SystemDialogueMental scriptSystemDialogueMental;
    Baloes_Controller scriptSystemDialoguePlayer;
    Canvas_MainScript scriptCanvas_MainScript;
    Teleporte scriptTeleporte;
    [HideInInspector] public Controller_DialoguePerTime script_Controller_DialoguePerTime;
    [HideInInspector] public NPC1_MainScript scriptNPC1_MainScript;
    [HideInInspector] public Interativos scriptInterativos;
    [HideInInspector] public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues;
    Audio_MainScript scriptAudio_MainScript;
    #endregion

    public GameObject imgpreta;
    #region Bools
    bool Dia_0;
    bool Dia_1;
    bool Dia_2;
    bool Dia_3;

    bool entrouNPC;
    bool entrouInterativo;
    bool entrouDPT;
    public bool entrouAsD;
    //bool entrouInterativoMENTAL;
    bool entrouTeleporte;

    public bool posicionarPlayerQuarto;
    #endregion

    public int contadorDeDias = 0;

    public GameObject startPointQuarto;
    // public NPC1_MainScript scriptMoleke_01;
    // public NPC1_MainScript scriptMoleke_03;

    private void Awake()
    {
        scriptGameController = GameObject.Find("GameController").GetComponent<GameController>();
        scriptPlatformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        scriptSystemDialogueMental = GameObject.Find("Player").GetComponent<SystemDialogueMental>();
        scriptSystemDialoguePlayer = GameObject.Find("Player").GetComponent<Baloes_Controller>();
        scriptCanvas_MainScript = GameObject.Find("_MainCanvas").GetComponent<Canvas_MainScript>();
        // scriptNPC1_MainScript = GameObject.Find("_Robozinho").GetComponent<NPC1_MainScript>();
        scriptAudio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
    }

    // Use this for initialization
    void Start()
    {
        contadorDeDias = 1;

        posicionarPlayerQuarto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorDeDias == 0)
        {
            Dia_0 = true;
            Dia_1 = false;
            Dia_2 = false;
            Dia_3 = false;
        }
        if (contadorDeDias == 1)
        {
            Dia_0 = false;
            Dia_1 = true;
            Dia_2 = false;
            Dia_3 = false;
        }
        if (contadorDeDias == 2)
        {
            Dia_0 = false;
            Dia_1 = false;
            Dia_2 = true;
            Dia_3 = false;
        }
        if (contadorDeDias == 3)
        {
            Dia_0 = false;
            Dia_1 = false;
            Dia_2 = false;
            Dia_3 = true;
        }

        if (Dia_0)
        {

        }
        if (Dia_1)
        {
            if (posicionarPlayerQuarto)
            {
                transform.position = startPointQuarto.transform.position;
                posicionarPlayerQuarto = false;
            }
        }
        if (Dia_2)
        {

        }
        if (Dia_3)
        {

        }

        #region ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////  IFs DE CONTROLE
        if (entrouNPC)
        {
            if (scriptNPC1_MainScript.interagir_sem_apertar_E)
            {
                scriptNPC1_MainScript.start_conversa_Balao();
                entrouNPC = false;
                if (scriptNPC1_MainScript.interagir_somente_1_vez)
                {
                    scriptNPC1_MainScript.boxC.enabled = false;
                    scriptNPC1_MainScript.exclamation_sprite.gameObject.SetActive(false);
                }
            }
            else if (Input.GetKeyDown("e"))
            {
                scriptAudio_MainScript.tocar_interacao();
                scriptNPC1_MainScript.start_conversa_Balao();
                entrouNPC = false;
                if (scriptNPC1_MainScript.interagir_somente_1_vez)
                {
                    scriptNPC1_MainScript.boxC.enabled = false;
                }
            }

        }



        if (entrouInterativo)
        {
            if (scriptInterativos.interagir_sem_apertar_E)
            {
                if (scriptInterativos.sera_Balao)
                {
                    scriptInterativos.start_conversa_balao();
                    entrouInterativo = false;
                }
                if (scriptInterativos.sera_pensamento)
                {
                    scriptInterativos.start_conversa_mental();
                    entrouInterativo = false;
                }
                if (scriptInterativos.sera_notificacao)
                {
                    scriptInterativos.start_notidicacao();
                    entrouInterativo = false;
                }
                if (scriptInterativos.interagir_somente_1_vez)
                {
                    scriptInterativos.boxC.enabled = false;
                    scriptInterativos.exclamation_sprite.gameObject.SetActive(false);
                }
            }
            else if (Input.GetKeyDown("e"))
            {
                scriptAudio_MainScript.tocar_interacao();
                if (scriptInterativos.sera_Balao)
                {
                    scriptInterativos.start_conversa_balao();
                    entrouInterativo = false;
                }
                if (scriptInterativos.sera_pensamento)
                {
                    scriptInterativos.start_conversa_mental();
                    entrouInterativo = false;
                }
                if (scriptInterativos.interagir_somente_1_vez)
                {
                    scriptInterativos.boxC.enabled = false;
                    scriptInterativos.exclamation_sprite.gameObject.SetActive(false);
                }
            }
        }



        if (entrouDPT)
        {
            if (script_Controller_DialoguePerTime.interagir_sem_apertar_E)
            {
                script_Controller_DialoguePerTime.comecar_sistema();
                entrouDPT = false;
                script_Controller_DialoguePerTime.boxC.enabled = false;
            }
            if (Input.GetKeyDown("e"))
            {
                script_Controller_DialoguePerTime.comecar_sistema();
                entrouDPT = false;
                script_Controller_DialoguePerTime.boxC.enabled = false;
            }
        }



        if (entrouAsD)
        {

            if (script_Controller_Assorted_Dialogues.interagir_sem_apertar_E)
            {
                script_Controller_Assorted_Dialogues.comecarInteracao();
                if (script_Controller_Assorted_Dialogues.gameObject.name == "notidiMae")
                {
                    entrouAsD = false;
                }
                //entrouAsD = false;
                //script_Controller_Assorted_Dialogues.boxC.enabled = false;
            }
            else if (Input.GetKeyDown("e"))
            {
                script_Controller_Assorted_Dialogues.comecarInteracao();
                //entrouAsD = false;
                //script_Controller_Assorted_Dialogues.boxC.enabled = false;
            }
        }



        if (entrouTeleporte)
        {
            if (scriptTeleporte.semApertar_E)
            {
                if (scriptTeleporte.cortaCena)
                {
                    StartCoroutine(pegarCortaCenaCoroutine());
                }
                else if (scriptTeleporte.metro)
                {
                    StartCoroutine(pegarMetroCoroutine());

                }
                else
                {
                    StartCoroutine(pegarTeleporteCoroutine());

                }
            }
            if (Input.GetKeyDown("e"))
            {

                scriptAudio_MainScript.tocar_interacao();
                if (scriptTeleporte.cortaCena)
                {
                    StartCoroutine(pegarCortaCenaCoroutine());
                }
                else if (scriptTeleporte.metro)
                {
                    StartCoroutine(pegarMetroCoroutine());

                }
                else if (scriptTeleporte.comendoDia1 || scriptTeleporte.comendoDia2 || scriptTeleporte.comendoDia3 || scriptTeleporte.mercado || scriptTeleporte.papelaria)
                {
                    StartCoroutine(teleporteComIMGs());
                }
                else
                {
                    StartCoroutine(pegarTeleporteCoroutine());
                }
                entrouTeleporte = false;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////  IFs DE CONTROLE
        #endregion
    }

    #region IENUMERATORs

    public IEnumerator teleporteComIMGs()
    {
        if (scriptTeleporte.comendoDia1)
        {
            scriptTeleporte.noahComendo_Dia1.enabled = true;
        }
        else if (scriptTeleporte.comendoDia2)
        {
            scriptTeleporte.noahComendo_Dia2.enabled = true;
        }
        else if (scriptTeleporte.comendoDia3)
        {
            scriptTeleporte.noahComendo_Dia3.enabled = true;
        }
        else if (scriptTeleporte.mercado)
        {
            scriptTeleporte.noahMercado.enabled = true;
        }
        else if (scriptTeleporte.papelaria)
        {
            scriptTeleporte.noahPapelaria.enabled = true;
        }
        scriptPlatformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(3f);
        scriptTeleporte.teleportar();
        scriptTeleporte.gameObject.SetActive(false);
        scriptPlatformer2DUserControl.iNeverFreeze = true;
        yield return new WaitForSeconds(0.5f);
        scriptTeleporte.noahComendo_Dia1.enabled = false;
        scriptTeleporte.noahComendo_Dia2.enabled = false;
        scriptTeleporte.noahComendo_Dia3.enabled = false;
        scriptTeleporte.noahMercado.enabled = false;
        scriptTeleporte.noahPapelaria.enabled = false;
       
    }

    public IEnumerator pegarCortaCenaCoroutine()
    {
        imgpreta.gameObject.SetActive(true);
        scriptPlatformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(1.5f);
        scriptTeleporte.teleportar();
        scriptPlatformer2DUserControl.iNeverFreeze = true;
        yield return new WaitForSeconds(0.5f);
        imgpreta.gameObject.SetActive(false);
    }

    IEnumerator pegarMetroCoroutine()
    {
        scriptCanvas_MainScript.imgMetro.gameObject.SetActive(true);
        scriptPlatformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(0.5f);
        scriptTeleporte.teleportar();
        yield return new WaitForSeconds(7f);
        scriptPlatformer2DUserControl.iNeverFreeze = true;
        yield return new WaitForSeconds(0.5f);
        scriptCanvas_MainScript.imgMetro.gameObject.SetActive(false);

        // scriptAudio_MainScript.source.clip;
    }
    IEnumerator pegarTeleporteCoroutine()
    {
        scriptCanvas_MainScript.imgPreta.gameObject.SetActive(true);
        scriptPlatformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(0.5f);
        scriptTeleporte.teleportar();
        scriptPlatformer2DUserControl.iNeverFreeze = true;
        yield return new WaitForSeconds(0.5f);
        scriptCanvas_MainScript.imgPreta.gameObject.SetActive(false);



    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Dia_4")
        {
            if (collision.gameObject.name == finalizarDEMO.name)
            {
                SceneManager.LoadScene(6);
            }
        }

        if (collision.gameObject.CompareTag("NPC1"))
        {
            scriptNPC1_MainScript = collision.GetComponent<NPC1_MainScript>();
            entrouNPC = true;
        }
        if (collision.gameObject.CompareTag("Interativo"))
        {
            scriptInterativos = collision.GetComponent<Interativos>();
            entrouInterativo = true;
        }
        if (collision.gameObject.CompareTag("DPT"))
        {
            script_Controller_DialoguePerTime = collision.GetComponent<Controller_DialoguePerTime>();
            entrouDPT = true;
        }
        if (collision.gameObject.CompareTag("AsD"))
        {
            script_Controller_Assorted_Dialogues = collision.GetComponent<Controller_Assorted_Dialogues>();
            entrouAsD = true;
        }
        if (collision.gameObject.CompareTag("Teleporte"))
        {
            scriptTeleporte = collision.GetComponent<Teleporte>();
            entrouTeleporte = true;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC1"))
        {
            entrouNPC = false;
        }
        if (collision.gameObject.CompareTag("Interativo"))
        {
            entrouInterativo = false;
        }
        if (collision.gameObject.CompareTag("AsD"))
        {
            entrouAsD = false;
        }
        if (collision.gameObject.CompareTag("DPT"))
        {
            entrouDPT = false;
        }
        if (collision.gameObject.CompareTag("Teleporte"))
        {
            entrouTeleporte = false;
        }
    }
}
