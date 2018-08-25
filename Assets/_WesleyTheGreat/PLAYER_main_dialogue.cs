using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_main_dialogue : MonoBehaviour
{
    [Header("--HABILIDADES--")]
    public bool add_diario;
    public bool travarMovimentacao;

    [Space(15)]
    [Header("--Objetos da conversa--")]
    public GameObject[] objs_de_dialogo_1;
    public GameObject[] objs_de_dialogo_2;


    int current_objs_array_index;
    bool podeInteragir;
    [HideInInspector] public bool ja_respondeu;

    BoxCollider2D boxC;

    Audio_MainScript script_Audio_MainScript;
    Platformer2DUserControl script_Platformer2DUserControl;

    // Use this for initialization
    void Awake()
    {
        boxC = GetComponent<BoxCollider2D>();
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (podeInteragir && Input.GetKeyDown("e"))
        {
            
            comecarInteracao();
        }
    }
    public void comecarInteracao()
    {
        boxC.enabled = false;
        podeInteragir = false;
        current_objs_array_index = 0;

        if (travarMovimentacao)
        {
            script_Platformer2DUserControl.iNeverFreeze = false;
        }

        
        for (int i = 0; i < objs_de_dialogo_1.Length; i++)
        {
            if (objs_de_dialogo_1[i] != objs_de_dialogo_1[current_objs_array_index])
            {
                objs_de_dialogo_1[i].SetActive(false);
            }
            else
            {
                objs_de_dialogo_1[current_objs_array_index].SetActive(true);
            }
        }
       
    }
    public void passaOBJ()
    {
        current_objs_array_index += 1;

        for (int i = 0; i < objs_de_dialogo_1.Length; i++)
        {
            if (current_objs_array_index == objs_de_dialogo_1.Length - 1)
            {
                objs_de_dialogo_1[i].SetActive(false);
                terminarInteracao();
            }
            else if (objs_de_dialogo_1[i] != objs_de_dialogo_1[current_objs_array_index])
            {
                objs_de_dialogo_1[i].SetActive(false);
            }
            else
            {
                objs_de_dialogo_1[current_objs_array_index].SetActive(true);
            }
        }
        

    }

    public void terminarInteracao()
    {
        
        boxC.enabled = true;
        script_Platformer2DUserControl.iNeverFreeze = true;
        current_objs_array_index = 0;
        if (ja_respondeu)
        {
            objs_de_dialogo_1 = objs_de_dialogo_2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        podeInteragir = true;
        /* if (collision.gameObject.CompareTag("Player"))
         {

         }*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        podeInteragir = false;
        /* if (collision.gameObject.CompareTag("Player"))
         {

         }*/
    }
}
