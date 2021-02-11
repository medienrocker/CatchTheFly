using UnityEngine;

public class EnemyCrapController : MonoBehaviour {
	Rigidbody2D rb2D;

	[SerializeField] float moveSpeed;

	[SerializeField] float timeBetweenMove;
	float timeBetweenMoveCounter;

	[SerializeField] float timeToMove;
	float timeToMoveCounter;

	bool isMoving;

	Vector3 moveDirection;




	void Start() {
		rb2D = GetComponent<Rigidbody2D>();

		timeBetweenMoveCounter = Random.Range(0f, timeBetweenMove);
		// or timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

		timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);


	}

	void Update() {
		// MoveEnemy(); deactivated because "ChasingTarget-Script" is attached
	}

	void MoveEnemy() {
		if (isMoving) {
			timeToMoveCounter -= Time.deltaTime;

			rb2D.velocity = moveDirection;
			if (timeToMoveCounter < 0f) {
				isMoving = false;
				//timeBetweenMoveCounter = timeBetweenMove;
				timeBetweenMoveCounter = Random.Range(0f, timeBetweenMove);
			}
		}
		else {
			timeBetweenMoveCounter -= Time.deltaTime;
			rb2D.velocity = Vector2.zero;
			if (timeBetweenMoveCounter < 0f) {
				isMoving = true;
				//timeToMoveCounter = timeToMove;
				timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

				moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
			}
		}
	}
}
