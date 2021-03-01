using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	float nextSpawnTime;
	[SerializeField] Transform spawnPoint;

	[SerializeField] GameObject powerUpPrefab;
	[SerializeField] float spawnDelay = 5f;

	void Update() {
		if (ShouldSpawn()) {
			Spawn();
		}
	}

	void Spawn() {
		nextSpawnTime = Time.time + spawnDelay;
		Vector3 newSpawnPoint = new Vector3(Random.Range(spawnPoint.position.x - 7f, spawnPoint.position.x + 7f),spawnPoint.position.y, spawnPoint.position.z);
		Instantiate(powerUpPrefab, newSpawnPoint, transform.rotation);
	}

	bool ShouldSpawn() {
		return Time.time >= nextSpawnTime;
	}
}
