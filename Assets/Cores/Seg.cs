using System;

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
        public static readonly Seg Empty = new Seg(0, 0);

        public int start;
        public int length;

        public Seg(in int start, in int width = 1)
        {
            this.start = start;
            length = width;
        }

        public static Seg operator -(Seg lhs, Seg rhs) => OpSubtraction(lhs, rhs); //todo
        public static bool operator ==(Seg lhs, Seg rhs) => lhs.start == rhs.start && lhs.length == rhs.length;
        public static bool operator !=(Seg lhs, Seg rhs) => !(lhs == rhs);

        public bool Equals(Seg other) => start == other.start && length == other.length;
        public override bool Equals(object obj) => obj is Seg other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(start, length);

        public override string ToString()
        {
            return "Seq(" + start + "," + length + ")";
        }


        private static Seg OpSubtraction(Seg lhs, Seg rhs, int cycle = 10)
        {
            var lhsArray = Seg2Array(lhs, cycle);
            var rhsArray = Seg2Array(rhs, cycle);

            // subtraction
            for (var i = 0; i < rhsArray.Length; i++)
            {
                if (rhsArray[i] > 0) lhsArray[i] = 0;
            }

            return Array2Seg(lhsArray);

            int[] Seg2Array(Seg lhs, int cycle)
            {
                var lhsFrames = new int[cycle];
                for (int i = 0; i < lhs.length; i++)
                {
                    lhsFrames[(i + lhs.start) % cycle] = i + 1;
                }

                return lhsFrames;
            }

            Seg Array2Seg(int[] array)
            {
                var arrayLength = array.Length;

                int start = 0;
                int min = Int32.MaxValue;
                for (var i = 0; i < arrayLength; i++)
                {
                    var order = array[i];
                    if (order == 0) continue;
                    if (order < min)
                    {
                        start = i;
                        min = order;
                    }
                }

                var length = 0;
                for (var i = 0; i < arrayLength; i++)
                {
                    var order = array[(i + start) % arrayLength];
                    if (order == 0) break;
                    length++;
                }

                return new Seg(start, length);
            }
        }
    }
}