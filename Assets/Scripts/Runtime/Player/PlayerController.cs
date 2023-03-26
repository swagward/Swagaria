using System;
using System.Runtime.CompilerServices;
using PixelWorlds.Runtime.Data;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

namespace PixelWorlds.Runtime.Player
{
    public class PlayerController : MonoBehaviour
    {
        public ItemClass item;
        
        
        [Header("Player Control")] 
        [SerializeField] private float speed;
        public Vector3Int mousePos { get; private set; }
        public int playerReach;

        [Header("Jump/Ground Detection")] 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float jumpForce;
        private const KeyCode JumpKey = KeyCode.Space;
        
        //Misc
        private Rigidbody2D _rb2;
        private bool _facingRight;
        private float _horizontal;
        private Camera _mainCam = Camera.main;
        
        public void Spawn(int x, int y)
        {
            transform.position = new Vector2(x, y + 3);

            _rb2 = GetComponent<Rigidbody2D>();
            _rb2.freezeRotation = true;
        }

        private void Update()
        {
            if (GameManager.IsPaused) return;

            _horizontal = Input.GetAxisRaw("Horizontal");
        }
    }
}
