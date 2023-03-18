using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.World
{
    [RequireComponent(typeof(Grid))]
    public class TerrainGenerator : MonoBehaviour
    {
        private Tilemap[] _tilemaps;

        private void Awake() => Init();
        private void Init()
        {
            TerrainConfig.Init();
            
            _tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (var tilemap in _tilemaps)
                tilemap.ClearAllTiles();
        }
    }
}
