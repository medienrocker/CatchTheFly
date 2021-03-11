using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb2D;
	public Transform groundCheckPoint1, groundCheckPoint2;
	public LayerMask whatIsGround;
	bool isGrounded;

	public Animator anim;
	public SpriteRenderer spriteRenderer;

	float inputHorizontal;

	public float moveSpeed = 5f;
	public float jumpForce = 18f;
	bool jumpButtonDown;
	bool jumpButtonUp;

	public float hangTime = .2f;
	float hangTimeCounter;

	public float jumpBufferLength = .1f;
	float jumpBufferCounter; 
	bool isJumping;
	
	bool isMoving;
	public Vector2 lastMove;

	static bool playerExists;

	bool isAttacking;
	[SerializeField] float attackTime = .5f;
	float attackTimeCounter;
	[SerializeField] float timeBetweenAttacks = 1f;
	float timeBetweenAttacksCounter;

	public string startPoint;

	void Awake() {
		lastMove = new Vector2(0f, -1f); ;
	}


	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		if (!playerExists) {
			playerExists = true;
			DontDestroyOnLoad(transform.gameObject);
		}
		else {
			Destroy(gameObject);
		}

		attackTimeCounter = attackTime;
		timeBetweenAttacksCounter = timeBetweenAttacks;
	}

	void Update() {
		inputHorizontal = Input.GetAxisRaw("Horizontal");

		jumpButtonDown = Input.GetButtonDown("Jump");
		jumpButtonUp = Input.GetButtonUp("Jump");

		PlayerFlipSprite();
		CheckIfGrounded();

		if (!isAttacking) {
			PlayerMove();

			if (Input.GetKeyDown(KeyCode.Space) && timeBetweenAttacksCounter <= 0) {
				isAttacking = true;
				attackTimeCounter = attackTime;
				rb2D.velocity = Vector2.zero;
				//anim.SetBool("isAttacking", isAttacking);
				//attackFlame.Play();

			}
		}

		if (attackTimeCounter > 0f) {
			attackTimeCounter -= Time.deltaTime;
		}

		if (timeBetweenAttacksCounter > 0f) {
			timeBetweenAttacksCounter -= Time.deltaTime;
		}

		if (attackTimeCounter <= 0) {
			timeBetweenAttacksCounter = timeBetweenAttacks;
			isAttacking = false;
			//anim.SetBool("isAttacking", isAttacking);
			//attackFlame.Stop();
		}

		PlayerJump();

		anim.SetBool("isAttacking", isAttacking);
		anim.SetFloat("MoveX", inputHorizontal);
		//anim.SetFloat("MoveY", moveY);
		anim.SetBool("isMoving", isMoving);
		anim.SetBool("isGrounded", CheckIfGrounded());
		anim.SetBool("isJumping", CheckIfJumping());
		anim.SetFloat("LastMoveX", lastMove.x);
		anim.SetFloat("VerticalSpeed", rb2D.velocity.y);
		//anim.SetFloat("LastMoveY", lastMove.y);

		print(rb2D.velocity);
	}



	private void PlayerMove() {
		isMoving = false;

		// move Player horizontally
		if (inputHorizontal > 0.5 || inputHorizontal < -0.5) {
			isMoving = true;
			rb2D.velocity = new Vector2(inputHorizontal * moveSpeed, rb2D.velocity.y);
			lastMove = new Vector2(inputHorizontal, 0f);
		}

		if (inputHorizontal < 0.5 && inputHorizontal > -0.5) {
			rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
		}

		
		//rb2D.velocity = new Vector2(inputHorizontal * moveSpeed, rb2D.velocity.y);
	}

	bool CheckIfGrounded() {
		// check if Player on ground
		isGrounded = Physics2D.OverlapCircle(groundCheckPoint1.position, .1f, whatIsGround) ||
			Physics2D.OverlapCircle(groundCheckPoint2.position, .1f, whatIsGround);
		return isGrounded;
	}

	void PlayerJump() {
		// manage hang time (coyote time) - can Jump shortly after leaving Plattform
		if (CheckIfGrounded()) {
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

	bool CheckIfJumping() {
		isJumping = false;
		if (Input.GetButton("Jump") && !CheckIfGrounded()) {
			isJumping = true;
		}
		return isJumping;
	}

	void PlayerFlipSprite() {
		// flip Player
		if (inputHorizontal > 0f) {
			spriteRenderer.flipX = true;
		}
		else if (inputHorizontal < 0f) {
			spriteRenderer.flipX = false;
		}
	}
}
