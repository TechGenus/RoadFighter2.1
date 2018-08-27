using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParallax1: MonoBehaviour {
	public GameObject[] players;
	public Vector3 parallaxSpeed;
	public float separationDistance;
	
	private Camera cam;
	private Transform[] childTransforms;
	private int childWithMaxYCoordinateIndex = 0;
	private float minDespawnDistance;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		minDespawnDistance = separationDistance + cam.orthographicSize;
		Debug.Log(minDespawnDistance);
		
		childTransforms = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			childTransforms[i] = transform.GetChild(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < childTransforms.Length; i++) {
			if (childTransforms[i].position.y < -minDespawnDistance) {
				childTransforms[i].position = childTransforms[childWithMaxYCoordinateIndex].position + new Vector3(0, separationDistance, 0);
				childWithMaxYCoordinateIndex = i;
			}
			childTransforms[i].Translate(parallaxSpeed * Time.deltaTime);
		}
	}
}
