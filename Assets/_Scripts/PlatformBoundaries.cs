using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoundaries : MonoBehaviour {

    [SerializeField] private GameObject[] playerBoundaries;
    [SerializeField] private GameObject[] platformNoFall;
    public PlatformBoundaries platformBoundaries_Script;
    [SerializeField] private bool platformConnected;
    public bool isConnected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMovel"))
        {
            for (int i = 0; i < playerBoundaries.Length; i++)
            {
                if(playerBoundaries[i].activeSelf == true)
                {
                    playerBoundaries[i].SetActive(false);
                }
            }

            if (platformConnected)
            {
                isConnected = true;
            }

            if(platformNoFall != null){
                if (platformNoFall[0] != null)
                {
                    platformNoFall[0].SetActive(true);
                }
            }


            if(platformBoundaries_Script != null)
            {
                if (platformBoundaries_Script.isConnected)
                {
                    platformNoFall[0].SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMovel"))
        {
            for (int i = 0; i < playerBoundaries.Length; i++)
            {
                playerBoundaries[i].SetActive(true);
            }
            if(platformNoFall != null)
            {
                if (platformNoFall[0] != null)
                {
                    platformNoFall[0].SetActive(false);
                }
                if (platformNoFall[1] != null)
                {
                    platformNoFall[1].SetActive(false);
                }
            }
            if (platformConnected)
            {
                isConnected = false;
            }
        }
    }
}
