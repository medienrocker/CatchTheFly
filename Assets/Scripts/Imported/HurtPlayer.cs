using UnityEngine;

public class HurtPlayer : MonoBehaviour {
	
	[SerializeField] int damage;
	int currentDamage;
	[SerializeField] GameObject damageNumber;
	[SerializeField] GameObject hitParticles;
	[SerializeField] Transform hitPoint;


	PlayerStats playerStats;

	void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
	}

	void Update() {

	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			currentDamage = damage - playerStats.currentDefense;
			if (currentDamage < 0) {
				currentDamage = 0;
			}

			other.gameObject.GetComponent<PlayerHealthManager>().TakeHit(damage);
			Instantiate(hitParticles, hitPoint.position, hitPoint.rotation);

			var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.identity);
			clone.GetComponent<FloatingNumbers>().damageNumber = damage;

			//other.gameObject.GetComponent<PlayerHealthManager>().isFlashing = true;
		}
	}

	//void OnTriggerEnter2D(Collider2D other) {
	//	if (other.gameObject.tag == "Player") {
	//		currentDamage = damage - playerStats.currentDefense;
	//		if (currentDamage < 0) {
	//			currentDamage = 0;
	//		}

	//		other.gameObject.GetComponent<PlayerHealthManager>().TakeHit(damage);

	//		var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.identity);
	//		clone.GetComponent<FloatingNumbers>().damageNumber = damage;

	//		other.GetComponent<PlayerHealthManager>().isFlashing = true;
	//	}
	//}
}
