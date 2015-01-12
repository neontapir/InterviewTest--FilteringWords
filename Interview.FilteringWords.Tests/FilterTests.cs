using System;
using System.Collections.Generic;
using System.Linq;
using Interview.FilteringWords.Domain;
using NUnit.Framework;

namespace Interview.FilteringWords.Tests
{
    [TestFixture]
    public class FilterTests
    {
        private IFilter _filter;

        [SetUp]
        public void SetUp()
        {
            _filter = new MyFilter(6);
        }

        [Test]
        [TestCase(0, Category = "Constructor", Description = "Can't be zero", ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(-1,  Category = "Constructor", Description = "Can't be below zero", ExpectedException = typeof(ArgumentOutOfRangeException))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_LengthLowerZero(int stringLength)
        {
            _filter = new MyFilter(stringLength);
        }

        [Test]
        public void Apply_ListOfStringsWithoutSixLetterString_EmptyList()
        {
            var listWithoutSixLetterStrings = new[] { "al", "aver", "bar" };
            var result = _filter.Apply(listWithoutSixLetterStrings);

            const int expectedAmountOfWords = 0;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }

        [Test]
        public void Apply_ListOfStringWithSixLetterStringButNotFormedBySmallerStrings_EmptyList()
        {
            var listWithSixLetterString = new[] { "al", "aver", "albums" };
            var result = _filter.Apply(listWithSixLetterString);

            const int expectedAmountOfWords = 0;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }

        [Test]
        public void Apply_ListOfStringsWithSixLetterStringAndFormedBySmallerString_OneSixLetterString()
        {
            var listOfStrings = new[] { "al", "albums", "befoul", "bums" };
            var result = _filter.Apply(listOfStrings);

            const int expectedAmountOfStrings = 1;
            Assert.AreEqual(expectedAmountOfStrings, result.Count());
            const string expectedSixLetterString = "albums";
            Assert.AreEqual(expectedSixLetterString, result.First());
        }

        [Test]
        public void Apply_NullListOfStrings_EmptyList()
        {
            IEnumerable<string> nullListOfStrings = null;
            var result = _filter.Apply(nullListOfStrings);

            const int expectedAmountOfWords = 0;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }

        [Test]
        public void Apply_StringSuffixUsedTwice_TwoStringsMatched()
        {
            string[] listOfWords =
            {
                "or", "tail", "sail", "tailor", "sailor"
            }; // Suffix == or
            var result = _filter.Apply(listOfWords);

            const int expectedAmountOfWords = 2;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }

        [Test]
        public void Apply_TwoFragmentsButIncompleteWord_NoStringsMatched()
        {
            string[] listOfWords =
            {
                "or", "ta", "tailor",
            }; // Fragments == or, ta
            var result = _filter.Apply(listOfWords);

            const int expectedAmountOfWords = 0;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }

        [Test]
        public void Apply_StringPrefixUsedTwice_TwoStringsMatched()
        {
            string[] listOfWords =
            {
                "belive", "befoul", "be", "foul", "live"
            }; //Prefix == be
            var result = _filter.Apply(listOfWords);

            const int expectedAmountOfWords = 2;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }

        /*
         * Extra test to validate all the example input from the requirements paper.
         */
        [Test]
        public void Apply_RequirementsListOfStrings_EightStringsMatched()
        {
            string[] listOfWords =
            {
                "al", "albums", "aver", "bar", "barely", "be", "befoul", "bums", "by", "cat", "con",
                "convex", "ely", "foul", "here", "hereby", "jig", "jigsaw", "or", "saw", "tail", "tailor", "vex", "we",
                "weaver"
            };
            var result = _filter.Apply(listOfWords);

            const int expectedAmountOfWords = 8;
            Assert.AreEqual(expectedAmountOfWords, result.Count());
        }
    }
}