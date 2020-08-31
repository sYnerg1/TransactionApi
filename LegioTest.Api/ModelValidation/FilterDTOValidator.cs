using FluentValidation;
using LegioTest.Domain.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegioTest.Api.ModelValidation
{
    public class FilterDTOValidator : AbstractValidator<FilterDTO>
    {
        public FilterDTOValidator()
        {
            RuleFor(a => a.Status)
                .IsInEnum();

            RuleFor(a => a.Type)
                .IsInEnum();

            RuleFor(a => a.PageSize)
                .GreaterThanOrEqualTo(10);

            RuleFor(a => a.PageNumber)
                .GreaterThanOrEqualTo(1);
        }
    }
}
