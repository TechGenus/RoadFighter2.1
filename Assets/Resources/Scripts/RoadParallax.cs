using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParallax: MonoBehaviour {
	[HideInInspector] public float parallaxSpeed;
	public float maxParallaxSpeed;
	public float parallaxIncrementSpeed;
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
	}

	// Update is called once per frame
	void Update () {
		minDespawnDistance = camT.position.y - separationDistance - camOrthographicSize;
		maxDespawnDistance = camT.position.y + separationDistance + camOrthographicSize;

		DoParallaxing();
		ChangeParallaxSpeedBasedOnPlayerMovement();
	}

	void DoParallaxing() {
		for (int i = 0; i < childTransforms.Length; i++) {
			if (parallaxSpeed >= 0 && childTransforms[i].position.y < minDespawnDistance) {
				childTransforms[i].position = childTransforms[childWithMaxYCoordinateIndex].position + new Vector3(0, separationDistance, 0);
				childWithMaxYCoordinateIndex = i;
			}
			if (parallaxSpeed < 0 && childTransforms[i].position.y > maxDespawnDistance) {
				childTransforms[i].position = childTransforms[childWithMinYCoordinateIndex].position - new Vector3(0, separationDistance, 0);
				Debug.Log(childWithMinYCoordinateIndex);
				childWithMinYCoordinateIndex = i;
			}
			childTransforms[i].Translate(new Vector3(0, -parallaxSpeed, 0) * Time.deltaTime);
		}
	}

	void ChangeParallaxSpeedBasedOnPlayerMovement() {
		float player1VAxis = playerMovementScripts[0].vAxis;
		float player2VAxis = playerMovementScripts[1].vAxis;

		if (player1VAxis == 1 && player2VAxis == 1 && parallaxSpeed < maxParallaxSpeed) {
			parallaxSpeed += parallaxIncrementSpeed * Time.deltaTime;
		}
		else if (parallaxSpeed >= -maxParallaxSpeed && parallaxSpeed <= maxParallaxSpeed) {
			parallaxSpeed += parallaxIncrementSpeed * (player1VAxis + player2VAxis) / 2f * Time.deltaTime;
		}
		else if (parallaxSpeed < -maxParallaxSpeed) {
			parallaxSpeed = -maxParallaxSpeed;
		}
		else {
			parallaxSpeed = maxParallaxSpeed;
		}
	}
}
