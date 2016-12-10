using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Simple.Data.Core.Postgres.IntegrationTests.Insert
{
    public class InsertTests : TestBase
    {
        private static readonly DateTimeOffset Time = new DateTimeOffset(1978, 3, 8, 18, 0, 0, TimeSpan.Zero);

        [Fact]
        public async Task InsertReturnsRecord()
        {
            var db = await Target();
            const string text = "Marvin";
            var record = await db.SingleInsertTest.Insert(new {Text = text, Time});

            Assert.NotEqual(0, record.Id);
            Assert.Equal(text, record.Text);
            Assert.Equal(Time, record.Time);
        }

        [Fact]
        public async Task InsertWithNamedParametersReturnsRecord()
        {
            var db = await Target();
            const string text = "Zaphod";
            var record = await db.SingleInsertTest.Insert(Text: text, Time: Time);

            Assert.NotEqual(0, record.Id);
            Assert.Equal(text, record.Text);
            Assert.Equal(Time, record.Time);
        }

        protected override string SqlFileName { get; } = "Insert.sql";
    }
}
