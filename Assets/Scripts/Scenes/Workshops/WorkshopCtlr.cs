using Cores.Scenes.Workshops.Entities;
using Scenes.Workshops.Models;
using UnityEngine;

namespace Scenes.Workshops
{
    public class WorkshopCtlr : MonoBehaviour, IWorkshopFlow
    {
        [SerializeField] private FramesCtlr framesCtlr;
        [SerializeField] private CyclesCtlr cyclesCtlr;
        [SerializeField] private MenuCtlr menuCtlr;
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
            mold = App.Instance.mold;
            if (mold == null)
            {
                mold = new Mold();
            }


            await framesCtlr.LoadMold(mold);
            await cyclesCtlr.LoadMold(mold);
            await menuCtlr.LoadMold(mold);
            await moldCtlr.LoadMold(mold);

            StartWorkshop();
        }

        public void StartWorkshop() { }

        public void EndWorkshop() { }

        // public void OnPlayClick()
        // {
        //     Debug.Log("OnPlayClick");
        //
        //     App.Instance.map = mold.ToMap();
        //     App.Instance.mold = mold;
        //     Debug.Log("Play map cipher:\n" + MapEncoder.Encode(App.Instance.map));
        //     SceneStacker.LoadScene(SceneNames.Game);
        // }
    }

    interface IWorkshopFlow
    {
        void RunWorkshopFlow();
        void PrepareWorkshop();
        void StartWorkshop();
        void EndWorkshop();
    }
}