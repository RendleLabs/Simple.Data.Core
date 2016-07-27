using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core;
using Simple.Data.Core.Commands;
using Xunit;

namespace Simple.Data.Core.Tests
{
    public class ThingTest
    {
        [Fact]
        public void ReturnsAMemberWithCorrectNameAndParent()
        {
            dynamic thing = Database.Open("Foo");
            Assert.NotNull(thing);
            Thing actual = thing.Bar;
            Assert.NotNull(actual);
            Assert.Equal("Bar", actual.Name);
            Assert.Equal("Foo", actual.Parent.Name);
        }

        [Fact]
        public void GetReturnsAQuery()
        {
            dynamic thing = Database.Open("Foo");
            Query q = thing.Characters.Get(1);
            Assert.NotNull(q);
        }
    }
}
