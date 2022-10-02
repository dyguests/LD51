using Databases.Datas;
using Databases.Encoders;
using Tools;
using UnityEngine;

namespace Scenes.Mains.Models
{
    public class LevelItemCtlr : MonoBehaviour
    {
        private int index;

        public void Created(int index)
        {
            this.index = index;
        }

        public void OnClick()
        {
            var mapCipher = MapCiphers.datas[index];
            var map = MapEncoder.Decode(mapCipher);
            App.Instance.map = map;
            SceneStacker.LoadScene(SceneNames.Game);
        }
    }
}