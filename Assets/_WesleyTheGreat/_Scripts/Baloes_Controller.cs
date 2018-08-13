using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Baloes_Controller : MonoBehaviour
{

    public enum FSMState { MoveState, InteractingNPC, InteractingIT, PerguntandoState};
    public FSMState state = FSMState.MoveState;

    NPC1_MainScript scriptNPC;
    Interativos scriptInterativos;
    Platformer2DUserControl scriptPlayer;

    //public bool entrouNPC = false;
    //public bool entrouIT = false;
    //public bool movingTRUE_interactingFALSE;

    public Canvas canvasPlayer;
    public Canvas canvasPlayerDialogueIT;
    public Canvas canvasPlayerPergunta;
    public Text textPlayer;
    public Text textPlayerInterativos;

    private void Awake()
    {
        canvasPlayer.enabled = false;
        canvasPlayerDialogueIT.enabled = false;
        canvasPlayerPergunta.enabled = false;
        scriptPlayer = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        
    }

    private void Update()
    {
        canvasPlayer.transform.position = new Vector2(transform.position.x , transform.position.y + 3);
        canvasPlayerDialogueIT.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y + 2);
        canvasPlayerPergunta.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y + 2);

        /* if (entrouNPC)
         {
             if (Input.GetKeyDown("e"))
             {
                 scriptNPC.state = NPC1_MainScript.FSMState.Interagindo;
                 state = FSMState.InteractingState;
                 entrouNPC = false;
                 scriptPlayer.iNeverFreeze = false;
             }
         }*/

        /* if (entrouIT)
         {
             textPlayer.text = scriptInterativos.arrayMain[scriptInterativos.currentArrayIndex];

             if (Input.GetKeyDown("e"))
             {
                 funcaoDoInterativo();
             }
         }*/
        
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.MoveState: Move(); break;
            case FSMState.PerguntandoState: Perguntando(); break;
            case FSMState.InteractingNPC: InteractingNPCState(); break;
            case FSMState.InteractingIT: InteractingITState(); break;
        }

    }

    private void Move()
    {
    }

    private void Perguntando()
    {
        canvasPlayerPergunta.enabled = true;
        //scriptPlayer.iNeverFreeze = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = FSMState.MoveState;
            canvasPlayerDialogueIT.enabled = false;
            canvasPlayer.enabled = false;
            canvasPlayerPergunta.enabled = false;
            scriptPlayer.iNeverFreeze = true;
        }
    }
    private void InteractingNPCState()
    {
        canvasPlayer.enabled = true;

        /*if (scriptNPC.currentDialogue == scriptNPC.textosDialogos.Length - 1)
        {
            canvasPlayer.enabled = false;
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = FSMState.MoveState;
            canvasPlayerDialogueIT.enabled = false;
            canvasPlayer.enabled = false;
            canvasPlayerPergunta.enabled = false;
            scriptPlayer.iNeverFreeze = true;
        }
    }
    private void InteractingITState()
    {
        /*if (scriptInterativos.state == Interativos.FSMState.InteragindoCOM)
        {
            canvasPlayerDialogueIT.enabled = true;
        }
        else
        {
            canvasPlayerDialogueIT.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = FSMState.MoveState;
            canvasPlayerDialogueIT.enabled = false;
            canvasPlayer.enabled = false;
            canvasPlayerPergunta.enabled = false;
            scriptPlayer.iNeverFreeze = true;
        }*/
    }

    public void funcaoDoNPC()
    {
        state = FSMState.InteractingNPC;
    }

    public void funcaoDoInterativo()
    {
        state = FSMState.InteractingIT;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC1"))
        {
            scriptNPC = collision.GetComponent<NPC1_MainScript>();
        }
        /*if (collision.gameObject.CompareTag("InterativoCOMDialogue"))
        {
            scriptInterativos = collision.GetComponent<Interativos>();
        }*/
        /*if (collision.gameObject.CompareTag("InterativoCOMDialogueMental"))
        {
            scriptInterativos = collision.GetComponent<Interativos>();
        }*/
    }













    /*public enum FSMState { MoveState, InteractingState, PerguntandoState };
    public FSMState state = FSMState.MoveState;

    verificarResposta scriptResposta;

    #region conversas do player
    string[] NOAH_SAN_ESCOLA_DIA1_MANHA = {"0", "1", "2", "3", "4", "&", "5", "&", "6", "&", "7", "8", "&", "9", "10", "§"};
    string[] NOAH_SAN_ESCOLA_DIA1_MANHA_RESPOSTAL = { "0", "1", "2", "2"};
    string[] NOAH_SAN_ESCOLA_DIA1_MANHA_RESPOSTAR = { "0", "&", "2", "2"};
    #endregion

    public string[] arrayDialogue;
    int currentTextDialogue;

    Canvas canvasNPC;
    Text textNPC;
    public Canvas canvasPlayer;
    public Canvas canvasPlayerPergunta;
    public Image imgBalaoL;
    public Image imgBalaoR;
    public Text textPlayer;

    public GameObject Caixa_Mental;


    bool trocaCaixa;
    private bool enterNPC;
    public bool movingTRUE_interactingFALSE;


    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        imgBalaoL.color = Color.green;
        imgBalaoR.color = Color.gray;
        canvasPlayerPergunta.enabled = false;
        Caixa_Mental.SetActive(false);
        StartCoroutine(coroutine());

    }

    // Update is called once per frame
    void Update()
    {
        canvasPlayer.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y + 2);
        canvasPlayerPergunta.transform.position = new Vector2(transform.position.x, transform.position.y + 2.5f);

        if (enterNPC)
        {
            if (Input.GetKeyDown("e"))
            {
                state = FSMState.InteractingState;
            }
        }
        if (state == FSMState.InteractingState)
        {

            if (trocaCaixa)
            {
                canvasPlayer.enabled = true;
                canvasNPC.enabled = false;
            }
            else
            {
                canvasPlayer.enabled = false;
                canvasNPC.enabled = true;
            }

            if (Input.GetKeyDown("q"))
            {
                currentTextDialogue += 1;
            }

            if (Input.GetKeyDown("m"))
            {
                state = FSMState.MoveState;
                canvasNPC.enabled = false;
                canvasPlayer.enabled = false;
            }
        }
        if (state == FSMState.PerguntandoState)
        {
            if (Input.GetKeyDown("a"))
            {
                imgBalaoL.color = Color.green;
                imgBalaoR.color = Color.gray;
            }
            if (Input.GetKeyDown("d"))
            {
                imgBalaoL.color = Color.gray;
                imgBalaoR.color = Color.green;
            }
            if (imgBalaoL.color == Color.green && Input.GetKeyDown("e"))
            {
                Debug.Log("LEFT");
                state = FSMState.InteractingState;
                currentTextDialogue = 0;
                // AKI ARRAY RESULTANTE DA RESPOSTA ANTERIOR!!!
                arrayDialogue = NOAH_SAN_ESCOLA_DIA1_MANHA_RESPOSTAL;
            }
            if (imgBalaoR.color == Color.green && Input.GetKeyDown("e"))
            {
                Debug.Log("RIGHT");
                state = FSMState.InteractingState;
                currentTextDialogue = 0;
                // AKI ARRAY RESULTANTE DA RESPOSTA ANTERIOR!!!
                arrayDialogue = NOAH_SAN_ESCOLA_DIA1_MANHA_RESPOSTAL;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case FSMState.MoveState: Move(); break;
            case FSMState.PerguntandoState: Perguntando(); break;
            case FSMState.InteractingState: Interacting(); break;
        }
        if (textPlayer.text == "&")
        {
            trocaCaixa = !trocaCaixa;
            currentTextDialogue += 1;
        }
        if (textPlayer.text == "§")
        {
            state = FSMState.PerguntandoState;
        }
    }

    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(11f);
        Caixa_Mental.SetActive(true);
    }

    public void Move()
    {
        movingTRUE_interactingFALSE = true;
        canvasPlayerPergunta.enabled = false;
        currentTextDialogue = 0;
    }
    public void Perguntando()
    {
        enterNPC = false;
        canvasNPC.enabled = false;
        canvasPlayer.enabled = false;
        canvasPlayerPergunta.enabled = true;

    }
    public void Interacting()
    {
        enterNPC = true;
        textNPC.text = arrayDialogue[currentTextDialogue];
        textPlayer.text = arrayDialogue[currentTextDialogue];

        movingTRUE_interactingFALSE = false;
        canvasPlayerPergunta.enabled = false;

        for (int t = 0; t < arrayDialogue.Length; t++)
        {
            if (currentTextDialogue == arrayDialogue.Length - 1)
            {
                canvasNPC.enabled = false;
                canvasPlayer.enabled = false;
                state = FSMState.MoveState;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ADD dias
        if (collision.gameObject.CompareTag("NPC1"))
        {
            scriptResposta = collision.GetComponent<verificarResposta>();
            canvasNPC = collision.GetComponentInChildren<Canvas>();
            textNPC = collision.GetComponentInChildren<Text>();
            if (scriptResposta.jaRespondeu == false)
            {
                arrayDialogue = NOAH_SAN_ESCOLA_DIA1_MANHA;
            }
            enterNPC = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC1"))
        {
            enterNPC = false;
        }
    }*/
}