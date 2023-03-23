using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.World
{
    [CreateAssetMenu(fileName = "newRuleTile", menuName = "Pixel Worlds/Rule Tile")]
    public class ScriptedRuleTile : RuleTile<ScriptedRuleTile.Neighbor>
    {
        public bool checkSelf;
        
        [field: SerializeField] public TileBase[] AlwaysConnect { get; private set; }
        [field: SerializeField] public TileBase[] SometimesConnect { get; private set; }
        [field: SerializeField] public TileBase[] RareStates { get; private set; }
        [field: SerializeField] public TileBase[] NeverConnect { get; private set; }

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            public const int Any = 3;
            public const int AlwaysConnect = 4;
            public const int SometimesConnect = 5;
            public const int RareState = 6;
            public const int NeverConnect = 7;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.This: return Check_This(tile);
                case Neighbor.NotThis: return Check_NotThis(tile);
                case Neighbor.Any: return Check_Any(tile);
                case Neighbor.AlwaysConnect: return AlwaysConnectToTile(tile);
                case Neighbor.SometimesConnect: return SometimesConnectToTile(tile);
                case Neighbor.RareState: return ConnectRareStates(tile);
                case Neighbor.NeverConnect: return NeverConnectToTile(tile);
            }
            return base.RuleMatch(neighbor, tile);
        }
    
        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            //Debug.Log($"{tilemap} + {position}");
            
            tilemap.RefreshTile(position);
            tilemap.RefreshTile(position + Vector3Int.down);
            tilemap.RefreshTile(position + Vector3Int.up);
            tilemap.RefreshTile(position + Vector3Int.left);
            tilemap.RefreshTile(position + Vector3Int.right);
            tilemap.RefreshTile(position + Vector3Int.right + Vector3Int.up);
            tilemap.RefreshTile(position + Vector3Int.right + Vector3Int.down);
            tilemap.RefreshTile(position + Vector3Int.left + Vector3Int.up);
            tilemap.RefreshTile(position + Vector3Int.left + Vector3Int.down);
        }

        private bool Check_This(TileBase tile)
        {
            return tile == this;
        }

        private bool Check_NotThis(TileBase tile)
        {
            return tile != this;
        }

        private bool Check_Any(TileBase tile)
        {
            if (checkSelf)
                return tile != null;
            else
                return tile != null && tile != this;
        }

        private bool AlwaysConnectToTile(TileBase tile)
        {
            return AlwaysConnect.Contains(tile);
        }
        
        private bool SometimesConnectToTile(TileBase tile)
        {
            return SometimesConnect.Contains(tile);
        }
        
        private bool ConnectRareStates(TileBase tile)
        {
            return RareStates.Contains(tile);
        }
        
        private bool NeverConnectToTile(TileBase tile)
        {
            return NeverConnect.Contains(tile);
        }
    }
}