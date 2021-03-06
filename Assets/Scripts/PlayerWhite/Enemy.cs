using UnityEngine;

public class Enemy : MonoBehaviour {

	public int enemyHealth = 10;
	//public Transform startPoint;
	[SerializeField] GameObject contactExplosion;
	[SerializeField] GameObject[] bloodSplashes;
	[SerializeField] Collider2D hurtEnemyCollider;
	[SerializeField] Collider2D hurtPlayerCollider;

	RipplePostProcessor camRipple;

	void Awake() {
		//transform.position = startPoint.position;
	}

	void Start() {
		camRipple = Camera.main.GetComponent<RipplePostProcessor>();
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

	void OnTriggerEnter2D(Collider2D collider) {
		ProcessCollision(collider.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		ProcessCollision(collision.gameObject);
	}

	void ProcessCollision(GameObject collision) {
		if (hurtPlayerCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
			//Instantiate(contactExplosion, transform.position, Quaternion.identity); // should be hurtPlayerEffect = to Do!!
			//camRipple.RippleEffect();
			transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
			Debug.Log("Hit Player!");
		}
		else if (hurtEnemyCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
			Debug.Log("Hit Enemy!");
			TakeDamage(10);
		}
		if (collision.gameObject.tag == "End") {
			Destroy(gameObject);
		}
	}

	public void TakeDamage(int damage) {
		enemyHealth -= damage;
	}
}
