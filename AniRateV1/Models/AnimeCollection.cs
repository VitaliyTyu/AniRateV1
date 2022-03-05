using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.Models
{
    internal class AnimeCollection
    {
        public string? Name { get; set; }
        public IList<AnimeTitle>? AnimeTitles { get; set; } // IList для того, чтобы в поле могла быть, ObservableCollection
    }
}
