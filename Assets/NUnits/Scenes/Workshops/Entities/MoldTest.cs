using System.Collections;
using Cores;
using Cores.Entities;
using Cores.Scenes.Workshops.Entities;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace NUnits.Scenes.Workshops.Entities
{
    public class MoldTest
    {
        private MoldObserver moldObserver;
        private Tile tile;

        [SetUp]
        public void SetUp()
        {
            moldObserver = new MoldObserver(this);
            tile = null;
        }

        [TearDown]
        public void TearDown()
        {
            moldObserver = null;
            tile = null;
        }

        [Test]
        public void NewMold()
        {
            var mold = new Mold();
            var ground = new Ground(0);
            mold.Insert(0, 0, ground);

            var tile = mold.Get(0, 0, 0);

            Assert.AreEqual(ground, tile);
        }

        [Test]
        public void InsertMold()
        {
            var mold = new Mold();
            mold.AddObserver(moldObserver);

            var ground = new Ground(0);

            Assert.IsNull(tile);
            mold.Insert(0, 0, ground);
            Assert.AreEqual(ground, tile);

            mold.RemoveObserver(moldObserver);
        }

        [UnityTest]
        public IEnumerator MoldTestWithEnumeratorPasses()
        {
            yield return null;
        }


        private void OnTileInserted(Tile tile)
        {
            this.tile = tile;
        }

        private class MoldObserver : IObserver<Mold.IUpdater>, Mold.IUpdater
        {
            private MoldTest moldTest;

            public MoldObserver(MoldTest moldTest)
            {
                this.moldTest = moldTest;

                Updater = this;
            }

            public Mold.IUpdater Updater { get; }

            public void OnTileInserted(Tile tile)
            {
                moldTest.OnTileInserted(tile);
            }
        }
    }
}