using AniRateV1.DAL.Context;
using AniRateV1.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Repositories
{
    class AnimeTitlesRepository : DbRepository<AnimeTitle>
    {
        public override IQueryable<AnimeTitle> Items => base.Items.Include(item => item.AnimeCollections);


        public AnimeTitlesRepository(AniRateV1DB db) : base(db)
        {
        }
    }
}
