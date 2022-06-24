
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS_project.Models;
using HRMS_Portal.Models;
using HRMS.Models;



namespace HRMS_project.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions<AppDbContext> _options;


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }
        
        public DbSet<Country> country { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<City> cities { get; set; }

        public DbSet<PostalCode> postalCode { get; set; }

        public DbSet<Bunit> bunits { get; set; }
        public  DbSet<Dept> dept { get; set; }
        public DbSet<SDept> sDepts { get; set; }
        public DbSet<RM> rMs { get; set; }
        public DbSet<RequestModel> requestModels { get; set; }
       public DbSet<onCheckpoint> onCheckpoint { get; set; }
        public DbSet<offcheckpoint> offCheckpoint { get; set; }
        public DbSet<assignee> assigneess { get; set; }
        public DbSet<CreateRole> createRoles { get; set; }
        public DbSet<HRMS_project.Models.ChangePassword> ChangePassword { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            
            base.OnModelCreating(modelBuilder);
        }
       


        public DbSet<HRMS_project.Models.ForgotPassword> ForgotPassword { get; set; }
      


      


    

    }





}
