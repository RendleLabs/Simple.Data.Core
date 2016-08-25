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
            dynamic thing = Database.Open("Foo", typeof(DummyAdapter));
            Assert.NotNull(thing);
            Thing actual = thing.Bar;
            Assert.NotNull(actual);
            Assert.Equal("Bar", actual.Name);
        }

        [Fact]
        public void GetReturnsAQuery()
        {
            dynamic thing = Database.Open("Foo", typeof(DummyAdapter));
            GetByCommand q = thing.Characters.GetById(1);
            Assert.NotNull(q);
        }
    }

    internal class DummyAdapter : Adapter
    {
        public DummyAdapter(string _)
        {

        }
        public override Task Execute(DataContext context)
        {
            return Task.FromResult<object>(null);
        }

        public override void Dispose()
        {
        }
    }
}
