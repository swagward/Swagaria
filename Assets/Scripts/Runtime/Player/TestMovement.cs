using System;
using PixelWorlds.Runtime.Data;
using PixelWorlds.Runtime.World;
using UnityEngine;

namespace PixelWorlds.Runtime.Player
{
    public class TestMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private Vector3 _velocity;
        public TerrainGenerator terrain;
        public Vector3Int mousePos;
        public Camera mainCam;

        private void Awake() => Init();
        private void Init()
        {
            mainCam = Camera.main;
            _velocity = transform.position;
            terrain = FindObjectOfType<TerrainGenerator>();
        }

        private void LateUpdate()
        {
            if (PauseControl.IsPaused) return;
            
            _velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) *
                       (moveSpeed * Time.deltaTime);
            transform.position += _velocity;
        }

        private void Update()
        {
            if (PauseControl.IsPaused) return;
            
            var worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.RoundToInt(worldPos.x - .5f);
            mousePos.y = Mathf.RoundToInt(worldPos.y - .5f);
            mousePos.z = 0;
            
            if(Input.GetMouseButton(0)) 
                terrain.RemoveTile(mousePos.x, mousePos.y, (int)TileLayer.Ground);
            else if(Input.GetMouseButton(1) && WorldData.GetTile(mousePos.x, mousePos.y, 1) is null) //MAKE SUR WATER CANT BE PLACED IN SOLID BLOCKS
                terrain.PlaceTile(TileAtlas.Water, mousePos.x, mousePos.y, true);
        }
    }
}