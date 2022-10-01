using System.Collections;
using Cores.Scenes.Workshops.Entities;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace NUnits
{
    public class MoldTest
    {
        [Test]
        public void MoldTestSimplePasses()
        {
            var mold = new Mold();
        }

        [UnityTest]
        public IEnumerator MoldTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}