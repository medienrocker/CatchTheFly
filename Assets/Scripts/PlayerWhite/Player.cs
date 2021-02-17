using UnityEngine;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpForce = 10f;

	// States
	bool isAlive = true;

	// Caches and references
	Rigidbody2D myRigidbody2D;
	Animator myAnimator;
	Collider2D myCollider2D;

	// Messages and methods
	void Start() {
		myRigidbody2D = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myCollider2D = GetComponent<Collider2D>();
	}

	void Update() {
		Run();
		Jump();
		FlipPlayerSprite();
	}

	void Run() {
		float moveX = Input.GetAxisRaw("Horizontal");
		myRigidbody2D.velocity = new Vector2(moveX * runSpeed, myRigidbody2D.velocity.y);

		bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon; // means > 0
		myAnimator.SetBool("isRunning", hasHorizontalSpeed);
	}

	void Jump() {
		if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
		if (Input.GetButtonDown("Jump")) {
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
			myRigidbody2D.velocity += jumpVelocityToAdd;
		}
	}

	void FlipPlayerSprite() {
		bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon; // means > 0
		if (hasHorizontalSpeed) {
			transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), transform.localScale.y);
		}
	}
}
