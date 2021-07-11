using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.SectionModel
{
    public class SectionDto
    {
        [Required]
        public string Name { get; set; }
        public int ParentProjectID { get; set; }
    }
}
