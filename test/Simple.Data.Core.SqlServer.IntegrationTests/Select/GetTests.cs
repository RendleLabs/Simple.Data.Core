using System;
using System.Threading.Tasks;
using Xunit;

namespace Simple.Data.Core.SqlServer.IntegrationTests.Select
{
    public class GetTests : TestBase
    {
        [Fact]
        public async Task GetsUsingNamedParameter()
        {
            var db = await Target();
            var record = await db.GetTest.Get(id: 1);
            Assert.Equal(1, record.Id);
            Assert.Equal("Pass", record.Text);
            Assert.Equal(new DateTimeOffset(1978, 3, 8, 0, 0, 0, TimeSpan.Zero), record.Time);
        }

        [Fact]
        public async Task GetsUsingAnonymousObject()
        {
            var db = await Target();
            var record = await db.GetTest.Get(id: 1);
            Assert.Equal(1, record.Id);
            Assert.Equal("Pass", record.Text);
            Assert.Equal(new DateTimeOffset(1978, 3, 8, 0, 0, 0, TimeSpan.Zero), record.Time);
        }

        protected override string SqlFileName { get; } = "Get.sql";
    }
}