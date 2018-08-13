using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Canvas_MainScript : MonoBehaviour
{
    Platformer2DUserControl scriptPlatformer2DUserControl;
    DIAProgress_Controller scriptDIAProgress_Controller;
    Interativos scriptInterativo;
    Audio_MainScript scriptAudio_MainScript;

    public GameObject caixaMental;
    public GameObject principalPause;
    public GameObject controles;
    public GameObject opcoes;
    public GameObject confirmarSaida;

    public Image imgPreta;
    public Image imgMetro;
    public Image imgWorm;
    public Image imgLogo;
    public Text textMainPensamento;

    public Text textNOAHApresentacao;

    public GameObject agradecimentosNOAH;
    public bool activate_CutscenePrologo;
    bool act_menuprincipal;
    bool act_desktop;

    public GameObject NoahPensando;
    public Toggle tg_FullScreen;
    public Slider sliderMUSICA;
    public Slider sliderEFEITOS;
    public AudioMixer am;

    #region Alterações Danilo
    private bool isPause = false;
    #endregion
    public bool entroNoFim;
    #region COROUTINES
    IEnumerator coroutine()
    {
        NoahPensando.SetActive(true);
        scriptPlatformer2DUserControl.iNeverFreeze = false;
        textMainPensamento.enabled = true;
        imgPreta.enabled = true;
        yield return new WaitForSeconds(8.5f);
        NoahPensando.SetActive(false);
        scriptAudio_MainScript.aplicar_prologo_OST();
        NoahPensando.SetActive(false);
        imgPreta.enabled = false;
       
        imgWorm.enabled = true;
        yield return new WaitForSeconds(2f);
       /* scriptInterativo.interagir_COM_Mental();*/
        yield return new WaitForSeconds(4f);
        
        textMainPensamento.enabled = false;
        //caixaMental.SetActive(false);
        //textMainPensamento.enabled = false;
        //imgWorm.enabled = false;
        //scriptPlatformer2DUserControl.iNeverFreeze = true;
    }
     
    public IEnumerator coroutineApresentacaoNOAH()
    {
        
        caixaMental.SetActive(false);
        imgLogo.gameObject.SetActive(true);
        scriptAudio_MainScript.pausarMusica();
        yield return new WaitForSeconds(2f);
        scriptAudio_MainScript.prologo_OST.Stop();
        scriptAudio_MainScript.tocar_logo();
        yield return new WaitForSeconds(12f);
        imgPreta.enabled = true;
        NoahPensando.SetActive(true);
        textNOAHApresentacao.gameObject.SetActive(true);
        //scriptDIAProgress_Controller.posicionarPlayerQuarto = true;
        //scriptPlatformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(0.5f);
        textNOAHApresentacao.text = "MEU NOME É NOAH, TENHO 15 ANOS E ESTOU COMEÇANDO HOJE MEU PRIMEIRO DIA NO ENSINO MÉDIO...";

        yield return new WaitForSeconds(7.5f);
        textNOAHApresentacao.text = "ESTOU MUITO NERVOSO COM ISSO, ESPERO QUE AS COISAS SEJAM MELHORES ESSE ANO E NÃO SEJA UM SACO COMO FOI ANO PASSADO...";

        yield return new WaitForSeconds(7.5f);
        textNOAHApresentacao.text = "SE VIEREM COM AQUELAS PIADINHAS DE NOVO EU... NA REAL, QUEM EU QUERO ENGANAR? NÃO VOU CONSEGUIR FAZER NADA MESMO...";

        yield return new WaitForSeconds(7.5f);
        textNOAHApresentacao.text = "TALVEZ ELES ESTEJAM ATÉ CERTOS AO MEU RESPEITO E... AAAAAAA MDS VOU ME ATRASAR!!!!!!!";
        yield return new WaitForSeconds(8.5f);
        SceneManager.LoadScene(2);
        /*scriptAudio_MainScript.aplicar_game_OST();
        scriptDIAProgress_Controller.posicionarPlayerQuarto = false;
       // scriptPlatformer2DUserControl.iNeverFreeze = true;


        //imgPreta.enabled = false;
        imgLogo.enabled = false;
        textNOAHApresentacao.enabled = false;*/
    }
    IEnumerator coroutineAgradecimentosNoah()
    {
        agradecimentosNOAH.SetActive(true);
        //scriptAudio_MainScript.pausarMusica();
        yield return new WaitForSeconds(1f);
        scriptAudio_MainScript.tocar_logo();
        //scriptPlatformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(20f);

    }
    #endregion

    private void Awake()
    {
        scriptDIAProgress_Controller = GameObject.Find("Player").GetComponent<DIAProgress_Controller>();
        scriptPlatformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        scriptAudio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        textNOAHApresentacao.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {

        if (PlayerPrefs.HasKey("volume_M"))
        {
            sliderMUSICA.value = PlayerPrefs.GetFloat("volume_M");
            sliderEFEITOS.value = PlayerPrefs.GetFloat("volume_E");
        }
        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        principalPause.SetActive(false);
        controles.SetActive(false);
        confirmarSaida.SetActive(false);
        opcoes.SetActive(false);
        isPause = false;

        //textMainPensamento.enabled = false;
       // imgPreta.enabled = false;
       // imgWorm.enabled = false;
        imgLogo.gameObject.SetActive(false);
        // textNOAHApresentacao.gameObject.SetActive(false);

        /*  if (activate_CutscenePrologo)
          {
              StartCoroutine(coroutine());
          }*/
        //StartCoroutine(coroutineApresentacaoNOAH());
        //StartCoroutine(coroutineAgradecimentosNoah());
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        am.SetFloat("ost_Volume", sliderMUSICA.value);
        am.SetFloat("sfx_Volume", sliderEFEITOS.value);
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            pausarComMenu();
        }
        if (entroNoFim)
        {
            if (scriptInterativo.state == Interativos.FSMState.soDeBoa)
            {
                StartCoroutine(coroutineAgradecimentosNoah());
                entroNoFim = false;
            }
        }
        if (tg_FullScreen.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

    }

    #region VOIDs ///////////////////////////////////////////////////////////////////////////////////////////////////////////////   VOIDs ///

    public void apresentacaoNOAH()
    {
        StartCoroutine(coroutineApresentacaoNOAH());
        
    }
    public void agradecimentosNoah()
    {
        entroNoFim = true;
        scriptInterativo = GameObject.Find("éNecesario3").GetComponent<Interativos>();
        //scriptInterativo.interagir_COM_Mental();
    }


    public void pausarComMenu()
    {
        scriptAudio_MainScript.tocar_MenuPause();
        Cursor.visible = true;
        isPause = true;
        principalPause.SetActive(true);
        controles.SetActive(false);
        Time.timeScale = 0f;
    }

    public void continuarJogo()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        Cursor.visible = false;
        isPause = false;
        principalPause.SetActive(false);
        controles.SetActive(false);
        opcoes.SetActive(false);
        confirmarSaida.SetActive(false);
        Time.timeScale = 1f;
    }

    public void irControles()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        principalPause.SetActive(false);
        controles.SetActive(true);
        opcoes.SetActive(false);
        confirmarSaida.SetActive(false);
    }

    public void irOpcoes()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        principalPause.SetActive(false);
        controles.SetActive(false);
        opcoes.SetActive(true);
        confirmarSaida.SetActive(false);
    }

    public void voltarPrincipal()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        principalPause.SetActive(true);
        controles.SetActive(false);
        opcoes.SetActive(false);
        confirmarSaida.SetActive(false);

        act_menuprincipal = false;
        act_desktop = false;

        PlayerPrefs.SetFloat("volume_M", sliderMUSICA.value);
        PlayerPrefs.SetFloat("volume_E", sliderEFEITOS.value);


    }

    public void irConfirmarSaida__MENU()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        principalPause.SetActive(false);
        controles.SetActive(false);
        confirmarSaida.SetActive(true);
        opcoes.SetActive(false);

        act_menuprincipal = true;
    }
    public void irConfirmarSaida__DESKTOP()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        principalPause.SetActive(false);
        controles.SetActive(false);
        confirmarSaida.SetActive(true);
        opcoes.SetActive(false);

        act_desktop = true;
    }

    public void resolution_1920()
    {
        Screen.SetResolution(1920,1080, tg_FullScreen);
    }
    public void resolution_1600()
    {
        Screen.SetResolution(1600, 900, tg_FullScreen);
    }
    public void resolution_1280()
    {
        Screen.SetResolution(1280, 720, tg_FullScreen);
    }

    public void MENUPRINCIPAL_SAIR()
    {
        scriptAudio_MainScript.tocar_MenuBotao();
        if (act_menuprincipal)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        else if (act_desktop)
        {
            Application.Quit();
        }
    }
    #endregion

}
