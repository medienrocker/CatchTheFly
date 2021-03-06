using UnityEngine;

public class MoveLeft : MonoBehaviour {

	[SerializeField] float moveSpeed = 5f;

	void Start() {
	}

	void Update() {
		transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
	}
}
