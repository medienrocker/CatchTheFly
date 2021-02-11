using UnityEngine;

public class VillagerMovement : MonoBehaviour {
	[SerializeField] float moveSpeed;
	Rigidbody2D rb2D;

	bool isWalking;

	[SerializeField] float walkTime;
	float walkCounter;
	[SerializeField] float waitTime;
	float waitCounter;

	int walkDirection;

	[SerializeField] Collider2D walkZone;
	Vector2 minWalkPoint;
	Vector2 maxWalkPoint;

	bool hasWalkZone;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();

		waitCounter = waitTime;
		walkCounter = walkTime;

		ChooseDirection();

		if (walkZone != null) {
			minWalkPoint = walkZone.bounds.min;
			maxWalkPoint = walkZone.bounds.max;
			hasWalkZone = true;
		}
	}

	void Update() {
		WalkAndWait();
	}

	void WalkAndWait() {
		if (isWalking) {
			walkCounter -= Time.deltaTime;

			switch (walkDirection) {
				case 0:
					rb2D.velocity = new Vector2(0, moveSpeed);
					if (hasWalkZone && transform.position.y > maxWalkPoint.y) {
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 1:
					rb2D.velocity = new Vector2(moveSpeed, 0);

					if (hasWalkZone && transform.position.x > maxWalkPoint.x) {
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 2:
					rb2D.velocity = new Vector2(0, -moveSpeed);

					if (hasWalkZone && transform.position.y < minWalkPoint.y) {
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
				case 3:
					rb2D.velocity = new Vector2(-moveSpeed, 0);
					if (hasWalkZone && transform.position.x < minWalkPoint.x) {
						isWalking = false;
						waitCounter = waitTime;
					}
					break;
			}

			if (walkCounter < 0) {
				isWalking = false;
				waitCounter = waitTime;
			}
		}
		else {
			waitCounter -= Time.deltaTime;

			rb2D.velocity = Vector2.zero;

			if (waitCounter < 0) {
				ChooseDirection();
			}
		}
	}

	void ChooseDirection() {
		walkDirection = Random.Range(0, 4);
		isWalking = true;
		walkCounter = walkTime;
	}
}
