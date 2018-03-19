using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNext : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("LoginScene");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
