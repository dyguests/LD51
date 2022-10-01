using System;
using System.Collections.Generic;
using System.Linq;
using Cores.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Cores.Scenes.Workshops.Entities
{
    public class Mold : ISubject<Mold.IUpdater>
    {
        private Vector2Int size;
        /// <summary>
        /// how many frames used.
        /// length in [1,8]
        /// </summary>
        private int length = 1;
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

        public void Insert(int x, int y, [NotNull] Tile tile)
        {
            Remove(x, y, tile.Frames.x, tile.Frames.y);

            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                tileRing = new SortedList<int, Tile>();
                tileRings[x, y] = tileRing;
            }

            tileRing.Add(tile.Frames.x, tile);
            tile.Inserted(x, y);

            subjectImplementation.NotifyObserver(updater => updater.OnTileInserted(tile));
        }

        private void Remove(in int x, in int y, in int framesStart, in int framesLength)
        {
            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                return;
            }

            for (var i = 0; i < tileRing.Count; i++)
            {
                var tile = tileRing[i];
                if (tile == null) continue;

                // tile.Frames.x
                // todo remove
                // tile.Removed();
                // tile.Changed();
            }
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

        public interface IUpdater
        {
            void OnTileInserted(Tile tile);
        }

        private readonly ISubject<IUpdater> subjectImplementation = new DefaultSubject<IUpdater>();
        public void AddObserver(IObserver<IUpdater> observer) => subjectImplementation.AddObserver(observer);
        public void RemoveObserver(IObserver<IUpdater> observer) => subjectImplementation.RemoveObserver(observer);
        public void NotifyObserver(Action<IUpdater> action) => subjectImplementation.NotifyObserver(action);
    }
}