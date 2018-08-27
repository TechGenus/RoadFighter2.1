using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficMovement : MonoBehaviour {
	public RoadParallax roadParallaxScript;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
