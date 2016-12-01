using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Simple.Data.Core.Postgres.IntegrationTests.Insert
{
    public class InsertTests : TestBase
    {
        [Fact]
        public async Task InsertReturnsRecord()
        {
            var db = await Target();
            const string text = "Marvin";
            var time = DateTimeOffset.UtcNow;
            var record = await db.SingleInsertTest.Insert(new {Text = text, Time = time});

            Assert.NotEqual(0, record.Id);
            Assert.Equal(text, record.Text);
            Assert.Equal(time, record.Time);
        }

        [Fact]
        public async Task InsertWithNamedParametersReturnsRecord()
        {
            var db = await Target();
            const string text = "Zaphod";
            var time = DateTimeOffset.UtcNow;
            var record = await db.SingleInsertTest.Insert(Text: text, Time: time);

            Assert.NotEqual(0, record.Id);
            Assert.Equal(text, record.Text);
            Assert.Equal(time, record.Time);
        }

        protected override string SqlFileName { get; } = "Insert.sql";
    }
}
