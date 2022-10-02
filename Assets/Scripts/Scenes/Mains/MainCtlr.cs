using Scenes.Mains.Models;
using Tools;
using UnityEngine;

namespace Scenes.Mains
{
    public class MainCtlr : MonoBehaviour, IMainFlow
    {
        [SerializeField] private LevelsCtlr levelsCtlr;

        private void Start()
        {
            RunMainFlow();
        }

        public void RunMainFlow()
        {
            PrepareMain();
        }

        public async void PrepareMain()
        {
            await levelsCtlr.LoadLevels();

            StartMain();
        }

        public void StartMain() { }

        public void EndMain() { }

        public void OnWorkshopClick()
        {
            Debug.Log("OnWorkshopClick");
            SceneStacker.LoadScene(SceneNames.Workshop);
        }
    }

    public interface IMainFlow
    {
        void RunMainFlow();
        void PrepareMain();
        void StartMain();
        void EndMain();
    }
}