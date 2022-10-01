namespace Cores.Entities
{
    public interface IPoolElement
    {
        void Acquired();
        void Released();
        void Destroyed();
    }
}