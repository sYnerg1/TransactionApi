using FluentValidation;
using LegioTest.Domain.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegioTest.Api.ModelValidation
{
    public class ExcelFilterValidator : AbstractValidator<ExcelFilterDTO>
    {
        public ExcelFilterValidator()
        {
            RuleFor(a => a.Status)
                .IsInEnum();

            RuleFor(a => a.Type)
                .IsInEnum();
        }
    }
}
