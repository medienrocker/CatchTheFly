using UnityEngine;

public class Food : MonoBehaviour {

	//[SerializeField] Transform startPoint;
	[SerializeField] GameObject foodExplosion;
	[SerializeField] GameObject[] bloodSplashes;

	RipplePostProcessor camRipple;

	void Start() {
		camRipple = Camera.main.GetComponent<RipplePostProcessor>();
	}

	void Update() {

	}

	void OnTriggerEnter2D(Collider2D collider) {
		ProcessCollision(collider.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		ProcessCollision(collision.gameObject);
	}

	void ProcessCollision(GameObject collision) {
		if (collision.gameObject.tag == "Player") {
			Instantiate(foodExplosion, transform.position, Quaternion.identity);
			Instantiate(bloodSplashes[Random.Range(0, bloodSplashes.Length)], transform.position, Quaternion.identity);
			camRipple.RippleEffect();
			//transform.position = startPoint.transform.position;
			Destroy(gameObject);
		}
		if (collision.gameObject.tag == "End") {
			//transform.position = startPoint.transform.position;
			Destroy(gameObject);
		}
	}
}
