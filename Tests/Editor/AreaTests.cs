using NUnit.Framework;

namespace Software10101.Units {
    public class AreaTests {
        [Test]
        public void TestAddition1() {
            Assert.AreEqual(
                Area.From(2.0),
                Area.From(1.0) + Area.From(1.0),
                float.Epsilon);
        }

        [Test]
        public void TestAddition2() {
            Assert.AreEqual(
                Area.From(2.0),
                Area.From(1.0) + 1.0,
                float.Epsilon);
        }

        [Test]
        public void TestAddition3() {
            Assert.AreEqual(
                Area.From(2.0),
                1.0 + Area.From(1.0),
                float.Epsilon);
        }

        [Test]
        public void TestSubtraction1() {
            Assert.AreEqual(
                Area.From(6.0),
                Area.From(8.0) - Area.From(2.0),
                float.Epsilon);
        }

        [Test]
        public void TestSubtraction2() {
            Assert.AreEqual(
                Area.From(6.0),
                Area.From(8.0) - 2.0,
                float.Epsilon);
        }

        [Test]
        public void TestSubtraction3() {
            Assert.AreEqual(
                Area.From(6.0),
                8.0 - Area.From(2.0),
                float.Epsilon);
        }

        [Test]
        public void TestMultiplication1() {
            Assert.AreEqual(
                Area.From(10.0),
                Area.From(5.0) * 2.0,
                float.Epsilon);
        }

        [Test]
        public void TestMultiplication2() {
            Assert.AreEqual(
                Area.From(10.0),
                5.0 * Area.From(2.0),
                float.Epsilon);
        }

        [Test]
        public void TestMultiplication3() {
            Assert.AreEqual(
                Volume.From(10.0),
                Area.From(2.0) * Length.From(5.0),
                float.Epsilon);
        }

        [Test]
        public void TestDivision1() {
            Assert.AreEqual(
                Area.From(3.0),
                Area.From(6.0) / 2.0,
                float.Epsilon);
        }

        [Test]
        public void TestDivision2() {
            Assert.AreEqual(
                2.0,
                Area.From(6.0) / Area.From(3.0),
                float.Epsilon);
        }

        [Test]
        public void TestDivision3() {
            Assert.AreEqual(
                Length.From(2.0),
                Area.From(6.0) / Length.From(3.0),
                float.Epsilon);
        }

        [Test]
        public void TestEqualsWithDelta() {
            Assert.True(Area.From(6.0).Equals(Area.From(6.0), Area.From(float.Epsilon)));
            Assert.False(Area.From(6.0).Equals(Area.From(7.0), Area.From(float.Epsilon)));
        }

        [Test]
        public void TestTo() {
            Assert.AreEqual(
                2.0,
                Area.From(6.0).To(Area.From(3.0)),
                float.Epsilon);
        }

        [Test]
        public void TestFrom() {
            Assert.AreEqual(
                12.0,
                Area.From(6.0, Area.From(2.0)),
                float.Epsilon);
        }
    }
}
