using Scenes.Workshops.Entities;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class ToolsCtlr : MonoBehaviour
    {
        private ToolType currentToolType = ToolType.None;
        public ToolType CurrentToolType => currentToolType;

        public void OnClearClick(bool on)
        {
            Debug.Log("OnGroundClick on:" + on);
            if (on)
            {
                currentToolType = ToolType.Clear;
            }
        }
        
        public void OnStartPointClick(bool on)
        {
            Debug.Log("OnStartPointClick on:" + on);
            if (on)
            {
                currentToolType = ToolType.StartPoint;
            }
        }

        public void OnEndPointClick(bool on)
        {
            Debug.Log("OnEndPointClick on:" + on);
            if (on)
            {
                currentToolType = ToolType.EndPoint;
            }
        }

        public void OnGroundClick(bool on)
        {
            Debug.Log("OnGroundClick on:" + on);
            if (on)
            {
                currentToolType = ToolType.Ground;
            }
        }
    }
}