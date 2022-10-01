using UnityEngine;

namespace Cores.Entities
{
    public abstract class Tile
    {
        protected Vector2Int pos;
        /// <summary>
        /// 从第几帧显示到第几帧
        /// frames.x = frameStart
        /// frames.y = frameDuration
        ///
        /// frames.x in [0,7]
        /// frames.y in [1,8]
        /// </summary>
        protected Vector2Int frames;

        public Vector2Int Pos => pos;
        public Vector2Int Frames => frames;

        public void Inserted(in int x, in int y)
        {
            pos = new Vector2Int(x, y);
        }
    }
}