using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Simple.Data.Core.SqlServer.IntegrationTests.Select
{
    public class GetByTests : TestBase
    {
        [Fact]
        public async Task GetsSingleRecord()
        {
            var db = await Target();
            var record = await db.GetByTest.GetById(1);
            Assert.Equal(1, record.Id);
            Assert.Equal("Pass", record.Text);
            Assert.Equal(new DateTimeOffset(1978, 3, 8, 0, 0, 0, TimeSpan.Zero), record.Time);
        }

        protected override string SqlFileName { get; } = "GetBy.sql";
    }
}