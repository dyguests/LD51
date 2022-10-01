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
            mold.Insert(0, 0, new Ground(0));

            mold.Get(0, 0, 0);
        }

        [UnityTest]
        public IEnumerator MoldTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}