using UnityEngine;

namespace Models
{
    public class EndPointCtlr : TileCtlr
    {
        private static EndPointCtlr sPrefab;

        private Vector2Int pos;

        private AreaCtlr areaCtlr;

        public static EndPointCtlr Generate(Vector2Int pos, AreaCtlr areaCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<EndPointCtlr>("Prefabs/Models/EndPoint");
            }

            var instantiate = Instantiate(sPrefab, areaCtlr.Pos2Position(pos), Quaternion.identity, areaCtlr.transform);
            instantiate.name = "EndPoint";
            instantiate.pos = pos;
            instantiate.areaCtlr = areaCtlr;

            instantiate.Created();

            return instantiate;
        }
    }
}