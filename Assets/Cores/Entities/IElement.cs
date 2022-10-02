namespace Cores.Entities
{
    public interface IElement
    {
        void Inserted(in int x, in int y);
        void Removed();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentFrame"></param>
        /// <param name="frameLength">map的总帧数</param>
        void UpdateFrameState(in int currentFrame, in int frameLength);
    }
}