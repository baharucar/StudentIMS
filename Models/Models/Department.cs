using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Department")]
        public string Depart { get; set; }
    }
}
