using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Workshops
{
    public class MoldInputCtlr : MonoBehaviour,
        IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        public IMoldInputHandler inputHandler;

        public void OnPointerDown(PointerEventData eventData)
        {
            inputHandler?.OnPress(GetLocalPosition(eventData));
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!eventData.eligibleForClick) return;
            inputHandler?.OnMove(GetLocalPosition(eventData));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputHandler?.OnRelease(GetLocalPosition(eventData));
        }


        private Vector3 GetLocalPosition(PointerEventData eventData)
        {
            var screenPoint = eventData.position;
            var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            var localPoint = transform.worldToLocalMatrix.MultiplyPoint(worldPoint);
            // var pos = new Vector2Int(localPoint.x.ClampInt(), localPoint.y.ClampInt());

            // Debug.Log("screenPoint:" + screenPoint + " worldPoint:" + worldPoint + " localPoint:" + localPoint + " pos:" + pos);
            return localPoint;
        }
    }

    public interface IMoldInputHandler
    {
        void OnPress(Vector3 position);
        void OnMove(Vector3 position);
        void OnRelease(Vector3 position);
    }
}