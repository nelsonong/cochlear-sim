using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetModifyUser : MonoBehaviour {

	public Text usernameObject;

	public void SetUser () {
		PlayerPrefs.SetString("modifyUser", usernameObject.text.TrimEnd());
	}
}
