using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this helps and partly goes after this
//https://www.youtube.com/watch?v=9l_hPxER6c8

//this function for the buttons in the main menu

public class UIManager : MonoBehaviour
{
	public void PlayGame() {

		SceneManager.LoadScene("Main");
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void ShowMenu() {

		SceneManager.LoadScene("Menu");
	}

	public void ShowAbout() {

		SceneManager.LoadScene("About");
	}
}
