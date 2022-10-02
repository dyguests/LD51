using Cores.Scenes.Workshops.Entities;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class CyclesCtlr : MonoBehaviour
    {
        private int cycle;
        public int Cycle
        {
            get => cycle;
            set
            {
                cycle = value;
                mold.Cycle = cycle;
                Debug.Log("Cycle changed: mold.Cycle:" + mold.Cycle);
            }
        }

        private Mold mold;

        public void OnCycleClick(int cycle, bool on)
        {
            if (on)
            {
                this.Cycle = cycle;
            }
        }

        public void LoadMold(Mold mold)
        {
            this.mold = mold;

            // todo 初次加载要不要更新 cycle toggle？暂时先不更新
        }

        public void UnloadMold()
        {
            this.mold = null;
        }
    }
}