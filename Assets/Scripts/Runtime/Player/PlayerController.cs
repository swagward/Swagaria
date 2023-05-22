using TerrariaClone.Runtime.Data;
using TerrariaClone.Runtime.Terrain;
using UnityEngine;

namespace TerrariaClone.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController Instance;
        public TerrainGenerator terrain;
        public Inventory inventory;
        public ItemClass itemToUse;

        [Header("Player Control")]
        [SerializeField] private float speed;
        public Vector2Int mousePos;
        public HealthManager health;
        public int reach;
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
        private Animator _anim;
        private Camera _mainCam;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            if (Instance is null)
                Instance = this;
            else Destroy(gameObject);
            
            _rb2 = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        public void Spawn(int x, int y)
        {
            GameManager.Initialized = true;
            transform.position = new Vector2(x + .5f, y + 3);

            terrain = FindObjectOfType<TerrainGenerator>();
            inventory = FindObjectOfType<Inventory>();
            GameManager.ParallaxShowing = true;

            _mainCam = Camera.main;
            var playerCam = FindObjectOfType<CameraController>();
            playerCam.SpawnCamera(x, y);
            audioPlayer = playerCam.GetComponent<AudioSource>();

            inventory.Init();
        }

        private void Update()
        {
            if (!GameManager.Initialized) return;
            if (PauseControl.IsPaused) return;

            _horizontal = Input.GetAxisRaw("Horizontal");

            var playerTrans = this.transform.position;
            playerTrans.x = Mathf.Clamp(this.transform.position.x, .5f, TerrainConfig.Settings.worldSize.x - .5f);
            playerTrans.y = Mathf.Clamp(this.transform.position.y, 2, TerrainConfig.Settings.worldSize.y - 2);
            this.transform.position = playerTrans;

            //Jumping
            if (Input.GetKeyDown(JumpKey) && IsGrounded())
                _rb2.velocity = new Vector2(_rb2.velocity.x, jumpForce);
               
            else if (Input.GetKeyUp(JumpKey) && _rb2.velocity.y > 0)
                _rb2.velocity = new Vector2(_rb2.velocity.x, _rb2.velocity.y * .5f);
                
            //Flip player sprite
            if (_facingRight && _horizontal < 0 || !_facingRight && _horizontal > 0)
                Flip();

            //Convert mouse position to screen space to interact with tiles
            var worldPos = (Vector2)_mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.RoundToInt(worldPos.x - .5f);
            mousePos.y = Mathf.RoundToInt(worldPos.y - .5f);
            
            //Set animations based on certain states
            if(Mathf.Abs(_horizontal) > .1f && IsGrounded())
                _anim.SetTrigger("Walking");
            else if (!IsGrounded())
                _anim.SetTrigger("Jumping");
            else if (Mathf.Abs(_horizontal) is 0)
                _anim.SetTrigger("Idling");

            //World manipulation
            //Remove if statements once inventory added
            if (Input.GetMouseButton(0))
                inventory.SelectedItem?.Use(this);
        }

        private void FixedUpdate()
            => _rb2.velocity = new Vector2(_horizontal * speed, _rb2.velocity.y);

        private bool IsGrounded()
            => Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
        
        private void Flip()
        {
            _facingRight = !_facingRight;
            var playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }
}
