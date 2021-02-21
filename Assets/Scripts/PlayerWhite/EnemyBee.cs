using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBee : MonoBehaviour {

	[SerializeField] Transform startPoint;
	[SerializeField] GameObject beeExplosion;

	void Update() {

	}
	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player") {
			Instantiate(beeExplosion, transform.position, Quaternion.identity);
			transform.position = startPoint.transform.position;
		}
		if (other.gameObject.tag == "End") {
			transform.position = startPoint.transform.position;
		}
	}
}
