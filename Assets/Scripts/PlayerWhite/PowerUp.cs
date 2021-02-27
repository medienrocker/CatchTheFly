﻿using System;
using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	[SerializeField] float multiplier = 1f;
	[SerializeField] GameObject pickupEffect;
	[SerializeField] float powerupTime = 3f;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			StartCoroutine(PickUp(other));
		}
	}

	IEnumerator PickUp(Collider2D player) {
		Instantiate(pickupEffect, transform.position, transform.rotation);
		player.gameObject.GetComponent<Player>().jumpMultiplier *= multiplier;

		GetComponent<Collider2D>().enabled = false;
		GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds(powerupTime);
		player.gameObject.GetComponent<Player>().jumpMultiplier /= multiplier;
		Destroy(gameObject);
	}

	//void OnCollisionEnter2D(Collision2D other) {

	//	if (other.gameObject.tag == "Player") {
	//		StartCoroutine(PickUp(other));
	//	}
	//	//else {
	//	//	Destroy(gameObject, 5f);
	//	//}
	//}

	//IEnumerator PickUp(Collision2D player) {
	//	Instantiate(pickupEffect, transform.position, transform.rotation);
	//	player.gameObject.GetComponent<Player>().jumpMultiplier *= multiplier;

	//	gameObject.GetComponent<Collider2D>().enabled = false;
	//	//gameObject.GetComponent<Renderer>().enabled = false;

	//	yield return new WaitForSeconds(powerupTime);
	//	player.gameObject.GetComponent<Player>().jumpMultiplier /= multiplier;
	//	Destroy(gameObject);
	//}
}
