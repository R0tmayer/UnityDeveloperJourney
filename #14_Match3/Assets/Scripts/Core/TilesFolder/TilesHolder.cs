using Core.Board;

namespace Core.TilesFolder
{
    public class TilesHolder
    {
        public readonly Tile[,] Tiles;

        public TilesHolder(int size)
        {
            Tiles = new Tile[size,size];
        }
    }
}