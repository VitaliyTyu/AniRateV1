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
    class AnimeCollectionsRepository : DbRepository<AnimeCollection>
    {
        public override IQueryable<AnimeCollection> Items => base.Items.Include(item => item.AnimeTitles);

        public AnimeCollectionsRepository(AniRateV1DB db) : base(db)
        {
        }
    }
}
