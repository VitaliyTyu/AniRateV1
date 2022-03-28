using AniRateV1.DAL.Entities;
using AniRateV1.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IRepository<AnimeTitle>, AnimeTitlesRepository>()
            .AddTransient<IRepository<AnimeCollection>, AnimeCollectionsRepository>()
            ;
    }
}
