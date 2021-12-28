using System.Collections.Generic;
using NUnit.Framework;

namespace BareLink.Test
{
    [TestFixture]
    public class FilterTest
    {
        private List<Filter> _filters;
        [SetUp]
        public void SetUp()
        {
            _filters = new List<Filter>
            {
                new Filter
                {
                    Id = 1,
                    Name = "Test filter",

                }
            };
        }

        [Test]
        public void BasicFilterTest()
        {
        }
    }
}
