using UnityEngine;

namespace Cores.Entities
{
    public abstract class Tile : IElement
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
        protected Seg frames;

        /// <summary>
        /// MapCipher 编码 解码 用
        /// </summary>
        public abstract string Type { get; }

        public Vector2Int Pos => pos;
        public Seg Frames
        {
            get => frames;
            set
            {
                var oldFrames = frames;
                frames = value;

                // todo on frame truncated
            }
        }

        public void Inserted(in int x, in int y)
        {
            pos = new Vector2Int(x, y);
        }

        public void Removed() { }
    }

    public interface IElement
    {
        void Inserted(in int x, in int y);
        void Removed();
    }
}