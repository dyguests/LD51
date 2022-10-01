using System.Collections;
using Cores.Entities;
using Cores.Scenes.Workshops.Entities;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace NUnits.Scenes.Workshops.Entities
{
    public class MoldTest
    {
        [Test]
        public void NewMold()
        {
            var mold = new Mold();
            var ground = new Ground(0);
            mold.Insert(0, 0, ground);

            var tile = mold.Get(0, 0, 0);

            Assert.AreEqual(ground, tile);
        }

        [UnityTest]
        public IEnumerator MoldTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}