using Cores.Entities;
using Scenes.Workshops.Models;
using UnityEngine;

namespace Models
{
    public class GroundCtlr : TileCtlr
    {
        private static GroundCtlr sPrefab;

        private Ground ground;

        private MoldCtlr moldCtlr;

        public static GroundCtlr Generate(Ground ground, MoldCtlr moldCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<GroundCtlr>("Prefabs/Models/Ground");
            }

            var instantiate = Instantiate(sPrefab, moldCtlr.Pos2Position(ground.Pos), Quaternion.identity, moldCtlr.transform);
            instantiate.name = "Indicator";
            instantiate.ground = ground;
            instantiate.moldCtlr = moldCtlr;

            instantiate.Created();

            return instantiate;
        }
    }
}