using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desativarDias: MonoBehaviour
{
    public GameObject[] dia1_A;
    public GameObject[] dia2_A;
    public GameObject[] dia3_A;
    public GameObject[] dia4_A;

    public GameObject[] dia1_D;
    public GameObject[] dia2_D;
    public GameObject[] dia3_D;
    public GameObject[] dia4_D;
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
               
    }
    public void ativar_Dia_1()
    {
        for (int i = 0; i < dia1_A.Length; i++)
        {
            dia1_A[i].SetActive(true);
        }
    }
    public void ativar_Dia_2()
    {
        for (int i = 0; i < dia2_A.Length; i++)
        {
            dia2_A[i].SetActive(true);
        }
    }
    public void ativar_Dia_3()
    {
        for (int i = 0; i < dia3_A.Length; i++)
        {
            dia3_A[i].SetActive(true);
        }
    }
    public void ativar_Dia_4()
    {
        for (int i = 0; i < dia4_A.Length; i++)
        {
            dia4_A[i].SetActive(true);
        }
    }
    public void desativar_Dia_1()
    {
        for(int i = 0; i < dia1_D.Length; i++)
        {
            dia1_D[i].SetActive(false);
        }
    }
    public void desativar_Dia_2()
    {
        for (int i = 0; i < dia2_D.Length; i++)
        {
            dia2_D[i].SetActive(false);
        }
    }
    public void desativar_Dia_3()
    {
        for (int i = 0; i < dia3_D.Length; i++)
        {
            dia3_D[i].SetActive(false);
        }
    }
    public void desativar_Dia_4()
    {
        for (int i = 0; i < dia4_D.Length; i++)
        {
            dia4_D[i].SetActive(false);
        }
    }
}
