using EmpsManagement.DAL.Models.Departments;
using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace EmpsManagement.DAL.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //}
       
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

            base.OnModelCreating(modelBuilder);
         
        }

        #region Tables
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> Employees { get; set; } 

        #endregion
    }
}
