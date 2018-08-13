using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuartoSystem : MonoBehaviour {

    public SpriteRenderer trof1;
    public Sprite[] spritesTrofeus;
    int currentSpriteTrofeus;

    public GameObject BTTs;
    public Button bttUp;
    public Button bttDown;


    // Use this for initialization
    void Start () {
        BTTs.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        trofeusSystem();
    }
    public void trofeusSystem()
    {
        trof1.sprite = spritesTrofeus[currentSpriteTrofeus];
        if(currentSpriteTrofeus == 0)
        {
            bttUp.gameObject.SetActive(false);
        }
        else
        {
            bttUp.gameObject.SetActive(true);
        }

        if (currentSpriteTrofeus == spritesTrofeus.Length - 1)
        {
            bttDown.gameObject.SetActive(false);
        }
        else
        {
            bttDown.gameObject.SetActive(true);
        }
    }
    public void ativar()
    {
        BTTs.SetActive(true);
    }

    public void desativar()
    {
        BTTs.SetActive(false);
    }

    public void passarLEFT()
    {
        if (currentSpriteTrofeus > 0)
        {
            currentSpriteTrofeus -= 1;
        }
    }

    public void passarRIGHT()
    {
        if (currentSpriteTrofeus < spritesTrofeus.Length - 1)
        {
            currentSpriteTrofeus += 1;
        }
    } 
    
}
