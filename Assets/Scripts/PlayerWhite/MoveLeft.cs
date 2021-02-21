using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLeft : MonoBehaviour {

	[SerializeField] float moveSpeed = 5f;
	//[SerializeField] Transform startPoint;
	Rigidbody2D myRigidbody2D;

	void Start() {
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update() {
		//transform.Translate(Vector3.left * moveSpedd * Time.deltaTime);
		myRigidbody2D.velocity = new Vector2(-moveSpeed, myRigidbody2D.velocity.y);
	}

	//void OnCollisionEnter2D(Collision2D other) {
	//	if (other.gameObject.tag == "End") {
	//		transform.position = startPoint.transform.position;
	//	}
	//}
}
