using Cores.Entities;
using UnityEngine;

namespace Models
{
    public class GroundCtlr : TileCtlr
    {
        private static GroundCtlr sPrefab;

        private Ground ground;

        private AreaCtlr areaCtlr;

        public static GroundCtlr Generate(Ground ground, AreaCtlr moldCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<GroundCtlr>("Prefabs/Models/Ground");
            }

            var instantiate = Instantiate(sPrefab, moldCtlr.Pos2Position(ground.Pos), Quaternion.identity, moldCtlr.transform);
            instantiate.name = "Ground" + ground.Pos;
            instantiate.ground = ground;
            instantiate.areaCtlr = moldCtlr;

            instantiate.Created();

            return instantiate;
        }
    }
}