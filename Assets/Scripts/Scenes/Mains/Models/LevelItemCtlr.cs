using Databases.Datas;
using Databases.Encoders;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Mains.Models
{
    public class LevelItemCtlr : MonoBehaviour
    {
        [SerializeField] private Image tick;

        private int index;

        private bool completed;

        private void Start()
        {
            completed = CacheUtils.GetLevelCompleted(index);
            Debug.Log("LevelItemCtlr completed:" + completed);
            tick.enabled = completed;
        }

        public void Created(int index)
        {
            this.index = index;
        }

        public void OnClick()
        {
            var mapCipher = MapCiphers.datas[index];
            var map = MapEncoder.Decode(mapCipher);
            map.levelId = index;
            App.Instance.map = map;
            SceneStacker.LoadScene(SceneNames.Game);
        }
    }
}