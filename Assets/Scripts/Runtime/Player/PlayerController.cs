using UnityEngine;

namespace PixelWorlds.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Control")] 
        [SerializeField] private float speed;
        private Vector2Int _mousePos;

        [Header("Jump/Ground Detection")] 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float jumpForce;
        private const KeyCode JumpKey = KeyCode.Space;
        private Rigidbody2D _rb2;

        //Misc
        private bool _facingRight = true;
        private float _horizontal;
        private Camera _mainCam;

        public void Spawn(int x, int y)
        {
            transform.position = new Vector2(x, y + 3);
            _mainCam = Camera.main;

            _rb2 = GetComponent<Rigidbody2D>();
            _rb2.freezeRotation = true;
        }

        private void Update()
        {
            if (PauseControl.IsPaused) return;

            _horizontal = Input.GetAxisRaw("Horizontal");
            
            //Jumping
            //Debug.Log(IsGrounded());
            if (Input.GetKeyDown(JumpKey) && IsGrounded())
                _rb2.velocity = new Vector2(_rb2.velocity.x, jumpForce);
            if (Input.GetKeyUp(JumpKey) && _rb2.velocity.y > 0)
                _rb2.velocity = new Vector2(_rb2.velocity.x, _rb2.velocity.y * .5f);

            //Convert mouse position to screen space to interact with tiles
            var worldPos = (Vector2)_mainCam.ScreenToWorldPoint(Input.mousePosition);
            _mousePos.x = Mathf.RoundToInt(worldPos.x - .5f);
            _mousePos.y = Mathf.RoundToInt(worldPos.y - .5f);
        }

        private void FixedUpdate()
            => _rb2.velocity = new Vector2(_horizontal * speed, _rb2.velocity.y);

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
        }
        
        private void Flip()
        {
            _facingRight = !_facingRight;
            var playerScale = transform.localScale;
            playerScale *= -1;
            transform.localScale = playerScale;
        }
    }
}
