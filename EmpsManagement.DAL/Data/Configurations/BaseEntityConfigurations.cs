

namespace EmpsManagement.DAL.Data.Configurations
{
    public class BaseEntityConfigurations : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatedBy).HasDefaultValueSql("GETDATE()");
            builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GETDATE()");  // recalculate on every update automatically
        }
    }
}
