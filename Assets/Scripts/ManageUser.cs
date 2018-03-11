using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		// Mark field red if user already exists.
		List<string> userList = PlayerPrefs.GetString("users").Split(';').ToList();
		if (userList.Exists(e => e.EndsWith(usernameText))) {
			Image usernameFieldImage = GameObject.FindWithTag("UsernameField").GetComponent<Image>();
			usernameFieldImage.color = Color.red;
			return;
		}

		// Add user.
		PlayerPrefs.SetString(usernameText, passwordText);
		string users = PlayerPrefs.GetString("users");
		PlayerPrefs.SetString("users", users + ";" + usernameText);

		// Set privilege.
		bool isAdmin = privilege == 0;
		if (isAdmin) {
			string admins = PlayerPrefs.GetString("admins");
			PlayerPrefs.SetString("admins", admins + ";" + usernameText);
			Debug.Log("Admins: " + PlayerPrefs.GetString("admins"));
		}

		// Reset fields and close panel.
		usernameField.text = "";
		passwordField.text = "";
		HidePanel();
	}

	void ModifyUser() {
		// Get username.
		Dropdown usernameDropdown = GameObject.FindWithTag("UsernameField").GetComponent<Dropdown>();
		string username = usernameDropdown.GetComponentInChildren<Text>().text;

		// Update username field.
		InputField passwordField = GameObject.FindWithTag("Password").GetComponent<InputField>();
		PlayerPrefs.SetString(username, passwordField.text);

		// Update privilege.
		Dropdown privilegeDropdown = GameObject.FindWithTag("Privilege").GetComponent<Dropdown>();
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		if (privilegeDropdown.value == 0 && !isAdmin) {
			admins.Add(username);
			PlayerPrefs.SetString("admins", string.Join(";", admins.ToArray()));
		} else if (privilegeDropdown.value == 1 && isAdmin) {
			admins.Remove(username);
			PlayerPrefs.SetString("admins", string.Join(";", admins.ToArray()));
		}

		HidePanel();
	}

	void UserChanged(int index) {
		// Get username.
		Dropdown usernameDropdown = GameObject.FindWithTag("UsernameField").GetComponent<Dropdown>();
		string username = usernameDropdown.GetComponentInChildren<Text>().text;

		// Update username field.
		string storedPassword = PlayerPrefs.GetString(username);
		InputField passwordField = GameObject.FindWithTag("Password").GetComponent<InputField>();
		passwordField.text = storedPassword;

		// Update privilege.
		Dropdown privilegeDropdown = GameObject.FindWithTag("Privilege").GetComponent<Dropdown>();
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		if (isAdmin) {
			privilegeDropdown.value = 0;
		} else {
			privilegeDropdown.value = 1;
		}
	}

	void UpdateUserList() {
		Dropdown usernamesDropdown = GameObject.FindWithTag("UsernameField").GetComponent<Dropdown>();
		List<string> users = PlayerPrefs.GetString("users").Split(';').ToList();
		usernamesDropdown.ClearOptions();
		usernamesDropdown.AddOptions(users);
	}

	void HidePanel() {
		addUserPanel.SetActive(false);
		modifyUserPanel.SetActive(false);
		manageUserPanel.SetActive(true);
	}
}
