using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ClickRectController : MonoBehaviour {

	public GameObject clickRectPrefab;

	//do not set in unity, is set by RectGroupController
	public float timeToClick;

	public static Text pointsCounterText;
	public static GameObject timeToClickTextObject;
	public static Text timeToClickText;
	public static Camera mainCam;

	public static int points;

	private Transform myTrans;
	private SpriteRenderer myspriteRend;

	private static float rectSideLength = 2.8f;

	//initialize the shiet and set TimeToClickText position
	void Start () 
	{
		myTrans = GetComponent<Transform> ();
		myspriteRend = GetComponent<SpriteRenderer> ();

		//text shiet
		pointsCounterText.text = points.ToString ();
		//timeToClickTextObject.GetComponent<RectTransform>().localPosition = new Vector3(transform.position.x, transform.position.y, 2);
	}

	//count down timeToClick and end if it reaches 0
	void Update() 
	{
		timeToClick -= Time.deltaTime;
		timeToClickText.text = Math.Round(timeToClick, 2).ToString ();
		Debug.Log (timeToClick.ToString ());

		if (timeToClick <= 0.0f) {
		}
			//GameOverController.GameOver (points);
	}

	//player clicks the rect
	void OnMouseDown()
	{
		//points
		pointsCounterText.text = points++.ToString();

		//spawn next rect
		NewRect();

		//become more transparent to indicate inactivity (0.5 x 3 is Color.gray)
		myspriteRend.color = new Color( 0.5f, 0.5f, 0.5f, 0.1f);

		//shift camera
		mainCam.transform.position = new Vector3 (myTrans.position.x, myTrans.position.y, -20);
	}

	//create next rect, destroy script on this one
	private void NewRect() 
	{
		//instantiate new rect
		Instantiate(clickRectPrefab, NextRectPos(), new Quaternion());

		//destroy this script
		Destroy(GetComponent<ClickRectController>());
	}

	//calc position for next rect
	private Vector3 NextRectPos() 
	{
		//calculate possible new positions
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

		throw(new System.Exception ("NextRectPos() has died..."));
	}
}
