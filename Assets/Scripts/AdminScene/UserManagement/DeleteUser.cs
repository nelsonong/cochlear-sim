using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeleteUser : MonoBehaviour {

	public GameObject usernameObject;
	public GameObject listItemObject;

	public void HandleDelete() {
		// Remove from user list.
		string username = usernameObject.GetComponent<Text>().text;
		List<string> users = PlayerPrefs.GetString("users").Split(';').ToList();
		users.Remove(username.TrimEnd());
		PlayerPrefs.SetString("users", string.Join(";", users.ToArray()));

		// Remove from admins.
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		if (isAdmin) {
			admins.Remove(username);
			PlayerPrefs.SetString("admins", string.Join(";", admins.ToArray()));
		}

		// Delete existing stat entry.
		StatsManager.instance.DeleteUserStats(username);

		// Remove from view.
		Destroy(listItemObject);
	}
}
