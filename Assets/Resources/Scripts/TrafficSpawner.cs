using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour {
	public GameObject[] trafficPrefabs;
	public Transform[] spawnPositions;
	public float respawnTimerCoolDown;

	private float respawnTimer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (respawnTimer < Time.time) {
			SpawnTraffic();
		}
	}

	void SpawnTraffic() {
		int randomSpawnIndex = (int)Random.Range(0, spawnPositions.Length - 1);
		int randomPrefabToSpawn = (int)Random.Range(0, trafficPrefabs.Length - 1);

		Instantiate(trafficPrefabs[randomPrefabToSpawn], spawnPositions[randomSpawnIndex].position, Quaternion.identity);
		respawnTimer = Time.time + respawnTimerCoolDown;
	}
}
