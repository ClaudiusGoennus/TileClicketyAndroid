using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

// Handles the first rectangle that appears on the screen
// it will blink repeatedly until it is clicked, it then
// disappears and spawns a new rectangle of ClickRect Type
public class ClickRectOGController : MonoBehaviour {

	public GameObject clickRectPrefab;

	public float blinkTimerSeconds;

	private SpriteRenderer mySpriteRend;
	private Transform myTrans;
	private Camera mainCam;

	private static float rectSideLength;

	void Start () 
	{
		mySpriteRend = GetComponent<SpriteRenderer>();
		myTrans = GetComponent<Transform> ();
		mainCam = Camera.main;

		rectSideLength = 2.8f;

		StartCoroutine (Blink());
	}

	// spawn new rect and detroy this one
	void OnMouseDown()
	{
		NewRect ();

		Destroy (gameObject);
	}

	// Blink
	private IEnumerator Blink()
	{
		while (true) {
			mySpriteRend.color = Color.grey;
			yield return new WaitForSeconds (blinkTimerSeconds);

			mySpriteRend.color = Color.white;
			yield return new WaitForSeconds (blinkTimerSeconds);
		}
	}

	//creates a new rect, adds a point, sets old rect to white and shifts the camera
	private void NewRect ()
	{
		//new element
		Instantiate ( clickRectPrefab, NextRectPos (), new Quaternion ());

		//instantiate public statics on new ClickRectController
		ClickRectController.points = 1;
		ClickRectController.mainCam = Camera.main;
		ClickRectController.timeToClickTextObject = GameObject.Find ("TimeToClickText");
		ClickRectController.timeToClickText = ClickRectController.timeToClickTextObject.GetComponent<Text> ();
		ClickRectController.pointsCounterText = GameObject.Find ("PointsCounterText").GetComponent<Text> ();

		//set color to white
		mySpriteRend.color = Color.white;

		//camera
		mainCam.transform.position = transform.position;

		//destroy this script
		Destroy(GetComponent<ClickRectOGController>());
	}

	//calculates position for next rect
	private Vector3 NextRectPos ()
	{
		//calculate possible new positions, the 1 is very important, or else it wont be seen by the camera
		List<Vector3> directions = new List<Vector3> () {
			//up
			new Vector3 (myTrans.position.x, myTrans.position.y + rectSideLength, 1),
			//right
			new Vector3 (myTrans.position.x + rectSideLength, myTrans.position.y, 1),
			//down
			new Vector3 (myTrans.position.x, myTrans.position.y - rectSideLength, 1),
			//left
			new Vector3 (myTrans.position.x - rectSideLength, myTrans.position.y, 1)
		};

		//choose which direction to spawn in
		var retVal = directions[UnityEngine.Random.Range(0, 4)];

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
