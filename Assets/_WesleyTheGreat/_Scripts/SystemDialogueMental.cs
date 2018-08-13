using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemDialogueMental : MonoBehaviour
{
    #region arrays de dialogo mental
    string[] NOAH_PRIMEIRO_DIALOGO_MENTAL = { "comprar o último jogo lançado da empresa x, só eu não joguei ainda!!!11!!onze!!!!", "" };
    string[] NOAH_LIVRARIA_GONDOLA_DIA1 = { "por que é tão difícil escolher entre um ou outro? esse é bom mas esse outro também parece ser bom...", "" };
    string[] NOAH_FINAL_PROLOGO_ATRASADO = { "aaaaa eu esqueci!!! tenho que voltar logo para casa", "" };
    string[] NOAH_ESCOLA_MEDO_DIA4 = { "1111111111111111111111", "2222222222222222222222", "33333333333333333333", "44444444444444444444", "5555555555555555555555555" };

    //Alterações Danilo
    [HideInInspector] public string[] FINALMG_PROLOGO_TIPO2_RES1 = { "Resposta 1: aaaaa eu esqueci!!! tenho que voltar logo para casa", "" };
    [HideInInspector] public string[] FINALMG_PROLOGO_TIPO2_RES2 = { "Resposta 2: aaaaa eu esqueci!!! tenho que voltar logo para casa", "" };
    [HideInInspector] public string[] FINALMG_PROLOGO_TIPO2_RES3 = { "Resposta 3: aaaaa eu esqueci!!! tenho que voltar logo para casa", "" };


    #endregion

    Platformer2DUserControl scriptPlayer;

    public enum FSMState { soDeBoa, Pensando };
    public FSMState state = FSMState.soDeBoa;

    public GameObject caixaMental;
    public Text textPensamento;

    [HideInInspector] public string[] arrayMain;
    [HideInInspector] public int currentArrayIndex;

    public Image imgEu;

    #region Escolhas do jogador nos puzzles de pensamento de escolha de itens boladao na rebimboca parafusetaria

    [HideInInspector] public int escolha_primeiro_puzzle_pivraria_dia1;

    #endregion

    #region BOOLs

    bool primeiroPuzzleMental;
    bool voltarPrimeiroPuzzleMental;
    public bool finalConversaComVendedorPrologo;

    #endregion

    // Alterações Danilo
    private GameController GController;
    public bool activateTextoMental = false;
    private Baloes_Controller systemDialoguePlayer_Script;

    // Fade out
    public GameObject FadeOut_Img;
    public GameObject FadeIn_Img_Logo;
    private float alphaLevel = 0;
    private float alphaLevel_Logo = 0;
    private float totalTime = 0;
    [SerializeField] private float timeToFade = 0;
    private bool activateFadeOut = false;
    private void Awake()
    {
        scriptPlayer = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        GController = GameObject.Find("GameController").GetComponent<GameController>();
        systemDialoguePlayer_Script = GetComponent<Baloes_Controller>();
    }

    // Use this for initialization
    void Start()
    {
        arrayMain = NOAH_PRIMEIRO_DIALOGO_MENTAL;
        //StartCoroutine(coroutineInicial());
    }

    // Update is called once per frame
    void Update()
    {
        if (finalConversaComVendedorPrologo)
        {
            arrayMain = NOAH_FINAL_PROLOGO_ATRASADO;
            currentArrayIndex = 0;
            state = FSMState.Pensando;
            //finalConversaComVendedorPrologo = false;
        }
        if (state == FSMState.Pensando && !finalConversaComVendedorPrologo)
        {
            if (Input.GetKeyDown("t"))
            {
                currentArrayIndex += 1;
                imgEu.enabled = false;
            }
        }
        else if (state == FSMState.Pensando && finalConversaComVendedorPrologo)
        {
            if (Input.GetKeyDown("t"))
            {
                currentArrayIndex += 1;
                imgEu.enabled = false;
                activateFadeOut = true;
                finalConversaComVendedorPrologo = false;
            }
        }

        if (primeiroPuzzleMental)
        {
            scriptPlayer.iNeverFreeze = false;
            if (currentArrayIndex == arrayMain.Length - 1)
            {
                state = FSMState.soDeBoa;
                scriptPlayer.iNeverFreeze = true;
                primeiroPuzzleMental = false;
            }
        }

        if (voltarPrimeiroPuzzleMental)
        {
            if (Input.GetKeyDown("e"))
            {
                arrayMain = NOAH_LIVRARIA_GONDOLA_DIA1;
                state = FSMState.Pensando;
            }
        }

        if (activateFadeOut)
        {
            FadeOut();
        }

    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.soDeBoa: soDeBoaState(); break;
            case FSMState.Pensando: pensandoState(); break;
        }
    }

    public void soDeBoaState()
    {
        //caixaMental.SetActive(false);
    }

    public void pensandoState()
    {
        

        //caixaMental.SetActive(true);
        textPensamento.text = arrayMain[currentArrayIndex];

        if (currentArrayIndex == arrayMain.Length - 1)
        {
            state = FSMState.soDeBoa;
            scriptPlayer.iNeverFreeze = true;
        }
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  VOIDs ///

    public void chamarPopUpMental()
    {
        state = FSMState.Pensando;
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  VOIDs ///

    IEnumerator coroutineInicial()
    {
        yield return new WaitForSeconds(11f);
        state = FSMState.Pensando;
    }

    //    private void OnTriggerEnter2D(Collider2D collision)
    //    {
    //		if (collision.gameObject.CompareTag("ThinkPlatforms_Tipo2"))
    //        {
    //            PensamentoNumberInfo scriptpNumber = collision.gameObject.GetComponent<PensamentoNumberInfo>();
    //            Canvas canvasScriptpNumber = collision.gameObject.GetComponentInChildren<Canvas>();
    //            escolha_primeiro_puzzle_pivraria_dia1 = scriptpNumber.numberPensamento;
    //            canvasScriptpNumber.enabled = true;
    //            voltarPrimeiroPuzzleMental = true;
    //        }
    //    }
    //    private void OnTriggerExit2D(Collider2D collision)
    //    {
    //		if (collision.gameObject.CompareTag("ThinkPlatforms_Tipo2"))
    //        {
    //            Canvas canvasScriptpNumber = collision.gameObject.GetComponentInChildren<Canvas>();
    //            canvasScriptpNumber.enabled = false;
    //            voltarPrimeiroPuzzleMental = false;
    //        }
    //    }

    private void FadeOut()
    {
        scriptPlayer.iNeverFreeze = false;
        FadeOut_Img.SetActive(true);
        //		totalTime += Time.deltaTime;
        //		if (alphaLevel = 1f) {
        //			if (totalTime >= timeToFade) {
        //				alphaLevel += .01f;
        //				totalTime = 0;
        //			}
        //		}
        //		FadeOut_Img.GetComponent<Image> ().color = new Color (0, 0, 0, alphaLevel);
        //		if (alphaLevel = 1f) {
        //			activateFadeOut = false;
        //		}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("IT_Estante") && GController.ip_Interactable)
        {
            primeiroPuzzleMental = true;
            arrayMain = NOAH_LIVRARIA_GONDOLA_DIA1;
            currentArrayIndex = 0;
            state = FSMState.Pensando;
        }
    }
}



