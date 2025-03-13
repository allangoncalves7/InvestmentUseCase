using InvestmentUseCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentUseCase.Infra.Data.EntitiesConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Agency)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.Account)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.DAC)
                .IsRequired()
                .HasMaxLength(1);

            builder.HasMany(c => c.Investments)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Agency);
            builder.HasIndex(p => p.Account);
            builder.HasIndex(p => p.DAC);
        }
    }
}
