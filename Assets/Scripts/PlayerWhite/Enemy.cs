using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] Transform startPoint;
	[SerializeField] GameObject contactExplosion;

	RipplePostProcessor camRipple;

	void Start() {
		camRipple = Camera.main.GetComponent<RipplePostProcessor>();
	}

	void Update() {

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
			Instantiate(contactExplosion, transform.position, Quaternion.identity);
			camRipple.RippleEffect();
			transform.position = startPoint.transform.position;
		}
		if (other.tag == "End") {
			transform.position = startPoint.transform.position;
		}
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
