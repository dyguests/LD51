using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Cores
{
    public interface ISubject<T>
    {
        void AddObserver(IObserver<T> observer);
        void RemoveObserver(IObserver<T> observer);
        void NotifyObserver([NotNull] Action<T> action);
    }

    public class DefaultSubject<T> : ISubject<T>
    {
        private readonly List<IObserver<T>> observers = new();

        public void AddObserver(IObserver<T> observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver<T> observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObserver(Action<T> action)
        {
            foreach (var observer in observers.ToArray())
            {
                action(observer.Updater);
            }
        }
    }
}