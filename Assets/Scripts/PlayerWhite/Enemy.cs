using System.Collections;
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
		Die();
	}

	void Die() {
		if (enemyHealth <= 0) {
			Instantiate(bloodSplashes[Random.Range(0, bloodSplashes.Length - 1)], transform.position, Quaternion.identity);
			Instantiate(contactExplosion, transform.position, Quaternion.identity);

			// Play explosion SFX
			string [] SFX = new string[] { "Enemy Explosion 1", "Enemy Explosion 2" };
			string randomExplosionSFX = SFX[Random.Range(0, SFX.Length)];
			AudioManager.instance.Play(randomExplosionSFX);

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

			StartCoroutine(IsPlayerHit(collision));

			// PLAY 'Hit Player Sound'
			AudioManager.instance.Play("Hit Player");

			transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
			//Debug.Log("Hit Player!");
		}
		else if (hurtEnemyCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
			//Debug.Log("Hit Enemy!");
			TakeDamage(10);
		}
		if (collision.gameObject.tag == "End") {
			Destroy(gameObject);
		}
	}

	IEnumerator IsPlayerHit(GameObject player) {
		player.gameObject.GetComponent<Player>().isPlayerHit = true;
		yield return new WaitForSeconds(0.1f);
		player.gameObject.GetComponent<Player>().isPlayerHit = false;
	}

	public void TakeDamage(int damage) {
		enemyHealth -= damage;
	}


}
