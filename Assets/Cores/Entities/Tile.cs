using UnityEngine;

namespace Cores.Entities
{
    public abstract class Tile : IElement
    {
        private Vector2Int pos;
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
        /// prePrevious -2
        /// previous -1
        /// current 0
        /// keep +1
        /// </summary>
        private FrameState frameState = FrameState.Current;

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
        /// <summary>
        /// 当前帧在帧循环中的状态
        /// </summary>
        public FrameState FrameState
        {
            get => frameState;
            set { frameState = value; }
        }

        public void Inserted(in int x, in int y)
        {
            pos = new Vector2Int(x, y);
        }

        public void Removed() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentFrame"></param>
        /// <param name="frameLength">map的总帧数</param>
        public void UpdateFrameState(in int currentFrame, in int frameLength)
        {
            // todo 此方法在 map 中 currentFrame 变化时也应用被调用。
            // todo 此方法在 mold 中 currentFrame 变化时也应用被调用。
            // todo 此方法在 mold 中 frameLength 变化时也应用被调用。

            var nextFrame = (currentFrame + 1) % frameLength;
            if (
                (frames.start <= currentFrame && currentFrame < frames.start + frames.length)
                || (frames.start - frameLength <= currentFrame && currentFrame < frames.start - frameLength + frames.length)
            )
            {
                if (
                    (frames.start <= nextFrame && nextFrame < frames.start + frames.length)
                    || (frames.start - frameLength <= nextFrame && nextFrame < frames.start - frameLength + frames.length)
                )
                {
                    FrameState = FrameState.Keep;
                }
                else
                {
                    FrameState = FrameState.Current;
                }

                return;
            }

            if (
                (frames.start <= nextFrame && nextFrame < frames.start + frames.length)
                || (frames.start - frameLength <= nextFrame && nextFrame < frames.start - frameLength + frames.length)
            )
            {
                FrameState = FrameState.Previous;
                return;
            }

            var nextNextFrame = (currentFrame + 2) % frameLength;
            if (
                (frames.start <= nextNextFrame && nextNextFrame < frames.start + frames.length)
                || (frames.start - frameLength <= nextNextFrame && nextNextFrame < frames.start - frameLength + frames.length)
            )
            {
                FrameState = FrameState.PrePrevious;
                return;
            }

            FrameState = FrameState.None;
        }
    }

    public interface IElement
    {
        void Inserted(in int x, in int y);
        void Removed();
    }
}