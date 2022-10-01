using Tools;
using UnityEngine;

namespace Models
{
    public abstract class AreaCtlr : MonoBehaviour
    {
        protected abstract Vector2Int Size { get; }

        protected Vector2Int Position2Pos(Vector2 position)
        {
            return new(
                (position.x + (Size.x - 1) / 2f).ClampInt(),
                (position.y + (Size.y - 1) / 2f).ClampInt()
            );
        }

        public Vector2 Pos2Position(Vector2Int pos) => new(pos.x - (Size.x - 1) / 2f, pos.y - (Size.y - 1) / 2f);
    }
}