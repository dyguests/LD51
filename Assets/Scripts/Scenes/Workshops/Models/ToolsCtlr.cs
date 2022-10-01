using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class ToolsCtlr : MonoBehaviour
    {
        public void OnGroundClick(bool on)
        {
            Debug.Log("OnGroundClick on:"+on);
        }
    }
}