using Cores;
using Cores.Entities;
using Cores.Scenes.Workshops.Entities;
using Cysharp.Threading.Tasks;
using Models;
using Scenes.Workshops.Entities;
using Tools;
using UnityEngine;
using UnityEngine.Pool;

namespace Scenes.Workshops.Models
{
    public class MoldCtlr : MonoBehaviour,
        IMoldFlow, IMoldAction
    {
        [SerializeField] private MoldInputCtlr inputCtlr;

        [Space] [SerializeField] private FramesCtlr framesCtlr;
        [SerializeField] private ToolsCtlr toolsCtlr;

        private InputHandler inputHandler;
        private MoldObserver moldObserver;

        private Mold mold;

        private void OnEnable()
        {
            inputHandler = new InputHandler(this);
            moldObserver = new MoldObserver(this);
        }

        private void OnDisable()
        {
            inputHandler = null;
        }

        public async UniTask LoadMold(Mold mold)
        {
            this.mold = mold;

            inputCtlr.inputHandler = inputHandler;
            mold.AddObserver(moldObserver);
        }

        public async UniTask UnloadMold()
        {
            inputCtlr.inputHandler = null;
            mold.RemoveObserver(moldObserver);
        }

        private Vector2Int Position2Pos(Vector2 position)
        {
            return new(
                (position.x + (mold.Size.x - 1) / 2f).ClampInt(),
                (position.y + (mold.Size.y - 1) / 2f).ClampInt()
            );
        }

        public Vector2 Pos2Position(Vector2Int pos) => new(pos.x - (mold.Size.x - 1) / 2f, pos.y - (mold.Size.y - 1) / 2f);


        private class InputHandler : IMoldInputHandler
        {
            private readonly MoldCtlr moldCtlr;

            private readonly IObjectPool<IndicatorCtlr> indicatorCtlrPool;
            private IndicatorCtlr currIndicatorCtlr;

            public InputHandler(MoldCtlr moldCtlr)
            {
                this.moldCtlr = moldCtlr;

                indicatorCtlrPool = new ObjectPool<IndicatorCtlr>(
                    () => IndicatorCtlr.Generate(this.moldCtlr, indicatorCtlrPool),
                    ctlr => ctlr.Acquired(),
                    ctlr => ctlr.Released(),
                    ctlr => ctlr.Destroyed(),
                    defaultCapacity: 4,
                    maxSize: 16
                );
            }

            public void OnPress(Vector3 position)
            {
                var pos = moldCtlr.Position2Pos(position);

                currIndicatorCtlr = indicatorCtlrPool.Get();
                currIndicatorCtlr.Appear(pos);

                var currentFrame = moldCtlr.framesCtlr.CurrentFrame;
                var currentToolType = moldCtlr.toolsCtlr.CurrentToolType;

                if (currentToolType == ToolType.Ground)
                {
                    Debug.Log("Insert ground pos:" + pos);
                    moldCtlr.mold.Insert(pos.x, pos.y, new Ground(currentFrame));
                }
            }

            public void OnMove(Vector3 position)
            {
                var pos = moldCtlr.Position2Pos(position);

                if (currIndicatorCtlr.Pos == pos) return;
                currIndicatorCtlr.Disappear();
                currIndicatorCtlr = indicatorCtlrPool.Get();
                currIndicatorCtlr.Appear(pos);
            }

            public void OnRelease(Vector3 position)
            {
                currIndicatorCtlr.Disappear();
                currIndicatorCtlr = null;
            }
        }

        private class MoldObserver : IObserver<Mold.IUpdater>, Mold.IUpdater
        {
            private readonly MoldCtlr moldCtlr;

            public MoldObserver(MoldCtlr moldCtlr)
            {
                this.moldCtlr = moldCtlr;

                Updater = this;
            }

            public Mold.IUpdater Updater { get; }

            public void OnTileInserted(Tile tile)
            {
                moldCtlr.InsertTile(tile);
            }
        }

        private void InsertTile(Tile tile)
        {
            if (tile is Ground ground)
            {
                GroundCtlr.Generate(ground, this);
            }
        }
    }

    public interface IMoldFlow
    {
        UniTask LoadMold(Mold mold);
        UniTask UnloadMold();
    }

    public interface IMoldAction { }
}