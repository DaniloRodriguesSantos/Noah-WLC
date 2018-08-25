using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject botao_Noah_J1;
	[SerializeField] private GameObject ponte_Noah_J1;
	[HideInInspector] public string interactable_State;
    private Transform playerTrans;
    #region Inputs
    [HideInInspector] public bool ip_Interactable;
	#endregion


	private Platformer2DUserControl platformer2dcontrol_Script;
	//public GameObject pensamentoEstanteLoja_Canvas;
	private PensamentoController pensamentoController_Script;
	[HideInInspector] public bool isThinking; 
	public SystemDialogueMental systemDialogueMental_Script;

    Canvas_MainScript scriptCanvas_MainScript;
    #region Prologo
    public bool ending_Prologo_ready;
	public GameObject Noah_Logo;

	public GameObject Atendente_Obj;
    #endregion

    #region Day Time Change
    public int dayChangeNumber;
    [HideInInspector] public bool activateDayChange;
    public GameObject[] dayTime_Morning_IMG;
    public GameObject[] dayTime_Afternoon_IMG;
    public GameObject[] dayTime_Night_IMG;
    #endregion

    public int nextSceneIndex;
    public Transform pravoltar_Dia1_Point;
    public Transform praIr_Dia1_Point;
    public Transform pravoltar_Dia2_Point;
    public Transform praIr_Dia2_Point;
    public Transform pravoltar_Dia3_Point;
    public Transform praIr_Dia3_Point;
    public GameObject completaFrase_Puzzle;

    private void Awake(){
		platformer2dcontrol_Script = GameObject.FindGameObjectWithTag ("Player").GetComponent<Platformer2DUserControl> ();
		pensamentoController_Script = GameObject.Find ("PensamentoController").GetComponent<PensamentoController> ();
        //pensamentoEstanteLoja_Canvas.SetActive (false);
        scriptCanvas_MainScript = GameObject.Find("_MainCanvas").GetComponent<Canvas_MainScript>();

        dayTime_Morning_IMG = GameObject.FindGameObjectsWithTag("MorningIMG");
        dayTime_Afternoon_IMG = GameObject.FindGameObjectsWithTag("AfternoonIMG");
        dayTime_Night_IMG = GameObject.FindGameObjectsWithTag("NightIMG");

        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void Start()
	{
        if (SceneManager.GetActiveScene().name == "Dia_1")
        {
            Atendente_Obj.SetActive(false);
        }

        for (int i = 0; i < dayTime_Morning_IMG.Length; i++)
        {
            if (dayTime_Morning_IMG[i].activeSelf == false)
            {
                dayTime_Morning_IMG[i].SetActive(true);
            }
        }

        for (int i = 0; i < dayTime_Afternoon_IMG.Length; i++)
        {

            dayTime_Afternoon_IMG[i].SetActive(false);

        }

        for (int i = 0; i < dayTime_Night_IMG.Length; i++)
        {

            dayTime_Night_IMG[i].SetActive(false);

        }

        resetCanRetry();
    }

	// Update is called once per frame
	void Update () {
		if (botao_Noah_J1.activeSelf == false) {
			ponte_Noah_J1.SetActive (true);
		} else {
			ponte_Noah_J1.SetActive (false);
		}

		Inputs ();
		Prologo();
        DayTimeChange();

        if (completaFrase_Puzzle != null)
        {
            if (completaFrase_Puzzle.activeSelf == false)
            {
                CheatMode();
            }
        }
        else
        {
            CheatMode();
        }
    }

    private void DayTimeChange()
    {
        if (activateDayChange)
        {
            if (dayChangeNumber == 4)
            {
                for (int i = 0; i < dayTime_Morning_IMG.Length; i++)
                {
                    dayTime_Morning_IMG[i].SetActive(false);
                }
                for (int i = 0; i < dayTime_Afternoon_IMG.Length; i++)
                {
                    dayTime_Afternoon_IMG[i].SetActive(true);
                }

                activateDayChange = false;
            }

            //if (dayChangeNumber == 6)
            //{
            //    for (int i = 0; i < dayTime_Afternoon_IMG.Length; i++)
            //    {
            //        dayTime_Afternoon_IMG[i].SetActive(false);
            //    }
            //    for (int i = 0; i < dayTime_Night_IMG.Length; i++)
            //    {
            //        dayTime_Night_IMG[i].SetActive(true);
            //    }

            //    activateDayChange = false;
            //}
        }
    }

    public void Inputs()
	{
		// Button to Interact with something
		ip_Interactable = Input.GetKeyDown(KeyCode.R);
	}

	void Prologo()
	{
		if (ending_Prologo_ready && Input.GetKeyDown (KeyCode.T)) {
            scriptCanvas_MainScript.apresentacaoNOAH();
            //platformer2dcontrol_Script.iNeverFreeze = false;
            ending_Prologo_ready = false;
        }
	}

    private void CheatMode()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            playerTrans.position = pravoltar_Dia1_Point.position;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            playerTrans.position = praIr_Dia1_Point.position;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            playerTrans.position = praIr_Dia2_Point.position;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            playerTrans.position = pravoltar_Dia2_Point.position;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerTrans.position = praIr_Dia3_Point.position;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerTrans.position = pravoltar_Dia3_Point.position;
        }
    }

    private void resetCanRetry()
    {
        if (SceneManager.GetActiveScene().name == "Dia_2")
        {
            PlayerPrefs.SetString("canRetry", "false");
        }
    }
}
