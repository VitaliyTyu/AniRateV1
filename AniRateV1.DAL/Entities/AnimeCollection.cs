using AniRateV1.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Entities
{
    public class AnimeCollection : NamedEntity
    {
        public virtual ICollection<AnimeTitle> AnimeTitles { get; set; } = new HashSet<AnimeTitle>();// IList для того, чтобы в поле могла быть, ObservableCollection
    }
}
