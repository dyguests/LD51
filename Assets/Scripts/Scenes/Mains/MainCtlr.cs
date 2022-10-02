using Tools;
using UnityEngine;

namespace Scenes.Mains
{
    public class MainCtlr : MonoBehaviour
    {
        public void OnWorkshopClick()
        {
            Debug.Log("OnWorkshopClick");
            SceneStacker.LoadScene(SceneNames.Workshop);
        }
    }
}