using NUnit.Framework;

namespace Software10101.Units {
    public class AreaTests {
        [Test]
        public void TestAddition1() {
            Assert.AreEqual(
                2.0,
                (Area.From(1.0, Area.SquareKilometer) + Area.From(1.0, Area.SquareKilometer))
                    .To(Area.SquareKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestSubtraction1() {
            Assert.AreEqual(
                6.0,
                (Area.From(8.0, Area.SquareKilometer) - Area.From(2.0, Area.SquareKilometer))
                    .To(Area.SquareKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestMultiplication1() {
            Assert.AreEqual(
                10.0,
                (Area.From(5.0, Area.SquareKilometer) * 2.0)
                    .To(Area.SquareKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestMultiplication2() {
            Assert.AreEqual(
                10.0,
                (5.0 * Area.From(2.0, Area.SquareKilometer))
                    .To(Area.SquareKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestMultiplication3() {
            Assert.AreEqual(
                10.0,
                (Area.From(2.0, Area.SquareKilometer) * Length.From(5.0, Length.Kilometer))
                    .To(Volume.CubicKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestDivision1() {
            Assert.AreEqual(
                3.0,
                (Area.From(6.0, Area.SquareKilometer) / 2.0)
                    .To(Area.SquareKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestDivision2() {
            Assert.AreEqual(
                2.0,
                Area.From(6.0, Area.SquareKilometer) / Area.From(3.0, Area.SquareKilometer),
                float.Epsilon);
        }

        [Test]
        public void TestDivision3() {
            Assert.AreEqual(
                2.0,
                (Area.From(6.0, Area.SquareKilometer) / Length.From(3.0, Length.Kilometer))
                    .To(Length.Kilometer),
                float.Epsilon);
        }

        [Test]
        public void TestEqualsWithDelta() {
            Assert.True(Area.From(6.0, Area.SquareKilometer).Equals(
                Area.From(6.0, Area.SquareKilometer),
                Area.From(float.Epsilon, Area.SquareKilometer)));
            Assert.False(Area.From(6.0, Area.SquareKilometer).Equals(
                Area.From(7.0, Area.SquareKilometer),
                Area.From(float.Epsilon, Area.SquareKilometer)));
        }

        [Test]
        public void TestTo() {
            Assert.AreEqual(
                2.0,
                Area.From(6.0, Area.SquareKilometer).To(Area.From(3.0, Area.SquareKilometer)),
                float.Epsilon);
        }

        [Test]
        public void TestFrom() {
            Assert.AreEqual(
                12.0,
                Area.From(6.0, Area.From(2.0, Area.SquareKilometer)).To(Area.SquareKilometer),
                float.Epsilon);
        }
    }
}
