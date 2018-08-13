using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    Platformer2DUserControl script_Platformer2DUserControl;

    #region
    #endregion

    #region publics
    public GameObject mainTutorial;
    public Text mainText;

    public float tempo_Entre_Tutos;
    public float tempo_Para_Iniciar;
    #endregion

    #region BOOLs

    bool b_Canetas;
    bool b_James;
    bool b_Metro_proximo_do_cristal;
    bool b_Metro_cristal_no_terminal;
    bool b_ao_aproximar_do_teletransportador;
    bool b_ao_aproximar_do_Metro;
    bool b_ao_aproximar_do_PainelDeControle;
    bool b_ao_iniciar_caca_palavras;
    bool b_ao_aproximar_do_CristalRoxo;
    bool b_quando_cristal_roxo_solo;
    bool b_ao_aproximar_do_CristalVerde;
    bool b_quando_cristal_verde_solo;

    #endregion

    #region colisores

    public GameObject o_Canetas;
    public GameObject o_James;
    public GameObject o_Metro_proximo_do_cristal;
    public GameObject o_Metro_cristal_no_terminal;
    public GameObject o_ao_aproximar_do_teletransportador;
    public GameObject o_ao_aproximar_do_Metro;
    public GameObject o_ao_aproximar_do_PainelDeControle;
    public GameObject o_ao_iniciar_caca_palavras;
    public GameObject o_ao_aproximar_do_CristalRoxo;
    public GameObject o_quando_cristal_roxo_solo;
    public GameObject o_ao_aproximar_do_CristalVerde;
    public GameObject o_quando_cristal_verde_solo;

    #endregion
    // Use this for initialization
    private void Awake()
    {
        script_Platformer2DUserControl = GetComponent<Platformer2DUserControl>();
    }
    void Start()
    {
        mainTutorial.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Prologo")
        {
            StartCoroutine(Inicial());
        }
    }

    // Update is called once per frame
    void Update()
    {

        #region IFs

        if (b_Canetas)
        {
            StartCoroutine(Canetas());
            b_Canetas = false;
        }
        if (b_James)
        {
            StartCoroutine(James());
            b_James = false;
        }
        if (b_Metro_proximo_do_cristal)
        {
            StartCoroutine(Metro_proximo_do_cristal());
            b_Metro_proximo_do_cristal = false;
        }
        if (b_Metro_cristal_no_terminal)
        {
            StartCoroutine(Metro_cristal_no_terminal());
            b_Metro_cristal_no_terminal = false;
        }
        if (b_ao_aproximar_do_teletransportador)
        {
            StartCoroutine(ao_aproximar_do_teletransportador());
            b_ao_aproximar_do_teletransportador = false;
        }
        if (b_ao_aproximar_do_Metro)
        {
            StartCoroutine(ao_aproximar_do_Metro());
            b_ao_aproximar_do_Metro = false;
        }
        if (b_ao_aproximar_do_PainelDeControle)
        {
            StartCoroutine(ao_aproximar_do_PainelDeControle());
            b_ao_aproximar_do_PainelDeControle = false;
        }
        if (b_ao_iniciar_caca_palavras)
        {
            StartCoroutine(ao_iniciar_caca_palavras());
            b_ao_iniciar_caca_palavras = false;
        }
        if (b_ao_aproximar_do_CristalRoxo)
        {
            StartCoroutine(ao_aproximar_do_CristalRoxo());
            b_ao_aproximar_do_CristalRoxo = false;
        }
        /*if (b_quando_cristal_roxo_solo)
        {
            StartCoroutine(quando_cristal_roxo_solo());
            b_quando_cristal_roxo_solo = false;
        }*/
        if (b_ao_aproximar_do_CristalVerde)
        {
            StartCoroutine(ao_aproximar_do_CristalVerde());
            b_ao_aproximar_do_CristalVerde = false;
        }
        /*if (b_quando_cristal_verde_solo)
        {
            StartCoroutine(quando_cristal_verde_solo());
            b_quando_cristal_verde_solo = false;
        }*/
        #endregion

    }


    #region COROUTINES

    IEnumerator Inicial()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(1f);
        script_Platformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(13.5f);
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Use as teclas 'A' e 'D' para se mover";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Você pode interagir com os balões de exclamação usando a tecla 'E'";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "A tecla 'E' também passa os diálogos";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Para acessar o menu aperte a tecla 'Esc'";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator Canetas()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(tempo_Para_Iniciar);
        mainText.text = "Aqui você se encontra dentro da cabeça de Noah";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Use a tecla 'ESPAÇO' para pular";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Faça sua escolha e confirme usando a tecla 'E'";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator James()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        yield return new WaitForSeconds(tempo_Para_Iniciar);
        mainText.text = "Novamente estamos dentro da cabeça de Noah";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Mas desta vez, selecione uma resposta e confirme usando a tecla 'E'";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator Metro_proximo_do_cristal()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Veja, um 'Cristal Azul' ele é conhecido por fornecer energia!";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Porém é necessário conectá-lo a um 'Terminal' para que ele funcione!";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "Segure 'E' para arrastar o 'Cristal Azul'";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator Metro_cristal_no_terminal()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Ótimo! Podemos ver pelo circuito, que ativamos a energia da ponte!";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator ao_aproximar_do_teletransportador()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Opa! Um 'Teletransportador' ele pode te levar para um nível acima ou abaixo";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator ao_aproximar_do_Metro()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Chegamos ao 'Metrô' aqui podemos nos locomover para outras regiões";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator ao_aproximar_do_PainelDeControle()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Um 'Painel de Controle'! Com ele, é possível mover a plataforma para um determinado lugar";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    IEnumerator ao_iniciar_caca_palavras()
    {
        Cursor.visible = false;
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Com o mouse, segure e arraste para selecionar a palavra encontrada! Boa sorte!";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
        Cursor.visible = true;
    }
    IEnumerator ao_aproximar_do_CristalRoxo()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Opa! Um 'Cristal Roxo'! Este cristal possui propriedades que permitem ampliar o alcance do 'Cristal Azul'";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "O 'Cristal Roxo' sozinho não possui efeito nenhum, ele deve ser colocado em um 'Terminal' com energia";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    /* IEnumerator quando_cristal_roxo_solo()
     {
         script_Platformer2DUserControl.iNeverFreeze = false;
         mainText.text = "O 'Cristal Roxo' sozinho não possui efeito nenhum, ele deve ser colocado num 'Terminal' com energia";
         mainTutorial.SetActive(true);

         yield return new WaitForSeconds(tempo_Entre_Tutos);
         mainTutorial.SetActive(false);
         script_Platformer2DUserControl.iNeverFreeze = true;
     }*/
    IEnumerator ao_aproximar_do_CristalVerde()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "Veja, um 'Cristal Verde'! Este cristal é bem interessante, ele pode transferir energia de um 'Terminal' para outro";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainText.text = "O 'Cristal Verde' sozinho não possui efeito, ele deve ser colocado em um 'Terminal' com energia";

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }
    /*IEnumerator quando_cristal_verde_solo()
    {
        script_Platformer2DUserControl.iNeverFreeze = false;
        mainText.text = "O 'Cristal Verde' sozinho não possui efeito nenhum, ele deve ser colocado num 'Terminal' com energia";
        mainTutorial.SetActive(true);

        yield return new WaitForSeconds(tempo_Entre_Tutos);
        mainTutorial.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
    }*/

    #endregion

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Prologo")
        {
            if (collision.gameObject.name == o_Canetas.name)
            {
                collision.gameObject.SetActive(false);
                b_Canetas = true;
            }
        }

        if (SceneManager.GetActiveScene().name == "Dia_1")
        {
            if (collision.gameObject.name == o_James.name)
            {
                collision.gameObject.SetActive(false);
                b_James = true;
            }
            if (collision.gameObject.name == o_Metro_proximo_do_cristal.name)
            {
                collision.gameObject.SetActive(false);
                b_Metro_proximo_do_cristal = true;
            }
            if (collision.gameObject.name == o_Metro_cristal_no_terminal.name)
            {
                collision.gameObject.SetActive(false);
                b_Metro_cristal_no_terminal = true;
            }
            if (collision.gameObject.name == o_ao_aproximar_do_teletransportador.name)
            {
                collision.gameObject.SetActive(false);
                b_ao_aproximar_do_teletransportador = true;
            }
            if (collision.gameObject.name == o_ao_aproximar_do_Metro.name)
            {
                collision.gameObject.SetActive(false);
                b_ao_aproximar_do_Metro = true;
            }
            if (collision.gameObject.name == o_ao_aproximar_do_PainelDeControle.name)
            {
                collision.gameObject.SetActive(false);
                b_ao_aproximar_do_PainelDeControle = true;
            }
            if (collision.gameObject.name == o_ao_iniciar_caca_palavras.name)
            {
                collision.gameObject.SetActive(false);
                b_ao_iniciar_caca_palavras = true;
            }
        }

        if (SceneManager.GetActiveScene().name == "Dia_2")
        {
            if (collision.gameObject.name == o_ao_aproximar_do_CristalRoxo.name)
            {
                collision.gameObject.SetActive(false);
                b_ao_aproximar_do_CristalRoxo = true;
            }
            /*if (collision.gameObject.name == o_quando_cristal_roxo_solo.name)
            {
                collision.gameObject.SetActive(false);
                b_quando_cristal_roxo_solo = true;
            }*/
            
            /*if (collision.gameObject.name == o_quando_cristal_verde_solo.name)
            {
                collision.gameObject.SetActive(false);
                b_quando_cristal_verde_solo = true;
            }*/
        }
        if (SceneManager.GetActiveScene().name == "Dia_3")
        {
            if (collision.gameObject.name == o_ao_aproximar_do_CristalVerde.name)
            {
                collision.gameObject.SetActive(false);
                b_ao_aproximar_do_CristalVerde = true;
            }
        }

    }
    #endregion
}
