using InvestmentUseCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentUseCase.Tests.Builders
{
    public class InvestmentProductBuilder
    {
        private string _name = "Product 1";
        private string _code = "P001";

        public InvestmentProduct Create(Guid id)
        {
            return new InvestmentProduct(id, _code, _name);
        }

        public InvestmentProduct Create()
        {
            return new InvestmentProduct(_code, _name);
        }
    }
}
