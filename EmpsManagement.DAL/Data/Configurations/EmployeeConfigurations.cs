using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Data.Configurations
{
    public class EmployeeConfigurations :BaseEntityConfigurations<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10 , 2)");
            builder.Property(E => E.Gender).
                   HasConversion( (gender) => gender.ToString()       // Convert enum to string for storage in Db
                   , (gender) => Enum.Parse<Gender>(gender) );  // Convert string back to enum when reading from Db
            builder.Property(E => E.EmployeeType)
                   .HasConversion((type) => type.ToString()
                   , (type) => (EmployeeType) Enum.Parse(typeof(EmployeeType), type));
        }
    }
}
