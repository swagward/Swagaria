using System;
using PixelWorlds.Runtime.Data;
using PixelWorlds.Runtime.World;
using UnityEngine;

namespace PixelWorlds.Runtime.Player
{
    public class TestMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private Vector3 velocity;
        public TerrainGenerator terrain;
        public Vector3Int mousePos;
        public Camera mainCam;

        private void Awake() => Init();
        private void Init()
        {
            //mainCam = Camera.main;
            velocity = transform.position;
            terrain = FindObjectOfType<TerrainGenerator>();
        }

        private void LateUpdate()
        {
            velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) *
                       (moveSpeed * Time.deltaTime);
            transform.position += velocity;
        }

        private void Update()
        {
            var worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.RoundToInt(worldPos.x - .5f);
            mousePos.y = Mathf.RoundToInt(worldPos.y - .5f);
            mousePos.z = 0;
            
            if(Input.GetMouseButton(0)) 
                terrain.RemoveTile(mousePos.x, mousePos.y, (int)TileLayer.Ground);
            else if(Input.GetMouseButton(1))
                terrain.PlaceTile(TileAtlas.Grass, mousePos.x, mousePos.y, true);
        }
    }
}