using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memoria_cartas : MonoBehaviour
{

    Main_Memoria script_Main_Memoria;

    Image img;
    // Use this for initialization
    void Awake()
    {

        img = GetComponent<Image>();
        script_Main_Memoria = GameObject.Find("_JogoDaMemoria").GetComponent<Main_Memoria>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void desativarIMG()
    {
        if (script_Main_Memoria.btt_1 == null)
        {
            script_Main_Memoria.btt_1 = GetComponent<Button>();
        }
        else if (script_Main_Memoria.btt_2 == null)
        {
            script_Main_Memoria.btt_2 = GetComponent<Button>();
        }
        
        img.enabled = false;
        script_Main_Memoria.countMAX += 1;
    }
}
