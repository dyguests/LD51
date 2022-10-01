using Cores.Entities;
using Cores.Scenes.Games.Entities;
using Models;
using UnityEngine;

namespace Scenes.Games.Models
{
    public class MapCtlr : AreaCtlr, IMapFlow
    {
        private Map map;

        protected override Vector2Int Size => map.Size;

        public void LoadMap(Map map)
        {
            this.map = map;

            var size = map.Size;
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var tileRings = map.GetRing(x, y);
                    if (tileRings == null) continue;
                    foreach (var tile in tileRings.Values)
                    {
                        InsertTile(tile);
                    }
                }
            }
        }

        public void UnloadMap() { }

        private void InsertTile(Tile tile)
        {
            if (tile is Ground ground)
            {
                GroundCtlr.Generate(ground, this);
            }
        }
    }

    public interface IMapFlow
    {
        void LoadMap(Map map);
        void UnloadMap();
    }
}