using UnityEngine;

public class ChasingTarget: MonoBehaviour {
	Transform target;
	[SerializeField] float moveSpeed;
	[SerializeField] string targetTag;

	void Start() {
		target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
	}

	void Update() {
		transform.position = Vector2.MoveTowards(
			transform.position, // current position
			target.position, // move to this position
			moveSpeed * Time.deltaTime); // with this speed
			//Random.Range(0.1f, moveSpeed) * Time.deltaTime); // with this speed
	}
}
