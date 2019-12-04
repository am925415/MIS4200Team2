using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MIS4200Team2.Models;  // This is needed to access the models 



namespace MIS4200Team2.DAL
{
    public class MIS4200Context : DbContext
    {
        public MIS4200Context() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MIS4200Context,
                MIS4200Team2.Migrations.MISContext.Configuration>("DefaultConnection"));
        }


        // Include each object here.  The value inside <> is the name of the class,
        // the value outside should generally be the plural of the class name
        // and is the name used to reference the entity in code 

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<EmployeeRecognitionNomination> employeeRecognitionNominations { get; set; }

        public System.Data.Entity.DbSet<MIS4200Team2.Models.EmployeeFullDetail> EmployeeFullDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
    
    
        
}
