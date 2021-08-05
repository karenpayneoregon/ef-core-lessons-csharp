using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NorthWindCoreLibrary.Models;

namespace NorthWindCoreUnitTest_InMemory.ValidationClasses
{
    public class CustomersValidator : AbstractValidator<Customers>
    {
        public CustomersValidator()
        {
            RuleFor(customer => customer.CompanyName).NotNull();
        }
    }
}
