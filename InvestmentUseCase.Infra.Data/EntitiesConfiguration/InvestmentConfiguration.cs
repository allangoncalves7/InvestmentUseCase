using InvestmentUseCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentUseCase.Infra.Data.EntitiesConfiguration
{
    public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
    {
        public void Configure(EntityTypeBuilder<Investment> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvestedCapital)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.HasOne(i => i.Customer)
                .WithMany(c => c.Investments)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.InvestmentProduct)
                .WithMany()
                .HasForeignKey(i => i.InvestmentProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
