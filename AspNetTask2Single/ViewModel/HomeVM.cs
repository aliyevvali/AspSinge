using AspNetTask2Single.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.ViewModel
{
    public class HomeVM
    {
        public List<Skills> skills { get; set; }
        public List<List> lists { get; set; }
        public List<Interests> interests { get; set; }
        public List<Experiences> experiences { get; set; }
        public List<Education> educations { get; set; }
        public List<AwardsAndCertifications> awards { get; set; }
        public List<AboutMe> aboutMes { get; set; }
    }
}
