using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStatsUser : MonoBehaviour {

	public Text usernameText;
	
	public void HandleSet() {
		PlayerPrefs.SetString("statsUser", usernameText.text.Trim());
	}
}
