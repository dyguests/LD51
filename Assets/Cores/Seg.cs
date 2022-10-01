namespace Cores
{
    /// <summary>
    /// 段
    /// 用于记录帧区间
    ///  例如 Seg(2,3) 的区间是：2,3,4
    /// 这是一个循环区间：
    ///  例如 maxFrame=8 时 Seq(6,4) 的区间是： 6,7,0,1
    /// </summary>
    public struct Seg
    {
        public int start;
        public int length;

        public Seg(in int start, in int width = 1)
        {
            this.start = start;
            length = width;
        }

        public static Seg operator -(Seg a, Seg b) => new Seg(a.start - b.start, a.length - b.length); //todo
        public static bool operator ==(Seg lhs, Seg rhs) => lhs.start == rhs.start && lhs.length == rhs.length;
        public static bool operator !=(Seg lhs, Seg rhs) => !(lhs == rhs);
    }
}