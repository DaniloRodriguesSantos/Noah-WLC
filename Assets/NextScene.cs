using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public int sceneIndex;
	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(sceneIndex);	
	}
}
