using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("GameOverText").GetComponent<Text> ().text = GameOverController.gameOverMessage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
