using System;

namespace Cores.Entities
{
    public class Ground : Tile, ISubject<Ground.IUpdater>
    {
        public Ground(int frameStart, int frameLength = 1)
        {
            frames = new Seg(frameStart, frameLength);
        }

        public override string Type { get; } = "G";

        public new interface IUpdater : Tile.IUpdater { }
        private new readonly ISubject<IUpdater> subjectImplementation = new DefaultSubject<IUpdater>();

        public void AddObserver(IObserver<IUpdater> observer)
        {
            (this as Tile).subjectImplementation.AddObserver(observer);
            subjectImplementation.AddObserver(observer);
        }

        public void RemoveObserver(IObserver<IUpdater> observer)
        {
            (this as Tile).subjectImplementation.RemoveObserver(observer);
            subjectImplementation.RemoveObserver(observer);
        }

        public void NotifyObserver(Action<IUpdater> action) => subjectImplementation.NotifyObserver(action);
    }
}