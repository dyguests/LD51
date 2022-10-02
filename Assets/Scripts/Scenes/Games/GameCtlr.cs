using Cores.Scenes.Games.Entities;
using Cysharp.Threading.Tasks;
using Databases.Datas;
using Databases.Encoders;
using Scenes.Games.Models;
using Tools;
using UnityEngine;

namespace Scenes.Games
{
    public class GameCtlr : MonoBehaviour, IGameFlow
    {
        private static GameCtlr sInstance;
        public static GameCtlr Instance => sInstance;


        [SerializeField] private MapCtlr mapCtlr;
        [SerializeField] private TimerCtlr timerCtlr;

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
                timerCtlr.LoadMap(map)
            );

            StartGame();
        }

        public void StartGame()
        {
            timerCtlr.StartCountdown();
            mapCtlr.SpawnPlayer();
        }

        public void WinGame()
        {
            Debug.Log("WinGame");

            CacheUtils.SetLevelCompleted(map.levelId);

            timerCtlr.EndCountdown();
            timerCtlr.SetWin();
            PlayerCtlr.Instance.Disable();
        }

        public void LoseGame()
        {
            Debug.Log("LoseGame");
            timerCtlr.EndCountdown();
            timerCtlr.SetLose();
            PlayerCtlr.Instance.Dead();
        }

        public void EndGame()
        {
            timerCtlr.EndCountdown();
        }

        public void RestartGame()
        {
            SceneStacker.ReloadScene();
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