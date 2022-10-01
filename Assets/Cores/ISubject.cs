using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Cores
{
    public interface ISubject<T>
    {
        void AddObserver(Cores.IObserver<T> observer);
        void RemoveObserver(Cores.IObserver<T> observer);
        void NotifyObserver([NotNull] Action<T> action);
    }

    public class DefaultSubject<T> : ISubject<T>
    {
        private readonly List<Cores.IObserver<T>> observers = new();

        public void AddObserver(Cores.IObserver<T> observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(Cores.IObserver<T> observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObserver(Action<T> action)
        {
            foreach (var observer in observers)
            {
                action(observer.Updater);
            }
        }
    }
}