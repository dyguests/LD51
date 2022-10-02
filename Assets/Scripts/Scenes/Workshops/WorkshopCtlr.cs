using Cores.Scenes.Workshops.Entities;
using Cores.Scenes.Workshops.Tools;
using Databases.Encoders;
using Scenes.Workshops.Models;
using Tools;
using UnityEngine;

namespace Scenes.Workshops
{
    public class WorkshopCtlr : MonoBehaviour, IWorkshopFlow
    {
        [SerializeField] private CyclesCtlr cyclesCtlr;
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

            await  cyclesCtlr.LoadMold(mold);
            await moldCtlr.LoadMold(mold);

            StartWorkshop();
        }

        public void StartWorkshop() { }

        public void EndWorkshop() { }

        public void OnPlayClick()
        {
            Debug.Log("OnPlayClick");

            App.Instance.map = mold.ToMap();
            Debug.Log("Play map cipher:\n" + MapEncoder.Encode(App.Instance.map));
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