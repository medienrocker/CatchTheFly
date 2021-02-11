using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb2D;
	public Transform groundCheckPoint1, groundCheckPoint2;
	public LayerMask whatIsGround;
	bool isGrounded;

	public Animator anim;
	public SpriteRenderer spriteRenderer;

	float inputHorizontal;

	public float moveSpeed;
	public float jumpForce;
	bool jumpButtonDown;
	bool jumpButtonUp;

	public float hangTime = .2f;
	float hangTimeCounter;

	public float jumpBufferLength = .1f;
	float jumpBufferCounter;


	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update() {
		inputHorizontal = Input.GetAxisRaw("Horizontal");

		jumpButtonDown = Input.GetButtonDown("Jump");
		jumpButtonUp = Input.GetButtonUp("Jump");

		// PlayerFlipSprite();
		PlayerMove();
		CheckIfGrounded();
		PlayerJump();
	}

	void FixedUpdate() {
	}

	private void PlayerMove() {
		// move Player horizontally
		rb2D.velocity = new Vector2(inputHorizontal * moveSpeed, rb2D.velocity.y);
	}

	private void CheckIfGrounded() {
		// check if Player on ground
		isGrounded = Physics2D.OverlapCircle(groundCheckPoint1.position, .1f, whatIsGround) ||
			Physics2D.OverlapCircle(groundCheckPoint2.position, .1f, whatIsGround);
	}

	void PlayerJump() {
		// manage hang time (coyote time) - can Jump shortly after leaving Plattform
		if (isGrounded) {
			hangTimeCounter = hangTime;
		}
		else {
			hangTimeCounter -= Time.deltaTime;
		}

		// manage jump buffer
		if (jumpButtonDown) {
			jumpBufferCounter = jumpBufferLength;
		}
		else {
			jumpBufferCounter -= Time.deltaTime;
		}

		// jump in the air
		if (jumpBufferCounter > 0f && hangTimeCounter > 0f) {
			rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
			jumpBufferCounter = 0f;
		}
		if (jumpButtonUp && rb2D.velocity.y > 0) {
			rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * .3f);
		}
	}

	void PlayerFlipSprite() {
		// flip Player
		if (inputHorizontal > 0f) {
			spriteRenderer.flipX = false;
		}
		else if (inputHorizontal < 0f) {
			spriteRenderer.flipX = true;
		}
	}
}
