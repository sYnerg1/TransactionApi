using LegioTest.Data.Repositories;
using LegioTest.Data.Repositories.Defaults;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Data
{
    public class DependencyLoader
    {
        public static void Load(IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();

        }
    }
}
