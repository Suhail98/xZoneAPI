using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace xZoneAPI.Models.ProjectModel
{
    public class ProjectWithNoId
    {

        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; }


        public int? OwnerID { get; set; }


    }
}
