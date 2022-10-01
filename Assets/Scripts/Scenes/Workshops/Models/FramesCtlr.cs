using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class FramesCtlr : MonoBehaviour
    {
        private int currentFrame = 0;
        public int CurrentFrame => currentFrame;

        public void OnFrameClick(int index, bool on)
        {
            if (on)
            {
                currentFrame = index;
            }
        }
    }
}