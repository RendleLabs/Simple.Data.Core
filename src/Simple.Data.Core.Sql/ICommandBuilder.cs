using System.Data.Common;

namespace Simple.Data.Core.Sql
{
    public interface ICommandBuilder
    {
        DbCommand Build();
    }
}