using UnityEngine;

namespace Tools
{
    [ExecuteAlways]
    public class SizePerfectCamera : MonoBehaviour
    {
        [SerializeField] private new Camera camera;

        [Space] [Tooltip("摄像机的尺寸范围(最小值，最大值)")] [SerializeField]
        private Vector2 sizeRange = new(3.6f, 6.4f);

        private Vector2Int screenSize;

        private void Start()
        {
            RefreshCamera();
        }

        private void Update()
        {
            RefreshCamera();
        }

        private void RefreshCamera()
        {
            if (screenSize.x == Screen.width && screenSize.y == Screen.height)
            {
                return;
            }

            screenSize = new Vector2Int(Screen.width, Screen.height);

            if (Screen.width * sizeRange.y < sizeRange.x * Screen.height)
            {
                // Debug.DrawLine(-Vector3.one, Vector3.one, Color.red);
                camera.orthographicSize = sizeRange.x * Screen.height / Screen.width / 2;
            }
            else if (Screen.width * sizeRange.x > sizeRange.y * Screen.height)
            {
                // Debug.DrawLine(-Vector3.one, Vector3.one, Color.blue);
                camera.orthographicSize = sizeRange.x / 2;
            }
            else if (Screen.width > Screen.height)
            {
                // Debug.DrawLine(-Vector3.one, Vector3.one, Color.yellow);
                camera.orthographicSize = sizeRange.y * Screen.height / Screen.width / 2;
            }
            else
            {
                // Debug.DrawLine(-Vector3.one, Vector3.one, Color.green);
                camera.orthographicSize = sizeRange.y / 2;
            }
        }
    }
}