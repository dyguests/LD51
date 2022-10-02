using Cores;
using Cores.Entities;
using Cores.Scenes.Workshops.Entities;
using Cysharp.Threading.Tasks;
using Models;
using Scenes.Workshops.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Scenes.Workshops.Models
{
    public class MoldCtlr : AreaCtlr,
        IMoldFlow, IMoldAction
    {
        [SerializeField] private MoldInputCtlr inputCtlr;

        [Space] [SerializeField] private FramesCtlr framesCtlr;
        [SerializeField] private ToolsCtlr toolsCtlr;
        [SerializeField] private LengthCtlr lengthCtlr;

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

        protected override Vector2Int Size => mold.Size;

        public async UniTask LoadMold(Mold mold)
        {
            this.mold = mold;

            var size = mold.Size;
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var tileRings = mold.GetRing(x, y);
                    if (tileRings == null) continue;
                    foreach (var tile in tileRings.Values)
                    {
                        InsertTile(tile);
                        // await UniTask.DelayFrame(1);
                    }
                }
            }

            StartPointCtlr.Generate(mold, this);
            EndPointCtlr.Generate(mold, this);

            await UniTask.Delay(250);

            inputCtlr.inputHandler = inputHandler;
            mold.AddObserver(moldObserver);
        }

        public async UniTask UnloadMold()
        {
            inputCtlr.inputHandler = null;
            mold.RemoveObserver(moldObserver);
        }

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

                Insert(pos);
            }

            public void OnMove(Vector3 position)
            {
                var pos = moldCtlr.Position2Pos(position);

                if (currIndicatorCtlr == null || currIndicatorCtlr.Pos == pos) return;
                currIndicatorCtlr.Disappear();
                currIndicatorCtlr = indicatorCtlrPool.Get();
                currIndicatorCtlr.Appear(pos);

                Insert(pos);
            }

            public void OnRelease(Vector3 position)
            {
                if (currIndicatorCtlr == null) return;
                currIndicatorCtlr.Disappear();
                currIndicatorCtlr = null;
            }

            private void Insert(Vector2Int pos)
            {
                var currentFrame = moldCtlr.framesCtlr.CurrentFrame;
                var currentToolType = moldCtlr.toolsCtlr.CurrentToolType;
                var frameLength = moldCtlr.lengthCtlr.FrameLength;

                Insert(pos, currentFrame, frameLength, currentToolType);
            }

            private void Insert(Vector2Int pos, int frameStart, int frameLength, ToolType toolType)
            {
                if (toolType == ToolType.StartPoint)
                {
                    if (pos == moldCtlr.mold.StartPoint)
                    {
                        return;
                    }

                    moldCtlr.mold.StartPoint = pos;

                    return;
                }
                else if (toolType == ToolType.EndPoint)
                {
                    if (pos == moldCtlr.mold.EndPoint)
                    {
                        return;
                    }

                    moldCtlr.mold.EndPoint = pos;

                    return;
                }

                if (pos == moldCtlr.mold.StartPoint || pos == moldCtlr.mold.EndPoint)
                {
                    return;
                }

                if (toolType == ToolType.Clear)
                {
                    // var tile = moldCtlr.mold.Get(pos.x, pos.y, frameStart);
                    moldCtlr.mold.Remove(pos.x, pos.y, frameStart, frameLength);
                    return;
                }

                if (toolType == ToolType.Ground)
                {
                    Debug.Log("Insert ground pos:" + pos);
                    var tile = moldCtlr.mold.Get(pos.x, pos.y, frameStart);
                    if (tile is Ground ground)
                    {
                        if (ground.Frames.start == frameStart && ground.Frames.length == frameLength)
                        {
                            return;
                        }
                    }

                    moldCtlr.mold.Insert(pos.x, pos.y, new Ground(frameStart, frameLength));
                    return;
                }
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
                tile.UpdateFrameState(mold.CurrentFrame, mold.FrameLength);
                var groundCtlr = GroundCtlr.Generate(ground, this);
                groundCtlr.AddComponent<MoldElementCtlr>();
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