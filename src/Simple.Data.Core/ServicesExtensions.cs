using System;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.Data.Core
{
    public static class ServicesExtensions
    {
        public static IServiceCollection UseSimpleData(this IServiceCollection services)
        {
            return services.AddSingleton<ISimpleData, SimpleData>();
        }

        public static IServiceCollection UseSimpleData(this IServiceCollection services,
            Action<SimpleDataOptions> optionFunc)
        {
            return services.AddSingleton<ISimpleData>(_ =>
            {
                var options = new SimpleDataOptions();
                optionFunc(options);
                return new SimpleData(options);
            });
        }
    }
}