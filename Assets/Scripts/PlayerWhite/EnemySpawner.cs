using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	float nextSpawnTime = 0f;
	[SerializeField] float spawnDelay = 10f;
	[SerializeField] GameObject enemyPrefab;
	//[SerializeField] int enemiesToSpawn = 10;
	//[SerializeField] GameObject[] enemies;
	//Enemy enemy;


	void Start() {

	}

	void Update() {

		if (ShouldSpawn()) {
			Spawn();
		}

		//for (int i = 0; i < enemies.Length; i++) {
		//	StartCoroutine(Spawn(i));
		//}
	}

	private void Spawn() {
		nextSpawnTime = Time.time + spawnDelay;
		Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
	}

	private bool ShouldSpawn() {
		return Time.time >= nextSpawnTime;
	}

	//IEnumerator Spawn(int enemyInArray) {
	//	Instantiate(enemies[enemyInArray], Vector3(enemy.startPoint.x, enemy.startPoint.y, enemy.startPoint.z), Quaternion.identity);
	//}
}
