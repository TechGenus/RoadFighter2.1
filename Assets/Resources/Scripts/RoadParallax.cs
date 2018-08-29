using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParallax: MonoBehaviour {
	public float separationDistance;
	public GameObject[] players = new GameObject[2];

	private Camera cam;
	private Transform camT;
	private float camOrthographicSize;
	private Transform[] childTransforms;
	private int childWithMaxYCoordinateIndex;
	private int childWithMinYCoordinateIndex;
	private float minDespawnDistance;
	private float maxDespawnDistance;
	private PlayerMovement[] playerMovementScripts = new PlayerMovement[2];
	private float camTPreviousPositionY;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		camT = cam.GetComponent<Transform>();
		camOrthographicSize = cam.orthographicSize;

		int childCount = transform.childCount;
		childWithMaxYCoordinateIndex = 0;
		childWithMinYCoordinateIndex = childCount-1;
		childTransforms = new Transform[childCount];
		for (int i = 0; i < childCount; i++) {
			childTransforms[i] = transform.GetChild(i);
		}

		playerMovementScripts[0] = players[0].GetComponent<PlayerMovement>();
		playerMovementScripts[1] = players[1].GetComponent<PlayerMovement>();

		minDespawnDistance = camT.position.y + childTransforms[childWithMinYCoordinateIndex].position.y - separationDistance;
		maxDespawnDistance = camT.position.y + childTransforms[childWithMaxYCoordinateIndex].position.y + separationDistance;
	}

	// Update is called once per frame
	void Update () {
		//minDespawnDistance = camT.position.y - separationDistance - camOrthographicSize;
		//maxDespawnDistance = camT.position.y + separationDistance + camOrthographicSize;
		DoParallaxingUpwards();
	}

	void DoParallaxingUpwards() {
		float tmpMinDespawnDistance = minDespawnDistance + camT.position.y;
		float tmpMaxDespawnDistance = maxDespawnDistance + camT.position.y;
		for (int i = 0; i < childTransforms.Length; i++) {
			if (childTransforms[i].position.y < tmpMinDespawnDistance) {
				childTransforms[i].position = childTransforms[childWithMaxYCoordinateIndex].position + new Vector3(0, separationDistance, 0);
				childWithMaxYCoordinateIndex = i;
			}
			else if (childTransforms[i].position.y > tmpMaxDespawnDistance) {
				childTransforms[i].position = childTransforms[childWithMinYCoordinateIndex].position - new Vector3(0, separationDistance, 0);
				childWithMinYCoordinateIndex = i;
				Debug.Log(childWithMinYCoordinateIndex);
			}
		}
	}
}
