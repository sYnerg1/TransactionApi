using LegioTest.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using LegioTest.Domain.Services.Defaults;

namespace LegioTest.Domain
{
   public  static class DependencyLoader
    {
        public static void Load(this IServiceCollection services)
        {
            Data.DependencyLoader.Load(services);
            services.AddScoped<ITransactionService,TransctionService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
