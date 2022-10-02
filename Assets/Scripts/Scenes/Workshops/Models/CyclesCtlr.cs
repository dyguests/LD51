using Cores.Scenes.Workshops.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class CyclesCtlr : MonoBehaviour
    {
        [SerializeField] private CyclesToggleCtlr[] cyclesToggleCtlrs;

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

        public async UniTask LoadMold(Mold mold)
        {
            this.mold = mold;

            cyclesToggleCtlrs[CycleToCycleToggleIndex(mold.Cycle)].Check(true);
        }

        public void UnloadMold()
        {
            this.mold = null;
        }

        public void OnCycleClick(int cycle, bool on)
        {
            if (on)
            {
                this.Cycle = cycle;
            }
        }

        private static int CycleToCycleToggleIndex(int cycle)
        {
            return cycle switch
            {
                10 => 0,
                5 => 1,
                2 => 2,
                1 => 3,
                _ => 0
            };
        }
    }
}