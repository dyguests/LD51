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

            slider.value = mold.FrameLength;
            frameToggleCtlrs[mold.CurrentFrame].Check(true);
        }

        public void OnFrameLengthChanged(Single single)
        {
            frameLength = (int) single;

            Debug.Log("frameLength:" + frameLength);
        }
    }
}