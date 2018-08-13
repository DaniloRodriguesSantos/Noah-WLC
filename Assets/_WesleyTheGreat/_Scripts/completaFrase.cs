using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class completaFrase : MonoBehaviour
{
    Platformer2DUserControl script_Platformer2DUserControl;
    Audio_MainScript scrip_Audio_MainScript;
    Canvas thisCanvas;
    Camera mainCamera;
    public Controller_Assorted_Dialogues script_Controller_Assorted_Dialogues;
    public InputField inputf;
    public Text text_Resposta;

    // Use this for initialization
    void Awake()
    {
        scrip_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        thisCanvas = GetComponentInParent<Canvas>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCanvas.enabled == true)
        {
            Cursor.visible = true;
            script_Platformer2DUserControl.iNeverFreeze = false;
        }
        thisCanvas.transform.position = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
    }
    public void confirmarResposta()
    {
        if (inputf.text != "")
        {
            text_Resposta.text = "R: " + inputf.text;
            inputf.text = "";
        }
    }
    public void finalizar()
    {
        scrip_Audio_MainScript.tocar_SinalEscola();
        script_Controller_Assorted_Dialogues.StartCoroutine(script_Controller_Assorted_Dialogues.sairDaSala_2());
        
    }


}
