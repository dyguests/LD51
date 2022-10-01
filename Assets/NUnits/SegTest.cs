using Cores;
using NUnit.Framework;

namespace NUnits
{
    [TestFixture]
    public class SegTest
    {
        [Test]
        public void TestNew()
        {
            var seg = new Seg();
            Assert.AreEqual(Seg.Empty, seg);
        }

        [Test]
        public void TestNew2()
        {
            var seg = new Seg(1);
            Assert.AreEqual(new Seg(1, 1), seg);
        }

        [Test]
        public void TestSub1()
        {
            var seg = new Seg(1);
            Assert.AreEqual(Seg.Empty, seg - seg);
        }

        [Test]
        public void TestSub2()
        {
            var l = new Seg(1, 2);
            var r = new Seg(2, 1);

            Assert.AreEqual(new Seg(1, 1), l - r);
        }

        [Test]
        public void TestSub3()
        {
            var l = new Seg(1, 2);
            var r = new Seg(2, 2);

            Assert.AreEqual(new Seg(1, 1), l - r);
        }

        [Test]
        public void TestSub4()
        {
            var l = new Seg(1, 2);
            var r = new Seg(0, 2);

            Assert.AreEqual(new Seg(2, 1), l - r);
        }

        [Test]
        public void TestSub4_2()
        {
            var l = new Seg(1, 2);
            var r = new Seg(1, 1);

            Assert.AreEqual(new Seg(2, 1), l - r);
        }

        [Test]
        public void TestSub5()
        {
            var l = new Seg(1, 4);
            var r = new Seg(2, 2);

            Assert.AreEqual(new Seg(1, 1), l - r);
        }

        [Test]
        public void TestSub6()
        {
            var l = new Seg(1, 4);
            var r = new Seg(2, 2);

            Assert.AreEqual(Seg.Empty, r - l);
        }

        [Test]
        public void TestSub7()
        {
            var l = new Seg(0, 8);
            var r = new Seg(2, 2);

            Assert.AreEqual(new Seg(0, 2), l - r);
        }

        [Test]
        public void TestSub8()
        {
            var l = new Seg(0, 8);
            var r = new Seg(2, 2);

            Assert.AreEqual(Seg.Empty, r - l);
        }

        [Test]
        public void TestSub9()
        {
            var l = new Seg(0, 6);
            var r = new Seg(5, 4);

            Assert.AreEqual(new Seg(1, 4), l - r);
        }

        [Test]
        public void TestSub10()
        {
            var l = new Seg(5, 6);
            var r = new Seg(7, 2);

            Assert.AreEqual(new Seg(5, 2), l - r);
        }

        [Test]
        public void TestSub11()
        {
            var l = new Seg(5, 6);
            var r = new Seg(0, 1);

            Assert.AreEqual(new Seg(5, 3), l - r);
        }

        [Test]
        public void TestSub12()
        {
            var l = new Seg(5, 6);
            var r = new Seg(1, 1);

            Assert.AreEqual(new Seg(5, 4), l - r);
        }
    }
}