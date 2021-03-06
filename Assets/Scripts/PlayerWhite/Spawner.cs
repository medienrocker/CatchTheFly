using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

	float nextSpawnTime = 0f;
	[SerializeField] float spawnDelay = 10f;
	[SerializeField] GameObject prefabToSpawn;
	public Transform spawnPoint;

	void Update() {
		if (ShouldSpawn()) {
			Spawn();
		}
	}

	private void Spawn() {
		nextSpawnTime = Time.time + spawnDelay;
		Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
	}

	private bool ShouldSpawn() {
		return Time.time >= nextSpawnTime;
	}
}
