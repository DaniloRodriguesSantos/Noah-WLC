using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_MainScript : MonoBehaviour {



    [Space(5)]
    [Header("OSTs")]

    public AudioSource prologo_OST;
    public AudioSource game_OST;
    public AudioSource pensamento_OST;

    [Space(5)]
    [Header("SFXs")]

    public AudioSource logo;
    public AudioSource digitandoCel;
    public AudioSource interacao;
    public AudioSource balao;
    public AudioSource mensagem;
    public AudioSource selecionarBotao;
    public AudioSource selecionarPlat;

    [Space(5)]
    [Header("Novos_SFXs")]

    public AudioSource cacaPalavras;
    public AudioSource cristalDesligado;
    public AudioSource cristalLigado;
    public AudioSource elevador;
    public AudioSource geloQuebrando;
    public AudioSource menuBotao;
    public AudioSource menuPause;

    public AudioSource metro;
    public AudioSource passagemSalas_Corrida;
    public AudioSource passagemSalas_portaDupla;
    public AudioSource passoNoah;
    public AudioSource platformFlutuante;
    public AudioSource puloNoah;
    public AudioSource sinalEscola;

    private void Awake()
    {
    }
    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name != "MenuPrincipal" && SceneManager.GetActiveScene().name != "Prologo")
        {
            aplicar_game_OST();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region ////////////////////////////////////////// voids OSTs
    public void aplicar_prologo_OST()
    {
        //prologo_OST.PlayOneShot(prologo_OST.clip);
        prologo_OST.Play();
    }
    public void aplicar_game_OST()
    {
        //game_OST.PlayOneShot(game_OST.clip);
        game_OST.Play();
    }
    public void aplicar_pensamento_OST()
    {
        //prologo_OST.PlayOneShot(prologo_OST.clip);
        pensamento_OST.Play();
    }
    #endregion

    #region ////////////////////////////////////////// voids SFXs
    public void tocar_logo()
    {
        logo.PlayOneShot(logo.clip);
    }
    public void tocar_interacao()
    {
        interacao.PlayOneShot(interacao.clip);
    }
    public void tocar_mensagem()
    {
        mensagem.PlayOneShot(mensagem.clip);
    }
    public void tocar_balao()
    {
        balao.PlayOneShot(balao.clip);
    }
    public void tocar_Botao()
    {
        selecionarBotao.PlayOneShot(selecionarBotao.clip);
    }
    public void tocar_Digitando()
    {
        digitandoCel.PlayOneShot(digitandoCel.clip);
    }



    public void tocar_CacaPalavras()
    {
        cacaPalavras.PlayOneShot(cacaPalavras.clip);
    }
    public void tocar_CristalDesligado()
    {
        cristalDesligado.PlayOneShot(cristalDesligado.clip);
    }
    public void tocar_CristalLigado()
    {
        cristalLigado.PlayOneShot(cristalLigado.clip);
    }
    public void tocar_Elevador()
    {
        elevador.PlayOneShot(elevador.clip);
    }
    public void tocar_GeloQuebrado()
    {
        geloQuebrando.PlayOneShot(geloQuebrando.clip);
    }
    public void tocar_MenuBotao()
    {
        menuBotao.PlayOneShot(menuBotao.clip);
    }
    public void tocar_MenuPause()
    {
        menuPause.PlayOneShot(menuPause.clip);
    }
    public void tocar_Metro()
    {
        metro.PlayOneShot(metro.clip);
    }
    public void tocar_Passagem_Corrida()
    {
        passagemSalas_Corrida.PlayOneShot(passagemSalas_Corrida.clip);
    }
    public void tocar_Passagem_porta()
    {
        passagemSalas_portaDupla.PlayOneShot(passagemSalas_portaDupla.clip);
    }
    public void tocar_PassoNoah()
    {
        passoNoah.PlayOneShot(passoNoah.clip);
    }
    public void tocar_PlatformFlutuante()
    {
        platformFlutuante.PlayOneShot(platformFlutuante.clip);
    }
    public void tocar_PuloNoah()
    {
        puloNoah.PlayOneShot(puloNoah.clip);
    }
    public void tocar_SinalEscola()
    {
        sinalEscola.PlayOneShot(sinalEscola.clip);
    }
    #endregion

    #region ////////////////////////////////////////// voids pause/play/etc...
    public void pausarMusica()
    {
    }
    public void tocarMusica()
    {
    }
    #endregion
    

    
}
