using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addDiario_Collision : MonoBehaviour {


    public Celular_MainScript script_Celular_MainScript;
    // Use this for initialization
    void Awake () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            script_Celular_MainScript.add_no_Diario();
            print("vezes");
        }
    }
}
