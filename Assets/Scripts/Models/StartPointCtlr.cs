using UnityEngine;

namespace Models
{
    public class StartPointCtlr : TileCtlr
    {
        private static StartPointCtlr sPrefab;

        private Vector2Int pos;

        private AreaCtlr areaCtlr;

        public static StartPointCtlr Generate(Vector2Int pos, AreaCtlr areaCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<StartPointCtlr>("Prefabs/Models/StartPoint");
            }

            var instantiate = Instantiate(sPrefab, areaCtlr.Pos2Position(pos), Quaternion.identity, areaCtlr.transform);
            instantiate.name = "StartPoint";
            instantiate.pos = pos;
            instantiate.areaCtlr = areaCtlr;

            instantiate.Created();

            return instantiate;
        }
    }
}