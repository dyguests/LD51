using Cores.Scenes.Games.Entities;
using Cores.Scenes.Workshops.Entities;

namespace Cores.Scenes.Workshops.Tools
{
    public static class MoldEx
    {
        public static Map ToMap(this Mold mold)
        {
            var size = mold.Size;

            var map = new Map(size.x, size.y, mold.FrameLength);

            return map;
        }
    }
}