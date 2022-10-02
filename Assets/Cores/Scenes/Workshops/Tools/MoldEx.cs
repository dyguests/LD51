using Cores.Scenes.Games.Entities;
using Cores.Scenes.Workshops.Entities;

namespace Cores.Scenes.Workshops.Tools
{
    public static class MoldEx
    {
        public static Map ToMap(this Mold mold)
        {
            var size = mold.Size;

            var map = new Map(size.x, size.y, mold.FrameLength)
            {
                Cycle = mold.Cycle,
            };

            for (int mX = 0; mX < size.x; mX++)
            {
                for (int mY = 0; mY < size.y; mY++)
                {
                    var tileRings = mold.GetRing(mX, mY);
                    if (tileRings == null) continue;
                    foreach (var pair in tileRings)
                    {
                        map.Insert(mX, mY, pair.Value);
                    }
                }
            }

            return map;
        }
    }
}