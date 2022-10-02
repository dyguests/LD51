using Cores;
using Cores.Entities;
using UnityEngine;

namespace Models
{
    public class StartPointCtlr : TileCtlr
    {
        private static StartPointCtlr sPrefab;

        private Area<Tile> area;

        private AreaCtlr areaCtlr;

        private AreaObserver areaObserver;

        public static StartPointCtlr Generate(Area<Tile> area, AreaCtlr areaCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<StartPointCtlr>("Prefabs/Models/StartPoint");
            }

            var instantiate = Instantiate(sPrefab, areaCtlr.Pos2Position(area.StartPoint), Quaternion.identity, areaCtlr.transform);
            instantiate.name = "StartPoint";
            instantiate.area = area;
            instantiate.areaCtlr = areaCtlr;

            instantiate.Created();

            return instantiate;
        }

        private void OnEnable()
        {
            areaObserver = new AreaObserver(this);
        }

        private void Start()
        {
            area.AddObserver(areaObserver);
        }

        private void OnDisable()
        {
            area.RemoveObserver(areaObserver);
        }

        private class AreaObserver : IObserver<Area<Tile>.IUpdater>, Area<Tile>.IUpdater
        {
            private StartPointCtlr startPointCtlr;

            public AreaObserver(StartPointCtlr startPointCtlr)
            {
                this.startPointCtlr = startPointCtlr;
                Updater = this;
            }

            public Area<Tile>.IUpdater Updater { get; }

            public void OnStartPointChanged(Vector2Int oldStartPoint, Vector2Int newStartPoint)
            {
                startPointCtlr.transform.localPosition = startPointCtlr.areaCtlr.Pos2Position(newStartPoint);
            }
        }
    }
}