using System;
using Cores.Scenes.Workshops.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Workshops.Models
{
    public class FramesCtlr : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private FrameToggleCtlr[] frameToggleCtlrs;

        private int currentFrame = 0;
        private int frameLength = 2;

        public int CurrentFrame
        {
            get => currentFrame;
            set
            {
                currentFrame = value;
                mold.CurrentFrame = currentFrame;
            }
        }
        public int FrameLength
        {
            get => frameLength;
            set
            {
                frameLength = value;
                mold.FrameLength = frameLength;
            }
        }

        private Mold mold;

        public async UniTask LoadMold(Mold mold)
        {
            this.mold = mold;

            slider.value = mold.FrameLength;
            frameToggleCtlrs[mold.CurrentFrame].Check(true);
        }

        public void OnFrameClick(int index, bool on)
        {
            if (on)
            {
                CurrentFrame = index;
            }
        }

        public void OnFrameLengthChanged(Single single)
        {
            FrameLength = (int) single;

            Debug.Log("frameLength:" + FrameLength);
        }
    }
}