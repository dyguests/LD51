using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Cores.Entities
{
    public interface IArea<T>
    {
        void Insert(in int x, in int y, [NotNull] T t);
        bool Contains(int x, int y);
        IReadOnlyDictionary<int, T> GetRing(int x, int y);
    }

    public abstract class Area<T> : IArea<T>
    {
        private Vector2Int size;
        /// <summary>
        /// how many frames used.
        /// length in [1,8]
        /// </summary>
        private int frameLength;
        private int cycle = 10;

        public Vector2Int Size => size;
        public int FrameLength
        {
            get => frameLength;
            set => frameLength = value;
        }
        public int Cycle
        {
            get => cycle;
            set => cycle = value;
        }

        protected readonly SortedList<int, T>[,] tileRings;

        protected Area(in int width, in int height, in int frameLength)
        {
            size = new Vector2Int(width, height);
            this.frameLength = frameLength;

            tileRings = new SortedList<int, T>[size.x, size.y];
        }

        public abstract void Insert(in int x, in int y, [NotNull] T t);

        public bool Contains(int x, int y)
        {
            return x >= 0 && x < size.x && y >= 0 && y < size.y;
        }

        public IReadOnlyDictionary<int, T> GetRing(int x, int y)
        {
            return tileRings[x, y];
        }
    }
}