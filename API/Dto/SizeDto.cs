using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Dto
{
    
    public class SizeDto
    {
        [Required]
        public string? SizeOfProduct { get; set; }

       public List<ProductSizeDto>? ProductSizes { get; set; }

    
    }

    public class UpdateSizeDto: SizeDto {
        public int Id { get; set; }
    }
}