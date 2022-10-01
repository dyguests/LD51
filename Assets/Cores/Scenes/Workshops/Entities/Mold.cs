using UnityEngine;

namespace Cores.Scenes.Workshops.Entities
{
    public class Mold
    {
        private Vector2Int size;
        public Vector2Int Size => size;

        public int frameCount;
        public int currentFrameIndex;

        public Mold(int width = 32, int height = 18)
        {
            size = new Vector2Int(width, height);
        }
    }
}