using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalizar_DIA : MonoBehaviour
{

    float tempo;
    Animator anin_ImgPreta;
    // Use this for initialization
    void Awake()
    {
        anin_ImgPreta = GameObject.Find("Imagem Preta").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        anin_ImgPreta.SetBool("alpha", true);
        if (tempo > 2)
        {
            if (SceneManager.GetActiveScene().name == "Prologo")
            {
                SceneManager.LoadScene("Dia_1");
            }
            else if (SceneManager.GetActiveScene().name == "Dia_1")
            {
                SceneManager.LoadScene("Dia_2");
            }
            else if (SceneManager.GetActiveScene().name == "Dia_2")
            {
                SceneManager.LoadScene("Dia_3");
            }
            else if (SceneManager.GetActiveScene().name == "Dia_3")
            {
                SceneManager.LoadScene("Dia_4");
            }
            else if (SceneManager.GetActiveScene().name == "Dia_4")
            {
                SceneManager.LoadScene("MenuPrincipal");
            }
        }
    }
}
