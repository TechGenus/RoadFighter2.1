using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParallax2 : MonoBehaviour {
	public float backgroundSize;
	public float parallaxSpeed;

	private Transform camT;
	private Transform[] layers;
	private float viewZone = 10;
	private int topIndex;
	private int bottomIndex;
	private float lastCameraY;

	// Use this for initialization
	void Start () {
		camT = Camera.main.transform;
		lastCameraY = camT.position.y;
		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			layers[i] = transform.GetChild(i);
		}

		topIndex = 0;
		bottomIndex = layers.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaY = camT.position.y - lastCameraY;
		transform.position += new Vector3(0, deltaY * parallaxSpeed, 0);
		lastCameraY = camT.position.y;

		if (camT.position.y < layers[bottomIndex].position.y + viewZone) {
			ScrollDown();
		}
		if (camT.position.y > layers[topIndex].position.y - viewZone) {
			ScrollUp();
		}
	}

	private void ScrollUp() {
		int lastDown = bottomIndex;
		layers[bottomIndex].position = new Vector3(layers[bottomIndex].position.x, layers[topIndex].position.y + backgroundSize, layers[bottomIndex].position.z);

		topIndex = bottomIndex;
		bottomIndex--;
		if (bottomIndex < 0) {
			bottomIndex = layers.Length - 1;
		}
	}

	private void ScrollDown() {
		int lastUp = topIndex;
		layers[topIndex].position = new Vector3(layers[topIndex].position.x, layers[bottomIndex].position.y - backgroundSize, layers[bottomIndex].position.z);

		bottomIndex = topIndex;
		topIndex++;
		if (topIndex == layers.Length) {
			topIndex = 0;
		}
	}
}
