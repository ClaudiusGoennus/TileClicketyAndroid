using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaboController : MonoBehaviour {

	public static ButtonController buttonController;
	public static GameMode1Controller gameMode1Controller;
	public static GameMode2Controller gameMode2Controller;

	private static bool blackTheme = true;
	private static Dictionary<string, Color> blackThemeColors;
	private static Dictionary<string, Color> whiteThemeColors;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void ToggleTheme()
	{
		if (blackTheme)
			;
		else
			;
	}
}
