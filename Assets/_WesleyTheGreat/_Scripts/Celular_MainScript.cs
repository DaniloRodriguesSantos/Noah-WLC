using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Celular_MainScript : MonoBehaviour
{
    public GameObject prefab_MiniGame;
    public Canvas Main_Canvas;
    PensamentoController script_PensamentoController;
    Audio_MainScript scriptAudio_MainScript;
    Platformer2DUserControl script_Platformer2DUserControl;

    public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues;
    Animator anin;
    Animator anim;
    public conversa_SAM script;
    public GameObject popUp;
    public Text textNome;
    public Text textMensagem;


    public bool celularzando;
    public bool primeiroPopUp = true;

    #region DIARIO

    public GameObject muzzle_Inicial_Diario;
    public GameObject muzzle_Final_Diario;
    public Image[] array_OBJs_Diario;

    #endregion

    #region telas do celular
    public RectTransform rTransform;

    public GameObject celular;
    public GameObject tela_Principal;
    public GameObject tela_Jogo;
    public GameObject tela_GPS;
    public GameObject tela_Zapzap;
    public GameObject tela_Diario;
    #endregion

    #region notifi and bolinhas

    [HideInInspector] public int countNotifi;
    public Text textNotificacao;
    public Image bolinha_textNotificacao;


    [HideInInspector] public int countNotifi_SAM;
    public Text textNotificacao_SAM;
    public Image bolinha_textNotificacao_SAM;


    [HideInInspector] public int countNotifi_MAE;
    public Text textNotificacao_MAE;
    public Image bolinha_textNotificacao_MAE;


    [HideInInspector] public int countNotifi_LEONARD;
    public Text textNotificacao_LEONARD;
    public Image bolinha_textNotificacao_LEONARD;





    [HideInInspector] public int countNotifi_DIARIO;
    public Text textNotificacao_DIARIO;
    public Image bolinha_textNotificacao_DIARIO;

    #endregion

    public GameObject muzzle_ConversaZapzap;
    public GameObject conversa_SAM;
    public GameObject conversa_MAE;
    public GameObject conversa_LEONARD;
    public GameObject zapzap_SAM;
    public GameObject zapzap_MAE;
    public GameObject zapzap_LEONARD;

    bool ativarMouse;
    float tempo_ativarMouse;
    GameObject miniCelular;
    public Image miniCel_Exclamation;

    Image noahComendo_1;
    Image noahComendo_2;
    Image noahComendo_3;
    Image noahMercado;
    Image noahPapelaria;
    public GameObject relogio_Boladao;

    // Alterações Danilo
    private Camera2DFollow camera2DFollow_Script;
    // Use this for initialization
    void Awake()
    {

        noahComendo_1 = GameObject.Find("Img_noahComendo_Dia1").GetComponent<Image>();
        noahComendo_2 = GameObject.Find("Img_noahComendo_Dia2").GetComponent<Image>();
        noahComendo_3 = GameObject.Find("Img_noahComendo_Dia3").GetComponent<Image>();
        noahMercado = GameObject.Find("Img_noahPapelaria").GetComponent<Image>();
        noahPapelaria = GameObject.Find("Img_noahMercado").GetComponent<Image>();


        miniCelular = GameObject.Find("MiniCel");
        script_PensamentoController = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        anim = GameObject.Find("_PopUpMensagem").GetComponent<Animator>();
        scriptAudio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        array_OBJs_Diario = muzzle_Inicial_Diario.GetComponentsInChildren<Image>();

        anin = celular.GetComponent<Animator>();

        // Alterações Danilo
        camera2DFollow_Script = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
    }
    private void Start()
    {
        script_Platformer2DUserControl.iNeverFreeze = true;
        primeiroPopUp = true;
        celular.SetActive(false);
        tela_Principal.SetActive(true);
        tela_Jogo.SetActive(false);
        tela_GPS.SetActive(false);
        tela_Zapzap.SetActive(false);
        tela_Diario.SetActive(false);

        miniCelular.SetActive(false);
        conversa_SAM.SetActive(false);
        conversa_MAE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (script_PensamentoController.entrouPensamento_Tipo2 || noahComendo_1.enabled || noahComendo_2.enabled || noahComendo_3.enabled || noahMercado.enabled || noahPapelaria.enabled || SceneManager.GetActiveScene().name == "Prologo")
        {
            miniCelular.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Dia_4" && relogio_Boladao.activeSelf)
        {
            miniCelular.SetActive(false);
        }
        else
        {
            miniCelular.SetActive(true);
        }

        #region counts de notificação

        countNotifi = countNotifi_MAE + countNotifi_SAM + countNotifi_LEONARD;
        textNotificacao.text = countNotifi.ToString();
        if (countNotifi > 0)
        {
            bolinha_textNotificacao.enabled = true;
            textNotificacao.enabled = true;
            miniCel_Exclamation.enabled = true;
        }
        else if (countNotifi_DIARIO > 0)
        {
            bolinha_textNotificacao_DIARIO.enabled = true;
            miniCel_Exclamation.enabled = true;
            textNotificacao_DIARIO.enabled = true;
        }
        else
        {
            bolinha_textNotificacao.enabled = false;
            textNotificacao.enabled = false;
            miniCel_Exclamation.enabled = false;


            bolinha_textNotificacao_DIARIO.enabled = false;
            miniCel_Exclamation.enabled = false;
            textNotificacao_DIARIO.enabled = false;
        }


        textNotificacao_SAM.text = countNotifi_SAM.ToString();
        if (countNotifi_SAM > 0)
        {
            bolinha_textNotificacao_SAM.enabled = true;
            textNotificacao_SAM.enabled = true;
        }
        else
        {
            bolinha_textNotificacao_SAM.enabled = false;
            textNotificacao_SAM.enabled = false;
        }


        textNotificacao_MAE.text = countNotifi_MAE.ToString();
        if (countNotifi_MAE > 0)
        {
            bolinha_textNotificacao_MAE.enabled = true;
            textNotificacao_MAE.enabled = true;
        }
        else
        {
            bolinha_textNotificacao_MAE.enabled = false;
            textNotificacao_MAE.enabled = false;
        }


        textNotificacao_LEONARD.text = countNotifi_LEONARD.ToString();
        if (countNotifi_LEONARD > 0)
        {
            bolinha_textNotificacao_LEONARD.enabled = true;
            textNotificacao_LEONARD.enabled = true;
        }
        else
        {
            bolinha_textNotificacao_LEONARD.enabled = false;
            textNotificacao_LEONARD.enabled = false;
        }



        textNotificacao_DIARIO.text = countNotifi_DIARIO.ToString();
        if (countNotifi_DIARIO > 0)
        {
            bolinha_textNotificacao_DIARIO.enabled = true;
            textNotificacao_DIARIO.enabled = true;
        }
        else
        {
            bolinha_textNotificacao_DIARIO.enabled = false;
            textNotificacao_DIARIO.enabled = false;
        }
        #endregion

        #region Pause
        /*if (Input.GetKeyDown(KeyCode.Tab) && !celularzando)
        {
            celularzando = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && celularzando && anin.GetCurrentAnimatorStateInfo(0).IsName("ENTRY"))
        {
            if (conversa_SAM.activeSelf == false && tela_GPS.activeSelf == false && tela_Jogo.activeSelf == false)
            {
                script_Platformer2DUserControl.iNeverFreeze = true;
                Cursor.visible = false;
                celularzando = false;
            }
        }*/

        if (celularzando)
        {
            Cursor.visible = true;
            script_Platformer2DUserControl.iNeverFreeze = false;
            celular.SetActive(true);
        }
        else
        {
            celular.SetActive(false);
            tela_Principal.SetActive(true);
            tela_Jogo.SetActive(false);
            tela_GPS.SetActive(false);
            tela_Zapzap.SetActive(false);
            tela_Diario.SetActive(false);

            conversa_SAM.SetActive(false);
            conversa_MAE.SetActive(false);
        }

        if (Input.GetAxis("Mouse X") > 0 && Input.GetAxis("Mouse Y") > 0)
        {
            ativarMouse = true;
        }
        else if (Input.GetAxis("Mouse X") < 0 && Input.GetAxis("Mouse Y") < 0)
        {
            ativarMouse = true;
        }

        if (ativarMouse == true)
        {
            Cursor.visible = true;
            tempo_ativarMouse += 1 * Time.deltaTime;
            if (tempo_ativarMouse > 2)
            {
                ativarMouse = false;
                Cursor.visible = false;
                tempo_ativarMouse = 0;

            }
        }
        #endregion


    }
    public void ativarCelular()
    {
        if (/*Input.GetKeyDown(KeyCode.Tab) &&*/ !celularzando)
        {
            celularzando = true;
        }
        else if (/*Input.GetKeyDown(KeyCode.Tab) &&*/ celularzando && anin.GetCurrentAnimatorStateInfo(0).IsName("ENTRY"))
        {
            if (conversa_SAM.activeSelf == false && conversa_MAE.activeSelf == false && conversa_LEONARD.activeSelf == false && tela_GPS.activeSelf == false && tela_Jogo.activeSelf == false)
            {
                script_Platformer2DUserControl.iNeverFreeze = true;
                // Cursor.visible = false;
                celularzando = false;
            }
        }
    }

    public void chamarPopUp()
    {
        StartCoroutine(coroutine());
    }
    public void add_no_Diario()
    {
        array_OBJs_Diario[0].transform.parent = muzzle_Final_Diario.transform;
        //array_OBJs_Diario = muzzle_Inicial_Diario.GetComponentsInChildren<Image>();

        rTransform.sizeDelta += new Vector2(0, +167);
        repor_Diario();
    }
    public void repor_Diario()
    {
        // array_OBJs_Diario[0].transform.parent = muzzle_Final_Diario.transform;
        array_OBJs_Diario = muzzle_Inicial_Diario.GetComponentsInChildren<Image>();

        // rTransform.sizeDelta += new Vector2(0, +167);
    }


    #region voids

    public void irGame()
    {
        /*anin.SetBool("virar", true);
        tela_Principal.SetActive(false);
        tela_Jogo.SetActive(true);
        tela_GPS.SetActive(false);
        tela_Zapzap.SetActive(false);
        tela_Diario.SetActive(false);*/

        prefab_MiniGame.SetActive(true);
        camera2DFollow_Script.changeCamera_MiniGWaves();
        Main_Canvas.enabled = false;
    }
    public void irGPS()
    {
        anin.SetBool("virar", true);
        tela_Principal.SetActive(false);
        tela_Jogo.SetActive(false);
        tela_GPS.SetActive(true);
        tela_Zapzap.SetActive(false);
        tela_Diario.SetActive(false);
    }
    public void irZap()
    {
        tela_Principal.SetActive(false);
        tela_Jogo.SetActive(false);
        tela_GPS.SetActive(false);
        tela_Zapzap.SetActive(true);
        tela_Diario.SetActive(false);


        desativar_Conversa_SAM();
        desativar_Conversa_MAE();
        desativar_Conversa_LEONARD();
    }
    public void irDiario()
    {
        tela_Principal.SetActive(false);
        tela_Jogo.SetActive(false);
        tela_GPS.SetActive(false);
        tela_Zapzap.SetActive(false);
        tela_Diario.SetActive(true);

        limparNotificacao_DIARIO();
    }
    public void voltarPrincipal()
    {
        anin.SetBool("virar", false);
        tela_Principal.SetActive(true);
        tela_Jogo.SetActive(false);
        tela_GPS.SetActive(false);
        tela_Zapzap.SetActive(false);
        tela_Diario.SetActive(false);
    }


    // /////////////////////////////////////////////////////////////////////////////////////////////////////// SAM
    public void ativar_Conversa_SAM()
    {
        conversa_SAM.SetActive(true);
        limparNotificacao_SAM();
    }
    public void desativar_Conversa_SAM()
    {
        conversa_SAM.SetActive(false);
    }
    public void addNotificacao_SAM()
    {
        countNotifi_SAM += 1;
        textNotificacao_SAM.text = countNotifi_SAM.ToString();
    }
    public void limparNotificacao_SAM()
    {
        countNotifi_SAM = 0;
        textNotificacao_SAM.text = "";
    }
    public void addZAPZAP_SAM()
    {
        zapzap_SAM.transform.parent = muzzle_ConversaZapzap.transform;
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////// MAE


    public void ativar_Conversa_MAE()
    {
        conversa_MAE.SetActive(true);
        limparNotificacao_MAE();
    }
    public void desativar_Conversa_MAE()
    {
        conversa_MAE.SetActive(false);
    }
    public void addZAPZAP_MAE()
    {
        zapzap_MAE.transform.parent = muzzle_ConversaZapzap.transform;
    }
    public void addNotificacao_MAE()
    {
        countNotifi_MAE += 1;
        textNotificacao_MAE.text = countNotifi_MAE.ToString();
    }
    public void limparNotificacao_MAE()
    {
        countNotifi_MAE = 0;
        textNotificacao_MAE.text = "";
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////// LEONARD


    public void ativar_Conversa_LEONARD()
    {
        conversa_LEONARD.SetActive(true);
        limparNotificacao_LEONARD();
    }
    public void desativar_Conversa_LEONARD()
    {
        conversa_LEONARD.SetActive(false);
    }
    public void addZAPZAP_LEONARD()
    {
        zapzap_LEONARD.transform.parent = muzzle_ConversaZapzap.transform;
    }
    public void addNotificacao_LEONARD()
    {
        countNotifi_LEONARD += 1;
        textNotificacao_LEONARD.text = countNotifi_LEONARD.ToString();
    }
    public void limparNotificacao_LEONARD()
    {
        countNotifi_LEONARD = 0;
        textNotificacao_LEONARD.text = "";
    }
    #endregion


    public void limparNotificacao_DIARIO()
    {
        countNotifi_DIARIO = 0;
        textNotificacao_DIARIO.text = "";
    }

    IEnumerator coroutine()
    {
        //script_Platformer2DUserControl.iNeverFreeze = false;
        scriptAudio_MainScript.tocar_mensagem();
        anim.SetBool("aparecer", true);
        yield return new WaitForSeconds(0f);
        anim.SetBool("aparecer", false);
        if (SceneManager.GetActiveScene().name == "Prologo")
        {
            if (primeiroPopUp)
            {
                //script_Controller_Assorted_Dialogues.comecarInteracao();
                primeiroPopUp = false;
            }
        }
        //script_Platformer2DUserControl.iNeverFreeze = true;
    }
}