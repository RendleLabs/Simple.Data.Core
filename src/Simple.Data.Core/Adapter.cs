using System.Threading.Tasks;

namespace Simple.Data.Core
{
    public abstract class Adapter
    {
        public abstract Task Execute(DataContext context);
    }
}