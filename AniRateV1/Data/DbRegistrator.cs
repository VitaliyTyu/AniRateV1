using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniRateV1.DAL.Context;
using AniRateV1.DAL.Repositories;

namespace AniRateV1.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration Configuration) => services
            .AddDbContext<AniRateV1DB>(opt => opt.UseSqlServer(Configuration.GetConnectionString(Configuration["Type"])))
            .AddTransient<DbInitializer>()
            .AddRepositories()
            ;
    }
}
