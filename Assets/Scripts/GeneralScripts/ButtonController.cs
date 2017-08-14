using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public void changeScene(string sceneName) 
	{
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	//toogle pause menu
	public void TogglePauseMenu()
	{
		GameObject pauseMenuPanel = GameObject.Find ("PauseMenuPanel");

		if (!pauseMenuPanel.activeSelf) 
		{
			Time.timeScale = 0.0f;
			pauseMenuPanel.SetActive (true);
		} 
		else 
		{
			Time.timeScale = 1.0f;
			pauseMenuPanel.SetActive (false);
		}
	}

	void Start()
	{
		//set options button to unicode cog - doesn't work... :(
//		char gear = '\Ufeff2699';
//		GameObject.Find ("OptionsButtonText").GetComponent<Text> ().text = gear.ToString();
	}
}
