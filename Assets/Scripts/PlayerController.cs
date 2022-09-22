using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    private const string JumpKey = "space";
    private const string LeftKey = "left";
    private const string RightKey = "right";
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private bool _canJump;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    [field: SerializeField] public bool UsePhysics { get; set; } = true;
    [field: SerializeField] public int Speed { get; set; } = 125;
    [field: SerializeField] public float MovementForce { get; set; } = 1250;
    [field: SerializeField] public float JumpForce { get; set; } = 1000;
    [field: SerializeField] public int JumpHeight { get; set; } = 500;
    [field: SerializeField] public int JumpThreshold { get; set; } = 100;
    [field: SerializeField] public float LeftLimit { get; set; } = -854;
    [field: SerializeField] public float RightLimit { get; set; } = 854;

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        if (UsePhysics) return;

        Destroy(gameObject.GetComponent<Rigidbody2D>());
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        Movement();

        if (UsePhysics)
            PhysicsJump();
        else
            ManualJump();
    }

    private void Movement()
    {
        if (Input.GetKey(LeftKey))
        {
            if (UsePhysics)
                _rigidbody2D.AddForce(new Vector2(-MovementForce * Time.deltaTime, 0));
            gameObject.transform.Translate(Speed * -1 * Time.deltaTime, 0, 0);

            _animator.SetBool(IsMoving, true);
            _spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(RightKey))
        {
            if (UsePhysics)
                _rigidbody2D.AddForce(new Vector2(MovementForce * Time.deltaTime, 0));
            gameObject.transform.Translate(Speed * Time.deltaTime, 0, 0);

            _animator.SetBool(IsMoving, true);
            _spriteRenderer.flipX = false;
        }
        else
        {
            _animator.SetBool(IsMoving, false);
        }
    }

    private void ManualJump()
    {
        if (gameObject.transform.position.y <= 0)
            _canJump = true;

        if (Input.GetKey(JumpKey) && _canJump && gameObject.transform.position.y < JumpThreshold)
        {
            gameObject.transform.Translate(0, JumpHeight * Time.deltaTime, 0);
        }
        else
        {
            _canJump = false;

            if (gameObject.transform.position.y > 0)
                gameObject.transform.Translate(0, -JumpHeight * Time.deltaTime, 0);
        }
    }

    private void PhysicsJump()
    {
        if (!Input.GetKeyDown(JumpKey) || !_canJump) return;
        {
            _canJump = false;
            _rigidbody2D.AddForce(new Vector2(0, JumpForce));
        }
    }

    // ReSharper disable once UnusedMember.Local
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("grass"))
            _canJump = true;
    }

    // ReSharper disable once UnusedMember.Local
    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        gameObject.transform.position = new Vector3(
            Mathf.Clamp(currentPosition.x, LeftLimit, RightLimit),
            currentPosition.y);
    }
}