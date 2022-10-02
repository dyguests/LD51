using Cores.Scenes.Games.Entities;
using Cores.Scenes.Workshops.Entities;

namespace Cores.Scenes.Workshops.Tools
{
    public static class MapEx
    {
        public static Mold ToMold(this Map map)
        {
            var size = map.Size;
            var mold = new Mold(size.x, size.y, map.FrameLength)
            {
                Cycle = map.Cycle,
                StartPoint = map.StartPoint,
                EndPoint = map.EndPoint
            };
            for (int mX = 0; mX < size.x; mX++)
            {
                for (int mY = 0; mY < size.y; mY++)
                {
                    var tileRings = map.GetRing(mX, mY);
                    if (tileRings == null) continue;
                    foreach (var pair in tileRings)
                    {
                        mold.Insert(mX, mY, pair.Value);
                    }
                }
            }

            return mold;
        }
    }
}