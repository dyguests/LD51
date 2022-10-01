using Cores.Scenes.Workshops.Entities;
using Cores.Scenes.Workshops.Tools;
using Scenes.Workshops.Models;
using Tools;
using UnityEngine;

namespace Scenes.Workshops
{
    public class WorkshopCtlr : MonoBehaviour, IWorkshopFlow
    {
        [SerializeField] private MoldCtlr moldCtlr;

        private Mold mold;

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
            mold = new Mold();

            await moldCtlr.LoadMold(mold);

            StartWorkshop();
        }

        public void StartWorkshop() { }

        public void EndWorkshop() { }

        public void OnPlayClick()
        {
            Debug.Log("OnPlayClick");

            App.Instance.map = mold.ToMap();

            SceneStacker.LoadScene(SceneNames.Game);
        }
    }

    interface IWorkshopFlow
    {
        void RunWorkshopFlow();
        void PrepareWorkshop();
        void StartWorkshop();
        void EndWorkshop();
    }
}