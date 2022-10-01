using Cores.Scenes.Games.Entities;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Scenes.Games.Models
{
    public class CountdownCtlr : MonoBehaviour, ICountdownFlow
    {
        [SerializeField] private TMP_Text text;

        private Map map;

        private int cycle;

        #region temp varaints

        private float countdown;
        private bool isCountdowning = false;

        #endregion

        private void FixedUpdate()
        {
            if (isCountdowning)
            {
                if (countdown <= 0f)
                {
                    countdown += cycle;
                }

                countdown -= Time.fixedDeltaTime;
            }
        }

        private void Update()
        {
            UpdateText();
        }

        public async UniTask LoadMap(Map map)
        {
            this.map = map;

            cycle = this.map.Cycle;
            countdown = cycle;
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
            isCountdowning = true;
        }

        public void EndCountdown()
        {
            isCountdowning = false;
        }

        private void UpdateText()
        {
            // text.text = Mathf.Max(0f, countdown).ToString("#0.000");
            text.text = Mathf.Max(0f, countdown).ToString("#0");
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