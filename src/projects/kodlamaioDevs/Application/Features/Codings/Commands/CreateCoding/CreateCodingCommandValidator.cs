using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Codings.Commands.CreateCoding
{
    public class CreateCodingCommandValidator : AbstractValidator<CreateCodingCommand>
    {
        public CreateCodingCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(2);
        }
    }
}
