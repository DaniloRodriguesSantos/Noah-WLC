using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour
{

    public GameObject[] objsCA_ToActivate;
    public GameObject[] objsCA_ToDeactivate;
    [SerializeField] private GameObject[] objsCR_ToActivate;
    [SerializeField] private GameObject[] objsCR_ToDeactivate;
    [SerializeField] private GameObject[] objsCV_ToActivate;
    [SerializeField] private GameObject[] objsCV_ToDeactivate;
    private int cristalSlots_Available = 0;
    [HideInInspector] public bool blueCristalEntered = false;
    [HideInInspector] public bool greenCristalEntered = false;
    [HideInInspector] public bool purpleCristalEntered = false;
    [HideInInspector] public bool terminalActivated_1 = false;
    [HideInInspector] public bool terminalActivated_2 = false;
    [SerializeField] private TerminalController terminalConected_Script;

    private bool principalCristalDeactv = false;
    [HideInInspector] public bool CristalDeactv = false;

    private void Start()
    {
        if(objsCA_ToActivate != null)
        {
            for (int i = 0; i < objsCA_ToActivate.Length; i++)
            {
                objsCA_ToActivate[i].SetActive(false);
            }
        }

        if (objsCR_ToActivate != null)
        {
            for (int i = 0; i < objsCR_ToActivate.Length; i++)
            {
                objsCR_ToActivate[i].SetActive(false);
            }
        }
        if (objsCV_ToActivate != null)
        {
            for (int i = 0; i < objsCV_ToActivate.Length; i++)
            {
                objsCV_ToActivate[i].SetActive(false);
            }
        }


    }

    private void Update()
    {
        if (terminalConected_Script != null)
        {
            if (terminalConected_Script.blueCristalEntered && terminalConected_Script.greenCristalEntered)
            {
                if (!terminalActivated_1)
                {
                    for (int i = 0; i < objsCV_ToActivate.Length; i++)
                    {
                        objsCV_ToActivate[i].SetActive(true);
                    }
                    blueCristalEntered = true;
                    CristalDeactv = false;
                    terminalActivated_1 = true;
                }
            }
        }

        if (terminalConected_Script)
        {
            if (!terminalConected_Script.blueCristalEntered || !terminalConected_Script.greenCristalEntered)
            {
                if (terminalConected_Script.CristalDeactv)
                {
                    if (terminalActivated_1)
                    {
                        for (int i = 0; i < objsCV_ToActivate.Length; i++)
                        {
                            objsCV_ToActivate[i].SetActive(false);
                        }
                        blueCristalEntered = false;
                        terminalActivated_1 = false;
                        CristalDeactv = true;
                    }
                }
            }
        }


        if (blueCristalEntered && purpleCristalEntered)
        {
            if (!terminalActivated_2)
            {
                for (int i = 0; i < objsCR_ToActivate.Length; i++)
                {
                    objsCR_ToActivate[i].SetActive(true);
                }
                for (int i = 0; i < objsCR_ToDeactivate.Length; i++)
                {
                    objsCR_ToDeactivate[i].SetActive(false);
                }
                principalCristalDeactv = false;
                terminalActivated_2 = true;
            }
        }

        if (!blueCristalEntered)
        {
            if (purpleCristalEntered)
            {
                if (!principalCristalDeactv)
                {
                    for (int i = 0; i < objsCR_ToActivate.Length; i++)
                    {
                        objsCR_ToActivate[i].SetActive(false);
                    }
                    for (int i = 0; i < objsCR_ToDeactivate.Length; i++)
                    {
                        objsCR_ToDeactivate[i].SetActive(true);
                    }
                    principalCristalDeactv = true;
                    terminalActivated_2 = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cristalSlots_Available < 3)
        {
            if (collision.gameObject.CompareTag("CristalAzul"))
            {
                blueCristalEntered = true;
                principalCristalDeactv = false;
                CristalDeactv = false;
                cristalSlots_Available++;
                for (int i = 0; i < objsCA_ToActivate.Length; i++)
                {
                    objsCA_ToActivate[i].SetActive(true);
                }
                for (int i = 0; i < objsCA_ToDeactivate.Length; i++)
                {
                    objsCA_ToDeactivate[i].SetActive(false);
                }
            }
            if (collision.gameObject.CompareTag("CristalRoxo"))
            {
                purpleCristalEntered = true;
                cristalSlots_Available++;
            }


            if (collision.gameObject.CompareTag("CristalVerde"))
            {
                greenCristalEntered = true;
                cristalSlots_Available++;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CristalAzul"))
        {
            blueCristalEntered = false;
            terminalActivated_1 = false;
            terminalActivated_2 = false;
            CristalDeactv = true;
            cristalSlots_Available--;
            for (int i = 0; i < objsCA_ToActivate.Length; i++)
            {
                objsCA_ToActivate[i].SetActive(false);
            }
            for (int i = 0; i < objsCA_ToDeactivate.Length; i++)
            {
                objsCA_ToDeactivate[i].SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("CristalRoxo"))
        {
            purpleCristalEntered = false;
            terminalActivated_2 = false;
            cristalSlots_Available--;
            for (int i = 0; i < objsCR_ToActivate.Length; i++)
            {
                objsCR_ToActivate[i].SetActive(false);
            }
            for (int i = 0; i < objsCR_ToDeactivate.Length; i++)
            {
                objsCR_ToDeactivate[i].SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("CristalVerde"))
        {
            greenCristalEntered = false;
            terminalActivated_1 = false;
            CristalDeactv = true;
            cristalSlots_Available--;
            for (int i = 0; i < objsCV_ToActivate.Length; i++)
            {
                objsCV_ToActivate[i].SetActive(false);
            }
        }
    }
}
