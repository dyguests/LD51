using System.IO;
using Cores;
using Cores.Entities;
using UnityEngine;

namespace Models
{
    public class GroundCtlr : TileCtlr
    {
        private static GroundCtlr sPrefab;

        [Space] [SerializeField] private BoxCollider2D cd;

        [Space] [SerializeField] private Sprite prePreviousSprite;
        [SerializeField] private Sprite previousSprite;
        [SerializeField] private Sprite currentSprite;
        [SerializeField] private Sprite keepSprite;

        private Ground ground;

        private AreaCtlr areaCtlr;

        private GroundObserver groundObserver;

        public static GroundCtlr Generate(Ground ground, AreaCtlr areaCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<GroundCtlr>("Prefabs/Models/Ground");
            }

            var instantiate = Instantiate(sPrefab, areaCtlr.Pos2Position(ground.Pos), Quaternion.identity, areaCtlr.transform);
            instantiate.name = "Ground" + ground.Pos;
            instantiate.ground = ground;
            instantiate.areaCtlr = areaCtlr;

            instantiate.Created();

            return instantiate;
        }

        private void OnEnable()
        {
            groundObserver = new GroundObserver(this);
        }

        private void Start()
        {
            ground.AddObserver(groundObserver);
        }

        private void OnDisable()
        {
            ground.RemoveObserver(groundObserver);
            groundObserver = null;
        }

        private class GroundObserver : IObserver<Ground.IUpdater>, Ground.IUpdater
        {
            private GroundCtlr groundCtlr;

            public GroundObserver(GroundCtlr groundCtlr)
            {
                this.groundCtlr = groundCtlr;
                Updater = this;
            }

            public Ground.IUpdater Updater { get; }

            public void OnFrameStateChanged(FrameState oldFrameState, FrameState newFrameState)
            {
                switch (newFrameState)
                {
                    case FrameState.None:
                    {
                        groundCtlr.gameObject.SetActive(false);
                        break;
                    }
                    case FrameState.PrePrevious:
                    {
                        groundCtlr.gameObject.SetActive(true);
                        groundCtlr.sr.sprite = groundCtlr.prePreviousSprite;
                        groundCtlr.cd.enabled = false;
                        break;
                    }
                    case FrameState.Previous:
                    {
                        groundCtlr.gameObject.SetActive(true);
                        groundCtlr.sr.sprite = groundCtlr.previousSprite;
                        groundCtlr.cd.enabled = false;
                        break;
                    }
                    case FrameState.Current:
                    {
                        groundCtlr.gameObject.SetActive(true);
                        groundCtlr.sr.sprite = groundCtlr.currentSprite;
                        groundCtlr.cd.enabled = true;
                        break;
                    }
                    case FrameState.Keep:
                    {
                        groundCtlr.gameObject.SetActive(true);
                        groundCtlr.sr.sprite = groundCtlr.keepSprite;
                        groundCtlr.cd.enabled = true;
                        break;
                    }
                    default: throw new InvalidDataException("error FrameState");
                }
            }
        }
    }
}