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
        private const int MaxFrames = 8;

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
            Remove(x, y, tile.Frames.start, tile.Frames.length);

            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                tileRing = new SortedList<int, Tile>();
                tileRings[x, y] = tileRing;
            }

            tileRing.Add(tile.Frames.start, tile);
            tile.Inserted(x, y);

            subjectImplementation.NotifyObserver(updater => updater.OnTileInserted(tile)); //todo ? 把监听 移动到 Tile.inserted中去?
        }

        private void Remove(in int x, in int y, in int framesStart, in int framesLength)
        {
            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                return;
            }

            var removingFrames = new Seg(framesStart, framesLength);

            for (var i = 0; i < tileRing.Count; i++)
            {
                var tile = tileRing[i];
                if (tile == null) continue;

                if (tile.Frames == removingFrames)
                {
                    tileRing[framesStart] = null;
                    tile.Removed();
                    break;
                }
                else if (tile.Frames.start < framesStart && framesStart < tile.Frames.start + tile.Frames.length)
                {
                    tile.Frames = new Seg(tile.Frames.start, framesStart - tile.Frames.start);
                    continue;
                }
                else if (tile.Frames.start < framesStart + MaxFrames && framesStart + MaxFrames < tile.Frames.start + tile.Frames.length)
                {
                    tile.Frames = new Seg(tile.Frames.start, framesStart + MaxFrames - tile.Frames.start);
                    continue;
                }
                else if (tile.Frames.start < framesStart + framesLength && framesStart + framesLength < tile.Frames.start + tile.Frames.length)
                {
                    var left = (framesStart + framesLength) % MaxFrames;
                    tile.Frames = new Seg(left, tile.Frames.start + tile.Frames.length - left);
                }

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
                    (tile.Frames.start <= frame && frame < tile.Frames.start + tile.Frames.length)
                    || (tile.Frames.start <= frame + MaxFrames && frame + MaxFrames < tile.Frames.start + tile.Frames.length)
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