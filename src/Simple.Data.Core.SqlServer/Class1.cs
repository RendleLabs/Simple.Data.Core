using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Sql;

namespace Simple.Data.Core.SqlServer
{
    public class SqlServerAdapter : Adapter
    {
        private readonly CriteriaHelper _criteriaHelper = new CriteriaHelper();

        public override Task Execute(DataContext context)
        {
            
        }

        private Task ExecuteGet(DataContext context)
        {
            var command = (GetCommand)context.Request.Command;

            _criteriaHelper.BinaryExpressionToWherePart(()command.Criteria)
        }
    }
}
