using UnityEngine;

namespace PixelWorlds.Runtime.Data
{
    public static class TileAtlas
    {
        //Generic Tiles
        public static readonly TileClass Grass = Resources.Load<TileClass>("Tiles/Blocks/Grass");
        public static readonly TileClass Dirt = Resources.Load<TileClass>("Tiles/Blocks/Dirt");
        public static readonly TileClass Stone = Resources.Load<TileClass>("Tiles/Blocks/Stone");

        //Addons
        public static readonly TileClass Vine = Resources.Load<TileClass>("Tiles/Addons/Vine");
        public static readonly TileClass OakTree = Resources.Load<TileClass>("Tiles/Addons/OakTree");
        public static readonly TileClass OakBranch = Resources.Load<TileClass>("Tiles/Addons/OakBranch");
        public static readonly TileClass Flower = Resources.Load<TileClass>("Tiles/Addons/Flower");
        
        
    }
}
