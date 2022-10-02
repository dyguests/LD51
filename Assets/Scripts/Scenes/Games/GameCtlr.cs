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
        private static GameCtlr sInstance;
        public static GameCtlr Instance => sInstance;


        [SerializeField] private MapCtlr mapCtlr;
        [SerializeField] private CountdownCtlr countdownCtlr;

        private Map map;

        void Awake()
        {
            sInstance = this;
        }

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

        public void WinGame()
        {
            Debug.Log("WinGame");
            countdownCtlr.EndCountdown();
        }

        public void LoseGame()
        {
            Debug.Log("LoseGame");
            countdownCtlr.EndCountdown();
            PlayerCtlr.Instance.Dead();
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
        void WinGame();
        void LoseGame();
        void EndGame();
    }
}