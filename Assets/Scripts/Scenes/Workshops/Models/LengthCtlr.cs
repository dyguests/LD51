using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Workshops.Models
{
    /// <summary>
    /// used by tile generator
    /// to insert tile with tile.frameLength
    /// </summary>
    public class LengthCtlr : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Slider slider;

        private int frameLength = 1;

        public int FrameLength => frameLength;
    }
}