using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisionTerminal : MonoBehaviour {
    Audio_MainScript script_Audio_MainScript;

    private void Awake()
    {
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("terminalForCollision"))
        {
            script_Audio_MainScript.tocar_CristalLigado();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("terminalForCollision"))
        {
            script_Audio_MainScript.tocar_CristalDesligado();
        }
    }
}
