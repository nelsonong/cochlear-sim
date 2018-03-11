using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadDefaultUserbase();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleLogin() {
		string usernameText = GameObject.FindWithTag("Username").GetComponent<InputField>().text;
		string passwordText = GameObject.FindWithTag("Password").GetComponent<InputField>().text;

		string storedPassword = PlayerPrefs.GetString(usernameText, "");

		bool passwordMatch = storedPassword != "" && storedPassword == passwordText;
		if (passwordMatch) {
			bool isAdmin = IsAdmin(usernameText);
			if (isAdmin) {
				SceneManager.LoadScene(0);
			} else {
				SceneManager.LoadScene(1);
			}
		} else {
			Image passwordFieldImage = GameObject.FindWithTag("PasswordField").GetComponent<Image>();
			passwordFieldImage.color = Color.red;
		}

		Debug.Log("username: " + usernameText + "; password: " + passwordText + "; passwordFound: " + storedPassword);
	}

	void LoadDefaultUserbase() {
		Debug.Log("Loaded default called");
		bool userbaseExists = PlayerPrefs.GetString("admin", "") != "";
		if (!userbaseExists) {
			Debug.Log("Userbase doesn't exist!");
			PlayerPrefs.SetString("root", "root");
			PlayerPrefs.SetString("admins", "root");
		}
	}

	bool IsAdmin(string username) {
		string[] adminsList = PlayerPrefs.GetString("admins").Split(';');
		foreach (string adminName in adminsList) {
			if (adminName == username) {
				return true;
			}
		}
		return false;
	}
}
