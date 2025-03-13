using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Infra.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace InvestmentUseCase.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentProduct> InvestmentProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new InvestmentProductConfiguration());
            builder.ApplyConfiguration(new InvestmentConfiguration());

            SeedData(builder);
        }
        private static void SeedData(ModelBuilder builder)
        {
            var customer1Id = Guid.Parse("5dfa53c1-973f-41d4-9366-cbbb2922b22e");
            var customer2Id = Guid.Parse("16aecf48-db83-479f-9556-913414c407bf");
            var customer3Id = Guid.Parse("edda9d43-96a1-497b-bd1e-d0f2385f47f5");
            var customer4Id = Guid.Parse("a7291cb8-e069-4a75-b53c-f0d139a1e442");
            var customer5Id = Guid.Parse("cfa5377c-b839-45f0-8ed3-c5c6fc067a8b");

            var product1Id = Guid.Parse("cd939f0a-fc4b-423c-b001-f7a558ba483d");
            var product2Id = Guid.Parse("0303d605-a32a-4587-b641-160f008e5d14");
            var product3Id = Guid.Parse("41ef04f6-83c9-46b5-bd38-f08f91b1a66e");
            var product4Id = Guid.Parse("0c62f3db-1214-48d0-98b0-fdcc9ffc93fe");

            var investment1Id = Guid.Parse("bff7a047-04d6-4cd0-9724-f316fc958f68");
            var investment2Id = Guid.Parse("627fffb3-1c2d-4272-851f-aa346f48b809");
            var investment3Id = Guid.Parse("642161a6-92d1-4df9-99e8-8f2a99bcf458");
            var investment4Id = Guid.Parse("cd2a8bc4-d995-46eb-9a11-b65bfa566be7");
            var investment5Id = Guid.Parse("0c11f32e-7543-4601-8056-7ef89cd775d8");

            builder.Entity<Customer>().HasData(
                new Customer(customer1Id, "João Silva", "joao_silva@gmail.com" , "1234", "56789", "1"),
                new Customer(customer2Id, "Maria dos Santos", "maria_santos@gmail.com", "4321", "98765", "2"),
                new Customer(customer3Id, "José Pereira", "jose.p@gmail.com", "5678", "12345", "3"),
                new Customer(customer4Id, "Ana Souza", "ana_s@gmail.com", "8765", "54321", "4"),
                new Customer(customer5Id, "Francisco Oliveira", "francisco@gmail.com", "9876", "678980", "5")
            );

            builder.Entity<InvestmentProduct>().HasData(
                 new InvestmentProduct(product1Id, "TD001", "Tesouro Direto"),
                 new InvestmentProduct(product2Id, "CDB002", "CDB Resgate Diário"),
                 new InvestmentProduct(product3Id, "CDB003", "CDB Pós Fixado"),
                 new InvestmentProduct(product4Id, "LCI004", "LCI Pré Fixado")
             );

            builder.Entity<Investment>().HasData(
                new Investment(investment1Id, product1Id, customer1Id, 1000.00m),
                new Investment(investment2Id, product2Id, customer2Id, 5000.00m),
                new Investment(investment3Id, product2Id, customer1Id, 5000.00m),
                new Investment(investment4Id, product3Id, customer3Id, 3000.00m),
                new Investment(investment5Id, product4Id, customer4Id, 2000.00m)
            );
        }
    }
}
