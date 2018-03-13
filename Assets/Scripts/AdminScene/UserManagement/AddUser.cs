using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Ricimi;

public class AddUser : MonoBehaviour {

	// Add user panel elements.
	public GameObject addUserPanel;
	public InputField usernameField;
	public GameObject usernameFieldBackground;
	public InputField passwordField;
	public Slider privilegeSlider;

	public void HandleAdd() {
		// Get field values.
		string username = usernameField.text;
		string password = passwordField.text;

		// Check if user already exists.
		List<string> users = PlayerPrefs.GetString("users").Split(';').ToList();
		bool userExists = users.Exists(e => e.EndsWith(username));
		if (!userExists) {
			// Add user.
			PlayerPrefs.SetString(username, password);
			users.Add(username);
			PlayerPrefs.SetString("users", string.Join(";", users.ToArray()));

			// Check for admin privileges.
			List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
			bool isAdmin = privilegeSlider.value == 1;
			if (isAdmin) {
				admins.Add(username);
				PlayerPrefs.SetString("admins", string.Join(";", admins.ToArray()));
			}

			RepopulateManageUsersPanel();
			DestroyThisPanel();
		} else {
			// Mark field red if password doesn't match.
			usernameFieldBackground.GetComponent<Image>().color = new Color32(133, 12, 12, 60);
		}
	}

	void RepopulateManageUsersPanel() {
		GameObject manageUsersPanel = GameObject.Find("Manage-Users-Popup(Clone)");
		PopulateManageUsersPanel script = manageUsersPanel.GetComponent<PopulateManageUsersPanel>();
		script.PopulatePanel();
	}

	void DestroyThisPanel() {
		Popup script = addUserPanel.GetComponent<Popup>();
		script.Close();
	}
}