using EmpsManagement.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Data.Configurations
{
    public class DepartmentConfigurations : BaseEntityConfigurations<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments"); 
            builder.Property(d => d.Name).HasColumnType("varchar(20)");
            builder.Property(d => d.Code).HasColumnType("varchar(20)");
            builder.Property(d => d.Description).HasColumnType("varchar(200)");
            builder.HasMany(d => d.Employees)
                   .WithOne(e => e.Department)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull); // Set to null if department is deleted

            base.Configure(builder); // Call the base configuration for BaseEntity


        }
    }
}
