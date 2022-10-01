using UnityEngine;

namespace Scenes.Workshops
{
    public class WorkshopCtlr : MonoBehaviour, IWorkshopFlow
    {
        private void Start()
        {
            RunWorkshopFlow();
        }

        public void RunWorkshopFlow()
        {
            PrepareWorkshop();
        }

        public async void PrepareWorkshop()
        {
            StartWorkshop();
        }

        public void StartWorkshop() { }

        public void EndWorkshop() { }
    }

    interface IWorkshopFlow
    {
        void RunWorkshopFlow();
        void PrepareWorkshop();
        void StartWorkshop();
        void EndWorkshop();
    }
}