using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Tests.Builders;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;

namespace InvestmentUseCase.Tests.DomainTests.Services.InvestmentTests
{
    [CollectionDefinition(nameof(InvestmentServiceCollection))]
    public class InvestmentServiceCollection : ICollectionFixture<InvestmentServiceFixture>
    {
    }

    public class InvestmentServiceFixture
    {
        public AutoMocker Mocker = new();
        public Mock<ILogger<InvestmentService>> LoggerMock = new();
        public InvestmentService GetInvestmentService()
        {
            Mocker = new AutoMocker();
            LoggerMock = new Mock<ILogger<InvestmentService>>();
            return Mocker.CreateInstance<InvestmentService>();
        }
        public void SetupGetById(Guid id)
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new InvestmentBuilder().Create(id));
        }
        public void SetupGetAll()
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync([new InvestmentBuilder().Create()]);
        }
        public void SetupExists(bool exists)
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Investment, bool>>>()))
                .ReturnsAsync(exists);
        }

        public void SetupGetByCustomerAndProduct()
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.GetByCustomerAndProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(new InvestmentBuilder().CreateWithCustomerAndProduct());
        }

        public void SetupGetAllByCustomer()
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.GetAllByCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync([new InvestmentBuilder().CreateWithCustomerAndProduct()]);
        }

        public void SetupAdd(Investment investment)
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.AddAsync(It.IsAny<Investment>()))
                .ReturnsAsync(investment);
        }
        public void SetupUpdate(Investment investment)
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.UpdateAsync(It.IsAny<Investment>()))
                .ReturnsAsync(investment);
        }
        public void SetupDelete()
        {
            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.DeleteAsync(It.IsAny<Investment>()))
                .Returns(Task.CompletedTask);
        }

        public void SetupInclude(Investment investment)
        {
            var investments = new List<Investment> { investment }.AsQueryable();

            Mocker.GetMock<IInvestmentRepository>()
                .Setup(x => x.Include(It.IsAny<Expression<Func<Investment, object>>[]>()))
                .Returns(investments);
        }
    }
}
