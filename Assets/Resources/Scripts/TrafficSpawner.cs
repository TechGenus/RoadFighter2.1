using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour {
	public GameObject[] trafficPrefabs;
	public Transform[] spawnPositions;
	public float respawnCoolDownTime;
	public float minRespawnCoolDownTime;
	public float respawnCoolDownTimeDecrementValue;


	private float respawnTimer;
	
	// Update is called once per frame
	void Update () {
		if (respawnTimer < Time.time) {
			SpawnTraffic();
		}
	}

	void SpawnTraffic() {
		int randomSpawnIndex = (int)Random.Range(0, spawnPositions.Length);
		int randomPrefabToSpawn = (int)Random.Range(0, trafficPrefabs.Length);

		Instantiate(trafficPrefabs[randomPrefabToSpawn], spawnPositions[randomSpawnIndex].position, trafficPrefabs[randomPrefabToSpawn].transform.rotation);
		respawnTimer = Time.time + respawnCoolDownTime;

		if (respawnCoolDownTime > minRespawnCoolDownTime) {
			respawnCoolDownTime -= respawnCoolDownTimeDecrementValue;
		}
	}
}
