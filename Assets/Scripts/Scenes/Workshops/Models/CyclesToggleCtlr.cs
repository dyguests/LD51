using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.Workshops.Models
{
    public class CyclesToggleCtlr : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        [Space] [SerializeField] private int cycle;
        [SerializeField] private UnityEvent<int, bool> onValueChanged;

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetDirty();
        }

#endif

        private void SetDirty()
        {
            text.text = "" + cycle;
        }

        public void OnToggleChanged(bool on)
        {
            onValueChanged.Invoke(cycle, on);
        }
    }
}