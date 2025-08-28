using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.ChecklistSections
{
    public class ChecklistSectionResponse
    {
        public int Id { get; set; }

        public List<SectionResponse>? Secciones { get; set; }
    }
}
