using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public InputField usernameField;
	public InputField passwordField;
	public GameObject passwordFieldBackground;

	void Start () {
		LoadDefaultUserbase();
	}

	public void HandleLogin() {
		// Get field values.
		string username = usernameField.text;
		string password = passwordField.text;

		// Check if password matches stored one.
		string storedPassword = PlayerPrefs.GetString(username, "");
		bool passwordMatch = storedPassword != "" && storedPassword == password;
		if (passwordMatch) {
			PlayerPrefs.SetString("currentUser", username);
			bool isAdmin = IsAdmin(username);
			SceneManager.LoadScene(isAdmin ? "AdminScene" : "UserScene");
		} else {
			// Mark field red if password doesn't match.
			passwordFieldBackground.GetComponent<Image>().color = new Color32(133, 12, 12, 40);
		}
	}

	void LoadDefaultUserbase() {
		bool userbaseExists = PlayerPrefs.GetString("root").Equals("root");
		if (!userbaseExists) {
			PlayerPrefs.SetString("root", "root");
			PlayerPrefs.SetString("admins", "root");
			PlayerPrefs.SetString("users", "root");
		}
	}

	bool IsAdmin(string username) {
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		if (isAdmin) {
			return true;
		}
		return false;
	}
}
