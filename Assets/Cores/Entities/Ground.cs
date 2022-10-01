namespace Cores.Entities
{
    public class Ground : Tile
    {
        public Ground(int frameStart, int frameLength = 1)
        {
            frames = new Seg(frameStart, frameLength);
        }

        public override string Type { get; } = "G";
    }
}