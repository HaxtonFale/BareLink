using BareLink.Models;
using NUnit.Framework;
// ReSharper disable StringLiteralTypo

namespace BareLink.Test
{
    [TestFixture]
    public class FilterTest
    {
        [Test]
        [TestCase(arguments: "test", ExpectedResult = true)]
        [TestCase(arguments: "testtest", ExpectedResult = false)]
        [TestCase(arguments: "", ExpectedResult = false)]
        public bool TryMatchTest(string input)
        {
            var filter = new Filter
            {
                Id = 1,
                Name = "Test filter",
                Pattern = @"^test$"
            };
            return filter.TryMatch(input, out _);
        }

        [Test]
        [TestCase(arguments: "test", ExpectedResult = "es")]
        [TestCase(arguments: "tett", ExpectedResult = null)]
        [TestCase(arguments: "testtest", ExpectedResult = "es")]
        [TestCase(arguments: "", ExpectedResult = null)]
        public string MatchResultTest(string input)
        {
            var filter = new Filter
            {
                Id = 1,
                Name = "Test filter",
                Pattern = @".s"
            };
            filter.TryMatch(input, out var output);
            return output;
        }
    }
}
