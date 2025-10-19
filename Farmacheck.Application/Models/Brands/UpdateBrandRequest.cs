using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Brands
{
    public class UpdateBrandRequest : BrandRequest
    {
        public int Id { get; set; }

        public bool? Estatus { get; set; }
    }
}
