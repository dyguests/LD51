using Cores.Scenes.Games.Entities;
using UnityEngine;

namespace Scenes.Games.Models
{
    public class MapCtlr : MonoBehaviour, IMapFlow
    {
        private Map map;

        public void LoadMap(Map map)
        {
            this.map = map;
        }

        public void UnloadMap() { }
    }

    public interface IMapFlow
    {
        void LoadMap(Map map);
        void UnloadMap();
    }
}