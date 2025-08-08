using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.SubBrands
{
    public class UpdateSubbrandRequest : SubbrandRequest
    {
        public int Id { get; set; }
    }
}
