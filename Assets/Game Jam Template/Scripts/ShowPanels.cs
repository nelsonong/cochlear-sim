using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject manageUsersPanel;
	public GameObject addUserPanel;
	public GameObject modifyUserPanel;

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		menuPanel.SetActive (false);
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
		menuPanel.SetActive (true);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	public void ResetToAdminMenu()
	{
		SceneManager.LoadScene("AdminMenu");
	}

	public void ResetToUserMenu()
	{
		SceneManager.LoadScene("UserMenu");
	}

	public void ResetToLogin()
	{
		SceneManager.LoadScene("LoginScreen");
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

	public void ShowManageUsersPanel()
	{
		manageUsersPanel.SetActive(true);
		menuPanel.SetActive(false);
	}

	public void HideManageUsersPanel()
	{
		manageUsersPanel.SetActive(false);
		menuPanel.SetActive(true);
	}

	public void ShowAddUserPanel()
	{
		addUserPanel.SetActive(true);
		manageUsersPanel.SetActive(false);
	}

	public void HideAddUserPanel()
	{
		addUserPanel.SetActive(false);
		manageUsersPanel.SetActive(true);
	}

	public void ShowModifyUserPanel()
	{
		modifyUserPanel.SetActive(true);
		manageUsersPanel.SetActive(false);
	}

	public void HideModifyUserPanel()
	{
		modifyUserPanel.SetActive(false);
		manageUsersPanel.SetActive(true);
	}
}
