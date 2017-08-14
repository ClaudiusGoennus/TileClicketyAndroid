using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//
// -------------- TODO ---------------
//
// * Game over message needs to display for a few seconds
//

public class RectSpawnController : MonoBehaviour
{
	public GameObject rectPrefab;
	public static int points = 0;
	//minWait, maxWait, interval handeled in RectGroupController
	public static double minWait = 0.3;
	public static double maxWait = 0.7;
	public static double interval = 120;
	//

	private Transform myTrans;
	private SpriteRenderer mySpriteRend;
	private Camera mainCam;
	private static Text pointsCounterText;

	private short framesSinceSpawn;
	private static float rectSideLength;

	// Use this for initialization
	void Start ()
	{
		myTrans = GetComponent<Transform> ();
		mySpriteRend = GetComponent<SpriteRenderer> ();
		pointsCounterText = GameObject.Find ("PointsCounterText").GetComponent<Text> ();
		mainCam = Camera.main;

		framesSinceSpawn = 0;
		rectSideLength = 2.8f;

		//set rect color
		mySpriteRend.color = Color.grey;
	}

	//game over if framesSinceSpawn isn't reset by clicking, before it reaches interval
	void Update ()
	{
		//game over if player isn't fast enough
		if (framesSinceSpawn > interval) {
			GameOverController.GameOver (points);
		}

		framesSinceSpawn++;
	}

	//add a point and spawn new rect
	void OnMouseDown ()
	{
		//points stuff
		points++;
		pointsCounterText.text = "Points: " + points.ToString ();
		Debug.Log("- minWait: " + minWait.ToString() + " - maxWait: " + maxWait.ToString());

		//set this rect to grey
		mySpriteRend.color = Color.white;

		//create new rect
		NewRect ();
	}

	//creates a new rect, sets old rect to white, shifts camera and destroys script on old rect
	private void NewRect ()
	{
		//new element
		var nextPos = NextRectPos ();
		Instantiate (rectPrefab, nextPos, new Quaternion ());

		//set color to grey
		mySpriteRend.color = Color.white;

		//camera
		mainCam.transform.position = new Vector3 (transform.position.x, transform.position.y, -10);

		//activate wrongClickScript on this rect
		//gameObject.AddComponent<WrongClickController>();
		//destroy this script on old object
		Destroy (GetComponent<RectSpawnController> ());
	}

	//calculates position for next rect
	private Vector2 NextRectPos ()
	{
		//calculate possible new positions
		List<Vector2> directions = new List<Vector2> () {
	      //up
			new Vector2 (myTrans.position.x, myTrans.position.y + rectSideLength),
		  //right
			new Vector2 (myTrans.position.x + rectSideLength, myTrans.position.y),
		  //down
		    new Vector2 (myTrans.position.x, myTrans.position.y - rectSideLength),
		  //left
			new Vector2 (myTrans.position.x - rectSideLength, myTrans.position.y)
		};

		//check if rect is surrounded
		for (int i = 0; i < 4; i++) {
			if (Physics2D.OverlapPoint (directions [i])) {
				if (i == 3)
					GameOverController.GameOver (points, "LOCKED IN");
			} 
			else 
				break;
		}
			
		//choose which direction to spawn in
		Vector2 retVal = directions[UnityEngine.Random.Range(0, 4)];

		//check if theres no rect at chosen position
		try {
			if (Physics2D.OverlapPoint (retVal))
				return NextRectPos ();
			else
				return retVal;
		} catch (System.Exception ex) { Debug.Log ("Overlap check exception: " + ex.ToString ()); }

		throw(new Exception ("NextRectPos() has died..."));
	}

}
