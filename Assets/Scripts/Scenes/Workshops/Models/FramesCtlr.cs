using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class FramesCtlr : MonoBehaviour
    {
        public void OnFrameClick(int index, bool on)
        {
            Debug.Log("OnFrameClick " + " index:" + index + "on:" + on);
        }
    }
}