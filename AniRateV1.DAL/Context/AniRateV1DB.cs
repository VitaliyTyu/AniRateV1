using AniRateV1.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Context
{
    public class AniRateV1DB : DbContext
    {
        public DbSet<AnimeTitle> AnimeTitles { get; set; }
        public DbSet<AnimeCollection> AnimeCollections { get; set; }
        public AniRateV1DB(DbContextOptions<AniRateV1DB> options) : base(options) { }
    }
}
