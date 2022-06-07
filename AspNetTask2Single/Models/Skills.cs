using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string WorkFlow { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo{ get; set; }
    }
}
