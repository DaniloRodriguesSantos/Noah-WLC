using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ativarSamEscolaDia3 : MonoBehaviour {

    public GameObject Sam;
    public movimento_Sam script_movimento_Sam;
    Platformer2DUserControl script_Platformer2DUserControl;

    // Use this for initialization
    void Awake () {
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Sam.SetActive(true);

            script_movimento_Sam.velocidade = -3;
            script_Platformer2DUserControl.iNeverFreeze = false;
            gameObject.SetActive(false);

        }
    }
}
