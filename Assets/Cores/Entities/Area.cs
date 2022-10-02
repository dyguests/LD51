using System;
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

    public abstract class Area<T> : IArea<T>, ISubject<Area<T>.IUpdater>
        where T : IElement
    {
        protected const int MaxFrames = 8;

        private Vector2Int size;
        /// <summary>
        /// how many frames used.
        /// length in [1,8]
        /// </summary>
        private int frameLength;

        public Vector2Int Size => size;
        public int FrameLength
        {
            get => frameLength;
            set => frameLength = value;
        }

        private int cycle = 10;
        public int Cycle
        {
            get => cycle;
            set => cycle = value;
        }

        private int currentFrame = 0;
        public int CurrentFrame
        {
            get => currentFrame;
            set
            {
                currentFrame = value;
                Debug.Log("currentFrame:" + currentFrame);
                foreach (var tileRing in tileRings)
                {
                    if (tileRing == null)
                    {
                        continue;
                    }

                    foreach (var tile in tileRing.Values)
                    {
                        tile.UpdateFrameState(currentFrame, frameLength);
                    }
                }
            }
        }
        public Vector2Int StartPoint
        {
            get => startPoint;
            set
            {
                var oldStartPoint = startPoint;
                startPoint = value;
                NotifyObserver(updater => updater.OnStartPointChanged(oldStartPoint, startPoint));
            }
        }
        public Vector2Int EndPoint
        {
            get => endPoint;
            set
            {
                var oldEndPoint = endPoint;
                endPoint = value;
                NotifyObserver(updater => updater.OnEndPointChanged(oldEndPoint, endPoint));
            }
        }

        protected Vector2Int startPoint = new(11, 8);
        protected Vector2Int endPoint = new(20, 8);
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

        public interface IUpdater
        {
            void OnStartPointChanged(Vector2Int oldStartPoint, Vector2Int newStartPoint) { }
            void OnEndPointChanged(Vector2Int oldEndPoint, Vector2Int newEndPoint) { }
        }

        internal readonly ISubject<IUpdater> subjectImplementation = new DefaultSubject<IUpdater>();
        public void AddObserver(IObserver<IUpdater> observer) => subjectImplementation.AddObserver(observer);
        public void RemoveObserver(IObserver<IUpdater> observer) => subjectImplementation.RemoveObserver(observer);
        public void NotifyObserver(Action<IUpdater> action) => subjectImplementation.NotifyObserver(action);

        public void NextFrame()
        {
            SetCurrentFrame((CurrentFrame + 1) % FrameLength);
        }

        private void SetCurrentFrame(int newFrame)
        {
            CurrentFrame = newFrame;
        }
    }
}