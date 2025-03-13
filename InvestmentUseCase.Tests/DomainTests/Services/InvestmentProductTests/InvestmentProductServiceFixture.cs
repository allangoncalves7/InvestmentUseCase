using InvestmentUseCase.Domain.Entities;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Tests.Builders;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;

namespace InvestmentUseCase.Tests.DomainTests.Services.InvestmentProductTests
{
    [CollectionDefinition(nameof(InvestmentProductServiceCollection))]
    public class InvestmentProductServiceCollection : ICollectionFixture<InvestmentProductServiceFixture>
    {
    }
    public class InvestmentProductServiceFixture
    {
        public AutoMocker Mocker = new();
        public Mock<ILogger<InvestmentProductService>> LoggerMock = new();

        public InvestmentProductService GetInvestmentProductService()
        {
            Mocker = new AutoMocker();
            LoggerMock = new Mock<ILogger<InvestmentProductService>>();

            return Mocker.CreateInstance<InvestmentProductService>();
        }

        public void SetupGetById(Guid id)
        {
            Mocker.GetMock<IInvestmentProductRepository>()
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new InvestmentProductBuilder().Create(id));
        }

        public void SetupGetAll()
        {
            Mocker.GetMock<IInvestmentProductRepository>()
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync([new InvestmentProductBuilder().Create()]);
        }

        public void SetupExists(bool exists)
        {
            Mocker.GetMock<IInvestmentProductRepository>()
                .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<InvestmentProduct, bool>>>()))
                .ReturnsAsync(exists);
        }

        public void SetupAdd(InvestmentProduct investmentProduct)
        {
            Mocker.GetMock<IInvestmentProductRepository>()
                .Setup(x => x.AddAsync(It.IsAny<InvestmentProduct>()))
                .ReturnsAsync(investmentProduct);
        }

        public void SetupUpdate(InvestmentProduct investmentProduct)
        {
            Mocker.GetMock<IInvestmentProductRepository>()
                .Setup(x => x.UpdateAsync(It.IsAny<InvestmentProduct>()))
                .ReturnsAsync(investmentProduct);
        }

        public void SetupDelete()
        {
            Mocker.GetMock<IInvestmentProductRepository>()
                .Setup(x => x.DeleteAsync(It.IsAny<InvestmentProduct>()))
                .Returns(Task.CompletedTask);
        }


    }
}
