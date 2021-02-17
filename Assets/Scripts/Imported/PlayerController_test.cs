using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2D;
	Animator anim;
	public ParticleSystem attackFlame;
	public Transform flamePoint;
	public Transform groundCheckPoint1, groundCheckPoint2;
	public LayerMask whatIsGround;

	bool isGrounded;

	[SerializeField]
	float moveSpeed = 10f;
	public float jumpForce;

	float moveX;
	float moveY;

	bool isMoving;
	public Vector2 lastMove;

	static bool playerExists;

	bool isAttacking;
	[SerializeField] float attackTime;
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
		flamePoint = GetComponent<Transform>();
		attackFlame.Stop();

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

		isGrounded = Physics2D.OverlapCircle(groundCheckPoint1.position, .1f, whatIsGround) ||
			Physics2D.OverlapCircle(groundCheckPoint2.position, .1f, whatIsGround);

		PlayerJump();

		if (!isAttacking) {
			PlayerMove();

			if (Input.GetKeyDown(KeyCode.Space) && timeBetweenAttacksCounter <= 0) {
				isAttacking = true;
				attackTimeCounter = attackTime;
				rb2D.velocity = Vector2.zero;
				anim.SetBool("isAttacking", true);
				attackFlame.Play();

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
			anim.SetBool("isAttacking", false);
			attackFlame.Stop();
		}



		anim.SetFloat("MoveX", moveX);
		//anim.SetFloat("MoveY", moveY);
		anim.SetBool("isMoving", isMoving);
		anim.SetFloat("LastMoveX", lastMove.x);
		//anim.SetFloat("LastMoveY", lastMove.y);

		//print(timeBetweenAttacksCounter);
	}

	void PlayerJump() {
		if (Input.GetButtonDown("Jump")) {
			rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
		}
	}

	void PlayerMove() {
		isMoving = false;

		moveX = Input.GetAxisRaw("Horizontal");
		//moveY = Input.GetAxisRaw("Vertical");

		if (moveX > 0.5 || moveX < -0.5) {
			isMoving = true;
			rb2D.velocity = new Vector2(moveX * moveSpeed, 0f);
			lastMove = new Vector2(moveX, 0f);
		}

		if (moveX < 0.5 && moveX > -0.5) {
			rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
		}

		// Y-Movement
		//if (moveY > 0.5 || moveY < -0.5) {
		//	isMoving = true;
		//	rb2D.velocity = new Vector2(0f, moveY * moveSpeed);
		//	lastMove = new Vector2(0f, moveY);
		//}

		//if (moveY < 0.5 && moveY > -0.5) {
		//	rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
		//}
	}
}
