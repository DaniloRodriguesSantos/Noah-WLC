using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class random_Char : MonoBehaviour
{
    Main_CacaPalavras script_Main_CacaPalavras;
    public bool letraAleatoria;
    [HideInInspector] public Image img;
    Text text;
    string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "Z" };
    [HideInInspector] public bool entrou;
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        script_Main_CacaPalavras = GameObject.Find("_Caça-Palavras").GetComponent<Main_CacaPalavras>();
        img = GetComponent<Image>();
    }
    private void Start()
    {
        img.color = Color.grey;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            resetMiniGame();
        }
        if (entrou)
        {

            script_Main_CacaPalavras.pos_Main = transform.position;
        }
    }

    public void resetMiniGame()
    {
        if (letraAleatoria)
        {
            int randomizer = Random.Range(0, letras.Length);
            text.text = letras[randomizer];
        }
    }
    private void OnMouseEnter()
    {
        entrou = true;
        if (Input.GetMouseButton(0))
        {
            img.color = Color.blue;
        }
    }

    private void OnMouseExit()
    {
        entrou = false;

        img.color = Color.grey;

    }

}

