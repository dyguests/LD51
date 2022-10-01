using System.Collections.Generic;
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

        private SortedList<int, Tile>[,] tileRings;

        public Mold(int width = 32, int height = 18)
        {
            size = new Vector2Int(width, height);
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
        }

        public bool Contains(int x, int y)
        {
            return x >= 0 && x < size.x && y >= 0 && y < size.y;
        }

        public Tile Get(int x, int y, int frame)
        {
            var tileRing = tileRings[x, y];
            if (tileRing == null) return null;
            foreach (var pair in tileRing)
            {
                var tile = pair.Value;

                if (
                    (tile.Frames.x <= frame && frame < tile.Frames.x + tile.Frames.y)
                    || (tile.Frames.x - 8 <= frame && frame < tile.Frames.x + tile.Frames.y - 8)
                )
                {
                    return tile;
                }
            }

            return null;
        }
    }
}