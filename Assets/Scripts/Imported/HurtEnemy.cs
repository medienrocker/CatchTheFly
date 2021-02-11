using UnityEngine;

public class HurtEnemy : MonoBehaviour {

	[SerializeField] int damage;
	int currentDamage;
	[SerializeField] GameObject hitParticles;
	[SerializeField] Transform hitPoint;
	[SerializeField] GameObject damageNumber;

	PlayerStats playerStats;

	void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
	}

	void Update() {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			currentDamage = damage + playerStats.currentAttack;

			Instantiate(hitParticles, hitPoint.position, hitPoint.rotation);
			other.gameObject.GetComponent<EnemyHealthManager>().TakeHit(currentDamage);
			var clone = (GameObject)Instantiate(damageNumber, hitPoint.position, Quaternion.identity);
			clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;

		}
	}
}
