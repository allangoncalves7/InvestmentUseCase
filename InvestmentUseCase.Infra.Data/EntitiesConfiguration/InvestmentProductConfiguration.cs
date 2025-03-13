using InvestmentUseCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentUseCase.Infra.Data.EntitiesConfiguration
{
    public class InvestmentProductConfiguration : IEntityTypeConfiguration<InvestmentProduct>
    {
        public void Configure(EntityTypeBuilder<InvestmentProduct> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Code);
        }
    }
}
