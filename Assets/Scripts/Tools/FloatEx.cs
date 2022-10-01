namespace Tools
{
    public static class FloatEx
    {
        private const int TempOffset = 1000;

        public static int ClampInt(this in float value)
        {
            return (int) (value + 0.5f + TempOffset) - TempOffset;
        }
    }
}