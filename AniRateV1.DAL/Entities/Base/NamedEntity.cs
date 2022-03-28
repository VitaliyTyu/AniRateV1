using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRateV1.DAL.Entities.Base
{
    public class NamedEntity : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}
