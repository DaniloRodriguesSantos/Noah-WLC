using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguirPlayer : MonoBehaviour {
    Transform player;
	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position;
    }
}
