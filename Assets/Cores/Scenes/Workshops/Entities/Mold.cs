using System;
using System.Collections.Generic;
using System.Linq;
using Cores.Entities;

namespace Cores.Scenes.Workshops.Entities
{
    public class Mold : Area<Tile>, ISubject<Mold.IUpdater>
    {
        public Mold(int width = 32, int height = 18, int frameLength = 2) : base(width, height, frameLength) { }

        public override void Insert(in int x, in int y, Tile tile)
        {
            Remove(x, y, tile.Frames.start, tile.Frames.length);

            var tileRing = tileRings[x, y];
            if (tileRing == null)
            {
                tileRing = new SortedList<int, Tile>();
                tileRings[x, y] = tileRing;
            }

            tileRing.Add(tile.Frames.start, tile);
            tile.UpdateFrameState(CurrentFrame, FrameLength);
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

                var removedFrames = tile.Frames - removingFrames;

                if (tile.Frames == removedFrames)
                {
                    continue;
                }

                if (removedFrames == Seg.Empty)
                {
                    // tileRing[framesStart] = null;
                    tileRing.Remove(framesStart);
                    tile.Removed();
                    continue;
                }

                if (tile.Frames.start == removedFrames.start)
                {
                    tile.Frames = removedFrames;
                }
                else
                {
                    tileRing.Remove(tile.Frames.start);
                    tile.Frames = removedFrames;
                    tileRing.Add(tile.Frames.start, tile);
                    // todo notify tile start changed.
                }

                // tile.Frames.x
                // todo remove
                // tile.Removed();
                // tile.Changed();
            }
        }

        public Tile Get(int x, int y, int frame)
        {
            return tileRings[x, y]?.Select(pair => pair.Value)
                .FirstOrDefault(tile =>
                    (tile.Frames.start <= frame && frame < tile.Frames.start + tile.Frames.length)
                    || (tile.Frames.start <= frame + MaxFrames && frame + MaxFrames < tile.Frames.start + tile.Frames.length)
                );
        }

        public new interface IUpdater : Area<Tile>.IUpdater
        {
            void OnTileInserted(Tile tile);
        }

        private new readonly ISubject<IUpdater> subjectImplementation = new DefaultSubject<IUpdater>();

        public void AddObserver(IObserver<IUpdater> observer)
        {
            (this as Area<Tile>).subjectImplementation.AddObserver(observer);
            subjectImplementation.AddObserver(observer);
        }

        public void RemoveObserver(IObserver<IUpdater> observer)
        {
            (this as Area<Tile>).subjectImplementation.RemoveObserver(observer);
            subjectImplementation.RemoveObserver(observer);
        }

        public void NotifyObserver(Action<IUpdater> action) => subjectImplementation.NotifyObserver(action);
    }
}