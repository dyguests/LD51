using System;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class FramesCtlr : MonoBehaviour
    {
        private int currentFrame = 0;
        private int frameLength = 1;

        public int CurrentFrame => currentFrame;
        public int FrameLength => frameLength;

        public void OnFrameClick(int index, bool on)
        {
            if (on)
            {
                currentFrame = index;
            }
        }

        public void OnFrameLengthChanged(Single single)
        {
            frameLength = (int) single;

            Debug.Log("frameLength:" + frameLength);
        }
    }
}