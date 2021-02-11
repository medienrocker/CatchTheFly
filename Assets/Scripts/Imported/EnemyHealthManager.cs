using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

	[SerializeField] int maxHealth;
	int currentHealth;

	PlayerStats playerStats;
	[SerializeField] int expToGive;

	void Start() {
		SetHealth();

		playerStats = FindObjectOfType<PlayerStats>();
	}

	void Update() {
		if (currentHealth <= 0) {
			//gameObject.SetActive(false);
			Destroy(gameObject);

			playerStats.AddExperience(expToGive);
		}
	}

	void SetHealth() {
		currentHealth = maxHealth;
	}

	public void TakeHit(int damage) {
		currentHealth -= damage;
	}
}
