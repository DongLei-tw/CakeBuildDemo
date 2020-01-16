using System;
using System.Linq;
using CakeDemo.Models;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class NewSyntaxTests
    {
        private CSharpNewSyntax _target;

        [SetUp]
        public void Setup()
        {
            _target = new CSharpNewSyntax();
        }

        #region C#6 null-conditional operator

        [Test]
        public void ShouldReturnSumWhenListHasItems()
        {
            var list = new[] { 1, 2, 3 }.ToList();
            var actual = _target.GetSum(list);

            Assert.AreEqual(6, actual);
        }

        [Test]
        public void ShouldReturnNegativeWhenListIsNull()
        {
            var actual = _target.GetSum(null);

            Assert.AreEqual(-1, actual);
        }

        #endregion

        #region OutVariables
        [Test]
        public void NewFeature_On_OutVariables_WithoutDiscard()
        {
            var actual = _target.ReturnThePositionStringAccordingTheGivenData(1, 2);

            Assert.AreEqual("You are at (1, 2)", actual);
        }

        [TestCase("123", true)]
        [TestCase("abc", false)]
        public void NewFeature_On_OutVariables_WithDiscard(string numStr, bool expected)
        {
            var actual = _target.CanGivenStringParseToInteger(numStr);

            Assert.AreEqual(expected, actual);
        }

        #endregion OutVariables

        #region Discard

        [TestCase(3, 4, 4)]
        [TestCase(10, 20, 20)]
        public void NewFeature_On_TupleDiscard(int x, int y, int expected)
        {
            var actual = _target.ReturnTheYPositionForGivenData(x, y);

            Assert.AreEqual($"The Coordinate Y is {expected}", actual);
        }

        [Test]
        public void NewFeature_On_DiscardStandalone()
        {
            var actual = _target.ReturnThenPositionAfterMoved(3, 4, 20);

            Assert.AreEqual("You are at (3, 24)", actual);
        }

        [Test]
        public void NewFeature_On_DiscardStandalone_V2()
        {
            var actual = _target.ReturnThenPositionAfterMoved(3, 4, 5);

            Assert.AreEqual("You are at (8, 4)", actual);
        }

        #endregion

        #region ValueTuple - Deconstruction

        [Test]
        public void NewFeature_On_ValueTuple()
        {
            var actual = _target.ShouldReturnEachSideLengthOfATriangle(3, 4, 5);
            var expected = "triangle with sides: 3, 4, 5";

            Assert.AreEqual(expected, actual);
        }

        #endregion ValueTuple

        #region PatternMatching_Is

        [TestCase(null, "")]
        [TestCase("2", "")]
        [TestCase(3, "***")]
        [TestCase(5, "*****")]
        public void NewFeature_On_PatternMatching_Is(object obj, string expected)
        {
            var actual = _target.GetALengthOfStarsForGivenObject(obj);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NewFeature_On_PatternMatching_Is_Circle()
        {
            var shape = new Circle(3);
            var expected = "circle with radius 3";
            var actual = _target.GetDescriptionForGivenShape(shape);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NewFeature_On_PatternMatching_Is_Rectangle()
        {
            var shape = new Rectangle(3, 4);
            var expected = "3 x 4 rectangle";
            var actual = _target.GetDescriptionForGivenShape(shape);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NewFeature_On_PatternMatching_Is_Unknown()
        {
            var shape = new Triangle(3, 4, 5);
            var expected = "<unknown shape>";
            var actual = _target.GetDescriptionForGivenShape(shape);

            Assert.AreEqual(expected, actual);
        }

        #endregion PatternMatching_Is

        #region PatternMatching_Switch

        [Test]
        public void NewFeature_On_SwitchStatementsWithPatterns_Circle()
        {
            var shape = new Circle(2);

            var actual = _target.ReturnThePropertyOfGiveObject(shape);

            Assert.AreEqual("circle with radius 2", actual);
        }

        [Test]
        public void NewFeature_On_SwitchStatementsWithPatterns_Square()
        {
            var shape = new Rectangle(3, 3);

            var actual = _target.ReturnThePropertyOfGiveObject(shape);

            Assert.AreEqual("3 x 3 square", actual);
        }

        [Test]
        public void NewFeature_On_SwitchStatementsWithPatterns_Rectangle()
        {
            var shape = new Rectangle(4, 5);

            var actual = _target.ReturnThePropertyOfGiveObject(shape);

            Assert.AreEqual("4 x 5 rectangle", actual);
        }

        [Test]
        public void NewFeature_On_SwitchStatementsWithPatterns_Default()
        {
            var shape = new Triangle(3, 4, 5);

            var actual = _target.ReturnThePropertyOfGiveObject(shape);

            Assert.AreEqual("<unknown shape>", actual);
        }

        [Test]
        public void NewFeature_On_SwitchStatementsWithPatterns_Null()
        {
            Assert.Throws<ArgumentNullException>(() => _target.ReturnThePropertyOfGiveObject((object)null));
        }

        #endregion PatternMatching_Switch

        #region Local functions

        [Test]
        public void ShouldGetFibonacci()
        {
            var actual = _target.Fibonacci(5);

            Assert.AreEqual(8, actual);
        }

        [Test]
        public void ShouldThrowExceptionWhenAugmentLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => _target.Fibonacci(-1));
        }

        #endregion

        #region Ref returns and locals

        [Test]
        public void ShouldReturnTheRefOfGivenNumber()
        {
            int[] array = { 1, 15, -39, 0, 7, 14, -12 };

            // aliases 7's place in the array
            ref int place = ref _target.Find(7, array);

            // replaces 7 with 9 in the array
            place = 9;

            Assert.AreEqual(9, array[4]);
        }

        [Test]
        public void ShouldThrowExceptionWhenGivenNotIsGivenList()
        {
            int[] array = { 1, 15, -39, 0, 7, 14, -12 };

            ref var _ = ref _target.Find(7, array);

            Assert.Throws<IndexOutOfRangeException>(
                () =>
                {
                    ref var index = ref _target.Find(2, array);
                });
        }

        #endregion

        #region Throw expressions

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenArgNotGiven()
        {
            Assert.Throws<ArgumentNullException>(() => new Person(null));
        }

        [Test]
        public void ShouldGetPersonFirstName()
        {
            var person = new Person("CSharp Dev");

            Assert.AreEqual("CSharp", person.GetFirstName());
        }

        [Test]
        public void ShouldThrowNotImplementedExceptionWhenGetLastName()
        {
            var person = new Person(string.Empty);

            Assert.Throws<NotImplementedException>(() => person.GetLastName());
        }

        #endregion
    }
}
