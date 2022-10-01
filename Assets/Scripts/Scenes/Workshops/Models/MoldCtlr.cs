using System;
using Cores.Scenes.Workshops.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class MoldCtlr : MonoBehaviour,
        IMoldFlow
    {
        [SerializeField] private MoldInputCtlr inputCtlr;

        private InputHandler inputHandler;

        private Mold mold;

        private void OnEnable()
        {
            inputHandler = new InputHandler();
        }

        private void OnDisable()
        {
            inputHandler = null;
        }

        public async UniTask LoadMold(Mold mold)
        {
            this.mold = mold;

            inputCtlr.inputHandler = inputHandler;
        }

        public async UniTask UnloadMold()
        {
            inputCtlr.inputHandler = null;
        }

        private class InputHandler : IMoldInputHandler
        {
            public void OnPress(Vector3 getLocalPosition)
            {
                throw new NotImplementedException();
            }

            public void OnMove(Vector3 getLocalPosition)
            {
                throw new NotImplementedException();
            }

            public void OnRelease(Vector3 getLocalPosition)
            {
                throw new NotImplementedException();
            }
        }
    }

    public interface IMoldFlow
    {
        UniTask LoadMold(Mold mold);
        UniTask UnloadMold();
    }

    public interface IMoldInputHandler
    {
        void OnPress(Vector3 getLocalPosition);
        void OnMove(Vector3 getLocalPosition);
        void OnRelease(Vector3 getLocalPosition);
    }
}