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

        private void FixedUpdate() { }

        public async UniTask LoadMap(Map map)
        {
            this.map = map;

            text.text = "" + this.map.Cycle;
        }

        public async UniTask UnloadMap()
        {
            this.map = null;
        }

        public void StartCountdown() { }

        public void EndCountdown() { }
    }

    public interface ICountdownFlow
    {
        UniTask LoadMap(Map map);
        UniTask UnloadMap();
        void StartCountdown();
        void EndCountdown();
    }
}