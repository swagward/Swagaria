using TerrariaClone.Runtime.Data;
using TerrariaClone.Runtime.Terrain;
using UnityEngine;

namespace TerrariaClone.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController Instance;
        public TerrainGenerator terrain;
        public ItemClass itemToUse;

        [Header("Player Control")]
        [SerializeField] private float speed;
        public Vector2Int mousePos;
        public int health;
        public AudioSource audioPlayer;

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

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            if (Instance is null)
                Instance = this;
            else Destroy(gameObject);
            
            _mainCam = Camera.main;
            _rb2 = GetComponent<Rigidbody2D>();
        }

        public void Spawn(int x, int y)
        {
            GameManager.Initialized = true;
            transform.position = new Vector2(x, y + 3);

            terrain = FindObjectOfType<TerrainGenerator>();
        }

        private void Update()
        {
            if (!GameManager.Initialized) return;
            if (PauseControl.IsPaused) return;

            _horizontal = Input.GetAxisRaw("Horizontal");
            
            //Jumping
            if (Input.GetKeyDown(JumpKey) && IsGrounded())
                _rb2.velocity = new Vector2(_rb2.velocity.x, jumpForce);
            if (Input.GetKeyUp(JumpKey) && _rb2.velocity.y > 0)
                _rb2.velocity = new Vector2(_rb2.velocity.x, _rb2.velocity.y * .5f);

            //Convert mouse position to screen space to interact with tiles
            var worldPos = (Vector2)_mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.RoundToInt(worldPos.x - .5f);
            mousePos.y = Mathf.RoundToInt(worldPos.y - .5f);

            if (Input.GetMouseButtonDown(1))
                terrain.RemoveTile(mousePos.x, mousePos.y, 1, true);
            //itemToUse.Use(this);

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
