using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phase3.Models
{
    public class CustomerModel
    {
        [Key]
        public int Id { get; set; }

        public string CName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Mobile_no { get; set; }
        public string Password { get; set; }
    }
}
