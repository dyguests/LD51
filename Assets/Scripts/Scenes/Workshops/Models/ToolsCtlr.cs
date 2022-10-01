using Scenes.Workshops.Entities;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class ToolsCtlr : MonoBehaviour
    {
        private ToolType currentToolType = ToolType.None;
        public ToolType CurrentToolType => currentToolType;

        public void OnGroundClick(bool on)
        {
            Debug.Log("OnGroundClick on:" + on);
            currentToolType = ToolType.Ground;
        }
    }
}