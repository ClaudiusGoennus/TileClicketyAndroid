using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour 
{
	public static string gameOverMessage;

	public static void GameOver(int points, string gameOverMessage = "TIMEOUT") 
	{
		if(gameOverMessage == "TIMEOUT")
			GameOverController.gameOverMessage = "GAME OVER\nYou weren't fast enough!\nWell, at least you got " + points.ToString() + " points.";

		SceneManager.LoadScene ("GameOverScene", LoadSceneMode.Single);
	}
}
