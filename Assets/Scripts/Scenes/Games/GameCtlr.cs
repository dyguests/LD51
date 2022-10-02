using Cores.Scenes.Games.Entities;
using Cysharp.Threading.Tasks;
using Databases.Datas;
using Databases.Encoders;
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
                map = MapEncoder.Decode(MapCiphers.Template);
            }
#endif

            // await mapCtlr.LoadMap(map);
            // await countdownCtlr.LoadMap(map);
            await UniTask.WhenAll(
                mapCtlr.LoadMap(map),
                countdownCtlr.LoadMap(map)
            );

            StartGame();
        }

        public void StartGame()
        {
            countdownCtlr.StartCountdown();
            mapCtlr.SpawnPlayer();
        }

        public void EndGame()
        {
            countdownCtlr.EndCountdown();
        }
    }

    public interface IGameFlow
    {
        void RunGameFlow();
        void PrepareGame();
        void StartGame();
        void EndGame();
    }
}