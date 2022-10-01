namespace Cores
{
    public interface IObserver<out T>
    {
        T Updater { get; }
    }
}