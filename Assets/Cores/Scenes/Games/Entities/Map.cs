using System.Collections.Generic;
using Cores.Entities;
using UnityEngine;

namespace Cores.Scenes.Games.Entities
{
    /// <summary>
    /// game map
    /// 游戏地图
    /// </summary>
    public class Map
    {
        private Vector2Int size;
        private int frameLength;

        private readonly SortedList<int, Tile>[,] tileRings;

        public Map(in int width, in int height, int frameLength)
        {
            size = new Vector2Int(width, height);
            this.frameLength = frameLength;

            tileRings = new SortedList<int, Tile>[size.x, size.y];
        }
    }
}