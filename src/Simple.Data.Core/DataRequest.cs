using System;
using Simple.Data.Core.Commands;

namespace Simple.Data.Core
{
    public class DataRequest
    {
        public DataRequest(DataContext context)
        {
            Context = context;
        }

        public DataContext Context { get; }
        public CommandBase Command { get; set; }
    }

    public class DataResponse : IDisposable
    {
        public DataResponse(DataContext context)
        {
            Context = context;
        }

        public DataContext Context { get; }
        public object Result { get; set; }
        public void Dispose()
        {
            (Result as IDisposable)?.Dispose();
        }
    }

    public class DataContext
    {
        public DataContext()
        {
            Request = new DataRequest(this);
            Response = new DataResponse(this);
        }

        public DataRequest Request { get; }
        public DataResponse Response { get; }
    }
}