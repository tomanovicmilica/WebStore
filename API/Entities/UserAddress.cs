using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class UserAddress : Address
    {
        [Key]
        public int Id { get; set; }
    }
}