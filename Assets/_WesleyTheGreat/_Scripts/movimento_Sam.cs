using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento_Sam : MonoBehaviour {

    Platformer2DUserControl script_Platformer2DUserControl;

    public bool movimento;
    public float velocidade;
    public GameObject Sam;

    public GameObject walk;
    public GameObject idle;
    // Use this for initialization
    void Awake () {
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
    }
	
	// Update is called once per frame
	void Update () {

        if (movimento)
        {
            script_Platformer2DUserControl.iNeverFreeze = false;
            walk.SetActive(true);
            idle.SetActive(false);
            transform.Translate(velocidade * Time.deltaTime,0,0);

            if(gameObject.name == "SamChega_Dia2Resposta_2")
            {
                Vector3 theScale = Sam.transform.localScale;
                theScale.x = -1;
                Sam.transform.localScale = theScale;
            }
        }
        else
        {
            walk.SetActive(false);
            idle.SetActive(true);
            transform.Translate(0, 0, 0);
        }

    }
   
}
