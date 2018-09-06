using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleporte : MonoBehaviour
{

    public Image noahComendo_Dia1;
    public Image noahComendo_Dia2;
    public Image noahComendo_Dia3;

    public Image noahPapelaria;
    public Image noahMercado;

    public bool comendoDia1;
    public bool comendoDia2;
    public bool comendoDia3;
    public bool mercado;
    public bool papelaria;

    public GameObject relogio;
    public GameObject sprite;
    public GameObject metroStation;
    public desativarDias script_desativarDias;
    Transform Player;
    public GameObject depoisDoMercado;
    public GameObject teleporteDepoisPapelaria;
    public GameObject teleporteDepoisPapelaria_2;
    public bool metro;
    public bool cortaCena;
    public bool desativarSprite;
    public bool semApertar_E;
    public bool isso_é_porta;

    Audio_MainScript script_Audio_MainScript;
    Platformer2DUserControl script_Platformer2DUserControl;

    //Alterações Danilo
    private PlatformerCharacter2D platformer2DCharacter_Script;
    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
        script_Platformer2DUserControl = Player.GetComponent<Platformer2DUserControl>();
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        platformer2DCharacter_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();
    }

    public void teleportar()
    {
        Player.position = metroStation.transform.position;
        script_Platformer2DUserControl.iNeverFreeze = true;

        //Alterações Danilo
        platformer2DCharacter_Script.allowDeactvArea = true;

        /*if (comendoDia1)
        {
            noahComendo_Dia1.enabled = true;
        }
        else if (comendoDia2)
        {
            noahComendo_Dia2.enabled = true;
        }
        else if (comendoDia3)
        {
            noahComendo_Dia3.enabled = true;
        }
        else if (mercado)
        {
            noahMercado.enabled = true;
        }
        else if (papelaria)
        {
            noahPapelaria.enabled = true;
        }*/


        if (isso_é_porta)
        {
            script_Audio_MainScript.tocar_Passagem_porta();
        }
        if (metro)
        {
            script_Audio_MainScript.tocar_Metro();
        }
        if (desativarSprite)
        {
            sprite.SetActive(false);
        }

        if (gameObject.name == "Teleporte(Sala<->Sala)_Dia4")
        {
            StartCoroutine(relogioDiogo());
            script_desativarDias.desativar_Dia_4();
            script_desativarDias.ativar_Dia_4();
        }
        if (gameObject.name == "Teleporte(mesa<->mesa)")
        {
            script_desativarDias.desativar_Dia_1();
            script_desativarDias.ativar_Dia_1();
        }
        if (gameObject.name == "Teleporte(mesa<->mesa) DIA 2")
        {

            script_desativarDias.desativar_Dia_2();
            script_desativarDias.ativar_Dia_2();

        }
        if (gameObject.name == "Teleporte(mesa<->mesa) DIA 3")
        {

            script_desativarDias.desativar_Dia_3();
            script_desativarDias.ativar_Dia_3();

        }
        if (gameObject.name == "Teleporte(merdado<->mardado)ida" && SceneManager.GetActiveScene().name == "Dia_3")
        {
            // StartCoroutine(dpoisMercado());
            depoisDoMercado.SetActive(true);
            teleporteDepoisPapelaria.SetActive(true);
            teleporteDepoisPapelaria_2.SetActive(false);
        }
        if (gameObject.name == "Teleporte(merdado<->mardado)ida" && SceneManager.GetActiveScene().name == "Dia_2")
        {
            // StartCoroutine(dpoisMercado());
            depoisDoMercado.SetActive(true);
            //teleporteDepoisPapelaria.SetActive(true);
            //teleporteDepoisPapelaria_2.SetActive(false);
        }
        if (gameObject.name == "Teleporte(FimDia2) DIA 2")
        {
            StartCoroutine(finalizarDia());
        }
    }


    IEnumerator dpoisMercado()
    {
        yield return new WaitForSeconds(2);
        depoisDoMercado.SetActive(true);
        gameObject.SetActive(false);
    }
    IEnumerator relogioDiogo()
    {
        relogio.SetActive(true);
        yield return new WaitForSeconds(3);
        relogio.SetActive(false);
        //depoisDoMercado.SetActive(true);
        //gameObject.SetActive(false);
    }
    IEnumerator finalizarDia()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(4);
    }
}
