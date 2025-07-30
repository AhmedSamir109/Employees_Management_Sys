


using EmpsManagement.DAL.Models.Shared;

namespace EmpsManagement.DAL.Data.Configurations
{
    public class BaseEntityConfigurations <T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETDATE()");  // recalculate on every update automatically
        }

       
    }
}
