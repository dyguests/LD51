using System;
using Cores.Scenes.Workshops.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class FramesCtlr : MonoBehaviour
    {
        private int currentFrame = 0;
        private int frameLength = 1;

        public int CurrentFrame
        {
            get => currentFrame;
            set
            {
                currentFrame = value;
                mold.CurrentFrame = currentFrame;
            }
        }
        public int FrameLength => frameLength;

        private Mold mold;

        public void OnFrameClick(int index, bool on)
        {
            if (on)
            {
                CurrentFrame = index;
            }
        }

        public async UniTask LoadMold(Mold mold)
        {
            this.mold = mold;
        }

        public void OnFrameLengthChanged(Single single)
        {
            frameLength = (int) single;

            Debug.Log("frameLength:" + frameLength);
        }
    }
}