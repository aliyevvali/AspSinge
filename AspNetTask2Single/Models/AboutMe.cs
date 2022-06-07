using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.Models
{
    public class AboutMe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Title { get; set; }
        public string Contact { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo{ get; set; }
    }
}
