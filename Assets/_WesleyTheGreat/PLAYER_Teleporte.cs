using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER_Teleporte : MonoBehaviour
{

    [Header("---HABILIDADES---")]
    public bool interagir_sem_apertar_E;
    public bool travar_movimentacao;
    [Space(10)]
    public bool this_is_a_TELEPORTE;
    public Transform Destiny;
    [Space(10)]
    public bool ativar_imagem;
    public Image Imagem;

    Animator anin_imgPreta;
    float tempo;
    bool podeInteragir;
    bool interagindo;

    Transform player_Transform;
    Platformer2DUserControl script_Platformer2DUserControl;
    BoxCollider2D boxC;

    private void Awake()
    {
        player_Transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        anin_imgPreta = GameObject.Find("Imagem Preta").GetComponent<Animator>();
        boxC = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if(anin_imgPreta.GetBool("alpha") == true)
        {
            anin_imgPreta.SetBool("alpha", false);
        }
    }

    void Update()
    {
        if (podeInteragir)
        {
            if (interagir_sem_apertar_E || Input.GetKeyDown("e"))
            {
                comecandoInteracao();
            }
            if (ativar_imagem)
            {
                Imagem.enabled = true;
            }
        }


        if (interagindo)
        {
            podeInteragir = false;
            tempo += Time.deltaTime;

            if (travar_movimentacao)
            {
                script_Platformer2DUserControl.iNeverFreeze = false;
            }

            if (this_is_a_TELEPORTE && tempo > 2)
            {
                player_Transform.position = Destiny.position;

                if (tempo > 2.5f)
                {
                    anin_imgPreta.SetBool("alpha", false);

                    if (tempo >= 4)
                    {
                        terminandoInteracao();
                    }
                }
            }

        }
    }


    public void comecandoInteracao()
    {
        podeInteragir = false;
        boxC.enabled = false;
        interagindo = true;
        anin_imgPreta.SetBool("alpha", true);
    }
    public void terminandoInteracao()
    {
        interagindo = false;
        tempo = 0;
        boxC.enabled = true;
        script_Platformer2DUserControl.iNeverFreeze = true;
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            podeInteragir = true;

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            podeInteragir = true;

        }
    }
}
