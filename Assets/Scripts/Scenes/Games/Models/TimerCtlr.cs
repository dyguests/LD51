using Cores.Scenes.Games.Entities;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Scenes.Games.Models
{
    public class TimerCtlr : MonoBehaviour, ICountdownFlow
    {
        [SerializeField] private TMP_Text text;

        private Map map;

        private int cycle;

        #region temp varaints

        private float time;
        private int timeInt;
        private bool isCounting = false;

        #endregion

        private void FixedUpdate()
        {
            if (isCounting)
            {
                if (time >= map.FrameLength)
                {
                    time -= map.FrameLength;
                    timeInt = 0;
                }

                if ((int) time > timeInt)
                {
                    timeInt = (int) time;
                    map.NextFrame();
                }

                time += Time.fixedDeltaTime;
                // Debug.Log("TimerCtlr FixedUpdate time:" + time);
            }
        }

        private void Update()
        {
            if (isCounting)
            {
                UpdateText();
            }
        }

        public async UniTask LoadMap(Map map)
        {
            this.map = map;

            cycle = this.map.Cycle;
            time = cycle;
            // text.text = "" + cycle;
            // text.text = string.Format("", countdown);
            UpdateText();
        }

        public async UniTask UnloadMap()
        {
            this.map = null;
        }

        public void StartCountdown()
        {
            isCounting = true;
        }

        public void EndCountdown()
        {
            isCounting = false;
        }

        private void UpdateText()
        {
            // text.text = Mathf.Max(0f, countdown).ToString("#0.000");
            text.text = "" + (timeInt + 1);
        }

        /// <summary>
        /// TODO tmp win
        /// </summary>
        public void SetWin()
        {
            text.text = "WIN";
        }

        public void SetLose()
        {
            text.text = "LOSE";
        }
    }

    public interface ICountdownFlow
    {
        UniTask LoadMap(Map map);
        UniTask UnloadMap();
        void StartCountdown();
        void EndCountdown();
    }
}