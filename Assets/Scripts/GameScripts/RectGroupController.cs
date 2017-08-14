using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//handles deleting them after a certain number has spawned
public class RectGroupController : MonoBehaviour {

	public short maxRects;

	private static Queue<GameObject> rects = new Queue<GameObject> ();

	private static int totalRectCount = 0;

	//add this to the list of rects
	void Start () {
		rects.Enqueue (gameObject);
		totalRectCount++;

		GetComponent<ClickRectController> ().timeToClick = (float)(10 * Math.Pow(totalRectCount, -1));
	}
	
	//every maxRects new rects, destroy the oldest one
	void Update () {
		if (rects.Count % maxRects == 0) {
			Destroy (rects.Dequeue ());
		}
	}
}
