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
        public Map(in int width, in int height, int frameLength)
        {
            size = new Vector2Int(width, height);
            this.frameLength = frameLength;

            tileRings = new SortedList<int, Tile>[size.x, size.y];
        }

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
        }
    }
}