using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageUser : MonoBehaviour {

	public GameObject manageUserPanel;
	public GameObject addUserPanel;
	public GameObject modifyUserPanel;

	void AddUser() {
		// Get field values.
		InputField usernameField = GameObject.FindWithTag("Username").GetComponent<InputField>();
		InputField passwordField = GameObject.FindWithTag("Password").GetComponent<InputField>();
		int privilege = GameObject.FindWithTag("Privilege").GetComponent<Dropdown>().value;

		string usernameText = usernameField.text;
		string passwordText = passwordField.text;

		Debug.Log("privilege: " + privilege);

		// Check if user already exists.
		string[] users = PlayerPrefs.GetString("users", "").Split(';');
		bool userExists = false;
		foreach (string user in users) {
			if (user == usernameText) {
				userExists = true;
			}
		}

		// Mark field red if user already exists.
		if (userExists) {
			Image usernameFieldImage = GameObject.FindWithTag("UsernameField").GetComponent<Image>();
			usernameFieldImage.color = Color.red;
			return;
		}

		// Add user.
		PlayerPrefs.SetString(usernameText, passwordText);
		PlayerPrefs.SetString("users", string.Join(";", users) + ";" + usernameText);

		Debug.Log("Users: " + PlayerPrefs.GetString("users"));

		// Set privilege.
		bool isAdmin = privilege == 0;
		if (isAdmin) {
			string admins = PlayerPrefs.GetString("admins");
			PlayerPrefs.SetString("admins", admins + ";" + usernameText);
			Debug.Log("Made it admin");
		}

		// Reset fields and close panel.
		usernameField.text = "";
		passwordField.text = "";
		HidePanel();
	}

	public void ModifyUser() {

	}

	void UpdateUserList() {
		Dropdown usernamesDropdown = GameObject.FindWithTag("UsernameField").GetComponent<Dropdown>();
		string[] users = PlayerPrefs.GetString("users").Split(';');
		usernamesDropdown.ClearOptions();
		usernamesDropdown.AddOptions(new List<string>(users));
	}

	void HidePanel() {
		addUserPanel.SetActive(false);
		modifyUserPanel.SetActive(false);
		manageUserPanel.SetActive(true);
	}
}
