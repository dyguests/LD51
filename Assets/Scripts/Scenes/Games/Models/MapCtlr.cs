using Cores.Entities;
using Cores.Scenes.Games.Entities;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;

namespace Scenes.Games.Models
{
    public class MapCtlr : AreaCtlr, IMapFlow
    {
        private Map map;

        private StartPointCtlr startPointCtlr;
        private EndPointCtlr endPointCtlr;

        protected override Vector2Int Size => map.Size;

        public async UniTask LoadMap(Map map)
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
                        // await UniTask.DelayFrame(1);
                    }
                }
            }

            var startPoint = map.StartPoint;
            startPointCtlr = StartPointCtlr.Generate(map, this);

            var endPoint = map.EndPoint;
            endPointCtlr = EndPointCtlr.Generate(map, this);

            await UniTask.Delay(250);
        }

        public async UniTask UnloadMap()
        {
            await UniTask.Delay(250);
        }

        private void InsertTile(Tile tile)
        {
            if (tile is Ground ground)
            {
                tile.UpdateFrameState(map.CurrentFrame, map.FrameLength);
                GroundCtlr.Generate(ground, this);
            }
        }

        public void SpawnPlayer()
        {
            startPointCtlr.SpawnPlayer();
        }
    }

    public interface IMapFlow
    {
        UniTask LoadMap(Map map);
        UniTask UnloadMap();
    }
}