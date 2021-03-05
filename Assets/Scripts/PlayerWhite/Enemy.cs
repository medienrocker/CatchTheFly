using UnityEngine;

public class Enemy : MonoBehaviour {

	public int enemyHealth = 10;
	public Transform startPoint;
	[SerializeField] GameObject contactExplosion;
	[SerializeField] GameObject[] bloodSplashes;
	[SerializeField] Collider2D hurtEnemyCollider;

	RipplePostProcessor camRipple;

	void Start() {
		camRipple = Camera.main.GetComponent<RipplePostProcessor>();
		transform.position = startPoint.position;
	}

	void Update() {
		if (enemyHealth <= 0) {
			Instantiate(bloodSplashes[Random.Range(0, bloodSplashes.Length)], transform.position, Quaternion.identity);
			Instantiate(contactExplosion, transform.position, Quaternion.identity);
			camRipple.RippleEffect();
			//transform.position = startPoint.transform.position;
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player" && !hurtEnemyCollider) {
			//Instantiate(contactExplosion, transform.position, Quaternion.identity);
			//camRipple.RippleEffect();
			transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
		}
		else if (collision.gameObject.tag == "Player" && hurtEnemyCollider) {
			Debug.Log("Player jumped on me");
			TakeDamage(10);
		}
		if (collision.gameObject.tag == "End") {
			Debug.Log("collision with END");
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		//if (other.tag == "Player") {
		//	//Instantiate(contactExplosion, transform.position, Quaternion.identity);
		//	//camRipple.RippleEffect();
		//	transform.position = new Vector3(transform.position.x +5f, transform.position.y, transform.position.z);
		//}
		//else if (other.tag == "Player" && hurtEnemyCollider) {
		//	TakeDamage(10);
		//}
		if (other.tag == "End") {
			Destroy(gameObject);
		}
	}

	public void TakeDamage(int damage) {
		enemyHealth -= damage;
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

	//void OnTriggerEnter2D(Collider2D other) {

	//	if (other.gameObject.tag == "Player") {
	//		Instantiate(contactExplosion, transform.position, Quaternion.identity);
	//		transform.position = startPoint.transform.position;
	//	}
	//	if (other.gameObject.tag == "End") {
	//		transform.position = startPoint.transform.position;
	//	}
	//}

}
