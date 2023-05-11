using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TerrariaClone.Runtime.Data
{
    public static class WorldData
    {
        public static Tilemap[] Tilemaps;
        private static TileCell[,,] _worldData;

        public static void Init(Vector2Int worldSize)
            => _worldData = new TileCell[worldSize.x, worldSize.y, Tilemaps.Length];

        public class TileCell
        {
            public readonly TileClass tile;
            private readonly ushort _x;
            private readonly ushort _y;
            private readonly ushort _z;

            public TileCell([CanBeNull] TileClass tile, int x, int y, int z)
            {
                this.tile = tile;
                _x = Convert.ToUInt16(x);
                _y = Convert.ToUInt16(y);
                _z = Convert.ToUInt16(z);
            }

            public static bool operator ==(TileCell a, TileClass b) => a.tile == b;
            public static bool operator !=(TileCell a, TileClass b) => a.tile != b;
            private bool Equals(TileCell other) => Equals(tile, other.tile);
            public override int GetHashCode() => tile != null ? tile.GetHashCode() : 0;
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((TileCell)obj);
            }
        }

        /// <summary>
        /// Sets/Removes data from the Tilemap array and the TileData array.
        /// </summary>
        /// <param name="tile">Set current to tile to any valid tile in Atlas</param>
        /// <param name="x">X position on a tilemap</param>
        /// <param name="y">Y position on a tilemap</param>
        /// <param name="z">Only use if you're removing a tile</param>
        public static void SetTile([CanBeNull] TileClass tile, int x, int y, int z = -1)
        {
            // By giving Z a default value of -1 it means the SetTile function can be called without the Z being specified
            // If its not used it'll the tiles Z coordinate instead
            // If the tile is null and the Z position is not specified then it cant get a tilemap to edit
            // So if both aren't specified throw out an error, however if a tile is specified then set Z to that tiles layer
            
            if (z is -1)
            {
                if (tile is null) 
                    throw new Exception("Z position required when setting tile to null");
                z = (int)tile.tileLayer;
            }
            
            // Automatically add to tilemap and set the tile in specified tilemap
            _worldData[x, y, z] = new TileCell(tile, x, y, z);
            Tilemaps[z].SetTile(new Vector3Int(x, y, 0), tile is null ? null : tile.tile);
        }
        
        /// <summary>
        /// Returns either the specified TileLayer's tile or the first tile to show if Z is set to null.
        /// </summary>
        /// <param name="x">X position on a tilemap</param>
        /// <param name="y">Y position on a tilemap</param>
        /// <param name="z">Specific TileLayer in world</param>
        /// <returns>Returns tile on specific Z layer</returns>
        public static TileClass GetTile(int x, int y, int z = -1)
        {   // Out of bounds
            if (x < 0 || x >= _worldData.GetLength(0)) return null;
            if (y < 0 || y >= _worldData.GetLength(1)) return null;

            // Returns the first tile it gets on the specific x/y coordinate
            if (z is -1)
            {
                //i dont understand this
                //made when drunk
                for (var i = 0; i < Tilemaps.Length; i++)
                    if (_worldData[x, y, i] is not null)
                        return _worldData[x, y, i].tile;
            }
    
            return _worldData[x, y, z] is null ? null : _worldData[x, y, z].tile;
        }
    }
}