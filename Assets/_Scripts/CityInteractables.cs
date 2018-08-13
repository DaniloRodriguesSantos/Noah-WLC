using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityInteractables : MonoBehaviour
{
    public enum FSMStates { BotaoActvDeactv, BotaoSimples, Teleportador, Caixa, BotaoPlatMovel, BotaoPlatMovelHorizontal, BotaoPlatMovelNiveis }
    public FSMStates state;

    #region General Variables
    private Animator IC_Interativo;
    [SerializeField] private bool isAlreadyActivated;
    Audio_MainScript scriptAudio_MainScript;
    private Transform playerTrans;
    private PlatformerCharacter2D platformer2dcharacter_script;
    #endregion

    #region Botao ActvDeactv State
    [Header("Botao ActvDeactv State")]
    [SerializeField] private GameObject Obj_BotaoActivates;
    [SerializeField] private GameObject Obj_BotaoDeactivates;
    #endregion

    #region Botao Simples State
    [Header("Botao Simples State")]
    [SerializeField] private GameObject Obj_BotaoSimplesDeactivates;
    #endregion

    #region Botao Plataforma Movel State
    [Space(10)]
    [Header("Botao PlatMovel State")]
    [SerializeField] private PlatformMovement MovingPlatform;
    [SerializeField] private Transform posBPlatform;
    private Platformer2DUserControl platformer2dUsercontrol_Script;

    // Horizontal
    [Space(10)]
    [Header("Botao PlatMovel Horizontal/Niveis State")]
    private bool interactedPlatHorizontal = false;
    [SerializeField] private Transform Left_posBPlatform;
    [SerializeField] private Transform Right_posBPlatform;

    [Space(10)]
    [SerializeField] private Transform[] Niveis_posBPlatform;
    private int numberOfLevels;
    private int selectedLevel = 0;
    #endregion

    #region Teleportador State
    [Space(10)]
    [Header("Teleportador State")]
    public Transform Teleport_goTo_Point;
    private GameObject player;
    private GameObject player_Anima;
    private Rigidbody2D player_rb;
    private CircleCollider2D player_circleCol;
    private BoxCollider2D player_boxCol;
    private float t;
    #endregion

    #region Caixa State
    //[Space(10)]
    //[Header("Caixa State")]
    [HideInInspector] public bool beingPushed;
    private float xPos;
    private Rigidbody2D CaixaRB;
    private Transform trans;
    #endregion

    #region Botao Plataforma Niveis
    [SerializeField] private GameObject[] PlatformLevel_Indicator;
    #endregion

    #region Botao Plataforma Horizontal
    [SerializeField] private GameObject[] PlatformHorizontal_Indicator;
    #endregion

    //#region Trial Logger
    //public TrialLogger trialLogger;
    //// participant id (string)
    //public string participantID = "0001";
    //#endregion
    private bool GiveInput_PlatformNiveis = false;
    private Camera2DFollow camera2DFollow_Script;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_Anima = GameObject.Find("AnimaNoah");
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player_circleCol = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        player_boxCol = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        player_rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        camera2DFollow_Script = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
        numberOfLevels = Niveis_posBPlatform.Length;
        scriptAudio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        platformer2dUsercontrol_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Platformer2DUserControl>();
        platformer2dcharacter_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();

        trans = GetComponent<Transform>();


        if (state != FSMStates.Caixa)
        {
            IC_Interativo = this.GetComponentInChildren<Animator>();
        }
        else if (state == FSMStates.Caixa)
        {
            CaixaRB = this.GetComponent<Rigidbody2D>();
        }

        //if (this.state == FSMStates.Caixa)
        //{
        //    Save_Caixa();
        //}
    }

    // Use this for initialization
    void Start()
    {

        if (this.state != FSMStates.Caixa)
        {
            IC_Interativo.gameObject.SetActive(false);

        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        if (this.state == FSMStates.BotaoActvDeactv)
        {
            Obj_BotaoActivates.SetActive(false);
            Obj_BotaoDeactivates.SetActive(true);
        }

        if (this.state == FSMStates.BotaoSimples)
        {
            Obj_BotaoSimplesDeactivates.SetActive(true);
        }

        //Save_Caixa();

        if (this.state == FSMStates.Caixa)
        {
            if (PlayerPrefs.HasKey("Caixa_PosX" + gameObject.name))
            {
                Load_Caixa();
            }
        }

        if (this.state == FSMStates.BotaoPlatMovelNiveis)
        {
            for (int i = 0; i < PlatformLevel_Indicator.Length; i++)
            {
                PlatformLevel_Indicator[i].SetActive(false);
            }
        }

        if (this.state == FSMStates.BotaoPlatMovelHorizontal)
        {
            for (int i = 0; i < PlatformHorizontal_Indicator.Length; i++)
            {
                PlatformHorizontal_Indicator[i].SetActive(false);
            }
        }

        // define the names of the custom datapoints we want to log
        // trial number, participant ID, trial start/end time are logged automatically


        //List<string> columnList = new List<string> { "ButtonAttempt"};
        //trialLogger.Initialize(participantID, columnList);
        //trialLogger.StartTrial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerPrefs.DeleteAll();
        }

        if (this.state == FSMStates.Caixa)
        {
            Caixa_State();
        }

        #region Platform Niveis
        if (state == FSMStates.BotaoPlatMovelNiveis)
        {
            if (this.interactedPlatHorizontal)
            {
                if (selectedLevel == 0)
                {
                    this.PlatformLevel_Indicator[0].SetActive(true);
                    this.PlatformLevel_Indicator[1].SetActive(false);
                    //this.PlatformLevel_Indicator[2].SetActive(false);
                }

                if (selectedLevel == 1)
                {
                    this.PlatformLevel_Indicator[0].SetActive(false);
                    this.PlatformLevel_Indicator[1].SetActive(true);
                    //this.PlatformLevel_Indicator[1].SetActive(false);
                }

                if (selectedLevel == 2)
                {
                    this.PlatformLevel_Indicator[0].SetActive(false);
                    this.PlatformLevel_Indicator[1].SetActive(false);
                    //this.PlatformLevel_Indicator[2].SetActive(true);
                }
            }
        }

        #endregion


        #region Platform Horizontal
        if (state == FSMStates.BotaoPlatMovelHorizontal)
        {
            if (this.interactedPlatHorizontal)
            {
                this.PlatformHorizontal_Indicator[0].SetActive(true);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (interactedPlatHorizontal)
                    {
                        this.PlatformHorizontal_Indicator[1].SetActive(true);
                        StartCoroutine(Give_PlatformHorizontal_Back(1, Left_posBPlatform));
                        interactedPlatHorizontal = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (interactedPlatHorizontal)
                    {
                        this.PlatformHorizontal_Indicator[2].SetActive(true);
                        StartCoroutine(Give_PlatformHorizontal_Back(1, Right_posBPlatform));
                        interactedPlatHorizontal = false;
                    }
                }
            }
        }



        #endregion


        //Debug.Log("Nível selecionado: " + selectedLevel);

        if (state == FSMStates.BotaoPlatMovelNiveis)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (interactedPlatHorizontal)
                {
                    if (selectedLevel > 0)
                    {
                        //trialLogger.trial["ButtonAttempt"] = "UpArrowPressed";
                        //trialLogger.EndTrial();
                        //trialLogger.StartTrial();
                        selectedLevel -= 1;
                    }


                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (interactedPlatHorizontal)
                {
                    if (selectedLevel < (numberOfLevels - 1))
                    {
                        //trialLogger.trial["ButtonAttempt"] = "DownArrowPressed";
                        //trialLogger.EndTrial();
                        //trialLogger.StartTrial();
                        selectedLevel += 1;
                    }


                }
            }
            if (interactedPlatHorizontal && GiveInput_PlatformNiveis)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MovingPlatform.nextPos = Niveis_posBPlatform[selectedLevel].localPosition;
                    this.PlatformLevel_Indicator[0].SetActive(false);
                    this.PlatformLevel_Indicator[1].SetActive(false);
                    //this.PlatformLevel_Indicator[2].SetActive(true);
                    StartCoroutine(Give_Mov_Back(0.3f));
                    interactedPlatHorizontal = false;

                }
            }
        }
    }


    private void FixedUpdate()
    {
        if (this.isAlreadyActivated)
        {
            switch (state)
            {
                case FSMStates.BotaoActvDeactv: BotaoActvDeactv_State(); break;
                case FSMStates.BotaoSimples: BotaoSimples_State(); break;
                case FSMStates.Teleportador: Teleportador_State(); break;
                case FSMStates.BotaoPlatMovel: BotaoPlatMovel_State(); break;
                case FSMStates.BotaoPlatMovelHorizontal: BotaoPlatMovelHorizontal_State(); break;
                case FSMStates.BotaoPlatMovelNiveis: BotaoPlatMovelNiveis_State(); break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            if (this.state != FSMStates.Caixa)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (!this.isAlreadyActivated)
                    {
                        IC_Interativo.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            if (this.state != FSMStates.Caixa)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (state == FSMStates.BotaoPlatMovelHorizontal)
                        {
                            interactedPlatHorizontal = true;
                        }
                        if (state == FSMStates.BotaoPlatMovelNiveis)
                        {
                            if (!interactedPlatHorizontal)
                            {
                                //trialLogger.StartTrial();
                                Debug.Log("Para com essa porra ai mermao");
                                interactedPlatHorizontal = true;
                                StartCoroutine(Give_Nice_Back(0.2f));
                            }
                        }
                        scriptAudio_MainScript.tocar_Botao();
                        if (!this.isAlreadyActivated)
                        {
                            this.isAlreadyActivated = true;
                            IC_Interativo.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        if (this.state == FSMStates.Caixa)
        {
            if (other.gameObject.CompareTag("ColisorCaixa"))
            {
                transform.SetParent(other.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            if (this.state != FSMStates.Caixa)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (!this.isAlreadyActivated)
                    {
                        IC_Interativo.gameObject.SetActive(false);
                    }
                }
            }
        }


        if (this.state == FSMStates.Caixa)
        {
            if (other.gameObject.CompareTag("ColisorCaixa"))
            {
                transform.parent = null;
            }
            //if (other.gameObject.CompareTag("Player"))
            //{
            //    Save_Caixa();
            //}
        }
    }

    private void Caixa_State()
    {
        if (!this.beingPushed)
        {
            CaixaRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            CaixaRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void BotaoActvDeactv_State()
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            Obj_BotaoActivates.SetActive(true);
            Obj_BotaoDeactivates.SetActive(false);

            // NÃO ESQUEÇA BABACA
            this.gameObject.SetActive(false);
        }

    }

    private void BotaoSimples_State()
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            Obj_BotaoSimplesDeactivates.SetActive(false);

            // NÃO ESQUEÇA BABACA
            gameObject.SetActive(false);
        }
    }

    private void BotaoPlatMovel_State()
    {
        //MovingPlatform.nextPos = posBPlatform.localPosition;

        //if (MovingPlatform.PlatformTrans.localPosition == MovingPlatform.nextPos)
        //{
        //    this.isAlreadyActivated = false;
        //}

        //// NÃO ESQUEÇA BABACA
        //this.gameObject.SetActive(false);
    }

    private void BotaoPlatMovelHorizontal_State()
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            if (interactedPlatHorizontal)
            {
                platformer2dUsercontrol_Script.iNeverFreeze = false;
            }
            else
            {
                if (MovingPlatform.PlatformTrans.localPosition == MovingPlatform.nextPos)
                {
                    this.isAlreadyActivated = false;
                }
            }

            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //{
            //    if (interactedPlatHorizontal)
            //    {
            //        MovingPlatform.nextPos = Left_posBPlatform.localPosition;
            //        StartCoroutine(Give_Mov_Back(0.3f));
            //        interactedPlatHorizontal = false;
            //    }
            //}
            //if (Input.GetKeyDown(KeyCode.RightArrow))
            //{
            //    if (interactedPlatHorizontal)
            //    {
            //        MovingPlatform.nextPos = Right_posBPlatform.localPosition;
            //        StartCoroutine(Give_Mov_Back(0.3f));
            //        interactedPlatHorizontal = false;
            //    }
            //}



            //// NÃO ESQUEÇA BABACA
            //this.gameObject.SetActive(false);
        }

    }

    private void BotaoPlatMovelNiveis_State()
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            if (interactedPlatHorizontal)
            {
                platformer2dUsercontrol_Script.iNeverFreeze = false;
            }
            else
            {
                if (MovingPlatform.PlatformTrans.localPosition == MovingPlatform.nextPos)
                {
                    GiveInput_PlatformNiveis = false;
                    //interactedPlatHorizontal = false;
                    this.isAlreadyActivated = false;
                }
            }

            //// NÃO ESQUEÇA BABACA
            //this.gameObject.SetActive(false);
        }

    }

    private void Teleportador_State()
    {
        if (!platformer2dcharacter_script.isLookingAtCaixa)
        {
            //playerTrans.position = Teleport_goTo_Point.position;
            playerTrans.position = new Vector3(Mathf.Lerp(playerTrans.position.x, Teleport_goTo_Point.position.x, t), Mathf.Lerp(playerTrans.position.y, Teleport_goTo_Point.position.y, t), Mathf.Lerp(playerTrans.position.z, Teleport_goTo_Point.position.z, t));
            t += 0.05f * Time.deltaTime;

            Vector3 dir = playerTrans.position - Teleport_goTo_Point.position;

            if (dir.magnitude <= 0.3f)
            {
                //player_rb.gravityScale = 3f;
                //player_circleCol.enabled = true;
                //player_boxCol.enabled = true;
                //platformer2dUsercontrol_Script.iNeverFreeze = true;

                StartCoroutine(Give_Bounds_Back(0.5f));
                player_Anima.SetActive(true);
                this.isAlreadyActivated = false;

            }
            else
            {
                platformer2dUsercontrol_Script.iNeverFreeze = false;
                player_rb.gravityScale = 0f;
                player_circleCol.enabled = false;
                player_boxCol.enabled = false;
                player_Anima.SetActive(false);
                //camera2DFollow_Script.bounds = false;


                if (platformer2dcharacter_script.cityStateInfo == "MetroEscola")
                {
                    camera2DFollow_Script.minCameraPos = camera2DFollow_Script.MetroEscola_minCam_Pos;
                    camera2DFollow_Script.maxCameraPos = camera2DFollow_Script.MetroEscola_maxCam_Pos;
                }

                if (platformer2dcharacter_script.cityStateInfo == "PortariaMetro")
                {
                    camera2DFollow_Script.minCameraPos = camera2DFollow_Script.PortariaMetro_minCam_Pos;
                    camera2DFollow_Script.maxCameraPos = camera2DFollow_Script.PortariaMetro_maxCam_Pos;
                }

                if (platformer2dcharacter_script.cityStateInfo == "PapelariaMercado")
                {
                    camera2DFollow_Script.minCameraPos = camera2DFollow_Script.PapelariaMercado_minCam_Pos;
                    camera2DFollow_Script.maxCameraPos = camera2DFollow_Script.PapelariaMercado_maxCam_Pos;
                }
            }
            //// NÃO ESQUEÇA BABACA
            //gameObject.SetActive(false);
        }

    }

    private IEnumerator Give_Mov_Back(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        platformer2dUsercontrol_Script.iNeverFreeze = true;
    }

    private IEnumerator Give_Nice_Back(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        GiveInput_PlatformNiveis = true;
    }

    private IEnumerator Give_Bounds_Back(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        //camera2DFollow_Script.bounds = true;
        player_rb.gravityScale = 3f;
        player_circleCol.enabled = true;
        player_boxCol.enabled = true;
        platformer2dUsercontrol_Script.iNeverFreeze = true;
    }

    private IEnumerator Give_PlatformHorizontal_Back(float seconds, Transform pointPos)
    {
        yield return new WaitForSecondsRealtime(seconds);
        for (int i = 0; i < PlatformHorizontal_Indicator.Length; i++)
        {
            PlatformHorizontal_Indicator[i].SetActive(false);
        }
        MovingPlatform.nextPos = pointPos.localPosition;
        platformer2dUsercontrol_Script.iNeverFreeze = true;
        ///StartCoroutine(Give_Mov_Back(0.3f));
    }

    public void Save_Caixa()
    {
        PlayerPrefs.SetFloat("Caixa_PosX" + gameObject.name, trans.position.x);
        PlayerPrefs.SetFloat("Caixa_PosY" + gameObject.name, trans.position.y);
        PlayerPrefs.SetFloat("Caixa_PosZ" + gameObject.name, trans.position.z);
        //print("afhuhbdfusdbf");
    }

    public void Load_Caixa()
    {
        trans.position = new Vector3(PlayerPrefs.GetFloat("Caixa_PosX" + gameObject.name), PlayerPrefs.GetFloat("Caixa_PosY" + gameObject.name), PlayerPrefs.GetFloat("Caixa_PosZ" + gameObject.name));
    }
}
