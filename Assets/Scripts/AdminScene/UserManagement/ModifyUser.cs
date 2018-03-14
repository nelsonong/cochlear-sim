using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Ricimi;

public class ModifyUser : MonoBehaviour {

	public GameObject modifyUserPanel;
	public InputField usernameField;
	public InputField passwordField;
	public Slider privilegeSlider;

	public void HandleModify() {
		string username = usernameField.text;
		string password = passwordField.text;

		// Set password.
		PlayerPrefs.SetString(username, password);

		// Set privilege.
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		if (privilegeSlider.value == 1 && !isAdmin) {
			admins.Add(username);
			PlayerPrefs.SetString("admins", string.Join(";", admins.ToArray()));
		} else if (privilegeSlider.value == 0 && isAdmin) {
			admins.Remove(username);
			PlayerPrefs.SetString("admins", string.Join(";", admins.ToArray()));
		}

		RepopulateListItem(username);
		DestroyThisPanel();
	}

	void RepopulateListItem(string username) {
		GameObject manageUsersPanel = GameObject.Find("Manage-Users-Popup(Clone)");
		PopulateManageUsersPanel script = manageUsersPanel.GetComponent<PopulateManageUsersPanel>();
		script.RepopulateListItem(username);
	}

	void DestroyThisPanel() {
		Popup script = modifyUserPanel.GetComponent<Popup>();
		script.Close();
	}
}
