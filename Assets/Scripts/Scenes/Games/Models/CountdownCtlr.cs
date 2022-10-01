using Cores.Scenes.Games.Entities;
using UnityEngine;

namespace Scenes.Games.Models
{
    public class CountdownCtlr : MonoBehaviour, ICountdownFlow
    {
        private Map map;

        public void LoadMap(Map map)
        {
            this.map = map;
        }

        public void UnloadMap()
        {
            this.map = null;
        }
    }

    public interface ICountdownFlow
    {
        void LoadMap(Map map);
        void UnloadMap();
    }
}