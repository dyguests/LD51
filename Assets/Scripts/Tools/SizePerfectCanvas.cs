using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Commons
{
    [RequireComponent(typeof(CanvasScaler))]
    [ExecuteAlways]
    public class SizePerfectCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasScaler canvasScaler;

        private float screenRatio;

        private void Update()
        {
            var screenRatio = Screen.width / (float) Screen.height;
            if (Math.Abs(this.screenRatio - screenRatio) < 0.001f)
            {
                return;
            }

            this.screenRatio = screenRatio;

            canvasScaler.matchWidthOrHeight = screenRatio < canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y ? 0f : 1f;
        }
    }
}