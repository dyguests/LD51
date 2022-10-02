using System.Collections.Generic;
using Cores.Entities;
using UnityEngine;

namespace Cores.Scenes.Games.Entities
{
    /// <summary>
    /// game map
    /// 游戏地图
    /// </summary>
    public class Map : Area<Tile>
    {
        public Map(in int width, in int height, int frameLength) : base(width, height, frameLength) { }

        public override void Insert(in int x, in int y, Tile tile)
        {
            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                tileRing = new SortedList<int, Tile>();
                tileRings[x, y] = tileRing;
            }

            tileRing.Add(tile.Frames.start, tile);
            tile.Inserted(x, y);

            // todo, 由于 map 不是游戏开始后加载的，所以这里先暂时不需要监听
        }
    }
}