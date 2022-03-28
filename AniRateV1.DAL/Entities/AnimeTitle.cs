using AniRateV1.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Entities
{
    public class AnimeTitle : NamedEntity
    {
        public string Description { get; set; }
        [Required]
        public int Rating { get; set; }

        public virtual ICollection<AnimeCollection> AnimeCollections { get; set; } = new HashSet<AnimeCollection>();
    }
}
