using Cores;
using Cores.Entities;
using Scenes.Games;
using UnityEngine;

namespace Models
{
    public class EndPointCtlr : TileCtlr
    {
        private static EndPointCtlr sPrefab;

        [Space] [SerializeField] private BoxCollider2D cd;

        private Area<Tile> area;

        private AreaCtlr areaCtlr;

        private AreaObserver areaObserver;

        public static EndPointCtlr Generate(Area<Tile> area, AreaCtlr areaCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<EndPointCtlr>("Prefabs/Models/EndPoint");
            }

            var instantiate = Instantiate(sPrefab, areaCtlr.Pos2Position(area.EndPoint), Quaternion.identity, areaCtlr.transform);
            instantiate.name = "EndPoint";
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
            private readonly EndPointCtlr endPointCtlr;

            public AreaObserver(EndPointCtlr endPointCtlr)
            {
                this.endPointCtlr = endPointCtlr;
                Updater = this;
            }

            public Area<Tile>.IUpdater Updater { get; }

            public void OnEndPointChanged(Vector2Int oldEndPoint, Vector2Int newEndPoint)
            {
                endPointCtlr.transform.localPosition = endPointCtlr.areaCtlr.Pos2Position(newEndPoint);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("OnTriggerEnter2D other:" + other.gameObject.name);

            if (!other.gameObject.CompareTag("Player")) return;

            cd.enabled = false;

            GameCtlr.Instance.WinGame();
        }
    }
}