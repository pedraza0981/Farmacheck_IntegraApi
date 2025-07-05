using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Infrastructure.Models.SubBrands
{
    public class UpdateSubbrandRequest : SubbrandRequest
    {        
        public int Id { get; set; }

        public bool? Estatus { get; set; }
    }
}
