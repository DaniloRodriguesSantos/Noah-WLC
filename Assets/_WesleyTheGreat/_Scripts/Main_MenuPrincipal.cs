using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Main_MenuPrincipal : MonoBehaviour
{
    CanvasGroup g_Canvas;

    GameObject _m_Principal;
    GameObject _m_Opcoes;
    GameObject _m_Creditos;


    bool bool_Jogar;
    bool bool_Opcoes;
    bool bool_Creditos;
    bool bool_Voltar;
    bool bool_Sair;
    public bool fadeIN;
    public bool fadeOUT;
    float count;

    public Toggle tg_fullScreen;
    public Slider sliderMUSICA;
    public Slider sliderSFX;
    public AudioMixer au;

    private void Awake()
    {
        _m_Principal = GameObject.Find("Img_MenuPrincipal");
        _m_Opcoes = GameObject.Find("Img_MenuOpções");
        _m_Creditos = GameObject.Find("Img_MenuCreditos");

        g_Canvas = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        _m_Principal.SetActive(true);
        _m_Opcoes.SetActive(false);
        _m_Creditos.SetActive(false);
    }

    void Update()
    {
        au.SetFloat("ost_Volume", sliderMUSICA.value);
        au.SetFloat("afx_Volume", sliderSFX.value);

        if (tg_fullScreen.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        Cursor.visible = true;
        if (bool_Jogar)
        {
            count += 1 * Time.deltaTime;
            if (count > 1.1f)
            {
                //anin_MenuPrincipal.SetBool("irFade_OUT", false);
                SceneManager.LoadScene(1);
                count = 0;
                bool_Jogar = false;
            }
        }
        else if (bool_Opcoes)
        {
            count += 1 * Time.deltaTime;
            if (count > 1.1f)
            {
                _m_Principal.SetActive(false);
                _m_Opcoes.SetActive(true);
                _m_Creditos.SetActive(false);
                count = 0;
                bool_Opcoes = false;
            }
        }
        else if (bool_Creditos)
        {
            count += 1 * Time.deltaTime;
            if (count > 1.1f)
            {
                _m_Principal.SetActive(false);
                _m_Opcoes.SetActive(false);
                _m_Creditos.SetActive(true);
                count = 0;
                bool_Creditos = false;
                //anin_MenuPrincipal.SetBool("irFade_OUT", false);
                // anin_Creditos.SetBool("irFade_IN", true);
            }
        }
        else if (bool_Voltar)
        {
            count += 1 * Time.deltaTime;
            if (count > 1.1f)
            {

                _m_Principal.SetActive(true);
                _m_Opcoes.SetActive(false);
                _m_Creditos.SetActive(false);
                count = 0;
                bool_Voltar = false;
                // anin_MenuPrincipal.SetBool("irFade_IN", true);

                // anin_Opcoes.SetBool("irFade_OUT", false);
                //anin_Creditos.SetBool("irFade_OUT", false);
            }
        }
        else if (bool_Sair)
        {
            count += 1 * Time.deltaTime;
            if (count > 1.1f)
            {


                count = 0;
                bool_Sair = false;

                Application.Quit();
            }
        }

        if (fadeIN)
        {
            fade_IN_gCanvas();
        }
        if (fadeOUT)
        {
            fade_OUT_gCanvas();
        }
    }

    public void resolution_1920()
    {
        Screen.SetResolution(1920, 1080, tg_fullScreen);
    }
    public void resolution_1600()
    {
        Screen.SetResolution(1600, 900, tg_fullScreen);
    }
    public void resolution_1280()
    {
        Screen.SetResolution(1280, 720, tg_fullScreen);
    }

    public void iniciarJOGO()
    {
        StartCoroutine(ativar_CanvasGroup());

        bool_Jogar = true;
    }
    public void entrarOPCOES()
    {
        StartCoroutine(ativar_CanvasGroup());

        bool_Opcoes = true;
    }
    public void entrarCREDITOS()
    {
        StartCoroutine(ativar_CanvasGroup());

        bool_Creditos = true;
    }
    public void voltarMENU()
    {
        StartCoroutine(ativar_CanvasGroup());

        bool_Voltar = true;
    }
    public void sairJOGO()
    {
        StartCoroutine(ativar_CanvasGroup());

        bool_Sair = true;
    }

    void fade_IN_gCanvas()
    {
        g_Canvas.alpha += 1f * Time.deltaTime;
        if (g_Canvas.alpha >= 1)
        {
            g_Canvas.alpha = 1;
        }
    }
    void fade_OUT_gCanvas()
    {
        g_Canvas.alpha -= 1f * Time.deltaTime;
        if (g_Canvas.alpha <= 0)
        {
            g_Canvas.alpha = 0;
        }
    }

    IEnumerator ativar_CanvasGroup()
    {
        g_Canvas.blocksRaycasts = false;
        fadeOUT = true;
        yield return new WaitForSeconds(1);
        fadeOUT = false;
        yield return new WaitForSeconds(0.1f);
        fadeIN = true;
        yield return new WaitForSeconds(1);
        fadeIN = false;
        g_Canvas.blocksRaycasts = true;
    }
}
