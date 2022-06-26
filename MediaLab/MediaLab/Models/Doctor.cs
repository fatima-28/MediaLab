using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLab.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Image { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        
        [Required(ErrorMessage = "This field is required!"), NotMapped]
        public IFormFile Photo { get; set; }
    }
}
