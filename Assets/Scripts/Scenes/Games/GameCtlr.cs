using Cores.Scenes.Games.Entities;
using Scenes.Games.Models;
using UnityEngine;

namespace Scenes.Games
{
    public class GameCtlr : MonoBehaviour, IGameFlow
    {
        [SerializeField] private MapCtlr mapCtlr;
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

            mapCtlr.LoadMap(map);

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