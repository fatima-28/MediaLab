using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLab.ViewModels.DoctorsVM
{
    public class CreateDoctorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Image { get; set; }
        [Required(ErrorMessage ="This field is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Job { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This field is required!"),NotMapped]
        public IFormFile Photo { get; set; }
    }
}
