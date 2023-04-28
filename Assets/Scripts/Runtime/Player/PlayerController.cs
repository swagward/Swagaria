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
        private Animator _anim;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            if (Instance is null)
                Instance = this;
            else Destroy(gameObject);
            
            _mainCam = Camera.main;
            _rb2 = GetComponent<Rigidbody2D>();

            _anim = GetComponent<Animator>();
        }



        public void Spawn(int x, int y)
        {
            GameManager.Initialized = true;
            transform.position = new Vector2(x, y + 3);

            terrain = FindObjectOfType<TerrainGenerator>();
        }

        private void Update()
        {
            Debug.Log(IsGrounded());


            //if (!GameManager.Initialized) return;
            if (PauseControl.IsPaused) return;

            _horizontal = Input.GetAxisRaw("Horizontal");

            //Jumping
            if (Input.GetKeyDown(JumpKey) && IsGrounded())
                _rb2.velocity = new Vector2(_rb2.velocity.x, jumpForce);
               
            if (Input.GetKeyUp(JumpKey) && _rb2.velocity.y > 0)
                _rb2.velocity = new Vector2(_rb2.velocity.x, _rb2.velocity.y * .5f);
                
            

            //Flip player sprite
            if (_facingRight && _horizontal < 0 || !_facingRight && _horizontal > 0)
                Flip();

            //Convert mouse position to screen space to interact with tiles
            var worldPos = (Vector2)_mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.RoundToInt(worldPos.x - .5f);
            mousePos.y = Mathf.RoundToInt(worldPos.y - .5f);

            //Animations
            if (Mathf.Abs(_horizontal) > .1f && IsGrounded()) //if moving and on ground
                _anim.SetBool("Move", true);
            else if ((Mathf.Abs(_horizontal) > .1f && !IsGrounded()) || !IsGrounded()) //if moving and in air OR just in air
                _anim.SetBool("Jump", true);
            else
            {
                _anim.SetBool("Move", false);
                _anim.SetBool("Jump", false);
            }

            //World manipulation
            //Remove if statements once inventory added
            /*if (Input.GetMouseButtonDown(1))
                terrain.PlaceTile(TileAtlas.Torch, mousePos.x, mousePos.y, false, true);
            else if (Input.GetMouseButton(0))
                terrain.RemoveTile(mousePos.x, mousePos.y, 1, true);*/

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
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }
}
