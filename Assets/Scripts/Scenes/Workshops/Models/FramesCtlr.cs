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
        private int frameLength = 10;

        public int CurrentFrame
        {
            get => currentFrame;
            set
            {
                currentFrame = value;
                mold.CurrentFrame = currentFrame;

                Debug.Log("FramesCtlr CurrentFrame changed:"+CurrentFrame);
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

            // slider.value = mold.FrameLength;
            // frameToggleCtlrs[mold.CurrentFrame].Check(true);
            slider.value = mold.CurrentFrame;
        }

        public void OnFrameClick(int index, bool on)
        {
            if (on)
            {
                slider.value = index;
                CurrentFrame = index;
            }
        }

        public void OnCurrentFrameChanged(Single single)
        {
            var currentFrame = (int) single;

            Debug.Log("OnCurrentFrameChanged currentFrame:" + currentFrame);

            frameToggleCtlrs[currentFrame].Check(true);
        }
    }
}