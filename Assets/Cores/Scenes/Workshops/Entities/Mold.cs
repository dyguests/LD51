using System.Collections.Generic;
using System.Linq;
using Cores.Entities;
using UnityEngine;

namespace Cores.Scenes.Workshops.Entities
{
    public class Mold
    {
        private Vector2Int size;
        /// <summary>
        /// how many frames used.
        /// length in [1,8]
        /// </summary>
        public int length = 1;
        public Vector2Int Size => size;
        public int Length
        {
            get => length;
            set => length = value;
        }

        private readonly SortedList<int, Tile>[,] tileRings;

        public Mold(int width = 32, int height = 18)
        {
            size = new Vector2Int(width, height);
            tileRings = new SortedList<int, Tile>[size.x, size.y];
        }

        public void Insert(int x, int y, Ground ground)
        {
            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                tileRing = new SortedList<int, Tile>();
                tileRings[x, y] = tileRing;
            }

            tileRing.Add(ground.Frames.x, ground);
            // todo Exclude adjacent duplicates
        }

        public bool Contains(int x, int y)
        {
            return x >= 0 && x < size.x && y >= 0 && y < size.y;
        }

        public Tile Get(int x, int y, int frame)
        {
            return tileRings[x, y]?.Select(pair => pair.Value)
                .FirstOrDefault(tile =>
                    (tile.Frames.x <= frame && frame < tile.Frames.x + tile.Frames.y)
                    || (tile.Frames.x - 8 <= frame && frame < tile.Frames.x + tile.Frames.y - 8)
                );
        }
    }
}