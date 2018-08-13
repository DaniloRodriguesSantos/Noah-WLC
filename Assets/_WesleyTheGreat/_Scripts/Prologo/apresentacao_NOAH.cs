using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class apresentacao_NOAH : MonoBehaviour {

    Platformer2DUserControl scriptPlatformer2DUserControl;
    Audio_MainScript scriptAudio_MainScript;

    public Text textMainPensamento;
    public Image imgPreta;
    public Image imgWorm;

    public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues;
    public GameObject _caixaMental;
    //public Interativos script_Interativos;

    public GameObject noahPensando;



    private void Awake()
    {
        scriptPlatformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        scriptAudio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
    }
    // Use this for initialization
    void Start () {

        StartCoroutine(coroutine());
    }
	
	// Update is called once per frame
	void Update () {
        
    }
   
    IEnumerator coroutine()
    {
        noahPensando.SetActive(true);
        scriptPlatformer2DUserControl.iNeverFreeze = false;
        textMainPensamento.enabled = true;
        imgPreta.enabled = true;
        yield return new WaitForSeconds(8.5f);
        scriptAudio_MainScript.aplicar_prologo_OST();
        imgPreta.enabled = false;
        imgWorm.enabled = true;
        noahPensando.SetActive(false);
        yield return new WaitForSeconds(2f);
        script_Controller_Assorted_Dialogues.comecarInteracao();
        yield return new WaitForSeconds(3.5f);
        imgWorm.enabled = false;
        _caixaMental.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        script_Controller_Assorted_Dialogues.gameObject.SetActive(false);
        scriptPlatformer2DUserControl.iNeverFreeze = true;
    }
}
