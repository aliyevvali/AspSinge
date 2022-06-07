using AspNetTask2Single.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<AboutMe> AboutMes { get; set; }
        public DbSet<AwardsAndCertifications> AwardsAndCertifications { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<Interests> Interests { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<List> Lists { get; set; }

    }
}
