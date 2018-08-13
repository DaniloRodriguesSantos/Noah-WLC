using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniG_UIController : MonoBehaviour {

    public Text life_Indicator;
    private Controls controls_Script;

    private void Awake()
    {
        controls_Script = GameObject.Find("Player").GetComponent<Controls>();
    }
	
	// Update is called once per frame
	void Update () {
        life_Indicator.text = "Vida: " + controls_Script.player_life.ToString();
	}
}
