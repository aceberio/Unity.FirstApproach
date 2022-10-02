using UnityEngine;
using VContainer;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private bool _canJump;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private CharacterSettings _settings;

        [Inject]
        public void Construct(CharacterSettings settings) => _settings = settings;

        private void Awake()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _animator = gameObject.GetComponent<Animator>();
        }

        private void Start()
        {
            if (_settings.UsePhysics) return;

            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        private void Update()
        {
            Movement();

            if (_settings.UsePhysics)
                PhysicsJump();
            else
                ManualJump();
        }

        private void Movement()
        {
            if (Input.GetKey(_settings.LeftKey))
            {
                if (_settings.UsePhysics)
                    _rigidbody2D.AddForce(new Vector2(-_settings.MovementForce * Time.deltaTime, 0));
                gameObject.transform.Translate(_settings.Speed * -1 * Time.deltaTime, 0, 0);

                _animator.SetBool(IsMoving, true);
                _spriteRenderer.flipX = true;
            }
            else if (Input.GetKey(_settings.RightKey))
            {
                if (_settings.UsePhysics)
                    _rigidbody2D.AddForce(new Vector2(_settings.MovementForce * Time.deltaTime, 0));
                gameObject.transform.Translate(_settings.Speed * Time.deltaTime, 0, 0);

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

            if (Input.GetKey(_settings.JumpKey) && _canJump &&
                gameObject.transform.position.y < _settings.JumpThreshold)
            {
                gameObject.transform.Translate(0, _settings.JumpHeight * Time.deltaTime, 0);
            }
            else
            {
                _canJump = false;

                if (gameObject.transform.position.y > 0)
                    gameObject.transform.Translate(0, -_settings.JumpHeight * Time.deltaTime, 0);
            }
        }

        private void PhysicsJump()
        {
            if (!Input.GetKeyDown(_settings.JumpKey) || !_canJump) return;
            {
                _canJump = false;
                _rigidbody2D.AddForce(new Vector2(0, _settings.JumpForce));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("grass"))
                _canJump = true;
        }

        private void LateUpdate()
        {
            Vector3 currentPosition = transform.position;
            gameObject.transform.position = new Vector3(
                Mathf.Clamp(currentPosition.x, _settings.LeftLimit, _settings.RightLimit),
                currentPosition.y);
        }
    }
}