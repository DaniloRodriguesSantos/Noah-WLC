using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desativarSamDia3 : MonoBehaviour {

    public GameObject sam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (sam.activeSelf == true)
        {
            gameObject.SetActive(false);
        }

    }
}
