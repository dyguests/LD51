using Tools;
using UnityEngine;

namespace Scenes.Workshops
{
    public class MenuCtlr : MonoBehaviour
    {
        public void OnPlayClick()
        {
            Debug.Log("OnPlayClick");
            SceneStacker.LoadScene(SceneNames.Game);
        }
    }
}