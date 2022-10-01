using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Cores.Entities
{
    public interface IArea<T> { }

    public abstract class Area<T> : IArea<T>
    {
        protected Vector2Int size;
        /// <summary>
        /// how many frames used.
        /// length in [1,8]
        /// </summary>
        protected int frameLength;

        public Vector2Int Size => size;
        public int FrameLength
        {
            get => frameLength;
            set => frameLength = value;
        }

        protected SortedList<int, Tile>[,] tileRings;

        public abstract void Insert(in int x, in int y, [NotNull] T t);

        public bool Contains(int x, int y)
        {
            return x >= 0 && x < size.x && y >= 0 && y < size.y;
        }
    }
}