using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePerTime : MonoBehaviour
{
    public Controller_DialoguePerTime script_Controller_DialoguePerTime;
    Text textMain;
    public string[] dialogo;
    public int current_array_index;
    public float tempo_do_balao;
    float tempo;
    // Use this for initialization
    private void Awake()
    {
        textMain = GetComponentInChildren<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (script_Controller_DialoguePerTime.isOn == true)
        {
            tempo += 1 * Time.deltaTime;
            if (tempo > tempo_do_balao)
            {
                current_array_index += 1;
                tempo = 0;
            }

            textMain.text = dialogo[current_array_index];
            if (current_array_index == dialogo.Length - 1)
            {
                script_Controller_DialoguePerTime.passarNPC();
            }
        }
    }
}