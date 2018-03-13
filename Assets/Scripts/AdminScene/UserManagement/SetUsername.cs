using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUsername : MonoBehaviour {

	void Start () {
		Text usernameObject = GetComponent<Text>();
		usernameObject.text = PlayerPrefs.GetString("currentUser");
	}
}
