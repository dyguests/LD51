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
            if ((object) sPrefab == null)
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
            UpdateFrameState(ground.FrameState);

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
                groundCtlr.UpdateFrameState(newFrameState);
            }
        }

        private void UpdateFrameState(FrameState frameState)
        {
            switch (frameState)
            {
                case FrameState.None:
                {
                    gameObject.SetActive(false);
                    break;
                }
                case FrameState.PrePrevious:
                {
                    gameObject.SetActive(true);
                    sr.sprite = prePreviousSprite;
                    cd.enabled = false;
                    break;
                }
                case FrameState.Previous:
                {
                    gameObject.SetActive(true);
                    sr.sprite = previousSprite;
                    cd.enabled = false;
                    break;
                }
                case FrameState.Current:
                {
                    gameObject.SetActive(true);
                    sr.sprite = currentSprite;
                    cd.enabled = true;
                    break;
                }
                case FrameState.Keep:
                {
                    gameObject.SetActive(true);
                    sr.sprite = keepSprite;
                    cd.enabled = true;
                    break;
                }
                default: throw new InvalidDataException("error FrameState");
            }
        }
    }
}