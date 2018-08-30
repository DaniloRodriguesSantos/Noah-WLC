using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAYER_main_dialogue : MonoBehaviour
{
    [Header("--HABILIDADES--")]
    public bool add_diario;
    public bool travarMovimentacao;
    [Space(5)]
    public bool ativar_Pulgas_Assassinas_BOSS_Fight;
    
    [Space(15)]
    [Header("--Objetos da conversa--")]
    public GameObject[] objs_de_dialogo_1;
    public GameObject[] objs_de_dialogo_2;

    [Space(15)]
    [Header("--Sprites com animação--")]
    public bool ativar_sprite_normal;
    public bool ativar_sprite_recompensa;
    public GameObject sprite_normal;
    public GameObject sprite_recompensa;


    int current_objs_array_index;
    bool podeInteragir;
    [HideInInspector] public bool ja_respondeu;

    BoxCollider2D boxC;

    Audio_MainScript script_Audio_MainScript;
    Platformer2DUserControl script_Platformer2DUserControl;
    Animator anin_sprite;
    public GameObject miniG_PulgasAssassinas;
    Canvas main_Canvas;
    finalizar_DIA script_FinalizarDia;

    private Camera2DFollow camera2DFollow_Script;
    // Use this for initialization
    void Awake()
    {
        anin_sprite = GetComponentInChildren<Animator>();
        boxC = GetComponent<BoxCollider2D>();
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
       // miniG_PulgasAssassinas = GameObject.Find("MiniG_Boss");
        main_Canvas = GameObject.Find("_MainCanvas").GetComponent<Canvas>();
        script_FinalizarDia = GameObject.Find("_MainCanvas").GetComponent<finalizar_DIA>();
        camera2DFollow_Script = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
    }
    void Start()
    {
        //miniG_PulgasAssassinas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (podeInteragir && Input.GetKeyDown("e"))
        {

            comecarInteracao();
        }


        if (ativar_sprite_normal)
        {
            sprite_normal.SetActive(true);
            sprite_recompensa.SetActive(false);
        }
        else if (ativar_sprite_recompensa)
        {
            sprite_normal.SetActive(false);
            sprite_recompensa.SetActive(true);
        }
        else
        {
            sprite_normal.SetActive(false);
            sprite_recompensa.SetActive(false);
        }
    }
    public void comecarInteracao()
    {
        boxC.enabled = false;
        podeInteragir = false;
        current_objs_array_index = 0;

        ativar_sprite_normal = true;
        ativar_sprite_recompensa = false;

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

        if (ativar_Pulgas_Assassinas_BOSS_Fight)
        {
            sprite_normal.SetActive(false);
            boxC.enabled = false;
            script_Platformer2DUserControl.iNeverFreeze = false;
            if (SceneManager.GetActiveScene().name == "Dia_2")
            {
                main_Canvas.enabled = false;
                miniG_PulgasAssassinas.SetActive(true);
                camera2DFollow_Script.changeCamera_MiniGBoss();
            }
            else if (SceneManager.GetActiveScene().name == "Dia_3" && PLAYER_Static.SAM_BOSS == "ruim")
            {
                miniG_PulgasAssassinas.SetActive(true);
                camera2DFollow_Script.changeCamera_MiniGBoss();
                main_Canvas.enabled = false;
            }
            else
            {
                script_Platformer2DUserControl.iNeverFreeze = true;
                script_FinalizarDia.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        podeInteragir = true;
        anin_sprite.SetBool("comecar", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        podeInteragir = false;
        anin_sprite.SetBool("comecar", false);
    }
}
