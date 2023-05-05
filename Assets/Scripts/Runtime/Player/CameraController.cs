using UnityEngine;
using TerrariaClone.Runtime.Terrain;

namespace TerrariaClone.Runtime.Player
{
    public class CameraController : MonoBehaviour
    {
        private Camera _mainCam;
        private Transform _player;

        [SerializeField] private float orthoSize;
        [SerializeField] private float smoothTime;
        [SerializeField] private float cameraZoom;

        private void Start()
            => _mainCam = GetComponent<Camera>();

        private void Update()
        {
            _mainCam.orthographicSize = Mathf.Clamp(_mainCam.orthographicSize, 20, 60);
            _mainCam.orthographicSize = orthoSize;
            
        }


        public void SpawnCamera(int x, int y)
        {
            _player = FindObjectOfType<PlayerController>().transform;
            this.transform.position = new Vector3(x, y, -10);
        }

        private void FixedUpdate()
        {
            var position = this.transform.position;

            //move camera with player with added offset
            position.x = Mathf.Lerp(position.x, _player.transform.position.x, smoothTime * Time.deltaTime);
            position.y = Mathf.Lerp(position.y, _player.transform.position.y + 1.5f, smoothTime * Time.deltaTime);
            position.z = -10;

            //clamp the camera so it doesnt show outside boundaries
            position.x = Mathf.Clamp(position.x, 0 + (cameraZoom * orthoSize), 
                                TerrainConfig.Settings.worldSize.x - (cameraZoom * orthoSize));

            this.transform.position = position;
        }
    }
}
