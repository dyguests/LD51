using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.Workshops.Models
{
    public class FrameToggleCtlr : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        [Space] [SerializeField] private int index;
        [SerializeField] private UnityEvent<int, bool> onValueChanged;

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetDirty();
        }

#endif

        private void SetDirty()
        {
            text.text = "" + (index + 1);
        }

        public void OnToggleChanged(bool on)
        {
            onValueChanged.Invoke(index, on);
        }
    }
}