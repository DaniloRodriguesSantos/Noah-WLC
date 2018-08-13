using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Main_CacaPalavras : MonoBehaviour
{
    [HideInInspector] public Vector3 pos_Main;
    GameObject toParent;
    private LineRenderer line;
    private Vector3 mousePos;
    public Material material;
    private int currLines = 0;
    public GameObject panel;

    Canvas thisCanvas;
    Camera mainCamera;
    [Header("Configuração 1")]
    public int[] letrasSolidas_config_1;
    public string[] palavra_config_1;

    [Space(20)]
    [Header("Configuração 2")]
    public int[] letrasSolidas_config_2;
    public string[] palavra_config_2;

    [Space(20)]
    [Header("Configuração 3")]
    public int[] letrasSolidas_config_3;
    public string[] palavra_config_3;

    public bool config_1;
    public bool config_2;
    public bool config_3;

    bool acertou_Palavra_1;
    bool acertou_Palavra_2;
    bool acertou_Palavra_3;
    public string namePrimeiroSelect;
    public string nameSegundoSelect;

    [Space(20)]
    public Text[] Letras;
    public random_Char[] script_random_Char;


    public Image img_efeito_minigame;
    Platformer2DUserControl script_Platformer2DUserControl;
    Teleporte teleportePescola;
    Audio_MainScript script_Audio_MainScript;
    public GameObject teleporteEscola;
    public GameObject teleporteSaindoAula2;


    private void Awake()
    {
        Letras = GetComponentsInChildren<Text>();
        script_random_Char = GetComponentsInChildren<random_Char>();
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        thisCanvas = GetComponentInParent<Canvas>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        toParent = GameObject.Find("toParent");
        //img_efeito_minigame = GameObject.Find("img_efeitoMiniGame").GetComponent<Image>();
        script_Platformer2DUserControl = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
        if (SceneManager.GetActiveScene().name == "Dia_1")
        {
            teleportePescola = GameObject.Find("Teleporte(acabandoPuzzle)").GetComponent<Teleporte>();
        }
        //teleporteEscola = GameObject.Find("Teleporte(Escola<->Sala)");
    }
    private void Start()
    {
        reset_CacaPalavras();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            Cursor.visible = true;
            script_Platformer2DUserControl.iNeverFreeze = false;
        }

        print(currLines);
        thisCanvas.transform.position = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
        if (Input.GetKeyDown("k"))
        {
            reset_CacaPalavras();
        }

        if (Input.GetMouseButtonDown(0))
        {

            for (int i = 0; i < script_random_Char.Length; i++)
            {
                if (line == null && script_random_Char[i].entrou)
                {
                    script_random_Char[i].img.color = Color.blue;
                    namePrimeiroSelect = script_random_Char[i].name;
                    createLine();
                    line.SetPosition(0, pos_Main);
                    line.SetPosition(1, pos_Main);
                }
            }

        }
        else if (Input.GetMouseButtonUp(0) && line)
        {
            line.SetPosition(1, pos_Main);
            line = null;
            currLines++;
            for (int i = 0; i < script_random_Char.Length; i++)
            {
                if (script_random_Char[i].entrou)
                {
                    nameSegundoSelect = script_random_Char[i].name;
                }

                if (config_1)
                {
                    if (namePrimeiroSelect == script_random_Char[letrasSolidas_config_1[0]].gameObject.name || nameSegundoSelect == script_random_Char[letrasSolidas_config_1[0]].gameObject.name)
                    {
                        if (namePrimeiroSelect == script_random_Char[letrasSolidas_config_1[4]].gameObject.name || nameSegundoSelect == script_random_Char[letrasSolidas_config_1[4]].gameObject.name)
                        {
                            acertou_Palavra_1 = true;
                        }
                        else
                        {
                            LineRenderer LR;
                            int cont = currLines - 1;
                            LR = GameObject.Find("Line" + cont).GetComponent<LineRenderer>();
                            LR.enabled = false;
                        }
                    }
                    else
                    {
                        LineRenderer LR;
                        int cont = currLines - 1;
                        LR = GameObject.Find("Line" + cont).GetComponent<LineRenderer>();
                        LR.enabled = false;
                    }
                }
                else if (config_2)
                {
                    if (namePrimeiroSelect == script_random_Char[letrasSolidas_config_2[0]].gameObject.name || nameSegundoSelect == script_random_Char[letrasSolidas_config_2[0]].gameObject.name)
                    {
                        if (namePrimeiroSelect == script_random_Char[letrasSolidas_config_2[5]].gameObject.name || nameSegundoSelect == script_random_Char[letrasSolidas_config_2[5]].gameObject.name)
                        {
                            acertou_Palavra_2 = true;
                        }
                        else
                        {
                            LineRenderer LR;
                            int cont = currLines - 1;
                            LR = GameObject.Find("Line" + cont).GetComponent<LineRenderer>();
                            LR.enabled = false;
                        }
                    }
                    else
                    {
                        LineRenderer LR;
                        int cont = currLines - 1;
                        LR = GameObject.Find("Line" + cont).GetComponent<LineRenderer>();
                        LR.enabled = false;
                    }
                }
                else if (config_3)
                {
                    if (namePrimeiroSelect == script_random_Char[letrasSolidas_config_3[0]].gameObject.name || nameSegundoSelect == script_random_Char[letrasSolidas_config_3[0]].gameObject.name)
                    {
                        if (namePrimeiroSelect == script_random_Char[letrasSolidas_config_3[5]].gameObject.name || nameSegundoSelect == script_random_Char[letrasSolidas_config_3[5]].gameObject.name)
                        {
                            acertou_Palavra_3 = true;
                        }
                        else
                        {
                            LineRenderer LR;
                            int cont = currLines - 1;
                            LR = GameObject.Find("Line" + cont).GetComponent<LineRenderer>();
                            LR.enabled = false;
                        }
                    }
                    else
                    {
                        LineRenderer LR;
                        int cont = currLines - 1;
                        LR = GameObject.Find("Line" + cont).GetComponent<LineRenderer>();
                        LR.enabled = false;
                    }
                }
            }
        }
        else if (Input.GetMouseButton(0) && line)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(1, mousePos);
        }

        if (acertou_Palavra_1)
        {
            for (int i = 0; i < letrasSolidas_config_1.Length; i++)
            {
                acertou_Palavra_1 = false;
                script_Audio_MainScript.tocar_CacaPalavras();
                script_random_Char[letrasSolidas_config_1[i]].img.color = Color.green;
                StartCoroutine(sairDaSala());
                
            }
        }
        if (acertou_Palavra_2)
        {
            for (int i = 0; i < letrasSolidas_config_2.Length; i++)
            {
                script_random_Char[letrasSolidas_config_2[i]].img.color = Color.green;
                StartCoroutine(sairDaSala());

            }
        }
        if (acertou_Palavra_3)
        {
            for (int i = 0; i < letrasSolidas_config_3.Length; i++)
            {
                script_random_Char[letrasSolidas_config_3[i]].img.color = Color.green;
                StartCoroutine(sairDaSala());
            }
        }
    }

    private void createLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.transform.parent = toParent.transform;
        line.sortingLayerName = "Foreground";
        line.sortingOrder = 11;
        line.material = material;
        line.SetVertexCount(2);
        line.SetWidth(0.35f, 0.35f);
        line.useWorldSpace = false;

    }

    IEnumerator sairDaSala()
    {
        if(SceneManager.GetActiveScene().name == "Dia_1")
        {
            teleporteSaindoAula2.SetActive(false);

        }
        script_Audio_MainScript.tocar_SinalEscola();
        img_efeito_minigame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
       
        teleportePescola.teleportar();
        teleporteEscola.SetActive(false);
        panel.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        img_efeito_minigame.gameObject.SetActive(false);
        script_Platformer2DUserControl.iNeverFreeze = true;
        gameObject.SetActive(false);
        panel.SetActive(true);
    }
    public void reset_CacaPalavras()
    {
        for (int i = 0; i < script_random_Char.Length; i++)
        {
            script_random_Char[i].letraAleatoria = true;
        }
        if (config_1)
        {
            // 1
            script_random_Char[letrasSolidas_config_1[0]].letraAleatoria = false;
            Letras[letrasSolidas_config_1[0]].text = palavra_config_1[0];

            // 2 
            script_random_Char[letrasSolidas_config_1[1]].letraAleatoria = false;
            Letras[letrasSolidas_config_1[1]].text = palavra_config_1[1];

            // 3
            script_random_Char[letrasSolidas_config_1[2]].letraAleatoria = false;
            Letras[letrasSolidas_config_1[2]].text = palavra_config_1[2];

            // 4
            script_random_Char[letrasSolidas_config_1[3]].letraAleatoria = false;
            Letras[letrasSolidas_config_1[3]].text = palavra_config_1[3];

            //5
            script_random_Char[letrasSolidas_config_1[4]].letraAleatoria = false;
            Letras[letrasSolidas_config_1[4]].text = palavra_config_1[4];

            
        }
        else if (config_2)
        {
            // 1
            script_random_Char[letrasSolidas_config_2[0]].letraAleatoria = false;
            Letras[letrasSolidas_config_2[0]].text = palavra_config_2[0];

            // 2 
            script_random_Char[letrasSolidas_config_2[1]].letraAleatoria = false;
            Letras[letrasSolidas_config_2[1]].text = palavra_config_2[1];

            // 3
            script_random_Char[letrasSolidas_config_2[2]].letraAleatoria = false;
            Letras[letrasSolidas_config_2[2]].text = palavra_config_2[2];

            // 4
            script_random_Char[letrasSolidas_config_2[3]].letraAleatoria = false;
            Letras[letrasSolidas_config_2[3]].text = palavra_config_2[3];

            //5
            script_random_Char[letrasSolidas_config_2[4]].letraAleatoria = false;
            Letras[letrasSolidas_config_2[4]].text = palavra_config_2[4];

            // 6
            script_random_Char[letrasSolidas_config_2[5]].letraAleatoria = false;
            Letras[letrasSolidas_config_2[5]].text = palavra_config_2[5];
        }
        else if (config_3)
        {
            // 1
            script_random_Char[letrasSolidas_config_3[0]].letraAleatoria = false;
            Letras[letrasSolidas_config_3[0]].text = palavra_config_3[0];

            // 2 
            script_random_Char[letrasSolidas_config_3[1]].letraAleatoria = false;
            Letras[letrasSolidas_config_3[1]].text = palavra_config_3[1];

            // 3
            script_random_Char[letrasSolidas_config_3[2]].letraAleatoria = false;
            Letras[letrasSolidas_config_3[2]].text = palavra_config_3[2];

            // 4
            script_random_Char[letrasSolidas_config_3[3]].letraAleatoria = false;
            Letras[letrasSolidas_config_3[3]].text = palavra_config_3[3];

            //5
            script_random_Char[letrasSolidas_config_3[4]].letraAleatoria = false;
            Letras[letrasSolidas_config_3[4]].text = palavra_config_3[4];

            // 6
            script_random_Char[letrasSolidas_config_3[5]].letraAleatoria = false;
            Letras[letrasSolidas_config_3[5]].text = palavra_config_3[5];
        }
        for (int i = 0; i < script_random_Char.Length; i++)
        {
            script_random_Char[i].resetMiniGame();
        }

    }
}