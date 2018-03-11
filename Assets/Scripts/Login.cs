using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	void Start () {
		LoadDefaultUserbase();
	}

	void HandleLogin() {
		// Get field values.
		string usernameText = GameObject.FindWithTag("Username").GetComponent<InputField>().text;
		string passwordText = GameObject.FindWithTag("Password").GetComponent<InputField>().text;

		// Check if password matches stored one.
		string storedPassword = PlayerPrefs.GetString(usernameText, "");
		bool passwordMatch = storedPassword != "" && storedPassword == passwordText;
		if (passwordMatch) {
			bool isAdmin = IsAdmin(usernameText);
			if (isAdmin) {
				SceneManager.LoadScene("AdminMenu");
			} else {
				SceneManager.LoadScene("UserMenu");
			}
		} else {
			// Mark field red if password doesn't match.
			Image passwordFieldImage = GameObject.FindWithTag("PasswordField").GetComponent<Image>();
			passwordFieldImage.color = Color.red;
		}
	}

	void LoadDefaultUserbase() {
		bool userbaseExists = PlayerPrefs.GetString("admin", "") != "";
		if (!userbaseExists) {
			Debug.Log("Loaded default.");
			PlayerPrefs.SetString("root", "root");
			PlayerPrefs.SetString("admins", "root");
			PlayerPrefs.SetString("users", "root");
		}
	}

	bool IsAdmin(string username) {
		string[] admins = PlayerPrefs.GetString("admins").Split(';');
		foreach (string admin in admins) {
			if (admin == username) {
				return true;
			}
		}
		return false;
	}
}
