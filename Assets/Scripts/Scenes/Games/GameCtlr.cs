using Ciphers;
using Cores.Scenes.Games.Entities;
using Databases;
using Scenes.Games.Models;
using UnityEngine;

namespace Scenes.Games
{
    public class GameCtlr : MonoBehaviour, IGameFlow
    {
        [SerializeField] private MapCtlr mapCtlr;
        [SerializeField] private CountdownCtlr countdownCtlr;

        private Map map;

        private void Start()
        {
            RunGameFlow();
        }

        public void RunGameFlow()
        {
            PrepareGame();
        }

        public async void PrepareGame()
        {
            map = App.Instance.map;

#if UNITY_EDITOR
            if (map == null)
            {
                map = MapEncoder.Decode(MapCiphers.Test);
            }
#endif

            mapCtlr.LoadMap(map);
            countdownCtlr.LoadMap(map);

            StartGame();
        }

        public void StartGame() { }

        public void EndGame() { }
    }

    public interface IGameFlow
    {
        void RunGameFlow();
        void PrepareGame();
        void StartGame();
        void EndGame();
    }
}