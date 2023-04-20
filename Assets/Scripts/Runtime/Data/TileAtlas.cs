using UnityEngine;

namespace PixelWorlds.Runtime.Data
{
    public static class TileAtlas
    {
        //Generic Tiles
        public static readonly TileClass Grass = Resources.Load<TileClass>("Tiles/Blocks/Grass");
        public static readonly TileClass Dirt = Resources.Load<TileClass>("Tiles/Blocks/Dirt");
        public static readonly TileClass Stone = Resources.Load<TileClass>("Tiles/Blocks/Stone");
        public static readonly TileClass OakWood = Resources.Load<TileClass>("Tiles/Blocks/OakWood");
        public static readonly TileClass StoneBricks = Resources.Load<TileClass>("Tiles/Blocks/StoneBricks");
        
        //Ores
        public static readonly OreTileClass CopperOre = Resources.Load<OreTileClass>("Tiles/Blocks/CopperOre");
        public static readonly OreTileClass IronOre = Resources.Load<OreTileClass>("Tiles/Blocks/IronOre");
        public static readonly OreTileClass SilverOre = Resources.Load<OreTileClass>("Tiles/Blocks/SilverOre");
        public static readonly OreTileClass GoldOre = Resources.Load<OreTileClass>("Tiles/Blocks/GoldOre");

        //Addons
        public static readonly TileClass Vine = Resources.Load<TileClass>("Tiles/Addons/Vine");
        public static readonly TileClass OakTree = Resources.Load<TileClass>("Tiles/Addons/OakTree");
        public static readonly TileClass OakBranch = Resources.Load<TileClass>("Tiles/Addons/OakBranch");
        public static readonly TileClass Flower = Resources.Load<TileClass>("Tiles/Addons/Flower");
        
        //Backgrounds
        public static readonly TileClass StoneWall = Resources.Load<TileClass>("Tiles/Walls/StoneBG");
        public static readonly TileClass DirtWall = Resources.Load<TileClass>("Tiles/Walls/DirtBG");
        public static readonly TileClass OakWoodWall = Resources.Load<TileClass>("Tiles/Walls/OakWoodBG");
        
        //Liquids
        public static readonly LiquidTileClass Water = Resources.Load<LiquidTileClass>("Tiles/Liquids/Water");
        public static readonly LiquidTileClass Lava = Resources.Load<LiquidTileClass>("Tiles/Liquids/Lava");
        public static readonly LiquidTileClass Honey = Resources.Load<LiquidTileClass>("Tiles/Liquids/Honey");
        public static readonly LiquidTileClass Shimmer = Resources.Load<LiquidTileClass>("Tiles/Liquids/Shimmer"); //cum
    }
}