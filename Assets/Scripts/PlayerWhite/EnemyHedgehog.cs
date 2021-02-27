using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHedgehog: MonoBehaviour {

	[SerializeField] Transform startPoint;
	[SerializeField] GameObject hedgehogExplosion;

	void Update() {

	}

	//void OnCollisionEnter2D(Collision2D other) {

	//	if (other.gameObject.tag == "Player") {
	//		Instantiate(hedgehogExplosion, transform.position, Quaternion.identity);
	//		transform.position = startPoint.transform.position;
	//	}
	//	if (other.gameObject.tag == "End") {
	//		transform.position = startPoint.transform.position;
	//	}
	//}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			Instantiate(hedgehogExplosion, transform.position, Quaternion.identity);
			transform.position = startPoint.transform.position;
		}
		if (other.gameObject.tag == "End") {
			transform.position = startPoint.transform.position;
		}
	}
}
