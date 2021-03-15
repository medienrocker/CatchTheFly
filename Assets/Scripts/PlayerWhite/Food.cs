using UnityEngine;

public class Food : MonoBehaviour {

	//[SerializeField] Transform startPoint;
	[SerializeField] GameObject foodExplosion;
	[SerializeField] GameObject[] bloodSplashes;
	[SerializeField] float YOffset = 0f;

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
			//var splashPosition = new Vector3(transform.position.x, transform.position.y + YOffset, transform.position.z);
			//Instantiate(bloodSplashes[Random.Range(0, bloodSplashes.Length)], splashPosition, Quaternion.identity);
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
