using System;
using System.Collections.Generic;
using System.Linq;
using Simple.Data.Core;
using Simple.Data.Core.Commands;
using Xunit;

namespace Simple.Data.Core.Tests
{
    public class ThingTest
    {
        private static readonly dynamic Thing = new SimpleData().Open("Foo", typeof(DummyAdapter));

        [Fact]
        public void ReturnsAMemberWithCorrectNameAndParent()
        {
            Assert.NotNull(Thing);
            Thing actual = Thing.Bar;
            Assert.NotNull(actual);
            Assert.Equal("Bar", actual.Name);
        }

        [Fact]
        public void GetByReturnsAQuery()
        {
            GetByCommand q = Thing.Characters.GetById(1);
            Assert.NotNull(q);
        }

        [Fact]
        public void GetWithNamedParametersReturnsAQuery()
        {
            GetCommand q = Thing.Characters.Get(id: 1);
            Assert.NotNull(q);
        }
    }
}
